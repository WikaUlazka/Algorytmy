class Program
{
    static void Main(string[] args)
    {
        string inputFile = "In0201.txt";
        string outputFile = "Out0201.txt";

        try
        {
            string[] lines = File.ReadAllLines(inputFile);
            int n = int.Parse(lines[0]);
            int[] numbers = lines[1].Split(' ').Select(int.Parse).ToArray();

            QuickSort(numbers, 0, n - 1);

            File.WriteAllText(outputFile, string.Join(" ", numbers));
            Console.WriteLine("Dane zostały posortowane i zapisane do pliku Out0201.txt.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }

    static void QuickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high);
            QuickSort(array, low, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, high);
        }
    }

    static int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }
        Swap(array, i + 1, high);
        return i + 1;
    }

    static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}

