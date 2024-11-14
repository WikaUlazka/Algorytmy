using System.Data;

public class Program
{
    public static void Main()
    {
        string fileContent = System.IO.File.ReadAllText("In0201.txt");
        string numbersText = fileContent.Split(Environment.NewLine)[1];
        List<int> numbers = numbersText.Split(" ").Select(x => int.Parse(x)).ToList();
        List<int> sorted = CountSort(numbers);

        string outPut = string.Join(" ", sorted);
        System.IO.File.WriteAllText("Out02.01.txt", outPut);
    }

    public static List<int> CountSort(List<int> numbers)
    {
        List<int> counters = new List<int>(new int[21001]);

        foreach (int number in numbers)
        {
            counters[number + 10000]++;
        }

        List<int> result = new List<int>();

        int i = 0;
        foreach (int counter in counters)
        {
            int number = i - 10000;
            for (int j = 0; j < counter; j++)
            {
                result.Add(number);
            }
            i++;
        }
        return result;
    }
}
