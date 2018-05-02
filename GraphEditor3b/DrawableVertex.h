#pragma once

using namespace Microsoft::Graphics::Canvas::Geometry;
using namespace Platform;
using namespace Windows::Foundation::Numerics;

ref class DrawableVertex
{
public:
    DrawableVertex();
    ~DrawableVertex();

    float2 Position;
    Guid VertexId;
    CanvasGeometry Circle;

};

