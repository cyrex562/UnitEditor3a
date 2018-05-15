using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace GraphEditor3b3
{
    public enum ChangeType
    {
        None,
        Added,
        Modified,
        Removed,
    };

    public class VertexChangedEventArgs : EventArgs
    {
        public GraphVertex ChangedVertex { get; set; }
        public ChangeType ChangeType { get; set; }
    }

    public class EdgeChangedEventArgs : EventArgs
    {
        public GraphEdge ChangedEdge { get; set; }
        public ChangeType ChangeType { get; set; }
    }

    public delegate void VertexChangedEventHandler(Object sender, VertexChangedEventArgs e);
    public delegate void EdgeChangedEventHandler(Object sender, EdgeChangedEventArgs e);

    // Graph class
    [DataContract]
    public class Graph
    {
        public UInt32 VertexCounter { get; set; }
        public UInt32 EdgeCounter { get; set; }
        //[DataMember]
        //public Guid GraphId { get; set; }
        [DataMember]
        //public Dictionary<Guid, GraphVertex> Vertices { get; set; }
        public Dictionary<UInt32, GraphVertex> Vertices { get; set; }
        [DataMember]
        //public Dictionary<Guid, GraphEdge> Edges { get; set; }
        public Dictionary<UInt32, GraphEdge> Edges { get; set; }
        // adjacent matrix: a 2-D matrix in which the rows represent source vertices and columns represent dest vertices
        //[DataMember]
        //public Dictionary<Guid, List<Guid>> AdjacencyMatrix { get; set; }
        // incidence matrix: a 2-D boolean matrix in which the rows represent vertices and columns represent edges.
        // List of edges for a given Vertex ID
        //[DataMember]
        //public Dictionary<Guid, List<Guid>> IncidenceMatrix { get; set; }

        public DrawableGraphLayout Layout { get; set; }
        public ChangeType LastVertexChange { get; set; }
        public ChangeType LastEdgeChange { get; set; }
        public Boolean FitToView { get; set; }
        private Random rng;

        /// <summary>
        /// 
        /// </summary>
        public Graph()
        {
            Debug.WriteLine("UGraph()");
            this.Vertices = new Dictionary<UInt32, GraphVertex>();
            this.Edges = new Dictionary<UInt32, GraphEdge>();
            //this.GraphId = new Guid();
            //this.AdjacencyMatrix = new Dictionary<Guid, List<Guid>>();
            //this.IncidenceMatrix = new Dictionary<Guid, List<Guid>>();
            this.VertexCounter = 0;
            this.EdgeCounter = 0;
            this.Layout = DrawableGraphLayout.None;
            this.FitToView = Defines.FIT_GRAPH_TO_VIEW;
            this.rng = new Random();
        }

        public void Draw(CanvasDrawingSession cds)
        {
            Debug.WriteLine("drawing graph");
            foreach (KeyValuePair<UInt32, GraphVertex> kvp in this.Vertices)
            {
                kvp.Value.Draw(cds);
            }

            foreach (KeyValuePair<UInt32, GraphEdge> kvp in this.Edges)
            {
                kvp.Value.Draw(cds);
            }
        }

        public void LayoutGraphRandom(CanvasControl canvas)
        {
            this.Layout = DrawableGraphLayout.Random;

            foreach (KeyValuePair<UInt32, GraphVertex> kvp in this.Vertices)
            {
                UInt32 minX = Defines.VERTEX_SIZE;
                UInt32 minY = Defines.VERTEX_SIZE;
                UInt32 maxX = 0;
                UInt32 maxY = 0;
                UInt32 paddingValue = Defines.VERTEX_SIZE - Defines.MAX_VERT_SPACE;
                if (this.FitToView == true)
                {

                    maxX = (UInt32)canvas.ActualWidth - paddingValue;
                    maxY = (UInt32)canvas.ActualHeight - paddingValue;
                } 
                else
                {
                    maxX = Defines.VERTEX_SIZE * ((UInt32)this.Vertices.Count + Defines.MAX_VERT_SPACE);
                    maxY = maxX;
                }

                kvp.Value.Position = new Vector2(
                    this.rng.Next((Int32)minX, (Int32)maxX), 
                    this.rng.Next((Int32)minY, (Int32)maxY));
                kvp.Value.Circle = CanvasGeometry.CreateCircle(canvas, kvp.Value.Position, kvp.Value.VertexSize);
            }

            foreach (KeyValuePair<UInt32, GraphEdge> kvp in this.Edges)
            {
                CanvasPathBuilder pathBuilder = new CanvasPathBuilder(canvas);
                kvp.Value.HeadPosition = this.Vertices[kvp.Value.HeadVertexId].Position;
                kvp.Value.TailPosition = this.Vertices[kvp.Value.TailVertexId].Position;
                pathBuilder.BeginFigure(kvp.Value.HeadPosition);
                pathBuilder.AddLine(kvp.Value.TailPosition);
                pathBuilder.EndFigure(CanvasFigureLoop.Open);
                kvp.Value.Line = CanvasGeometry.CreatePath(pathBuilder);
            }
        }

        //public static Graph LayoutDGraphRandom(
        //    Boolean fitGraphToView,
        //    Random rng,
        //    CanvasControl canvas,
        //    Graph graph)
        //{
        //    Debug.WriteLine("Laying out graph in random pattern");
        //    //Dictionary<Guid, DrawableVertex> dVerts;
        //    //Dictionary<Guid, DrawableEdge> dEdges;
        //    Graph dg = new Graph();
        //    dg.Layout = DrawableGraphLayout.Random;
        //    dg.FitToView = fitGraphToView;

        //    foreach (KeyValuePair<Guid, GraphVertex> kvp in graph.Vertices)
        //    {
        //        Int32 min_x = Defines.VERTEX_SIZE;
        //        Int32 min_y = Defines.VERTEX_SIZE;
        //        Int32 max_x = 0;
        //        Int32 max_y = 0;
        //        if (fitGraphToView == true)
        //        {
        //            max_x = (Int32)canvas.ActualWidth - Defines.VERTEX_SIZE - Defines.MAX_VERT_SPACE;
        //            max_y = (Int32)canvas.ActualHeight - Defines.VERTEX_SIZE - Defines.MAX_VERT_SPACE;
        //        }
        //        else
        //        {
        //            max_x = Defines.VERTEX_SIZE * (graph.Vertices.Count + Defines.MAX_VERT_SPACE);
        //            max_y = Defines.VERTEX_SIZE * (graph.Vertices.Count + Defines.MAX_VERT_SPACE);
        //        }

        //        Vector2 circlePos = new Vector2(rng.Next(min_x, max_x), rng.Next(min_y, max_y));
        //        GraphVertex dn = new GraphVertex
        //        {
        //            Position = circlePos,
        //            VertexId = kvp.Value.VertexId,
        //            Circle = CanvasGeometry.CreateCircle(canvas, circlePos, Defines.VERTEX_SIZE)
        //        };
        //        dg.Vertices[dn.VertexId] = dn;
        //    }

        //    foreach (KeyValuePair<Guid, GraphEdge> kvp in graph.Edges)
        //    {
        //        CanvasPathBuilder pathBuilder = new CanvasPathBuilder(canvas);

        //        GraphEdge de = new GraphEdge
        //        {
        //            EdgeId = kvp.Value.EdgeId,
        //            HeadVertexId = kvp.Value.HeadVertexId,
        //            TailVertexId = kvp.Value.TailVertexId,
        //        };
        //        de.HeadPosition = dg.Vertices[de.HeadVertexId].Position;
        //        de.TailPosition = dg.Vertices[de.TailVertexId].Position;
        //        pathBuilder.BeginFigure(de.HeadPosition);
        //        pathBuilder.AddLine(de.TailPosition);
        //        pathBuilder.EndFigure(CanvasFigureLoop.Open);
        //        de.Line = CanvasGeometry.CreatePath(pathBuilder);
        //        dg.Edges[de.EdgeId] = de;
        //    }

        //    //canvas.Invalidate();
        //    return dg;
        //}

        protected virtual void OnVerticesChanged(VertexChangedEventArgs e)
        {
            Debug.WriteLine("on vertices changed");
            VertexChangedEventHandler handler = VerticesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnEdgesChanged(EdgeChangedEventArgs e)
        {
            Debug.Write("on edges changed");
            EdgeChangedEventHandler handler = EdgesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event VertexChangedEventHandler VerticesChanged;
        public event EdgeChangedEventHandler EdgesChanged;

        /// <summary>
        /// adjacent: whether there is an edge from vertex x to vertex y
        /// </summary>
        /// <param name="headVertexId"></param>
        /// <param name="tailVertexId"></param>
        /// <returns></returns>
        public Boolean Adjacent(UInt32 headVertexId, UInt32 tailVertexId)
        {
            Debug.WriteLine("checking if vertices are adjacent");
            if (this.Vertices.ContainsKey(headVertexId) == false)
            {
                return false;
            }

            GraphVertex uv = this.Vertices[headVertexId];
            return uv.Neighbors.Contains(tailVertexId);
            //foreach (UInt32 neighId in uv.Neighbors)
            //{
            //    if (neighId == tailVertexId)
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        /// <summary>
        /// neighbors: list all neighbors of a given vertex.
        /// </summary>
        /// <param name="vertexId"></param>
        /// <returns></returns>
        public SortedSet<UInt32> Neighbors(UInt32 vertexId)
        {
            Debug.WriteLine("Getting vertexes' neighbors");
            GraphVertex uv = this.Vertices[vertexId];
            return uv.Neighbors;
        }

        /// <summary>
        /// check for the existence of a vertex with a given ID
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public Boolean VertexExists(UInt32 nodeId) => this.Vertices.ContainsKey(nodeId);

        /// <summary>
        /// Add a vertex
        /// </summary>
        /// <param name="vertexToAdd"></param>
        /// <returns></returns>
        public Boolean AddVertex(GraphVertex vertexToAdd)
        {
            Debug.WriteLine("Adding vertex");
            //if (VertexExists(vertexToAdd.VertexId) == true)
            //{
            //    return false;
            //}

            vertexToAdd.VertexId = this.VertexCounter;
            this.Vertices.Add(vertexToAdd.VertexId, vertexToAdd);
            //this.AdjacencyMatrix.Add(vertexToAdd.VertexId, new List<Guid>());
            //this.IncidenceMatrix.Add(vertexToAdd.VertexId, new List<Guid>());
            this.LastVertexChange = ChangeType.Added;
            OnVerticesChanged(new VertexChangedEventArgs { ChangedVertex = vertexToAdd});
            this.VertexCounter++;
            return true;
        }

        /// <summary>
        /// Remove a vertex
        /// </summary>
        public Boolean RemoveVertex(UInt32 vertexToRemove)
        {
            Debug.WriteLine("Removing vertex");
            if (!VertexExists(vertexToRemove))
            {
                return false;
            }
            GraphVertex RemovedVertex = this.Vertices[vertexToRemove];
            this.Vertices.Remove(vertexToRemove);
            //this.AdjacencyMatrix.Remove(vertexToRemove);
            this.LastVertexChange = ChangeType.Removed;
            OnVerticesChanged(new VertexChangedEventArgs { ChangedVertex = RemovedVertex });
            return true;
        }

        /// <summary>
        /// Add an Edge between two
        /// </summary>
        /// <param name="edgeToAdd"></param>
        /// <returns></returns>
        public Boolean AddEdge(GraphEdge edgeToAdd)
        {
            Debug.WriteLine("Adding edge");
            //if (this.Edges.ContainsKey(edgeToAdd.EdgeId))
            //{
            //    return false;
            //}
            edgeToAdd.EdgeId = this.EdgeCounter;
            this.Edges.Add(edgeToAdd.EdgeId, edgeToAdd);
            this.Vertices[edgeToAdd.HeadVertexId].AddNeighbor(edgeToAdd.TailVertexId);
            this.Vertices[edgeToAdd.TailVertexId].AddNeighbor(edgeToAdd.HeadVertexId);
            //this.AdjacencyMatrix[edgeToAdd.HeadVertexId].Add(edgeToAdd.TailVertexId);
            //this.AdjacencyMatrix[edgeToAdd.TailVertexId].Add(edgeToAdd.HeadVertexId);
            //this.IncidenceMatrix[edgeToAdd.HeadVertexId].Add(edgeToAdd.EdgeId);
            //this.IncidenceMatrix[edgeToAdd.TailVertexId].Add(edgeToAdd.EdgeId);
            OnEdgesChanged(new EdgeChangedEventArgs { ChangedEdge = edgeToAdd, ChangeType = ChangeType.Added });
            this.LastEdgeChange = ChangeType.Added;
            this.EdgeCounter++;
            return true;
        }

        /// <summary>
        /// Remove an edge
        /// </summary>
        /// <param name="edgeToRemove"></param>
        /// <returns></returns>
        public Boolean RemoveEdge(UInt32 edgeToRemove)
        {
            Debug.WriteLine("Removing edge");
            GraphEdge ue = this.Edges[edgeToRemove];
            UInt32 headVertexId = ue.HeadVertexId;
            UInt32 tailVertexId = ue.TailVertexId;
            if (!this.Edges.ContainsKey(edgeToRemove))
            {
                return false;
            }
            this.Edges.Remove(edgeToRemove);
            //this.AdjacencyMatrix[headVertexId].Remove(tailVertexId);
            //this.AdjacencyMatrix[tailVertexId].Remove(headVertexId);
            //this.IncidenceMatrix[headVertexId].Remove(edgeToRemove);
            //this.IncidenceMatrix[tailVertexId].Remove(edgeToRemove);
            OnEdgesChanged(new EdgeChangedEventArgs { ChangedEdge = ue });
            this.LastEdgeChange = ChangeType.Removed;
            return true;
        }

        /// <summary>
        /// Get a vertexes' value
        /// </summary>
        /// <param name="vertexId"></param>
        /// <returns></returns>
        public Int32 GetVertexValue(UInt32 vertexId)
        {
            Debug.WriteLine("getting vertex value");
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
        public Boolean SetVertexValue(UInt32 vertexId, Int32 newValue)
        {
            Debug.WriteLine("setting vertex value");
            if (!this.VertexExists(vertexId))
            {
                return false;
            }
            GraphVertex uv = this.Vertices[vertexId];
            uv.Value = newValue;
            OnVerticesChanged(new VertexChangedEventArgs { ChangedVertex = uv});
            this.LastVertexChange = ChangeType.Modified;
            return true;
        }

        /// <summary>
        /// Get an edge's value
        /// </summary>
        /// <param name="edgeId"></param>
        /// <returns></returns>
        public Int32 GetEdgeValue(UInt32 edgeId)
        {
            Debug.WriteLine("getting edge value");
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
        public Boolean SetEdgeValue(UInt32 edgeId, Int32 newValue)
        {
            Debug.WriteLine("setting edge value");
            if (!this.Edges.ContainsKey(edgeId))
            {
                return false;
            }
            this.Edges[edgeId].Value = newValue;
            OnEdgesChanged(new EdgeChangedEventArgs { ChangedEdge = this.Edges[edgeId] });
            this.LastEdgeChange = ChangeType.Modified;
            return true;
        }

        // The current edge generation technique in this function makes several assumptions that may not scale.
        // 1) compositeId may overflow for values close to MAX_INT * 2
        // 2) cannot generate edges with directionality
        // 3) cannot generate edges with cardinality
        // 4) cannot generate edges that are self-referencing
        private static Tuple<UInt32, UInt32> GenerateEdge(
            Random rng, 
            SortedSet<Tuple<UInt32,UInt32>> currentEdgePairs,
            UInt32 highestVertexId)
        {
            UInt32 headId;
            UInt32 tailId;
            UInt32 compositeId;
            Tuple<uint, uint> outTuple;

            while(true)
            {
                headId = (uint)rng.Next(0, (int)highestVertexId);
                while (true)
                {
                    tailId = (uint)rng.Next(0, (int)highestVertexId);
                    if (headId != tailId)
                    {
                        break;
                    }
                }
                outTuple = new Tuple<uint, uint>(headId, tailId);
                if (currentEdgePairs.Add(outTuple) == true)
                {
                    break;
                }
            }

            return outTuple;
        }

        public static Graph GenerateRandomGraph(
            UInt32 minNumVerts,
            UInt32 maxNumVerts,
            Double edgeProb)
        {
            Debug.WriteLine("generate random graph");

            Random rng = new Random();
            Int32 numVerts = rng.Next((Int32)minNumVerts, (Int32)maxNumVerts);
            Graph outGraph = new Graph();

            // Create the desired number of vertices
            for (int i = 0; i < numVerts; i++)
            {
               
                GraphVertex gv = new GraphVertex
                {
                    Value = rng.Next()
                };
                outGraph.AddVertex(gv);
            }

            int minNumEdges = 0;
            int maxNumEdges = (int)((outGraph.Vertices.Count * outGraph.Vertices.Count) * edgeProb);
            int numEdges = rng.Next(minNumEdges, maxNumEdges);
            SortedSet<Tuple<uint,uint>> edgePairs = new SortedSet<Tuple<uint,uint>>();
            for (int i = 0; i < numEdges; i++)
            {
                Tuple<uint, uint> edgeTuple = GenerateEdge(rng, edgePairs, outGraph.VertexCounter);
                GraphEdge ge = new GraphEdge
                {
                    HeadVertexId = edgeTuple.Item1,
                    TailVertexId = edgeTuple.Item2,
                    Value = rng.Next()
                };
                outGraph.AddEdge(ge);
            }


            //foreach (KeyValuePair<UInt32, GraphVertex> kvp1 in outGraph.Vertices)
            //{
            //    foreach (KeyValuePair<UInt32, GraphVertex> kvp2 in outGraph.Vertices)
            //    {
            //        if (kvp1.Value.VertexId != kvp2.Value.VertexId)
            //        {
            //            Double val = rng.NextDouble();
            //            if (val >= edgeProb)
            //            {
            //                GraphEdge ge = new GraphEdge
            //                {
            //                    HeadVertexId = kvp1.Value.VertexId,
            //                    TailVertexId = kvp2.Value.VertexId,
            //                    Value = rng.Next()
            //                };
            //                outGraph.AddEdge(ge);
            //            }
            //        }
                    
            //    }
            //}

            return outGraph;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appContext"></param>
        /// <returns></returns>
        //public static Graph GenerateRandomGraph(
        //    Random rng,
        //    Int32 minNumVerts,
        //    Int32 maxNumVerts,
        //    Double edgeProb)
        //{
        //    Debug.WriteLine("generating random graph");
        //    Int32 numNodes = rng.Next(minNumVerts, maxNumVerts);
        //    Graph outGraph = new Graph();
            
        //    for (Int32 i = 0; i < numNodes; i++)
        //    {
        //        GraphVertex uv = new GraphVertex
        //        {
        //            Value = rng.Next()
        //        };
        //        outGraph.AddVertex(uv);
        //    }

        //    foreach (KeyValuePair<Guid, GraphVertex> kvp1 in outGraph.Vertices)
        //    {
        //        foreach (KeyValuePair<Guid, GraphVertex> kvp2 in outGraph.Vertices)
        //        {
        //            if (kvp1.Value.VertexId != kvp2.Value.VertexId)
        //            {
        //                Int32 prob = rng.Next(1, 10);
        //                if (prob >= edgeProb * 10)
        //                {
        //                    GraphEdge ue = new GraphEdge
        //                    {
        //                        HeadVertexId = kvp1.Value.VertexId,
        //                        TailVertexId = kvp2.Value.VertexId,
        //                        Value = rng.Next()
        //                    };
        //                    outGraph.Edges.Add(ue.EdgeId, ue);
        //                    kvp1.Value.AddNeighbor(kvp2.Value.VertexId);
        //                    kvp1.Value.AddEdge(ue.EdgeId);
        //                    kvp2.Value.AddNeighbor(kvp1.Value.VertexId);
        //                    kvp2.Value.AddEdge(ue.EdgeId);

        //                }
        //            }
        //        }
        //    }

        //    return outGraph;

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static String GraphToJson(Graph graph)
        {
            Debug.WriteLine("converting graph to JSON object");
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Graph));
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
        //public static Graph JsonToGraph(String graphString)
        //{
        //    Debug.WriteLine("converting JSON object to graph");
        //    Graph loadedGraph = new Graph();
        //    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(graphString));
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(loadedGraph.GetType());
        //    loadedGraph = ser.ReadObject(ms) as Graph;
        //    return loadedGraph;
        //}

        public void reloadGraphFromJson(String graphString)
        {
            return; 
        }

        public void ClearGraph()
        {
            this.Vertices.Clear();
            this.Edges.Clear();
        }
    }
}