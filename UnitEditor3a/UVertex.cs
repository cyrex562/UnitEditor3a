using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnitEditor3a
{


    [DataContract]
    public class UVertex
    {
        //private int value;
        [DataMember]
        private List<Guid> neighbors;
        [DataMember]
        private List<Guid> edges;
        [DataMember]
        public Guid VertexId { get; set; }
        [DataMember]
        public int Value { get; set; }

        public List<Guid> Neighbors
        {
            get
            {
                return this.neighbors;
            }
        }

        public UVertex()
        {
            this.neighbors = new List<Guid>();
            this.edges = new List<Guid>();
            this.VertexId = Guid.NewGuid();
            this.Value = -1;
        }

        public bool NodeInNeighbors(Guid nodeId)
        {
            foreach(Guid neighId in this.neighbors) {
                if (neighId == nodeId)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddNeighbor(Guid newNeighbor)
        {
            if (NodeInNeighbors(newNeighbor) == false)
            {
                this.neighbors.Add(newNeighbor);
            }
        }

        public void AddEdge(Guid newEdge)
        {
            if (EdgeInEdges(newEdge) == false)
            {
                this.edges.Add(newEdge);
            }
        }

        // 
        public bool EdgeInEdges(Guid edgeIdTerm)
        {
            foreach(Guid edgeId in this.edges)
            {
                if (edgeIdTerm == edgeId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}