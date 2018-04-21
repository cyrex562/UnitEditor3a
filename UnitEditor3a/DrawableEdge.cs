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
        public Vector2 StartPos { get; set; }
        public Vector2 EndPos { get; set; }
        public int Value { get; set; }

        public DrawableEdge()
        {
            this.StartPos = new Vector2(0, 0);
            this.EndPos = new Vector2(0, 0);
            this.Value = -1;
        }
    }
}
