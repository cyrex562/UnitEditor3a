#pragma once

#include "defines.h"

using namespace Microsoft::Graphics::Canvas::Geometry;
using namespace Microsoft::Graphics::Canvas;
using namespace Microsoft::Graphics::Canvas::UI::Xaml;
using namespace Platform;
using namespace Windows::Foundation::Numerics;
using namespace Windows::UI;
using namespace Microsoft::Graphics::Canvas::Brushes;

ref class DrawableVertex
{
public:
    DrawableVertex();
    ~DrawableVertex();

    float2 Position;
    Guid VertexId;
    CanvasGeometry Circle;

    void Draw(CanvasDrawingSession^ cds);
};

