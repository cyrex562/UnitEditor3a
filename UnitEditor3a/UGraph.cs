using System;
using System.Collections.Generic;

namespace UnitEditor3a
{
    // Graph class
    public class UGraph
    {
        private Dictionary<Guid, UVertex> vertices;
        private Dictionary<Guid, UEdge> edges;
        
        // adjacent matrix: a 2-D matrix in which the rows represent source vertices and colums represent dest vertices
        //private bool[,] _adjacencyMatrix;
        // TODO: implement adjacency matrix

        // incidence matrix: a 2-D boolean matrix in which the rows represent vertices and columns represent edges.
        //private bool[,] _incidenceMatrix;
        // TODO: implement incidence matrix

        public UGraph()
        {
            this.vertices = new Dictionary<Guid, UVertex>();
            this.edges = new Dictionary<Guid, UEdge>();
            //this._adjacencyMatrix = new bool[Defines.INIT_NUM_ADJ_MAT_ELE, 
            //    Defines.INIT_NUM_ADJ_MAT_ELE];
            //this._incidenceMatrix = new bool[Defines.INIT_NUM_INC_MAT_ELE, 
            //    Defines.INIT_NUM_INC_MAT_ELE];
        }

        public int VertexCount => vertices.Count;

        public int EdgeCount => edges.Count;

        public bool Adjacent(UVertex x, UVertex y)
        {
            return false;
        }

        public Dictionary<Guid, UVertex> Vertices => this.vertices;

        public Dictionary<Guid, UEdge> Edges => this.edges;

        // adjacent: whether there is an edge from vertex x to vertex y
        public List<UVertex> Neighbors(UVertex x)
        {
            List<UVertex> outList = new List<UVertex>();
            return outList;
        }

        public bool VertexExists(Guid nodeId)
        {
            return this.vertices.ContainsKey(nodeId);
        }

        
        public bool AddVertex(UVertex vertexToAdd)
        {
            if (VertexExists(vertexToAdd.VertexId) == true)
            {
                return false;
            }

            this.vertices.Add(vertexToAdd.VertexId, vertexToAdd);
            return true;
        }


        public bool RemoveVertex(UVertex vertexToRemove)
        {
            return false;
        }

        public bool AddEdge(UVertex x, UVertex y)
        {
            return false;
        }

        public bool RemoveEdge(UVertex x, UVertex y)
        {
            return false;
        }

        public int GetVertexValue(UVertex x)
        {
            return -1;
        }

        public bool SetVertexValue(UVertex x, int newValue)
        {
            return false;
        }

        public int GetEdgeValue(UVertex x, UVertex y)
        {
            return -1;
        }

        public bool SetEdgeValue(UVertex x, UVertex y)
        {
            return false;
        }
    }
}