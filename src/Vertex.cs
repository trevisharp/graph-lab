using System.Collections.Generic;

namespace GraphLab;

/// <summary>
/// Represents a data of vertex.
/// </summary>
public struct Vertex
{
    private static int nextVertexId = 0;
    public Vertex()
    {
        this.Id = nextVertexId++;
        this.inEdges = [];
        this.outEdges = [];
    }

    public readonly int Id;
    private readonly List<Edge> inEdges;
    private readonly List<Edge> outEdges;
    public IEnumerable<Edge> InEdges
    {
        get
        {
            foreach (var edge in inEdges)
                yield return edge;
        }
    }
    public IEnumerable<Edge> OutEdges
    {
        get
        {
            foreach (var edge in outEdges)
                yield return edge;
        }
    }
}