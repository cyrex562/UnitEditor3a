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
        //public List<UInt32> Neighbors { get; set; }
        public SortedSet<UInt32> Neighbors { get; set; }
        [DataMember]
        //public List<UInt32> Edges { get; set; }
        public SortedSet<UInt32> Edges { get; set; }
        [DataMember]
        public UInt32 VertexId { get; set; }
        [DataMember]
        public Int32 Value { get; set; }

        // non-json members
        public Vector2 Position { get; set; }
        public CanvasGeometry Circle { get; set; }
        public Boolean Selected { get; set; }
        public UInt32 LineWidth { get; set; }
        public UInt32 VertexSize { get; set; }
        public Color LineColor { get; set; }
        public Color SelectedLineColor { get; set; }
        public Boolean Redraw { get; set; }

        public GraphVertex()
        {
            this.Neighbors = new SortedSet<UInt32>();
            this.Edges = new SortedSet<UInt32>();
            this.Value = -1;
            this.Position = Vector2.Zero;
            this.Selected = false;
            this.LineWidth = Defines.DEF_VERT_LINE_WIDTH;
            this.LineColor = Defines.DEF_VERT_LINE_COLOR;
            this.SelectedLineColor = Defines.SEL_VERT_LINE_COLOR;
            this.VertexSize = Defines.VERTEX_SIZE;
            this.Redraw = true;
        }

        public Boolean NodeInNeighbors(UInt32 nodeId)
        {
            Debug.WriteLine("checking for neighbor");
            return this.Neighbors.Contains(nodeId);

            //foreach (UInt32 neighId in this.Neighbors)
            //{
            //    if (neighId == nodeId)
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        public void AddNeighbor(UInt32 newNeighbor)
        {
            Debug.WriteLine("adding neighbor");
            if (NodeInNeighbors(newNeighbor) == false)
            {
                this.Neighbors.Add(newNeighbor);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEdge"></param>
        public void AddEdge(UInt32 newEdge)
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
        public Boolean EdgeInEdges(UInt32 edgeIdTerm)
        {
            Debug.WriteLine("checking for incident edge");
            //foreach (UInt32 edgeId in this.Edges)
            //{
            //    if (edgeIdTerm == edgeId)
            //    {
            //        return true;
            //    }
            //}

            //return false;
            return this.Edges.Contains(edgeIdTerm);
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
                return String.Format("{0}", this.VertexId);
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
