#include "pch.h"
#include "DrawableEdge.h"


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
    cds.DrawGeometry(Line, )
}
