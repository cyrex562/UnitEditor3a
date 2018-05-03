#include "pch.h"
#include "DrawableEdge.h"

using namespace Microsoft::Graphics::Canvas::Geometry;
using namespace Microsoft::Graphics::Canvas;
using namespace Microsoft::Graphics::Canvas::UI::Xaml;
using namespace Platform;
using namespace Windows::Foundation::Numerics;
using namespace Windows::UI;
using namespace Microsoft::Graphics::Canvas::Brushes;

DrawableEdge::DrawableEdge()
{
    HeadPosition = float2::zero();
    TailPosition = float2::zero();
}


DrawableEdge::~DrawableEdge()
{
}

void DrawableEdge::Draw(CanvasDrawingSession^ cds)
{
    cds->DrawGeometry(Line, DEF_EDGE_COLOR, DEF_EDGE_LINE_WIDTH);
}
