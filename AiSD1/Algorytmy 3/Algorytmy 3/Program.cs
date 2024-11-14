public class Program
{
    static Solution MaxSubArraySum(int[] numbers)
    {
        int max_so_far = int.MinValue;
        int max_ending_here = 0;
        int start = 0;
        int end = 0;
        int s = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            max_ending_here += numbers[i];

            if (max_so_far < max_ending_here)
            {
                max_so_far = max_ending_here;
                start = s;
                end = i;
            }

            if (max_ending_here < 0)
            {
                max_ending_here = 0;
                s = i + 1;
            }
        }

        return new Solution()
        {
            Start = start,
            End = end,
            Sum = max_so_far
        };
    }



    public static void Main()
    {
        int[] numbers = System.IO.File.ReadAllText("In0103.txt").Split(Environment.NewLine).Skip(1).Select(x => int.Parse(x)).ToArray();

        Solution solution = MaxSubArraySum(numbers);
        using StreamWriter writetext = new StreamWriter("Out0103.txt");
        writetext.WriteLine($"{solution.Start + 1}, {solution.End + 1}, {solution.Sum}");
    }
}

public class Solution
{
    public int Start { get; set; }
    public int End { get; set; }
    public int Sum { get; set; }
}