using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace UnitEditor3a
{
    /// <summary>
    /// 
    /// </summary>
    public static class GraphUtils
    {
        //private static readonly GraphUtils instance = new GraphUtils();

        /// <summary>
        /// 
        /// </summary>
        //private GraphUtils()
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        //public static GraphUtils Instance
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appContext"></param>
        /// <returns></returns>
        public static UGraph GenerateRandomGraph(AppContext appContext)
        {
            Int32 numNodes = appContext.RandomSource.Next(Defines.MIN_NUM_NODES, Defines.MAX_NUM_NODES);
            //appContext.CurrentGraph = new UGraph();
            UGraph outGraph  = new UGraph();
            //appContext.CurrentGraphState = GraphState.New;

            for (Int32 i = 0; i < numNodes; i++)
            {
                UVertex uv = new UVertex
                {
                    Value = appContext.RandomSource.Next()
                };
                outGraph.AddVertex(uv);
            }

            foreach (KeyValuePair<Guid, UVertex> kvp1 in outGraph.Vertices)
            {
                foreach (KeyValuePair<Guid, UVertex> kvp2 in outGraph.Vertices)
                {
                    if (kvp1.Value.VertexId != kvp2.Value.VertexId)
                    {
                        Int32 prob = appContext.RandomSource.Next(1, 10);
                        if (prob >= Defines.EDGE_PROBABILITY * 10)
                        {
                            UEdge ue = new UEdge
                            {
                                HeadVertexId = kvp1.Value.VertexId,
                                TailVertexId = kvp2.Value.VertexId,
                                Value = appContext.RandomSource.Next()
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static String GraphToJson(UGraph graph)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UGraph));
            MemoryStream memStream = new MemoryStream();
            serializer.WriteObject(memStream, graph);
            Byte[] json = memStream.ToArray();
            String jsonString = Encoding.UTF8.GetString(json, 0, json.Length);
            return jsonString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphString"></param>
        /// <returns></returns>
        public static UGraph JsonToGraph(String graphString)
        {
            UGraph loadedGraph = new UGraph();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(graphString));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(loadedGraph.GetType());
            loadedGraph = ser.ReadObject(ms) as UGraph;
            return loadedGraph;
        }
    }
}
