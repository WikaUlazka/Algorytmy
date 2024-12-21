class Node
{
    public int Value;
    public Node Left;
    public Node Right;

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

class BinarySearchTree
{
    private Node Root;
    public void Insert(int value)
    {
        Root = InsertRecursively(Root, value);
    }

    private Node InsertRecursively(Node root, int value)
    {
        if (root == null)
        {
            return new Node(value);
        }

        if (value < root.Value)
        {
            root.Left = InsertRecursively(root.Left, value);
        }
        else if (value > root.Value)
        {
            root.Right = InsertRecursively(root.Right, value);
        }

        return root;
    }

    public string PreOrderTraversal()
    {
        return PreOrderRecursively(Root).TrimEnd(',');
    }

    private string PreOrderRecursively(Node root)
    {
        if (root == null)
        {
            return string.Empty;
        }

        return $"{root.Value}, " + PreOrderRecursively(root.Left) + PreOrderRecursively(root.Right);
    }
}

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "In0206.txt";
        string outputFile = "Out0206.txt";

        try
        {
            string input = File.ReadAllText(inputFile).Trim();
            int[] numbers = input.Split(' ').Select(int.Parse).ToArray();

            BinarySearchTree bst = new BinarySearchTree();
            foreach (int number in numbers)
            {
                bst.Insert(number);
            }

            string traversalResult = bst.PreOrderTraversal();

            File.WriteAllText(outputFile, traversalResult);

            Console.WriteLine($"Dane zapisano do pliku {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }
}
