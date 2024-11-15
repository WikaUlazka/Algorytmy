class Program
{
    static void Main(string[] args)
    {
        string fileContent = System.IO.File.ReadAllText("In0204.txt");
        int n = int.Parse(fileContent);

        List<string> moves = new List<string>();

        Hanoi(n, 1, 2, 3, moves);

        using (StreamWriter writer = new StreamWriter("Out0204.txt"))
        {
            writer.WriteLine($"N={n}");
            writer.WriteLine(string.Join(", ", moves));
        }
    }

    static void Hanoi(int n, int start, int target, int auxiliary, List<string> moves)
    {
        if (n == 1)
        {
            moves.Add($"{start}->{target}");
            return;
        }

        Hanoi(n - 1, start, auxiliary, target, moves);
        moves.Add($"{start}->{target}");
        Hanoi(n - 1, auxiliary, target, start, moves);
    }
}

