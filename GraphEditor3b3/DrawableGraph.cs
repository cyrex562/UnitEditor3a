using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace GraphEditor3b3
{
    class DrawableGraph
    {
        public Dictionary<Guid, DrawableVertex> DrawableVertices { get; set; }
        public Dictionary<Guid, DrawableEdge> DrawableEdges { get; set; }
        public DrawableGraphLayout Layout { get; set; }
        public Boolean FitToView { get; set; }

        public DrawableGraph()
        {
            this.DrawableVertices = new Dictionary<Guid, DrawableVertex>();
            this.DrawableEdges = new Dictionary<Guid, DrawableEdge>();
            this.Layout = DrawableGraphLayout.None;
            this.FitToView = true;
        }

        public void Draw(CanvasDrawingSession cds)
        {
            foreach (KeyValuePair<Guid, DrawableVertex> kvp in this.DrawableVertices)
            {
                kvp.Value.Draw(cds);
            }

            foreach (KeyValuePair<Guid, DrawableEdge> kvp in this.DrawableEdges)
            {
                kvp.Value.Draw(cds);
            }
        }

        public void Relayout()
        {
            if (this.Layout == DrawableGraphLayout.Random)
            {

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

        public static DrawableGraph LayoutDGraphRandom(
            Boolean fitGraphToView,
            Random rng,
            CanvasControl canvas,
            Graph graph)
        {
            //Dictionary<Guid, DrawableVertex> dVerts;
            //Dictionary<Guid, DrawableEdge> dEdges;
            DrawableGraph dg = new DrawableGraph();
            dg.Layout = DrawableGraphLayout.Random;
            dg.FitToView = fitGraphToView;

            foreach (KeyValuePair<Guid, Vertex> kvp in graph.Vertices)
            {
                Int32 min_x = Defines.VERTEX_SIZE;
                Int32 min_y = Defines.VERTEX_SIZE;
                Int32 max_x = 0;
                Int32 max_y = 0;
                if (fitGraphToView == true)
                {
                    max_x = (Int32)canvas.ActualWidth - Defines.VERTEX_SIZE - Defines.MAX_VERTEX_SPACE;
                    max_y = (Int32)canvas.ActualHeight - Defines.VERTEX_SIZE - Defines.MAX_VERTEX_SPACE;
                }
                else
                {
                    max_x = Defines.VERTEX_SIZE * (graph.Vertices.Count + Defines.MAX_VERTEX_SPACE);
                    max_y = Defines.VERTEX_SIZE * (graph.Vertices.Count + Defines.MAX_VERTEX_SPACE);
                }

                Vector2 circlePos = new Vector2(rng.Next(min_x, max_x), rng.Next(min_y, max_y));
                DrawableVertex dn = new DrawableVertex
                {
                    Position = circlePos,
                    VertexId = kvp.Value.VertexId,
                    Circle = CanvasGeometry.CreateCircle(canvas, circlePos, Defines.VERTEX_SIZE)
                };
                dg.DrawableVertices[dn.VertexId] = dn;
            }

            foreach (KeyValuePair<Guid, Edge> kvp in graph.Edges)
            {
                CanvasPathBuilder pathBuilder = new CanvasPathBuilder(canvas);

                DrawableEdge de = new DrawableEdge
                {
                    EdgeId = kvp.Value.EdgeId,
                    HeadVertexId = kvp.Value.HeadVertexId,
                    TailVertexId = kvp.Value.TailVertexId,
                };
                de.HeadPosition = dg.DrawableVertices[de.HeadVertexId].Position;
                de.TailPosition = dg.DrawableVertices[de.TailVertexId].Position;
                pathBuilder.BeginFigure(de.HeadPosition);
                pathBuilder.AddLine(de.TailPosition);
                pathBuilder.EndFigure(CanvasFigureLoop.Open);
                de.Line = CanvasGeometry.CreatePath(pathBuilder);
                dg.DrawableEdges[de.EdgeId] = de;
            }

            //canvas.Invalidate();
            return dg;
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
