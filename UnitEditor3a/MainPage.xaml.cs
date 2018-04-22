using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Diagnostics;
using Microsoft.Graphics.Canvas;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnitEditor3a
{
    // Graph object
    
    // neighbors: lists all vertices y such that there is an edge from x to y
    // add_vertex: add a vertex x if it does not exist
    // remove_vertex: remove vertex x if it exists
    // add_edge: add edge from vertex X to vertex Y if it does not exist
    // remove_edge: remove edge from vertex X to vertex Y if it exists
    // get_vertex_value: retrieve value for vertrex
    // set_vertex_value: set value for vertex
    // get_edge_value: return value for edge
    // set_edge_value: set value for edge.
    // adjacency list: every vertex stores a list of adjacent vertices.
    // vertices can also store incident edges and edges can store incident vertices
    // adjacent matrix: a 2-D matrix in which the rows represent source 
    //  vertices and colums represent dest vertices
    // incidence matrix: a 2-D boolean matrix in which the rows represent vertices and columns represent edges.

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        private Dictionary<Guid, DrawableVertex> drawableVertices;
        private Dictionary<Guid, DrawableEdge> drawableEdges;

        private Random rng;

        private bool fitGraphToView;

        private UGraph currentGraph;


      
        public MainPage()
        {
            Debug.WriteLine("MainPage()");
            this.InitializeComponent();
            this.drawableVertices = new Dictionary<Guid, DrawableVertex>();
            this.drawableEdges = new Dictionary<Guid, DrawableEdge>();
            this.rng = new Random();
            this.fitGraphToView = false;
        }

        public void GenerateRandomGraph()
        {
            //drawableNodes.Clear();
            //// create a graph data structure
            //int nodeCount = rng.Next(Defines.MIN_NUM_NODES, Defines.MAX_NUM_NODES);

            //for (int i = 0; i < nodeCount; i++)
            //{
            //    int x = rng.Next(Defines.MIN_X, Defines.MAX_X);
            //    int y = rng.Next(Defines.MIN_Y, Defines.MAX_Y);
            //    int val = rng.Next();
            //    DrawableNode dn = new DrawableNode
            //    {
            //        Position = new Vector2(x, y)
            //    };
            //    drawableNodes[dn.NodeId] = dn;
            //    Debug.WriteLine(dn, string.Format("dn {0}", i));
            //}

            int numNodes = rng.Next(Defines.MIN_NUM_NODES, Defines.MAX_NUM_NODES);
            this.currentGraph = new UGraph();
            for (int i = 0; i < numNodes; i++)
            {
                UVertex uv = new UVertex();
                uv.Value = rng.Next();
                this.currentGraph.AddVertex(uv);
            }

            // add edges
            // for each pair of edges in the graph, pseudo-randomly determine if edge should exist

            foreach (KeyValuePair<Guid, UVertex> kvp1 in this.currentGraph.Vertices)
            {
                foreach(KeyValuePair<Guid,UVertex> kvp2 in this.currentGraph.Vertices)
                {
                    if (kvp1.Value.VertexId != kvp2.Value.VertexId)
                    {
                        int prob = this.rng.Next(1, 10);
                        if (prob >= Defines.EDGE_PROBABILITY * 10)
                        {
                            UEdge ue = new UEdge
                            {
                                HeadVertexId = kvp1.Value.VertexId,
                                TailVertexId = kvp2.Value.VertexId,
                                Value = this.rng.Next()
                            };
                            this.currentGraph.Edges.Add(ue.EdgeId, ue);
                        }
                    }
                    
                }
            }

            // layout the generated graph
            LayoutDGraphRandom();
        }

        void DrawNode(CanvasDrawingSession cds, DrawableVertex dn)
        {
            cds.DrawCircle(dn.Position, Defines.VERTEX_SIZE, Defines.DEFAULT_NODE_COLOR, Defines.NODE_LINE_WIDTH);
        }

        private void DrawEdge(CanvasDrawingSession cds, DrawableEdge de)
        {
            cds.DrawLine(de.HeadPosition, de.TailPosition, Defines.DEF_EDGE_COLOR, Defines.DEF_EDGE_LINE_WIDTH);
        }

        DrawableVertex GetDrawableNodeByNodeId(Guid nodeId) => this.drawableVertices[nodeId];

        void LayoutDGraphRandom()
        {

            // clear the lists of drawable nodes and edges
            this.drawableEdges.Clear();
            this.drawableVertices.Clear();
            // iterate over the current graph's nodes
            foreach (KeyValuePair<Guid, UVertex> kvp in this.currentGraph.Vertices)
            {
                // generate a drawable node 
                Int32 min_x = 0;
                Int32 min_y = 0;
                Int32 max_x = 0;
                Int32 max_y = 0;
                if (this.fitGraphToView)
                {
                    max_x = (Int32)MainDrawingCanvas.ActualWidth;
                    max_y = (Int32)MainDrawingCanvas.ActualWidth;
                }
                else
                {
                    max_x = Defines.VERTEX_SIZE * (this.currentGraph.VertexCount + Defines.MAX_VERTEX_SPACE);
                    max_y = Defines.VERTEX_SIZE * (this.currentGraph.VertexCount + Defines.MAX_VERTEX_SPACE);
                }

                DrawableVertex dn = new DrawableVertex
                {
                    Position = new Vector2(this.rng.Next(min_x, max_x), this.rng.Next(min_y, max_y)),
                    VertexId = kvp.Value.VertexId
                };
                // store the drawable node
                this.drawableVertices[dn.VertexId] = dn;
            }
            
            
            // interate over the current graph's edges
            foreach(KeyValuePair<Guid, UEdge> kvp in this.currentGraph.Edges)
            {
                // generate a drawable edge
                DrawableEdge de = new DrawableEdge
                {
                    EdgeId = kvp.Value.EdgeId,
                    HeadVertexId = kvp.Value.HeadVertexId,
                    TailVertexId = kvp.Value.TailVertexId,
                };
                de.HeadPosition = GetDrawableNodeByNodeId(de.HeadVertexId).Position;
                de.TailPosition = GetDrawableNodeByNodeId(de.TailVertexId).Position;
                // store the drawable edge
                this.drawableEdges[de.EdgeId] = de;
            }

            // invalidate the curent  canvas, triggering re-draw
            MainDrawingCanvas.Invalidate();
        }

        void LayoutDGraphGrid()
        {
            // clear the lists of drawable nodes and edges
            // iterate over the current graph's nodes
            // generate a drawable node 
            // store the drawable node
            // interate over the current graph's edges
            // generate a drawable edge
            // store the drawable edge
            // invalidate the curent  canvas, triggering re-draw
        }

        void DrawGraph(CanvasDrawingSession cds,
                       Dictionary<Guid, DrawableVertex> drawableNodes)
        {
            foreach (KeyValuePair<Guid, DrawableVertex> kvp in drawableNodes) 
            {
                DrawNode(cds, kvp.Value);
            }

            foreach (KeyValuePair<Guid, DrawableEdge> kvp in drawableEdges)
            {
                DrawEdge(cds, kvp.Value);
            }
        }

        

        private void CanvasControl_Draw(CanvasControl sender, 
                                        CanvasDrawEventArgs args)
        {
            Debug.WriteLine("CanvasControl_Draw()");
            //args.DrawingSession.DrawEllipse(155,115,80,30,Colors.Black, 3);
            //args.DrawingSession.DrawText("Hello, World!", 100, 100, Colors.Yellow);
            //args.DrawingSession.DrawCircle(new Vector2(700, 800), 40, Colors.Black, 3);
            //args.DrawingSession.DrawCircle(new Vector2(500, 300), 40, Colors.Black, 3);
            //args.DrawingSession.DrawLine(new Vector2(700,800), new Vector2(500,300), Colors.Black, 3);
            //args.DrawingSession.DrawCircle(new Vector2(200, 900), 40, Colors.Black, 3);
            //args.DrawingSession.DrawLine(new Vector2(200,900), new Vector2(500,300), Colors.Black, 3);
            //args.DrawingSession.DrawLine(new Vector2(200,900), new Vector2(700,800), Colors.Black, 3);
            // draw the graph data structure
            DrawGraph(args.DrawingSession, drawableVertices);
        }

        private void CanvasControl_OnCreateResources(
            CanvasControl sender, 
            CanvasCreateResourcesEventArgs args)
        {
            Debug.WriteLine("CanvasControl_OnCreateResources()");
        }

        private void GenerateRandomGraphBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("Generate button clicked");
            GenerateRandomGraph();
            this.MainDrawingCanvas.Invalidate();
        }

        private void FitGraphToViewChkBx_Checked(Object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Fit Graph to View Checkbox Checked");
            this.fitGraphToView = true;
            // TODO: re-layout graph overlapping in physical view space
        }

        private void FitGraphToViewChkBx_Unchecked(Object sender, RoutedEventArgs e)
        {
            this.fitGraphToView = false;
            // TODO: re-layout graph non-overlapping in virtual view space
            Debug.WriteLine("Fit Graph to Vew Checkbox Un-Checked");
        }

        private void LayoutGraphView()
        {
            throw new NotImplementedException();
            // load graph from current/selected UnitGraph
            //
        }
        
        private void RandomLayoutBtn_OnClick(Object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Random Layout button clicked");
            LayoutDGraphRandom();
        }
    }
}
