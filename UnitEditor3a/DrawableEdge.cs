using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UnitEditor3a
{
    public class DrawableEdge
    {
        public Vector2 HeadPosition { get; set; }
        public Vector2 TailPosition { get; set; }
        public Guid HeadVertexId { get; set; }
        public Guid TailVertexId { get; set;}
        public Guid EdgeId { get; set; }

        public DrawableEdge()
        {
            this.HeadPosition = Vector2.Zero;
            this.TailPosition = Vector2.Zero;
            this.HeadVertexId = Guid.Empty;
            this.TailVertexId = Guid.Empty;
            this.EdgeId = Guid.Empty;
        }
    }
}
