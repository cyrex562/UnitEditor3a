using System;
using System.Collections.Generic;

namespace UnitEditor3a
{
    

    public class UVertex
    {
        //private int value;
        private List<Guid> neighbors;
        private List<Guid> edges;

        public Guid VertexId { get; set; }
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

        public bool AddNeighbor(Guid newNeighbor)
        {
            if (NodeInNeighbors(newNeighbor) == true)
            {
                return false;
            }

            this.neighbors.Add(newNeighbor);
            return true;
        }
    }
}