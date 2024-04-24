namespace GraphLab;

/// <summary>
/// Represents a data of a edge.
/// </summary>
public class Edge(Vertex from, Vertex to, float cost = 0)
{
    private static int nextEdgeId = 0;
    public readonly float Cost = cost;
    public readonly int Id = nextEdgeId++;
    public readonly Vertex From = from;
    public readonly Vertex To = to;
}