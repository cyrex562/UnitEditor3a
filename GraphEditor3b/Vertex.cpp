#include "pch.h"
#include "Vertex.h"


Vertex::Vertex()
{
    Neighbors = ref new Vector<Guid>();
    Edges = ref new Vector<Guid>();
    VertexId = Guid::Guid();
}


Vertex::~Vertex()
{
}

boolean Vertex::IsNeighbor(Guid vertexId)
{
    for (Guid neighVertId : Neighbors) {
        if (vertexId == neighVertId) {
            return true;
        }
    }
    return false;
}

boolean Vertex::IsEdgeIncident(Guid edgeId)
{
    for (Guid memberEdgeId : Edges) {
        if (memberEdgeId == edgeId) {
            return true;
        }
    }
}


