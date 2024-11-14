public class Program
{
    public static void Main()
    {
        string fileContnet = System.IO.File.ReadAllText("In0101.txt");
        int n = Int32.Parse(fileContnet.Split(" ")[0]);
        int k = Int32.Parse(fileContnet.Split(" ")[1]);
        int SN1Result = SN1(n, k);
        int SN3Result = SN3(n, k);
        Console.WriteLine(SN1Result);
        Console.WriteLine(SN3Result);
        using StreamWriter writetext = new StreamWriter("Out0101.txt");
        writetext.WriteLine($"n={n} k={k}");
        writetext.WriteLine($"SN1 = {SN1Result}");
        writetext.WriteLine($"SN3 = {SN3Result}");
    }
    public static int SN1(int n, int k)
    {
        int numerator = Factorial(n);
        int denominator = Factorial(k) * Factorial(n - k);
        return (numerator / denominator);
    }

    public static int SN3(int n, int k)
    {
        if (k == 0 || k == n)
        {
            return 1;
        }
        return SN3(n - 1, k - 1) + SN3(n - 1, k);
    }

    public static int Factorial(int n)
    {
        return Enumerable.Range(1, n).Aggregate(1, (p, item) => p * item);
    }

}
