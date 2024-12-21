class KnapsackSolver
{
    static void Main(string[] args)
    {
        string inputFile = "In0302.txt";
        string outputFile = "Out0302.txt";

        var input = File.ReadAllLines(inputFile);
        var firstLine = input[0].Split(' ');
        int n = int.Parse(firstLine[0]);
        int W = int.Parse(firstLine[1]);

        var items = new List<(int Value, int Weight)>();
        for (int i = 1; i <= n; i++)
        {
            var parts = input[i].Split(' ');
            int value = int.Parse(parts[0]);
            int weight = int.Parse(parts[1]);
            items.Add((value, weight));
        }

        var solutions = Knapsack(n, W, items);
        File.WriteAllLines(outputFile, solutions.Select(s => string.Join(" ", s)));
    }

    static List<List<int>> Knapsack(int n, int W, List<(int Value, int Weight)> items)
    {
        int[,] dp = new int[n + 1, W + 1];

        for (int i = 1; i <= n; i++)
        {
            var (value, weight) = items[i - 1];
            for (int w = 0; w <= W; w++)
            {
                if (weight <= w)
                {
                    dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - weight] + value);
                }
                else
                {
                    dp[i, w] = dp[i - 1, w];
                }
            }
        }

        var solutions = new List<List<int>>();
        int maxValue = dp[n, W];

        void FindSolutions(int i, int w, List<int> current)
        {
            if (i == 0 || w == 0)
            {
                if (current.Sum(idx => items[idx - 1].Value) == maxValue)
                {
                    solutions.Add(new List<int>(current));
                }
                return;
            }

            if (dp[i, w] == dp[i - 1, w])
            {
                FindSolutions(i - 1, w, current);
            }

            if (w >= items[i - 1].Weight && dp[i, w] == dp[i - 1, w - items[i - 1].Weight] + items[i - 1].Value)
            {
                current.Add(i);
                FindSolutions(i - 1, w - items[i - 1].Weight, current);
                current.RemoveAt(current.Count - 1);
            }
        }

        FindSolutions(n, W, new List<int>());
        return solutions;
    }
}
