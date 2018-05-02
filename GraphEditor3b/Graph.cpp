#include "pch.h"
#include "Graph.h"


Graph::Graph()
{
    GraphId = Guid::Guid();
    Vertices = ref new Map<Guid, Vertex^>();
    Edges = ref new Map<Guid, Edge^>();
    //AdjacencyMatrix = ref new Map<Guid, Vector<Guid>^>();
    //IncidenceMatrix = ref new Map<Guid, Vector<Guid>^>();
}


Graph::~Graph()
{
}

boolean Graph::IsAdjacent(Guid headVertexId, Guid tailVertexId)
{
    if (!Vertices->HasKey(headVertexId)) {
        return false;
    }

    if (!Vertices->HasKey(tailVertexId)) {
        return false;
    }

    for (Guid vertexId : Vertices->Lookup(headVertexId)->Neighbors) {
        if (vertexId == tailVertexId) {
            return true;
        }
    }

    return false;
}

Vector<Guid>^ Graph::GetNeighbors(Guid vertexId)
{
    if (Vertices->HasKey(vertexId)) {
        return Vertices->Lookup(vertexId)->Neighbors;
    }
    return nullptr;
}

boolean Graph::VertexExists(Guid vertexId) {
    return Vertices->HasKey(vertexId);
}

boolean Graph::AddVertex(Vertex^ vertexToAdd) {
    if (Vertices->HasKey(vertexToAdd->VertexId)) {
        return false;
    }

    Vertices->Insert(vertexToAdd->VertexId, vertexToAdd);
    return true;
}

boolean Graph::RemoveVertex(Guid vertexToRemove) {
    if (!Vertices->HasKey(vertexToRemove)) {
        return false;
    }

    Vertices->Remove(vertexToRemove);
    return false;
}

int32 Graph::GetVertexValue(Guid vertexId) {
    if (!Vertices->HasKey(vertexId)) {
        return -1;
    }

    return Vertices->Lookup(vertexId)->Value;
}

boolean Graph::SetVertexValue(Guid vertexId, int32 newValue) {
    if (!Vertices->HasKey(vertexId)) {
        return false;
    }

    Vertices->Lookup(vertexId)->Value = newValue;
    return true;
}

int32 Graph::GetEdgeValue(Guid edgeId) {
    if (!Edges->HasKey(edgeId)) {
        return -1;
    }

    return Edges->Lookup(edgeId)->Value;
}

boolean Graph::SetEdgeValue(Guid edgeId, int32 newValue) {
    if (!Edges->HasKey(edgeId)) {
        return false;
    }

    Edges->Lookup(edgeId)->Value = newValue;
    return true;
}





