#include "pch.h"
#include "DrawableGraph.h"


DrawableGraph::DrawableGraph()
{
    DrawableVertices = ref new Map<Guid, DrawableVertex^>();
    DrawableEdges = ref new Map<Guid, DrawableEdge^>();
}


DrawableGraph::~DrawableGraph()
{
}
