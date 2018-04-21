using System;
using System.Collections.Generic;

namespace UnitEditor3a
{
    

    public class UGraph
    {
        

        private Dictionary<Guid, UNode> _nodes;
        private Dictionary<Guid, UEdge> _edges;
        private bool[,] _adjacencyMatrix;
        private bool[,] _incidenceMatrix;
        
        public UGraph(int numNodes)
        {
            this._nodes = new Dictionary<Guid, UNode>();
            this._edges = new Dictionary<Guid, UEdge>();
            this._adjacencyMatrix = new bool[Defines.INIT_NUM_ADJ_MAT_ELE, 
                Defines.INIT_NUM_ADJ_MAT_ELE];
            this._incidenceMatrix = new bool[Defines.INIT_NUM_INC_MAT_ELE, 
                Defines.INIT_NUM_INC_MAT_ELE];
        }

        public int NodeCount => _nodes.Count;

        public int EdgeCount => _edges.Count;

        public bool Adjacent(UNode x, UNode y)
        {
            return false;
        }

        // adjacent: whether there is an edge from vertex x to vertex y
        public List<UNode> Neighbors(UNode x)
        {
            List<UNode> outList = new List<UNode>();
            return outList;
        }

        public bool NodeExists(Guid nodeId)
        {
            return this._nodes.ContainsKey(nodeId);
        }

        
        public bool AddVertex(UNode newNode)
        {
            if (NodeExists(newNode.NodeId) == true)
            {
                return false;
            }

            this._nodes.Add(newNode.NodeId, newNode);
            return true;
        }


        public bool RemoveVertex(UNode nodeToRemove)
        {
            return false;
        }

        public bool AddEdge(UNode x, UNode y)
        {
            return false;
        }

        public bool RemoveEdge(UNode x, UNode y)
        {
            return false;
        }

        public int GetNodeValue(UNode x)
        {
            return -1;
        }

        public bool SetNodeValue(UNode x, int newValue)
        {
            return false;
        }

        public int GetEdgeValue(UNode x, UNode y)
        {
            return -1;
        }

        public bool SetEdgeValue(UNode x, UNode y)
        {
            return false;
        }
    }
}