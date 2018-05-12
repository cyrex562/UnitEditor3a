using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;

namespace GraphEditor3b3
{
    public class DrawableVertex
    {
        public Vector2 Position { get; set; }
        public Guid VertexId { get; set; }
        public CanvasGeometry Circle { get; set; }
        public Boolean Selected { get; set; }
        public DrawableVertex()
        {
            this.Position = Vector2.Zero;
            this.VertexId = Guid.NewGuid();
            this.Circle = null;
            this.Selected = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return base.ToString() + String.Format("X: {0}, Y: {1}, VertexId: {2},",  this.Position.X,  this.Position.Y, this.VertexId);
        }

        public void Draw(CanvasDrawingSession cds)
        {
            if (this.Selected)
            {
                cds.DrawGeometry(this.Circle, Defines.SEL_VERT_COLOR, Defines.VERT_LINE_WIDTH);
            }
            else
            {
                cds.DrawGeometry(this.Circle, Defines.DEF_VERT_COLOR, Defines.VERT_LINE_WIDTH);
            }
        } 

        public void ToggleSelect()
        {
            if (this.Selected)
            {
                this.Selected = false;
            } else
            {
                this.Selected = true;
            }
        }
    }
}
