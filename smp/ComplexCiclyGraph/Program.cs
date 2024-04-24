using System;
using System.Linq;

using GraphLab;

var graph = Graph.CreateFromMatrix(
    10, 10, CostFunctions.Rand
);
var prim = graph.Prim();
var cyclePrim = prim.Clone();
var v1 = cyclePrim.GetRandomVertex();
var v2 = cyclePrim.GetRandomVertex();
cyclePrim.Connect(v1, v2);

Console.WriteLine(graph.SearchCycle());
Console.WriteLine(prim.SearchCycle());
Console.WriteLine(cyclePrim.SearchCycle());