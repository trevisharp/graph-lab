using System;
using System.Collections.Generic;

namespace GraphLab;

/// <summary>
/// Represents a generic graph.
/// </summary>
public class Graph
{
    private Dictionary<int, LinkedListNode<Edge>> edgeSet = [];
    private Dictionary<int, LinkedListNode<Vertex>> vertexSet = [];
    private LinkedList<Edge> edges = [];
    private LinkedList<Vertex> vertexs = [];

    /// <summary>
    /// Get the vertex collection of this Graph.
    /// </summary>
    public IEnumerable<Vertex> Vertexes
    {
        get
        {
            foreach (var vertex in this.vertexs)
                yield return vertex;
        }
    }
    
    /// <summary>
    /// Get the edge collection of this Graph.
    /// </summary>
    public IEnumerable<Edge> Edges
    {
        get
        {
            foreach (var edge in this.edges)
                yield return edge;
        }
    }

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
    public Edge AddEdge(Edge edge)
    {
        var id = edge.Id;
        var node = this.edges.AddLast(edge);
        this.edgeSet.Add(id, node);
        return edge;
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
        var node = this.vertexs.AddLast(vertex);
        this.vertexSet.Add(id, node);
        return vertex;
    }

    /// <summary>
    /// Get a Random Vertex from Graph.
    /// </summary>
    public Vertex GetRandomVertex()
    {
        var index = Random.Shared.Next(this.vertexs.Count);
        var crr = this.vertexs.First;
        for (int i = 0; i < index; i++)
            crr = crr.Next;
        return crr.Value;
    }

    /// <summary>
    /// Copy the Graph data
    /// </summary>
    public Graph Clone()
    {
        var graph = new Graph();

        foreach (var vertex in this.vertexs)
            graph.AddVertex(vertex);
        
        foreach (var edge in this.edges)
            graph.AddEdge(edge);
        
        return graph;
    }

    /// <summary>
    /// Execute Prim's algorithm getting a minimal minimum spanning tree.
    /// </summary>
    /// <returns>A clone of original graph with Prim's aplied.</returns>
    public Graph Prim()
    {
        var graph = new Graph();
        int missing = this.vertexs.Count;
        if (missing == 0)
            return graph;

        var first = this.vertexs.First.Value;
        graph.AddVertex(first);
        missing--;

        PriorityQueue<Edge, float> queue = new();
        foreach (var edge in first.outEdges)
            if (Contains(edge))
                queue.Enqueue(edge, edge.Cost);

        while (missing > 0)
        {
            if (queue.Count == 0)
                break; 
            var edge = queue.Dequeue();

            if (!Contains(edge.To))
                continue;
            
            if (graph.Contains(edge.To))
                continue;
            
            missing--;
            graph.AddEdge(edge);
            graph.AddVertex(edge.To);

            foreach (var newEdge in edge.To.outEdges)
                if (Contains(newEdge))
                    queue.Enqueue(newEdge, newEdge.Cost);
        }

        return graph;
    }

    /// <summary>
    /// Search and return true if this graph has a cycle.
    /// </summary>
    /// <returns></returns>
    public bool SearchCycle()
    {
        int missing = this.vertexs.Count;
        if (missing == 0)
            return false;

        HashSet<Vertex> hash = [];
        var first = this.vertexs.First.Value;
        hash.Add(first);
        missing--;

        Queue<Edge> queue = new();
        foreach (var edge in first.outEdges)
            if (Contains(edge))
                queue.Enqueue(edge);

        while (missing > 0)
        {
            if (queue.Count == 0)
                break; 
            var edge = queue.Dequeue();

            if (!Contains(edge.To))
                continue;
            
            if (hash.Contains(edge.To))
                return true;
            
            missing--;
            hash.Add(edge.To);

            foreach (var newEdge in edge.To.outEdges)
                if (Contains(newEdge))
                    queue.Enqueue(newEdge);
        }

        return false;


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
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m - 1; j++)
            {
                graph.BiConnect(
                    mat[j * n + i],
                    mat[(j + 1) * n + i],
                    costFunction(i, j, i, j + 1)
                );
            }
        }

        return graph;
    }
}
