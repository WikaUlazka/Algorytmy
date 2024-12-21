using System.Text;

class MagicCube
{
    static void Main(string[] args)
    {
        string inputPath = "In0203.txt";
        string outputPath = "Out0203.txt";

        var inputLines = File.ReadAllLines(inputPath);
        int n = int.Parse(inputLines[0]) + 1;
        string T = inputLines[1];

        var TSequence = GenerateRepeatedSequence(T, n * n * n);

        char[,,] cube = new char[n, n, n];
        int index = 0;

        for (int i = 0; i < n; i++)
        {
            char[,] square = new char[n, n];

            if (i % 2 == 0)
                FillSquareRecursive(square, TSequence, ref index, 0, true);
            else
                FillSquareRecursive(square, TSequence, ref index, 0, false);

            for (int x = 0; x < n; x++)
                for (int y = 0; y < n; y++)
                    cube[i, x, y] = square[x, y];
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine($"n={n - 1}, T={T}");
            for (int i = 0; i < n; i++)
            {
                writer.WriteLine($"Tablica {i}");
                for (int x = 0; x < n; x++)
                {
                    writer.Write("[");
                    for (int y = 0; y < n; y++)
                    {
                        writer.Write(cube[i, x, y]);
                        if (y < n - 1) writer.Write(", ");
                    }
                    writer.WriteLine("]");
                }
            }
        }
    }

    static string GenerateRepeatedSequence(string T, int length)
    {
        StringBuilder sb = new StringBuilder();
        while (sb.Length < length)
        {
            sb.Append(T);
        }
        return sb.ToString();
    }

    static void FillSquareRecursive(char[,] square, string T, ref int index, int layer, bool normalOrder)
    {
        int n = square.GetLength(0);
        if (layer >= (n + 1) / 2)
            return;


        for (int col = layer; col < n - layer; col++)
            square[layer, col] = T[index++ % T.Length];


        for (int row = layer + 1; row < n - layer; row++)
            square[row, n - layer - 1] = T[index++ % T.Length];


        for (int col = n - layer - 2; col >= layer; col--)
            square[n - layer - 1, col] = T[index++ % T.Length];


        for (int row = n - layer - 2; row > layer; row--)
            square[row, layer] = T[index++ % T.Length];

        FillSquareRecursive(square, T, ref index, layer + 1, normalOrder);

        if (!normalOrder && layer == 0)
        {
            ReverseSquare(square);
        }
    }

    static void ReverseSquare(char[,] square)
    {
        int n = square.GetLength(0);
        char[,] reversed = new char[n, n];

        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < n; y++)
            {
                reversed[n - x - 1, n - y - 1] = square[x, y];
            }
        }

        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < n; y++)
            {
                square[x, y] = reversed[x, y];
            }
        }
    }
}
