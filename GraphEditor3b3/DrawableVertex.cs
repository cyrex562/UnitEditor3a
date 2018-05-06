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
        /// <summary>
        /// 
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid VertexId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CanvasGeometry Circle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DrawableVertex()
        {
            this.Position = Vector2.Zero;
            this.VertexId = Guid.NewGuid();
            this.Circle = null;
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
            cds.DrawGeometry(this.Circle, Defines.DEFAULT_NODE_COLOR, Defines.NODE_LINE_WIDTH);
        } 
    }
}
