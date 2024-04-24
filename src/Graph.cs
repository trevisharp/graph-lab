using System.Collections.Generic;

namespace GraphLab;

/// <summary>
/// Represents a generic graph.
/// </summary>
public class Graph
{
    private Dictionary<int, LinkedListNode<int>> edgeSet = [];
    private Dictionary<int, LinkedListNode<int>> vertexSet = [];
    private LinkedList<int> edges = [];
    private LinkedList<int> vertexs = [];

    /// <summary>
    /// Return true if graph contains a edge.
    /// </summary>
    public bool Contains(Edge edge)
        => edgeSet.ContainsKey(edge.Id);

    /// <summary>
    /// Create a new edge between 'from' to 'to' and add to Graph.
    /// </summary>
    public Edge Connect(Vertex from, Vertex to)
        => this.AddEdge(new(from, to));

    /// <summary>
    /// Create two new edges from v1 to v2 and from v2 to v1 and add to Graph.
    /// </summary>
    /// <returns>Return the edge between v1 and v2.</returns>
    public Edge BiConnect(Vertex v1, Vertex v2)
    {
        this.AddEdge(new(v2, v1));
        return this.AddEdge(new(v1, v2));
    }

    /// <summary>
    /// Add a existing vertex to Graph.
    /// </summary>
    public Edge AddEdge(Edge vertex)
    {
        var id = vertex.Id;
        var node = this.edges.AddLast(id);
        this.edgeSet.Add(id, node);
        return vertex;
    }

    /// <summary>
    /// Return true if graph contains a vertex.
    /// </summary>
    public bool Contains(Vertex vertex)
        => vertexSet.ContainsKey(vertex.Id);
    
    /// <summary>
    /// Create a new vertex and add to Graph.
    /// </summary>
    public Vertex AddVertex()
        => this.AddVertex(new());

    /// <summary>
    /// Add a existing vertex to Graph.
    /// </summary>
    public Vertex AddVertex(Vertex vertex)
    {
        var id = vertex.Id;
        var node = this.vertexs.AddLast(id);
        this.vertexSet.Add(id, node);
        return vertex;
    }
}