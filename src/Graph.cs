using System.Collections.Generic;

namespace GraphLab;

using Internal;

/// <summary>
/// Represents a generic graph.
/// </summary>
public class Graph
{
    private HashSet<(int, int)> edgeSet = [];
    private Dictionary<int, LinkedListNode<int>> vertexSet = [];
    private LinkedList<int> vertexs = [];

    public Vertex AddVertex()
        => this.AddVertex(
            VertexManager.CreateVertex()
        );

    public Vertex AddVertex(Vertex vertex)
    {
        var id = vertex.Id;
        var node = this.vertexs.AddLast(id);
        this.vertexSet.Add(id, node);
        return vertex;
    }
}