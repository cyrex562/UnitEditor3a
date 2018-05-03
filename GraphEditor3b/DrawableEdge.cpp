#include "pch.h"
#include "DrawableEdge.h"

using namespace Microsoft::Graphics::Canvas::Geometry;
using namespace Microsoft::Graphics::Canvas;
using namespace Microsoft::Graphics::Canvas::UI::Xaml;

DrawableEdge::DrawableEdge()
{
    HeadPosition = float2::zero();
    TailPosition = float2::zero();
}


DrawableEdge::~DrawableEdge()
{
}

void DrawableEdge::Draw(CanvasDrawingSession cds)
{
    cds.DrawGeometry(Line, DEF_EDGE_COLOR, DEF_EDGE_LINE_WIDTH);
}
