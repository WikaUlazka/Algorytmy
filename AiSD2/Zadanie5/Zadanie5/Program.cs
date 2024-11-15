class Program
{
    static void Main(string[] args)
    {
        string inputFilePath = "In0205.txt";
        string outputFilePath = "Out0205.txt";

        var graphData = File.ReadAllLines(inputFilePath);

        int n = int.Parse(graphData[0]);

        List<int>[] graph = new List<int>[n + 1];
        for (int i = 1; i <= n; i++)
        {
            graph[i] = new List<int>();
            if (!string.IsNullOrWhiteSpace(graphData[i]))
            {
                string[] neighbors = graphData[i].Split();
                foreach (string neighbor in neighbors)
                {
                    graph[i].Add(int.Parse(neighbor));
                }
            }
        }

        bool[] visited = new bool[n + 1];
        List<int> dfsOrder = new List<int>();
        DFS(1, graph, visited, dfsOrder);

        bool isConnected = true;
        for (int i = 1; i <= n; i++)
        {
            if (!visited[i])
            {
                isConnected = false;
                break;
            }
        }

        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            if (isConnected)
            {
                writer.WriteLine("Graf spójny");
            }
            else
            {
                writer.WriteLine("Graf niespójny");
            }
            writer.WriteLine(string.Join(" ", dfsOrder));
        }
    }

    static void DFS(int node, List<int>[] graph, bool[] visited, List<int> dfsOrder)
    {
        visited[node] = true;
        dfsOrder.Add(node);

        foreach (int neighbor in graph[node])
        {
            if (!visited[neighbor])
            {
                DFS(neighbor, graph, visited, dfsOrder);
            }
        }
    }
}
