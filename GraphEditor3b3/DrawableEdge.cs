using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Numerics;

namespace GraphEditor3b3
{
    public class DrawableEdge
    {
        /// <summary>
        /// 
        /// </summary>
        public Vector2 HeadPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Vector2 TailPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid HeadVertexId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TailVertexId { get; set;}

        /// <summary>
        /// 
        /// </summary>
        public Guid EdgeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CanvasGeometry Line { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DrawableEdge()
        {
            this.HeadPosition = Vector2.Zero;
            this.TailPosition = Vector2.Zero;
            this.HeadVertexId = Guid.Empty;
            this.TailVertexId = Guid.Empty;
            this.EdgeId = Guid.Empty;
        }

        public void Draw(CanvasDrawingSession cds)
        {
            cds.DrawGeometry(this.Line, Defines.DEFAULT_NODE_COLOR, Defines.NODE_LINE_WIDTH);
        }
    }
}
