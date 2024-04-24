using GraphLab;

var graph = Graph.CreateFromMatrix(
    10, 10, CostFunctions.Rand
);
var prim = graph.Prim();

