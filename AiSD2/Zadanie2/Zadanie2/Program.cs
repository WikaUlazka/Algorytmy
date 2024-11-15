public class Program
{
    public static int counter = 0;
    public static Dictionary<int, int> memo = new Dictionary<int, int>();
    public static void Main()
    {
        string fileContent = System.IO.File.ReadAllText("In0202.txt");
        int n = int.Parse(fileContent);

        List<int> list = new List<int>();

        for (int j = 0; j < int.MaxValue; j++)
        {
            int result = Fib(j);
            if (result > n)
            {
                break;
            }
            else
            {
                list.Add(result);
            }
        }
        string outPut = string.Join(", ", list);
        System.IO.File.WriteAllText("Out02.02.txt", outPut);
    }


    public static int Fib(int n)
    {
        if (n == 0)
        {
            return 0;
        }
        if (n == 1)
        {
            return 1;
        }
        if (memo.ContainsKey(n))
        {
            return memo[n];
        }

        counter++;
        int result = Fib(n - 1) + Fib(n - 2);
        memo[n] = result;
        return result;
    }

}
