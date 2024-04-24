using System.Collections.Generic;

namespace GraphLab.Internal;

internal class VertexManager
{
    private static Dictionary<int, List<int>> edgeMap = [];
    private static int nextVertexId = 0;

    internal static Vertex CreateVertex()
    {
        var id = nextVertexId++;
        var vertex = new Vertex {
            Id = id,
            Edges = []
        };
        edgeMap.Add(id, vertex.Edges);
        return vertex;
    }
}