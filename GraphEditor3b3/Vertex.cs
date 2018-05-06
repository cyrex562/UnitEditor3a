using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GraphEditor3b3
{


    [DataContract]
    public class Vertex
    {
        [DataMember]
        public List<Guid> Neighbors { get; set; }
        [DataMember]
        public List<Guid> Edges { get; set; }
        [DataMember]
        public Guid VertexId { get; set; }
        [DataMember]
        public Int32 Value { get; set; }

        public Vertex()
        {
            this.Neighbors = new List<Guid>();
            this.Edges = new List<Guid>();
            this.VertexId = Guid.NewGuid();
            this.Value = -1;
        }

        public Boolean NodeInNeighbors(Guid nodeId)
        {
            foreach(Guid neighId in this.Neighbors) {
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
                this.Neighbors.Add(newNeighbor);
            }
        }

        public void AddEdge(Guid newEdge)
        {
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
            foreach(Guid edgeId in this.Edges)
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