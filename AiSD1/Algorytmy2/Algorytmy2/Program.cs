public class Program
{
    public static void Main()
    {
        string fileContent = System.IO.File.ReadAllText("In0102.txt");
        string[] spitedContent = fileContent.Split(System.Environment.NewLine);
        int n = int.Parse(spitedContent[0].Split(" ")[0]);
        int k = int.Parse(spitedContent[0].Split(" ")[1]);
        Console.WriteLine(n);
        Console.WriteLine(k);
        List<int> numbers = spitedContent.Skip(1).Select(int.Parse).ToList();
        numbers.Sort();
        numbers.ForEach(x => Console.WriteLine(x));


        int lowestIndex = 0;
        int highestIndex = numbers.Count - 1;
        int amount = 0;

        using StreamWriter writetext = new StreamWriter("Out0102.txt");

        while (true)
        {
            int lowestNumber = numbers[lowestIndex];
            int highestNumber = numbers[highestIndex];

            if (lowestIndex == highestIndex)
            {
                writetext.WriteLine(lowestNumber);
                amount++;
                break;
            }
            if (lowestIndex > highestIndex)
            {
                break;
            }

            if (lowestNumber + highestNumber <= k)
            {
                writetext.WriteLine($"{lowestNumber} {highestNumber}");
                lowestIndex++;
                highestIndex--;
                amount++;
            }
            else
            {
                writetext.WriteLine($"{highestNumber}");
                highestIndex--;
                amount++;
            }
        }
        writetext.WriteLine(amount);
    }
}
