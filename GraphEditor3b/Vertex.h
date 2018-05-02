#pragma once

using namespace Platform;
using namespace Platform::Collections;
using namespace Windows::Foundation::Collections;

ref class Vertex
{
public:
    Vertex();
    ~Vertex();

    Vector<Guid>^ Neighbors;
    Vector<Guid>^ Edges;
    Guid VertexId;
    int32 Value;

    boolean IsNeighbor(Guid vertexId);

    boolean IsEdgeIncident(Guid edgeId);


};

