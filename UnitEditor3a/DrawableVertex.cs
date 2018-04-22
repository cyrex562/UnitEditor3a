using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UnitEditor3a
{
    public class DrawableVertex
    {
        //public int X { get; set; }
        //public int Y { get; set; }

        public Vector2 Position { get; set; }

        public Guid VertexId { get; set; }

        // TODO: add node ID
        public DrawableVertex()
        {
            this.Position = Vector2.Zero;
            this.VertexId = Guid.Empty;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("X: {0}, Y: {1}, VertexId: {2},",  this.Position.X,  this.Position.Y, this.VertexId);
        }
    }
}
