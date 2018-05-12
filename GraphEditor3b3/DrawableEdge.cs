using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Numerics;

namespace GraphEditor3b3
{
    public class DrawableEdge
    {
        public Vector2 HeadPosition { get; set; }
        public Vector2 TailPosition { get; set; }
        public Guid HeadVertexId { get; set; }
        public Guid TailVertexId { get; set;}
        public Guid EdgeId { get; set; }
        public CanvasGeometry Line { get; set; }
        public Boolean Selected { get; set; }

        public DrawableEdge()
        {
            this.HeadPosition = Vector2.Zero;
            this.TailPosition = Vector2.Zero;
            this.HeadVertexId = Guid.Empty;
            this.TailVertexId = Guid.Empty;
            this.EdgeId = Guid.Empty;
            this.Selected = false;
        }

        public void Draw(CanvasDrawingSession cds)
        {
            if (this.Selected)
            {
                cds.DrawGeometry(this.Line, Defines.SEL_EDGE_COLOR, Defines.DEF_EDGE_LINE_WIDTH);
            }
            else
            {
                cds.DrawGeometry(this.Line, Defines.DEF_EDGE_COLOR, Defines.DEF_EDGE_LINE_WIDTH);
            }
            
        }

        public void ToggleSelect()
        {
            if (this.Selected)
            {
                this.Selected = false;
            }
            else
            {
                this.Selected = true;
            }
        }
    }
}
