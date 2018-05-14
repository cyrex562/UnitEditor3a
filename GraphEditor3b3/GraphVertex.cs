using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System.Numerics;
using Windows.UI;

namespace GraphEditor3b3
{
    [DataContract]
    public class GraphVertex
    {
        [DataMember]
        public List<Guid> Neighbors { get; set; }
        [DataMember]
        public List<Guid> Edges { get; set; }
        [DataMember]
        public Guid VertexId { get; set; }
        [DataMember]
        public Int32 Value { get; set; }

        // non-json members
        public Vector2 Position { get; set; }
        public CanvasGeometry Circle { get; set; }
        public Boolean Selected { get; set; }
        public Int32 LineWidth { get; set; }
        public Int32 VertexSize { get; set; }
        public Color LineColor { get; set; }
        public Color SelectedLineColor { get; set; }
        public Boolean Redraw { get; set; }

        public GraphVertex()
        {
            this.Neighbors = new List<Guid>();
            this.Edges = new List<Guid>();
            this.VertexId = Guid.NewGuid();
            this.Value = -1;
            this.Position = Vector2.Zero;
            this.Selected = false;
            this.LineWidth = Defines.DEF_VERT_LINE_WIDTH;
            this.LineColor = Defines.DEF_VERT_LINE_COLOR;
            this.SelectedLineColor = Defines.SEL_VERT_LINE_COLOR;
            this.VertexSize = Defines.VERTEX_SIZE;
            this.Redraw = true;
        }

        public Boolean NodeInNeighbors(Guid nodeId)
        {
            Debug.WriteLine("checking for neighbor");
            foreach (Guid neighId in this.Neighbors)
            {
                if (neighId == nodeId)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddNeighbor(Guid newNeighbor)
        {
            Debug.WriteLine("adding neighbor");
            if (NodeInNeighbors(newNeighbor) == false)
            {
                this.Neighbors.Add(newNeighbor);
            }
        }

        public void AddEdge(Guid newEdge)
        {
            Debug.WriteLine("adding edge");
            if (EdgeInEdges(newEdge) == false)
            {
                this.Edges.Add(newEdge);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edgeIdTerm"></param>
        /// <returns></returns>
        public Boolean EdgeInEdges(Guid edgeIdTerm)
        {
            Debug.WriteLine("checking for incident edge");
            foreach (Guid edgeId in this.Edges)
            {
                if (edgeIdTerm == edgeId)
                {
                    return true;
                }
            }

            return false;
        }

        public String ListItemText
        {
            get
            {
                return String.Format("{0}: {1}", this.VertexId, this.Value);
            }
        }

        public String ShortListItemText
        {
            get
            {
                return String.Format("{0}", this.VertexId.ToString().Substring(0, 6));
            }
        }

        public void Draw(CanvasDrawingSession cds)
        {
            Debug.WriteLine("Drawing Vertex");
            if (this.Selected == true)
            {
                cds.DrawGeometry(this.Circle, this.SelectedLineColor, this.LineWidth);
            }
            else
            {
                cds.DrawGeometry(this.Circle, this.LineColor, this.LineWidth);
            }
            this.Redraw = false;
        }

        public void Select()
        {
            this.Selected = true;
            this.Redraw = true;
        }

        public void Deselect()
        {
            this.Selected = false;
            this.Redraw = true;
        }
    }
}
