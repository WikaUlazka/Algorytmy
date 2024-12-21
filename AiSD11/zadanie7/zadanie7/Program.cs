class Program
{
    static void Main(string[] args)
    {
        string inputFile = "In0207.txt";
        string outputFile = "Out0207.txt";

        try
        {
            string[] lines = File.ReadAllLines(inputFile);
            int n = int.Parse(lines[0].Trim());
            int[] A = lines[1].Split(' ').Select(int.Parse).ToArray();
            int[] B = lines[2].Split(' ').Select(int.Parse).ToArray();

            HashSet<int> uniqueA = new HashSet<int>(A);
            HashSet<int> uniqueB = new HashSet<int>(B);

            var intersection = uniqueA.Intersect(uniqueB).ToList();
            intersection.Sort();

            File.WriteAllLines(outputFile, new[]
            {
                string.Join(" ", intersection),
                intersection.Count.ToString()
            });

            Console.WriteLine($"Dane zapisano do pliku {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }
}
