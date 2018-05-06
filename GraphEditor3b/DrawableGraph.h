#pragma once
#include "DrawableEdge.h"
#include "DrawableVertex.h"

#include "defines.h"

#include <random>

using namespace Microsoft::Graphics::Canvas;
using namespace Microsoft::Graphics::Canvas::Geometry;
using namespace Microsoft::Graphics::Canvas::UI::Xaml;
using namespace Platform;
using namespace Windows::Foundation::Numerics;
using namespace Platform::Collections;
using namespace Windows::Foundation::Collections;


ref class DrawableGraph
{
public:
    DrawableGraph();
    ~DrawableGraph();

    Guid GraphId;
    Map<Guid, DrawableVertex^>^ DrawableVertices;
    Map<Guid, DrawableEdge^>^ DrawableEdges;

    void Draw(CanvasDrawingSession^ cds);

    static DrawableGraph LayoutGraphRandom(CanvasControl^ canvas, boolean fitGraphToView, Graph^ graph);
};

