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
        

        private List<DrawableNode> drawableNodes;

        private List<DrawableEdge> drawableEdges;

        private Random rng;
        
        public MainPage()
        {
            Debug.WriteLine("MainPage()");
            this.InitializeComponent();
            this.drawableNodes = new List<DrawableNode>();
            this.drawableEdges = new List<DrawableEdge>();
            this.rng = new Random();
        }

        public void GenerateGraph()
        {
            drawableNodes.Clear();
            // create a graph data structure
            int nodeCount = rng.Next(Defines.MIN_NUM_NODES, Defines.MAX_NUM_NODES);
            
            for (int i = 0; i < nodeCount; i++)
            {
                int x = rng.Next(Defines.MIN_X, Defines.MAX_X);
                int y = rng.Next(Defines.MIN_Y, Defines.MAX_Y);
                int val = rng.Next();
                DrawableNode dn = new DrawableNode
                {
                    Position = new Vector2(x, y)
                };
                drawableNodes.Add(dn);
                Debug.WriteLine(dn, string.Format("dn {0}", i));
            }
        }

        void DrawNode(CanvasDrawingSession cds, DrawableNode dn)
        {
            cds.DrawCircle(dn.Position, Defines.NODE_SIZE, Defines.DEFAULT_NODE_COLOR, Defines.NODE_LINE_WIDTH);
        }

        void DrawGraph(CanvasDrawingSession cds,
                       List<DrawableNode> drawableNodes)
        {
            foreach (DrawableNode dn in drawableNodes) 
            {
                DrawNode(cds, dn);
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
            DrawGraph(args.DrawingSession, drawableNodes);
        }

        private void CanvasControl_OnCreateResources(
            CanvasControl sender, 
            CanvasCreateResourcesEventArgs args)
        {
            Debug.WriteLine("CanvasControl_OnCreateResources()");
        }

        private void GenerateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("Generate button clicked");
            GenerateGraph();
            this.MainDrawingCanvas.Invalidate();
        }
    }
}
