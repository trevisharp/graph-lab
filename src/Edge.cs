namespace GraphLab;

/// <summary>
/// Represents a data of a edge.
/// </summary>
public class Edge
{
    public Edge(Vertex from, Vertex to, float cost = 0)
    {
        this.Cost = cost;
        this.From = from;
        this.To = to;
        from.outEdges.Add(this);
        to.inEdges.Add(this);
    }

    private static int nextEdgeId = 0;
    public readonly float Cost;
    public readonly int Id = nextEdgeId++;
    public readonly Vertex From;
    public readonly Vertex To;
}