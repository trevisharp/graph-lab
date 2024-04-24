using System;
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
    public Edge Connect(Vertex from, Vertex to, float cost = 0)
        => this.AddEdge(new(from, to, cost));

    /// <summary>
    /// Create two new edges from v1 to v2 and from v2 to v1 and add to Graph.
    /// </summary>
    /// <returns>Return the edge between v1 and v2.</returns>
    public Edge BiConnect(Vertex v1, Vertex v2, float cost = 0)
    {
        this.AddEdge(new(v2, v1, cost));
        return this.AddEdge(new(v1, v2, cost));
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

    /// <summary>
    /// Create a full conected graph over a matrix. You can
    /// apply a specific cost function to every edge.
    /// The default cost is zero.
    /// </summary>
    public static Graph CreateFromMatrix(
        int n, int m = 1,
        Func<int, int, int, int, float> costFunction = null
    )
    {
        if (costFunction is null)
            costFunction = CostFunctions.Zero;

        var graph = new Graph();
        var mat = new Vertex[n * m];

        for (int j = 0; j < m; j++)
        {
            int lineStart = j * n;
            for (int i = 0; i < n; i++)
                mat[lineStart + i] = graph.AddVertex();
        }

        for (int j = 0; j < m; j++)
        {
            int lineStart = j * n;
            for (int i = 0; i < n - 1; i++)
            {
                graph.BiConnect(
                    mat[lineStart + i],
                    mat[lineStart + i + 1],
                    costFunction(i, j, i + 1, j)
                );
                
                int end = n - 1;
                graph.BiConnect(
                    mat[lineStart + end - i],
                    mat[lineStart + end - i - 1],
                    costFunction(end - i, j, end - i - 1, j)
                );
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m - 1; j++)
            {
                int end = m - 1;
                graph.BiConnect(
                    mat[j * n + i],
                    mat[(j + 1) * n + i],
                    costFunction(i, j, i, j + 1)
                );
                
                graph.BiConnect(
                    mat[(end - j) * n + i],
                    mat[(end - j - 1) * n + i],
                    costFunction(i, end - j, i, end - j - 1)
                );
            }
        }

        return graph;
    }
}