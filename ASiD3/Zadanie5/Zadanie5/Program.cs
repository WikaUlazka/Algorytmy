using System.Text;

public class HuffmanNode : IComparable<HuffmanNode>
{
    public char Character { get; set; }
    public int Frequency { get; set; }
    public HuffmanNode Left { get; set; }
    public HuffmanNode Right { get; set; }

    public int CompareTo(HuffmanNode other)
    {
        if (this.Frequency == other.Frequency)
            return this.Character.CompareTo(other.Character); // Stabilność przy sortowaniu
        return this.Frequency.CompareTo(other.Frequency);
    }
}

public class HuffmanCoding
{
    public static Dictionary<char, string> BuildHuffmanTree(string inputText, out Dictionary<char, int> frequencyTable)
    {
        // Obliczanie częstotliwości znaków
        frequencyTable = new Dictionary<char, int>();
        foreach (var ch in inputText)
        {
            if (frequencyTable.ContainsKey(ch))
                frequencyTable[ch]++;
            else
                frequencyTable[ch] = 1;
        }

        // Budowanie listy węzłów
        var nodes = new List<HuffmanNode>();
        foreach (var kvp in frequencyTable)
        {
            nodes.Add(new HuffmanNode { Character = kvp.Key, Frequency = kvp.Value });
        }

        // Budowanie drzewa Huffmana
        while (nodes.Count > 1)
        {
            nodes = nodes.OrderBy(n => n.Frequency).ThenBy(n => n.Character).ToList();

            var left = nodes[0];
            var right = nodes[1];
            nodes.RemoveRange(0, 2);

            var parent = new HuffmanNode
            {
                Character = '\0',
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right
            };
            nodes.Add(parent);
        }

        var root = nodes[0];
        var codes = new Dictionary<char, string>();
        BuildCodes(root, "", codes);

        return codes;
    }

    private static void BuildCodes(HuffmanNode node, string currentCode, Dictionary<char, string> codes)
    {
        if (node == null) return;

        if (node.Left == null && node.Right == null)
        {
            codes[node.Character] = currentCode;
        }

        BuildCodes(node.Left, currentCode + "0", codes);
        BuildCodes(node.Right, currentCode + "1", codes);
    }

    public static string Compress(string inputText, Dictionary<char, string> codes)
    {
        var compressed = new StringBuilder();
        foreach (var ch in inputText)
        {
            compressed.Append(codes[ch]);
        }
        return compressed.ToString();
    }

    public static string Decompress(string compressedText, Dictionary<char, string> codes)
    {
        var reverseCodes = codes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        var currentCode = new StringBuilder();
        var decompressed = new StringBuilder();

        foreach (var bit in compressedText)
        {
            currentCode.Append(bit);
            if (reverseCodes.ContainsKey(currentCode.ToString()))
            {
                decompressed.Append(reverseCodes[currentCode.ToString()]);
                currentCode.Clear();
            }
        }

        return decompressed.ToString();
    }
}

public class FileHandler
{
    public static string ReadInputFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    public static void WriteOutputFile(string filePath, string output)
    {
        File.WriteAllText(filePath, output);
    }

    public static void WriteCompressedFile(string filePath, string compressedData)
    {
        File.WriteAllText(filePath, compressedData);
    }
}

public class Program
{
    public static void Main()
    {
        // Wczytanie danych z pliku wejściowego
        string inputText = FileHandler.ReadInputFile("In0305.txt");

        // Budowanie drzewa Huffmana i kodów
        Dictionary<char, int> frequencyTable;
        var huffmanCodes = HuffmanCoding.BuildHuffmanTree(inputText, out frequencyTable);

        // Formatowanie tabeli częstotliwości
        var frequencyOutput = string.Join(", ", frequencyTable.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key} {kvp.Value}"));

        // Formatowanie tabeli z częstotliwościami dla kodów
        var frequencyDecimalOutput = string.Join(", ", frequencyTable.OrderBy(kvp => kvp.Key).Select(kvp =>
            $"{kvp.Key} {((double)kvp.Value / inputText.Length):F10}"));

        // Kodowanie węzłów w porządku częstotliwości
        var orderedNodes = string.Join(", ", frequencyTable.OrderBy(kvp => kvp.Value).ThenBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}"));

        // Formatowanie kodów Huffmana
        var huffmanCodeOutput = string.Join(", ", huffmanCodes.OrderBy(kvp => kvp.Value.Length).ThenBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}={kvp.Value}"));

        StringBuilder output = new StringBuilder();

        // Zapisz wyniki do Out0305.txt
        output.AppendLine(frequencyOutput);
        output.AppendLine(frequencyDecimalOutput);
        output.AppendLine(orderedNodes);
        output.AppendLine(huffmanCodeOutput);
        output.AppendLine("////////////////////////////////////////////");

        // Kompresja tekstu
        string compressedText = HuffmanCoding.Compress(inputText, huffmanCodes);

        // Zapisz skompresowane dane
        output.AppendLine(string.Join(" ", frequencyTable.Select(kvp => $"{kvp.Key}-{kvp.Value}")));
        output.AppendLine(compressedText);

        // Zapisz wyniki do pliku Out0305.txt
        FileHandler.WriteOutputFile("Out0305.txt", output.ToString());

        // Zapisz skompresowany tekst do pliku Huff.txt
        FileHandler.WriteCompressedFile("Huff.txt", compressedText);

        // Dekompresja
        string decompressedText = HuffmanCoding.Decompress(compressedText, huffmanCodes);
        Console.WriteLine("Decompressed text: " + decompressedText);
    }
}
