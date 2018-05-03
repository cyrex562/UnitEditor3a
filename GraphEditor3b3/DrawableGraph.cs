using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor3b3
{
    class DrawableGraph
    {
        public Dictionary<Guid, DrawableVertex> DrawableVertices { get; set; }
        public Dictionary<Guid, DrawableEdge> DrawableEdges { get; set; }
        public DrawableGraph()
        {
            this.DrawableVertices = new Dictionary<Guid, DrawableVertex>();
            this.DrawableEdges = new Dictionary<Guid, DrawableEdge>();
        }

        public void Draw(CanvasDrawingSession cds)
        {
            foreach (KeyValuePair<Guid, DrawableVertex> kvp in DrawableVertices)
            {
                kvp.Value.Draw(cds);
            }

            foreach (KeyValuePair<Guid, DrawableEdge> kvp in DrawableEdges)
            {
                kvp.Value.Draw(cds);
            }
        }

        public static void LayoutDGraphGrid()
        {
            // clear the lists of drawable nodes and edges
            // iterate over the current graph's nodes
            // generate a drawable node 
            // store the drawable node
            // interate over the current graph's edges
            // generate a drawable edge
            // store the drawable edge
            // invalidate the curent  canvas, triggering re-draw
            throw new NotImplementedException();
        }

        public static void LayoutDGraphRandom(
            AppContext appCtx,
            CanvasControl canvas,
            Dictionary<Guid, DrawableEdge> drawableEdges,
            Dictionary<Guid, DrawableVertex> drawableVertices)
        {
            drawableEdges.Clear();
            drawableVertices.Clear();
            foreach (KeyValuePair<Guid, Vertex> kvp in appCtx.CurrentGraph.Vertices)
            {
                Int32 min_x = Defines.VERTEX_SIZE;
                Int32 min_y = Defines.VERTEX_SIZE;
                Int32 max_x = 0;
                Int32 max_y = 0;
                if (appCtx.FitGraphToView == true)
                {
                    max_x = (Int32)canvas.ActualWidth - Defines.VERTEX_SIZE;
                    max_y = (Int32)canvas.ActualHeight - Defines.VERTEX_SIZE;
                }
                else
                {
                    max_x = Defines.VERTEX_SIZE * (appCtx.CurrentGraph.Vertices.Count + Defines.MAX_VERTEX_SPACE);
                    max_y = Defines.VERTEX_SIZE * (appCtx.CurrentGraph.Vertices.Count + Defines.MAX_VERTEX_SPACE);
                }

                Vector2 circlePos = new Vector2(appCtx.RandomSource.Next(min_x, max_x), appCtx.RandomSource.Next(min_y, max_y));
                DrawableVertex dn = new DrawableVertex
                {
                    Position = circlePos,
                    VertexId = kvp.Value.VertexId,
                    Circle = CanvasGeometry.CreateCircle(canvas, circlePos, Defines.VERTEX_SIZE)
                };
                drawableVertices[dn.VertexId] = dn;
            }

            foreach (KeyValuePair<Guid, Edge> kvp in appCtx.CurrentGraph.Edges)
            {
                CanvasPathBuilder pathBuilder = new CanvasPathBuilder(canvas);

                DrawableEdge de = new DrawableEdge
                {
                    EdgeId = kvp.Value.EdgeId,
                    HeadVertexId = kvp.Value.HeadVertexId,
                    TailVertexId = kvp.Value.TailVertexId,
                };
                de.HeadPosition = drawableVertices[de.HeadVertexId].Position;
                de.TailPosition = drawableVertices[de.TailVertexId].Position;
                pathBuilder.BeginFigure(de.HeadPosition);
                pathBuilder.AddLine(de.TailPosition);
                pathBuilder.EndFigure(CanvasFigureLoop.Open);
                de.Line = CanvasGeometry.CreatePath(pathBuilder);
                drawableEdges[de.EdgeId] = de;
            }

            canvas.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void LayoutGraphView()
        {
            throw new NotImplementedException();
        }
    }
}
