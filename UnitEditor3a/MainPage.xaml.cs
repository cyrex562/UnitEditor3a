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
    // adjacent: whether there is an edge from vertex x to vertex y
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


    public class UEdge {

    }

    public class UNode {

    }

    public class UGraph {
        private List<UNode> _nodes;
        private List<UEdge> _edges;
        private int[,] _adjacencyMatrix;
        private bool[,] _incidenceMatrix;
        private int _numNodes;

        public UGraph(int numNodes)
        {
            this._numNodes = numNodes;
            _incidenceMatrix = new bool[this._numNodes,this._numNodes];
            _adjacencyMatrix = new int[this._numNodes,this._numNodes];
            this._nodes = new List<UNode>();
            this._edges = new List<UEdge>();
        }

        public bool Adjacent(UNode x, UNode y)
        {
            return false;
        }

        public List<UNode> Neighbors(UNode x)
        {
            List<UNode> outList = new List<UNode>();
            return outList;
        }

        public bool AddVertex(UNode newNode)
        {
            return false;
        }

        public bool RemoveVertex(UNode nodeToRemove)
        {
            return false;
        }

        public bool AddEdge(UNode x, UNode y)
        {
            return false;
        }

        public bool RemoveEdge(UNode x, UNode y)
        {
            return false;
        }

        public int GetNodeValue(UNode x)
        {
            return -1;
        }

        public bool SetNodeValue(UNode x, int newValue)
        {
            return false;
        }

        public int GetEdgeValue(UNode x, UNode y)
        {
            return -1;
        }

        public bool SetEdgeValue(UNode x, UNode y)
        {
            return false;
        }
    }

    public class DrawableEdge {
        public Vector2 StartPos { get; set; }
        public Vector2 EndPos { get; set; }
        public int Value { get; set; }

        public DrawableEdge()
        {
            this.StartPos = new Vector2(0,0);
            this.EndPos = new Vector2(0, 0);
            this.Value = -1;
        }
    }


    // Represents the drawable version of a node.
    public class DrawableNode {
        public int X { get; set; }
        public int Y { get; set; }

        public int Val { get; set; }

        // TODO: add node ID
        public DrawableNode()
        {
            this.X = 0;
            this.Y = 0;
            this.Val = -1;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("X: {0}, Y: {1}, Val: {2}", this.X, this.Y, this.Val);
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        private const int NODE_SIZE = 40;
        private const int MIN_NUM_NODES = 3;
        private const int MAX_NUM_NODES = 9;
        private const int MIN_NODE_SPACE = 10;
        private const int MAX_NODE_SPACE = 20;
        private const int MIN_X = 0;
        private const int
            MAX_X = NODE_SIZE * (MAX_NUM_NODES + MAX_NODE_SPACE);
        private const int MIN_Y = 0;
        private const int MAX_Y = NODE_SIZE * (MAX_NUM_NODES + MAX_NODE_SPACE);
        private Color DEFAULT_NODE_COLOR = Colors.Black;
        private int NODE_LINE_WIDTH = 3;

        private List<DrawableNode> drawableNodes;


        public MainPage()
        {
            Debug.WriteLine("MainPage()");
            this.InitializeComponent();

            // create a graph data structure
            Random rng = new Random();
            int nodeCount = rng.Next(1, 5);
            drawableNodes = new List<DrawableNode>();

            for (int i = 0; i < nodeCount; i++) {
                int x = rng.Next(MIN_X, MAX_X);
                int y = rng.Next(MIN_Y, MAX_Y);
                int val = rng.Next();
                DrawableNode dn = new DrawableNode
                {
                    X = x,
                    Y = y,
                    Val = val
                };
                drawableNodes.Add(dn);
                Debug.WriteLine(dn, string.Format("dn {0}", i));
            }
        }

        void DrawNode(CanvasDrawingSession cds, DrawableNode dn)
        {
            cds.DrawCircle(new Vector2(dn.X, dn.Y), NODE_SIZE, DEFAULT_NODE_COLOR, NODE_LINE_WIDTH);
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
        }
    }
}
