using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace UnitEditor3a
{
    // Graph class
    [DataContract]
    public class UGraph
    {
        [DataMember]
        public Guid GraphId {get;set;}
        [DataMember]
        public Dictionary<Guid, UVertex> Vertices { get; set;  }
        [DataMember]
        public Dictionary<Guid, UEdge> Edges { get; set;  }

        // adjacent matrix: a 2-D matrix in which the rows represent source vertices and columns represent dest vertices
        [DataMember]
        public Dictionary<Guid, List<Guid>> AdjacencyMatrix { get; set;  }

        // incidence matrix: a 2-D boolean matrix in which the rows represent vertices and columns represent edges.
        // List of edges for a given Vertex ID
        [DataMember]
        public Dictionary<Guid, List<Guid>> IncidenceMatrix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UGraph()
        {
            Debug.WriteLine("UGraph()");
            this.Vertices = new Dictionary<Guid, UVertex>();
            this.Edges = new Dictionary<Guid, UEdge>();
            this.GraphId = new Guid();
            this.AdjacencyMatrix = new Dictionary<Guid, List<Guid>>();
            this.IncidenceMatrix = new Dictionary<Guid, List<Guid>>();
        }

        //public int VertexCount => this.Vertices.Count;

        //public int EdgeCount => this.Edges.Count;

        // adjacent: whether there is an edge from vertex x to vertex y
        public Boolean Adjacent(Guid headVertexId, Guid tailVertexId)
        {
            if (this.Vertices.ContainsKey(headVertexId) == false)
            {
                return false;
            }

            UVertex uv = this.Vertices[headVertexId];
          
            foreach (Guid neighId in uv.Neighbors)
            {
                if (neighId == tailVertexId)
                {
                    return true;
                }
            }

            return false;
        }
        
        // neighbors: list all neighbors of a given vertex.
        public List<Guid> Neighbors(Guid vertexId)
        {
            UVertex uv = this.Vertices[vertexId];
            return uv.Neighbors;
        }

        // check for the existence of a vertex with a given ID
        public Boolean VertexExists(Guid nodeId) => this.Vertices.ContainsKey(nodeId);

        // Add a vertex
        public Boolean AddVertex(UVertex vertexToAdd)
        {
            if (VertexExists(vertexToAdd.VertexId) == true)
            {
                return false;
            }

            this.Vertices.Add(vertexToAdd.VertexId, vertexToAdd);
            this.AdjacencyMatrix.Add(vertexToAdd.VertexId, new List<Guid>());
            this.IncidenceMatrix.Add(vertexToAdd.VertexId, new List<Guid>());
            return true;
        }

        // Remove a vertex
        public Boolean RemoveVertex(Guid vertexToRemove)
        {
            if (!VertexExists(vertexToRemove))
            {
                return false;
            }
            this.Vertices.Remove(vertexToRemove);
            this.AdjacencyMatrix.Remove(vertexToRemove);
            return true;
        }

        // Add an edge
        public Boolean AddEdge(UEdge edgeToAdd)
        {
            if (this.Edges.ContainsKey(edgeToAdd.EdgeId))
            {
                return false;
            }
            this.Edges.Add(edgeToAdd.EdgeId, edgeToAdd);
            this.AdjacencyMatrix[edgeToAdd.HeadVertexId].Add(edgeToAdd.TailVertexId);
            this.AdjacencyMatrix[edgeToAdd.TailVertexId].Add(edgeToAdd.HeadVertexId);
            this.IncidenceMatrix[edgeToAdd.HeadVertexId].Add(edgeToAdd.EdgeId);
            this.IncidenceMatrix[edgeToAdd.TailVertexId].Add(edgeToAdd.EdgeId);
            return true;
        }

        // Remove an edge
        public Boolean RemoveEdge(Guid edgeToRemove)
        {
            UEdge ue = this.Edges[edgeToRemove];
            Guid headVertexId = ue.HeadVertexId;
            Guid tailVertexId = ue.TailVertexId;
            if (!this.Edges.ContainsKey(edgeToRemove))
            {
                return false;
            }
            this.Edges.Remove(edgeToRemove);
            this.AdjacencyMatrix[headVertexId].Remove(tailVertexId);
            this.AdjacencyMatrix[tailVertexId].Remove(headVertexId);
            this.IncidenceMatrix[headVertexId].Remove(edgeToRemove);
            this.IncidenceMatrix[tailVertexId].Remove(edgeToRemove);
            return true;
        }

        // Get a vertexes' value
        public Int32 GetVertexValue(Guid vertexId)
        {
            if (!this.VertexExists(vertexId))
            {
                return -1;
            }
            return this.Vertices[vertexId].Value;
        }

        // Set a vertexe's value
        public Boolean SetVertexValue(Guid vertexId, Int32 newValue)
        {
            if (!this.VertexExists(vertexId))
            {
                return false;
            }
            UVertex uv = this.Vertices[vertexId];
            uv.Value = newValue;
            return true;
        }

        // Get an edge's value
        public Int32 GetEdgeValue(Guid edgeId)
        {
            if (!this.Edges.ContainsKey(edgeId))
            {
                return -1;
            }
            return this.Edges[edgeId].Value;
        }

        // Set an edge's value
        public Boolean SetEdgeValue(Guid edgeId, Int32 newValue)
        {
            if (!this.Edges.ContainsKey(edgeId))
            {
                return false;
            }
            this.Edges[edgeId].Value = newValue;
            return true;
        }
    }
}