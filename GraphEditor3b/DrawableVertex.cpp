#include "pch.h"
#include "DrawableVertex.h"
#include "defines.h"

DrawableVertex::DrawableVertex()
{
    Position = float2::zero();
}


DrawableVertex::~DrawableVertex()
{
}

void DrawableVertex::Draw(CanvasDrawingSession^ cds) {
    cds->DrawGeometry(Circle, DEF_VERT_LINE_COLOR, DEF_VERT_LINE_WIDTH);
}
