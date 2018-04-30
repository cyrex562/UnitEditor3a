using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitEditor3a
{
    public class AppContext
    {
        public GraphState CurrentGraphState { get; set; }
        public Random RandomSource { get; set; }
        public Dictionary<Guid, DrawableVertex> DrawableVertices { get; set; }
        public Dictionary<Guid, DrawableEdge> DrawableEdges { get;set; }
        public Boolean FitGraphToView { get; set; }
        public UGraph CurrentGraph { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public AppContext()
        {
            Debug.WriteLine("AppContext()");
            this.CurrentGraphState = GraphState.None;
            this.DrawableEdges = new Dictionary<Guid, DrawableEdge>();
            this.DrawableVertices = new Dictionary<Guid, DrawableVertex>();
            this.RandomSource = new Random();
            this.FitGraphToView = Defines.FIT_GRAPH_TO_VIEW;
            this.CurrentGraph = new UGraph();
        }
    }
}
