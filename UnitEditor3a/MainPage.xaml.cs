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
using Microsoft.Graphics.Canvas.Geometry;

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
            this.fitGraphToView = true;
        }

        public void GenerateRandomGraph()
        {
            int numNodes = rng.Next(Defines.MIN_NUM_NODES, Defines.MAX_NUM_NODES);
            this.currentGraph = new UGraph();
            for (int i = 0; i < numNodes; i++)
            {
                UVertex uv = new UVertex
                {
                    Value = rng.Next()
                };
                this.currentGraph.AddVertex(uv);
            }

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
                            kvp1.Value.AddNeighbor(kvp2.Value.VertexId);
                            kvp1.Value.AddEdge(ue.EdgeId);
                            kvp2.Value.AddNeighbor(kvp1.Value.VertexId);
                            kvp2.Value.AddEdge(ue.EdgeId);

                        }
                    }
                    
                }
            }

            LayoutDGraphRandom();
        }

        void DrawNode(CanvasDrawingSession cds, DrawableVertex dn)
        {
            //cds.DrawCircle(dn.Position, Defines.VERTEX_SIZE, Defines.DEFAULT_NODE_COLOR, Defines.NODE_LINE_WIDTH);
            cds.DrawGeometry(dn.Circle, Defines.DEFAULT_NODE_COLOR, Defines.NODE_LINE_WIDTH);
        }

        private void DrawEdge(CanvasDrawingSession cds, DrawableEdge de)
        {
            //cds.DrawLine(de.HeadPosition, de.TailPosition, Defines.DEF_EDGE_COLOR, Defines.DEF_EDGE_LINE_WIDTH);
            cds.DrawGeometry(de.Line, Defines.DEFAULT_NODE_COLOR, Defines.NODE_LINE_WIDTH);
        }

        DrawableVertex GetDrawableNodeByNodeId(Guid nodeId) => this.drawableVertices[nodeId];

        void LayoutDGraphRandom()
        {
            this.drawableEdges.Clear();
            this.drawableVertices.Clear();
            foreach (KeyValuePair<Guid, UVertex> kvp in this.currentGraph.Vertices)
            {
                Int32 min_x = Defines.VERTEX_SIZE;
                Int32 min_y = Defines.VERTEX_SIZE;
                Int32 max_x = 0;
                Int32 max_y = 0;
                if (this.fitGraphToView == true)
                {
                    max_x = (Int32)MainDrawingCanvas.ActualWidth - Defines.VERTEX_SIZE;
                    max_y = (Int32)MainDrawingCanvas.ActualHeight - Defines.VERTEX_SIZE;
                }
                else
                {
                    max_x = Defines.VERTEX_SIZE * (this.currentGraph.VertexCount + Defines.MAX_VERTEX_SPACE);
                    max_y = Defines.VERTEX_SIZE * (this.currentGraph.VertexCount + Defines.MAX_VERTEX_SPACE);
                }

                Vector2 circlePos = new Vector2(this.rng.Next(min_x, max_x), this.rng.Next(min_y, max_y));
                DrawableVertex dn = new DrawableVertex
                {
                    Position = circlePos,
                    VertexId = kvp.Value.VertexId,
                    Circle = CanvasGeometry.CreateCircle(MainDrawingCanvas, circlePos, Defines.VERTEX_SIZE)
                };
                this.drawableVertices[dn.VertexId] = dn;
            }

            foreach(KeyValuePair<Guid, UEdge> kvp in this.currentGraph.Edges)
            {
                CanvasPathBuilder pathBuilder = new CanvasPathBuilder(MainDrawingCanvas);
                
                DrawableEdge de = new DrawableEdge
                {
                    EdgeId = kvp.Value.EdgeId,
                    HeadVertexId = kvp.Value.HeadVertexId,
                    TailVertexId = kvp.Value.TailVertexId,
                };
                de.HeadPosition = GetDrawableNodeByNodeId(de.HeadVertexId).Position;
                de.TailPosition = GetDrawableNodeByNodeId(de.TailVertexId).Position;
                pathBuilder.BeginFigure(de.HeadPosition);
                pathBuilder.AddLine(de.TailPosition);
                pathBuilder.EndFigure(CanvasFigureLoop.Open);
                de.Line = CanvasGeometry.CreatePath(pathBuilder);
                this.drawableEdges[de.EdgeId] = de;
            }

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

        void DrawGraph(
            CanvasDrawingSession cds,
            Dictionary<Guid, 
            DrawableVertex> drawableNodes)
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

        private void CanvasControl_Draw(
            CanvasControl sender, 
            CanvasDrawEventArgs args)
        {
            Debug.WriteLine("CanvasControl_Draw()");
            DrawGraph(args.DrawingSession, drawableVertices);
        }

        private void CanvasControl_OnCreateResources(
            CanvasControl sender, 
            CanvasCreateResourcesEventArgs args)
        {
            Debug.WriteLine("CanvasControl_OnCreateResources()");
        }

        private void GenerateRandomGraphBtn_OnClick(
            object sender, 
            RoutedEventArgs e)
        {
            Debug.WriteLine("Generate button clicked");
            GenerateRandomGraph();
            this.MainDrawingCanvas.Invalidate();
        }

        private void FitGraphToViewChkBx_Checked(
            Object sender, 
            RoutedEventArgs e)
        {
            Debug.WriteLine("Fit Graph to View Checkbox Checked");
            this.fitGraphToView = true;
        }

        private void FitGraphToViewChkBx_Unchecked(
            Object sender, 
            RoutedEventArgs e)
        {
            this.fitGraphToView = false;
            Debug.WriteLine("Fit Graph to Vew Checkbox Un-Checked");
        }

        private void LayoutGraphView()
        {
            throw new NotImplementedException();
        }
        
        private void RandomLayoutBtn_OnClick(
            Object sender, 
            RoutedEventArgs e)
        {
            Debug.WriteLine("Random Layout button clicked");
            LayoutDGraphRandom();
        }

        private void MainCanvas_PointerMoved(object sender, PointerRoutedEventArgs e) {
            Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;
            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                // To get mouse state, we need extended pointer details.
                // We get the pointer info through the getCurrentPoint method
                // of the event argument. 
                Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(MainDrawingCanvas);
                Point ptrPos = ptrPt.Position;
                if (ptrPt.Properties.IsLeftButtonPressed)
                {
                    Debug.WriteLine(string.Format("left mouse btn: {0}", ptrPt.PointerId));
                    

                    //foreach (KeyValuePair<Guid, DrawableVertex> kvp in this.drawableVertices)
                    //{
                    //    Rect bounds = kvp.Value.
                    //}
                }
                if (ptrPt.Properties.IsMiddleButtonPressed)
                {
                    Debug.WriteLine(string.Format("middle mouse btn: {0}", ptrPt.PointerId));
                }
                if (ptrPt.Properties.IsRightButtonPressed)
                {
                    Debug.WriteLine(string.Format("right mouse btn: {0}", ptrPt.PointerId));
                }
            }

            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
        }

        private void MainCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;
            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                // To get mouse state, we need extended pointer details.
                // We get the pointer info through the getCurrentPoint method
                // of the event argument. 

                

                    Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(MainDrawingCanvas);
                Point ptrPos = ptrPt.Position;
                string mouseBtn = "";
                if (ptrPt.Properties.IsLeftButtonPressed)
                {
                    mouseBtn = "Left";

                    bool shapeFound = false;
                    foreach (KeyValuePair<Guid, DrawableVertex> kvp in this.drawableVertices)
                    {
                        Rect bounds = kvp.Value.Circle.ComputeBounds();
                        if (bounds.Contains(ptrPos) == true) {
                            Debug.WriteLine("vertex clicked!");
                            shapeFound = true;
                            break;
                        }
                    }

                    if (shapeFound == false)
                    {
                        foreach(KeyValuePair<Guid, DrawableEdge> kvp in this.drawableEdges)
                        {
                            Rect bounds = kvp.Value.Line.ComputeBounds();
                            if (bounds.Contains(ptrPos) == true)
                            {
                                Debug.WriteLine("edge clicked!");
                                break;
                            }
                        }
                    }
                }
                else if (ptrPt.Properties.IsRightButtonPressed)
                {
                    mouseBtn = "Right";
                }
                else if (ptrPt.Properties.IsMiddleButtonPressed)
                {
                    mouseBtn = "Middle";
                }
                Debug.WriteLine(string.Format("{2} mouse btn id {0} pressed at {1}", ptr.PointerId, ptrPos, mouseBtn));
            }
        }

        public void SaveGraphToFile_OnClick(Object sender, RoutedEventArgs e) {
            DataContractJsonSerializer serializer = DataContractJsonSerializer(typeof(UGraph));
            MemoryStream memStream = new MemoryStream();
            serializer.WriteObject(memStream, this.currentGraph);
            memStream.Position = 0;
            StreamReader sr = new StreamReader(memStream);
        }
    }

    
}
