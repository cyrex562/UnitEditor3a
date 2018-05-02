#pragma once

using namespace Microsoft::Graphics::Canvas::Geometry;
using namespace Microsoft::Graphics::Canvas;
using namespace Microsoft::Graphics::Canvas::UI::Xaml;
using namespace Platform;
using namespace Windows::Foundation::Numerics;
using namespace Windows::UI;
using namespace Microsoft::Graphics::Canvas::Brushes;


#define DEF_EDGE_COLOR Colors.Black
#define DEF_EDGE_LINE_WIDTH 3

ref class DrawableEdge
{
public:
    
    DrawableEdge();
    ~DrawableEdge();
    float2 HeadPosition;
    float2 TailPosition;
    Guid HeadVertexId;
    Guid TailVertexId;
    Guid EdgeId;
    CanvasGeometry Line;
    void Draw(CanvasDrawingSession cds);
};

