#include "pch.h"
#include "Edge.h"


Edge::Edge()
{
    Value = -1;
    Direction = EdgeDirection::None;
    EdgeId = Guid::Guid();
}


Edge::~Edge()
{
}
