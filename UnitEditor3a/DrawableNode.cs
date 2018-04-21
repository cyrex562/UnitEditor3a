using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UnitEditor3a
{
    public class DrawableNode
    {
        //public int X { get; set; }
        //public int Y { get; set; }

        public Vector2 Position { get; set; }

        public Guid NodeId { get; set; }

        // TODO: add node ID
        public DrawableNode()
        {
            this.Position = new Vector2(-1, -1);
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("X: {0}, Y: {1}", 
                this.Position.X, 
                this.Position.Y);
        }
    }
}
