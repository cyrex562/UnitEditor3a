#pragma once

#include "EdgeDirection.h"

using namespace Platform;
using namespace Platform::Collections;
using namespace Windows::Foundation::Collections;

ref class Edge
{
public:
    Edge();
    ~Edge();

    Guid HeadVertexId;
    Guid TailVertexId;
    int32 Value;
    EdgeDirection Direction;
    Guid EdgeId;
};

