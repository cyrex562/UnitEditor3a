using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GraphEditor3b3
{
    // Graph class
    [DataContract]
    public class Graph
    {
        [DataMember]
        public Guid GraphId { get; set; }
        [DataMember]
        public Dictionary<Guid, Vertex> Vertices { get; set; }
        [DataMember]
        public Dictionary<Guid, Edge> Edges { get; set; }
        // adjacent matrix: a 2-D matrix in which the rows represent source vertices and columns represent dest vertices
        [DataMember]
        public Dictionary<Guid, List<Guid>> AdjacencyMatrix { get; set; }
        // incidence matrix: a 2-D boolean matrix in which the rows represent vertices and columns represent edges.
        // List of edges for a given Vertex ID
        [DataMember]
        public Dictionary<Guid, List<Guid>> IncidenceMatrix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Graph()
        {
            Debug.WriteLine("UGraph()");
            this.Vertices = new Dictionary<Guid, Vertex>();
            this.Edges = new Dictionary<Guid, Edge>();
            this.GraphId = new Guid();
            this.AdjacencyMatrix = new Dictionary<Guid, List<Guid>>();
            this.IncidenceMatrix = new Dictionary<Guid, List<Guid>>();
        }

        /// <summary>
        /// adjacent: whether there is an edge from vertex x to vertex y
        /// </summary>
        /// <param name="headVertexId"></param>
        /// <param name="tailVertexId"></param>
        /// <returns></returns>
        public Boolean Adjacent(Guid headVertexId, Guid tailVertexId)
        {
            if (this.Vertices.ContainsKey(headVertexId) == false)
            {
                return false;
            }

            Vertex uv = this.Vertices[headVertexId];

            foreach (Guid neighId in uv.Neighbors)
            {
                if (neighId == tailVertexId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// neighbors: list all neighbors of a given vertex.
        /// </summary>
        /// <param name="vertexId"></param>
        /// <returns></returns>
        public List<Guid> Neighbors(Guid vertexId)
        {
            Vertex uv = this.Vertices[vertexId];
            return uv.Neighbors;
        }

        /// <summary>
        /// check for the existence of a vertex with a given ID
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public Boolean VertexExists(Guid nodeId) => this.Vertices.ContainsKey(nodeId);

        /// <summary>
        /// Add a vertex
        /// </summary>
        /// <param name="vertexToAdd"></param>
        /// <returns></returns>
        public Boolean AddVertex(Vertex vertexToAdd)
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

        /// <summary>
        /// Remove a vertex
        /// </summary>
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

        /// <summary>
        /// Add an edge
        /// </summary>
        /// <param name="edgeToAdd"></param>
        /// <returns></returns>
        public Boolean AddEdge(Edge edgeToAdd)
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

        /// <summary>
        /// Remove an edge
        /// </summary>
        /// <param name="edgeToRemove"></param>
        /// <returns></returns>
        public Boolean RemoveEdge(Guid edgeToRemove)
        {
            Edge ue = this.Edges[edgeToRemove];
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

        /// <summary>
        /// Get a vertexes' value
        /// </summary>
        /// <param name="vertexId"></param>
        /// <returns></returns>
        public Int32 GetVertexValue(Guid vertexId)
        {
            if (!this.VertexExists(vertexId))
            {
                return -1;
            }
            return this.Vertices[vertexId].Value;
        }

        /// <summary>
        /// Set a vertexe's value
        /// </summary>
        /// <param name="vertexId"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public Boolean SetVertexValue(Guid vertexId, Int32 newValue)
        {
            if (!this.VertexExists(vertexId))
            {
                return false;
            }
            Vertex uv = this.Vertices[vertexId];
            uv.Value = newValue;
            return true;
        }

        /// <summary>
        /// Get an edge's value
        /// </summary>
        /// <param name="edgeId"></param>
        /// <returns></returns>
        public Int32 GetEdgeValue(Guid edgeId)
        {
            if (!this.Edges.ContainsKey(edgeId))
            {
                return -1;
            }
            return this.Edges[edgeId].Value;
        }

        /// <summary>
        /// Set an edge's value
        /// </summary>
        /// <param name="edgeId"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public Boolean SetEdgeValue(Guid edgeId, Int32 newValue)
        {
            if (!this.Edges.ContainsKey(edgeId))
            {
                return false;
            }
            this.Edges[edgeId].Value = newValue;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appContext"></param>
        /// <returns></returns>
        public static Graph GenerateRandomGraph(Random rng)
        {
            Int32 numNodes = rng.Next(Defines.MIN_NUM_NODES, Defines.MAX_NUM_NODES);
            Graph outGraph = new Graph();
            
            for (Int32 i = 0; i < numNodes; i++)
            {
                Vertex uv = new Vertex
                {
                    Value = rng.Next()
                };
                outGraph.AddVertex(uv);
            }

            foreach (KeyValuePair<Guid, Vertex> kvp1 in outGraph.Vertices)
            {
                foreach (KeyValuePair<Guid, Vertex> kvp2 in outGraph.Vertices)
                {
                    if (kvp1.Value.VertexId != kvp2.Value.VertexId)
                    {
                        Int32 prob = rng.Next(1, 10);
                        if (prob >= Defines.EDGE_PROBABILITY * 10)
                        {
                            Edge ue = new Edge
                            {
                                HeadVertexId = kvp1.Value.VertexId,
                                TailVertexId = kvp2.Value.VertexId,
                                Value = rng.Next()
                            };
                            outGraph.Edges.Add(ue.EdgeId, ue);
                            kvp1.Value.AddNeighbor(kvp2.Value.VertexId);
                            kvp1.Value.AddEdge(ue.EdgeId);
                            kvp2.Value.AddNeighbor(kvp1.Value.VertexId);
                            kvp2.Value.AddEdge(ue.EdgeId);

                        }
                    }
                }
            }

            return outGraph;

        }

        public static String GraphToJson(Graph graph)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Graph));
            MemoryStream memStream = new MemoryStream();
            serializer.WriteObject(memStream, graph);
            Byte[] json = memStream.ToArray();
            String jsonString = Encoding.UTF8.GetString(json, 0, json.Length);
            return jsonString;
        }

        public static Graph JsonToGraph(String graphString)
        {
            Graph loadedGraph = new Graph();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(graphString));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(loadedGraph.GetType());
            loadedGraph = ser.ReadObject(ms) as Graph;
            return loadedGraph;
        }
    }
}