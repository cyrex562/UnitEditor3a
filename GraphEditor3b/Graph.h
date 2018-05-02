#pragma once

#include "Vertex.h"
#include "Edge.h"

using namespace Platform;
using namespace Platform::Collections;
using namespace Windows::Foundation::Collections;

ref class Graph
{
public:
    Graph();
    ~Graph();

    Guid GraphId;
    Map<Guid, Vertex^>^ Vertices;
    Map<Guid, Edge^>^ Edges;
    //Map<Guid, Vector<Guid>^>^ AdjacencyMatrix;
    //Map<Guid, Vector<Guid>^>^ IncidenceMatrix;

    boolean IsAdjacent(Guid headVertexId, Guid tailVertexId);

    Vector<Guid>^ GetNeighbors(Guid vertexId);

    boolean VertexExists(Guid vertexId);

    boolean AddVertex(Vertex^ vertexToAdd);

    boolean RemoveVertex(Guid vertexToRemove);

    int32 GetVertexValue(Guid vertexId);

    boolean SetVertexValue(Guid vertexId, int32 newValue);

    int32 GetEdgeValue(Guid edgeId);

    boolean SetEdgeValue(Guid edgeId, int32 newValue);
};

