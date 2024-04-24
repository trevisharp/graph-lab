using System.Collections.Generic;

namespace GraphLab;

public struct Vertex
{
    internal int Id { get; set; }
    internal List<int> Edges { get; set; }
}