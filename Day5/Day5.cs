namespace AdventOfCode2024.Day5;

public class Day5() : SolutionBase("Day5")
{
    public override string Part1(bool isExample = false)
    {
        var lines = (isExample ? ExampleLines : InputLines).ToList();

        var pageOrderingRules = lines.Take(lines.FindIndex(string.IsNullOrEmpty)).Select(line => line.Split("|").Select(int.Parse).ToList()).ToList();

        var updates = lines.Skip(lines.FindIndex(string.IsNullOrEmpty) + 1).Select(line => line.Split(",").Select(int.Parse).ToList()).ToList();

        var correctlyOrderedUpdates = updates.Where(update => IsUpdateCorrectlyOrdered(update, pageOrderingRules)).ToList();

        var totalCorrectlyOrderedUpdatesMiddlePageNumbers = (from update in correctlyOrderedUpdates let middleIndex = (int)Math.Floor((decimal)update.Count / 2) select update[middleIndex]).Sum();

        return totalCorrectlyOrderedUpdatesMiddlePageNumbers.ToString();
    }

    public override string Part2(bool isExample = false)
    {
        var lines = (isExample ? ExampleLines : InputLines).ToList();

        var pageOrderingRules = lines.Take(lines.FindIndex(string.IsNullOrEmpty)).Select(line => line.Split("|").Select(int.Parse).ToList()).ToList();

        var updates = lines.Skip(lines.FindIndex(string.IsNullOrEmpty) + 1).Select(line => line.Split(",").Select(int.Parse).ToList()).ToList();

        var incorrectlyOrderedUpdates = updates.Where(update => !IsUpdateCorrectlyOrdered(update, pageOrderingRules)).ToList();

        var reorderedUpdates = incorrectlyOrderedUpdates.Select(update => CorrectlyOrderUpdate(update, pageOrderingRules)).ToList();

        var totalReorderedUpdatesMiddlePageNumbers = (from update in reorderedUpdates let middleIndex = (int)Math.Floor(((decimal)update.Count) / 2) select update[middleIndex]).Sum();

        return totalReorderedUpdatesMiddlePageNumbers.ToString();
    }

    private record PageWithRules(int PageNumber, List<int> MustBeBefore);

    private static List<int> CorrectlyOrderUpdate(List<int> update, List<List<int>> pageOrderingRules)
    {
        var pageOrderingRulesThatApply = pageOrderingRules.Where(pageOrderingRule => DoesPageOrderingRuleApplyToUpdate(pageOrderingRule, update)).ToList();

        var pagesWithRules = update.Select(page => new PageWithRules(
            page,
            pageOrderingRulesThatApply.Where(pageOrderingRule => pageOrderingRule[0] == page).Select(pageOrderingRule => pageOrderingRule[1]).ToList()
        )).ToList();

        var correctlyOrderedUpdate = new List<int>();

        while (correctlyOrderedUpdate.Count < update.Count)
        {
            var pageThatMustComeLast = pagesWithRules.FirstOrDefault(page => page.MustBeBefore.Count == 0);
            if (pageThatMustComeLast == null)
            {
                throw new Exception("No page that must come last found");
            }

            correctlyOrderedUpdate = correctlyOrderedUpdate.Prepend(pageThatMustComeLast!.PageNumber).ToList();

            pagesWithRules = pagesWithRules.Select(page => new PageWithRules(
                page.PageNumber,
                page.MustBeBefore.Where(p => p != pageThatMustComeLast.PageNumber).ToList()
            )).Where(page => page.PageNumber != pageThatMustComeLast.PageNumber).ToList();
        }

        return correctlyOrderedUpdate;
    }

    private static bool IsUpdateCorrectlyOrdered(List<int> update, List<List<int>> pageOrderingRules)
    {
        return pageOrderingRules.All(pageOrderingRule => DoesUpdateMeetPageOrderingRule(update, pageOrderingRule));
    }

    private static bool DoesPageOrderingRuleApplyToUpdate(List<int> pageOrderingRule, List<int> update)
    {
        var indexOfFirstPage = update.IndexOf(pageOrderingRule[0]);
        var indexOfSecondPage = update.IndexOf(pageOrderingRule[1]);

        return indexOfFirstPage >= 0 && indexOfSecondPage >= 0;
    }

    private static bool DoesUpdateMeetPageOrderingRule(List<int> update, List<int> pageOrderingRule)
    {
        var indexOfFirstPage = update.IndexOf(pageOrderingRule[0]);
        var indexOfSecondPage = update.IndexOf(pageOrderingRule[1]);

        if (indexOfFirstPage < 0 || indexOfSecondPage < 0)
        {
            return true;
        }

        return indexOfFirstPage < indexOfSecondPage;
    }
}
