#include "pch.h"
#include "defines.h"
#include "DrawableEdge.h"
#include "DrawableVertex.h"
#include "DrawableGraph.h"
#include "Graph.h"
#include "Vertex.h"
#include "Edge.h"

using namespace Microsoft::Graphics::Canvas;
using namespace Microsoft::Graphics::Canvas::Geometry;
using namespace Microsoft::Graphics::Canvas::UI::Xaml;
using namespace Platform;
using namespace Windows::Foundation::Numerics;
using namespace Platform::Collections;
using namespace Windows::Foundation::Collections;
using namespace System;


DrawableGraph::DrawableGraph()
{
    DrawableVertices = ref new Map<Guid, DrawableVertex^>();
    DrawableEdges = ref new Map<Guid, DrawableEdge^>();
}


DrawableGraph::~DrawableGraph()
{
}

void DrawableGraph::Draw(CanvasDrawingSession^ cds) {
    for (auto dvkp : DrawableVertices) {
        dvkp->Value->Draw(cds);
    }

    for (auto dekp : DrawableEdges) {
        dekp->Value->Draw(cds);
    }
}

DrawableGraph DrawableGraph::LayoutGraphRandom(CanvasControl ^ canvas, boolean fitGraphToView, Graph^ graph, Random^ random)
{
    DrawableGraph^ dg = ref new DrawableGraph();
    
    for (auto vkp : graph->Vertices) {
        int32 minX = VERTEX_SIZE;
        int32 minY = VERTEX_SIZE;
        int32 maxX = 0;
        int32 maxY = 0;

        if (fitGraphToView) {
            maxX = canvas->ActualWidth - VERTEX_SIZE;
            maxY = canvas->ActualHeight - VERTEX_SIZE;
        }
        else {
            maxX = VERTEX_SIZE * (graph->Vertices->Size + MAX_VERT_SPACE);
            maxY = VERTEX_SIZE * (graph->Vertices->Size + MAX_VERT_SPACE);
        }

        float2 circlePos = new float2(random->)
    }

    return dg;

}
