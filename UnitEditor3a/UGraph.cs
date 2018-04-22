using System;
using System.Collections.Generic;

namespace UnitEditor3a
{
    public class UGraph
    {
        private Dictionary<Guid, UVertex> _vertices;
        private Dictionary<Guid, UEdge> _edges;
        private bool[,] _adjacencyMatrix;
        private bool[,] _incidenceMatrix;
        
        public UGraph()
        {
            this._vertices = new Dictionary<Guid, UVertex>();
            this._edges = new Dictionary<Guid, UEdge>();
            this._adjacencyMatrix = new bool[Defines.INIT_NUM_ADJ_MAT_ELE, 
                Defines.INIT_NUM_ADJ_MAT_ELE];
            this._incidenceMatrix = new bool[Defines.INIT_NUM_INC_MAT_ELE, 
                Defines.INIT_NUM_INC_MAT_ELE];
        }

        public int VertexCount => _vertices.Count;

        public int EdgeCount => _edges.Count;

        public bool Adjacent(UVertex x, UVertex y)
        {
            return false;
        }

        public Dictionary<Guid, UVertex> Vertices => this._vertices;

        public Dictionary<Guid, UEdge> Edges => this._edges;

        // adjacent: whether there is an edge from vertex x to vertex y
        public List<UVertex> Neighbors(UVertex x)
        {
            List<UVertex> outList = new List<UVertex>();
            return outList;
        }

        public bool VertexExists(Guid nodeId)
        {
            return this._vertices.ContainsKey(nodeId);
        }

        
        public bool AddVertex(UVertex vertexToAdd)
        {
            if (VertexExists(vertexToAdd.VertexId) == true)
            {
                return false;
            }

            this._vertices.Add(vertexToAdd.VertexId, vertexToAdd);
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