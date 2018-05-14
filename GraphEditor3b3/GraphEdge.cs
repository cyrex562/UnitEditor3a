using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System.Diagnostics;
using System.Numerics;
using Windows.UI;

namespace GraphEditor3b3
{
    [DataContract]
    public class GraphEdge
    {
        [DataMember]
        public Guid HeadVertexId { get; set; }
        [DataMember]
        public Guid TailVertexId { get; set; }
        [DataMember]
        public Int32 Value { get; set; }
        [DataMember]
        public EdgeDirection Direction { get; set; }
        [DataMember]
        public Guid EdgeId { get; set; }

        // non-json members
        public Vector2 HeadPosition { get; set; }
        public Vector2 TailPosition { get; set; }
        public CanvasGeometry Line { get; set; }
        public Boolean Selected { get; set; }
        public Int32 LineWidth { get; set; }
        public Color LineColor { get; set; }
        public Color SelectedLineColor { get; set; }
        public Boolean Redraw { get; set; }

        public GraphEdge()
        {
            this.HeadVertexId = Guid.Empty;
            this.TailVertexId = Guid.Empty;
            this.Value = -1;
            this.Direction = EdgeDirection.None;
            this.EdgeId = Guid.NewGuid();
            this.HeadPosition = Vector2.Zero;
            this.TailPosition = Vector2.Zero;
            this.Selected = false;
            this.LineWidth = Defines.DEF_EDGE_LINE_WIDTH;
            this.LineColor = Defines.DEF_EDGE_COLOR;
            this.SelectedLineColor = Defines.SEL_EDGE_COLOR;
            this.Redraw = true;
        }

        public String ListItemText
        {
            get
            {
                return String.Format("{0}: {1} -- {2}: {3}", this.EdgeId, this.HeadVertexId, this.TailVertexId, this.Value);
            }
        }

        public String ShortListItemText
        {
            get
            {
                return String.Format("{0}", this.EdgeId.ToString().Substring(0, 6));
            }
        }

        public void Select()
        {
            this.Selected = true;
            this.Redraw = true;
        }

        public void DeSelect()
        {
            this.Selected = false;
            this.Redraw = true;
        }

        public void Draw(CanvasDrawingSession cds)
        {
            if (this.Selected == true)
            {
                cds.DrawGeometry(this.Line, this.SelectedLineColor, this.LineWidth);
            }
            else
            {
                cds.DrawGeometry(this.Line, this.LineColor, this.LineWidth);
            }
        }
    }
}
