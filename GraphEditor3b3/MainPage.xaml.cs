using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GraphEditor3b3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Graph graph;
        DrawableGraph drawableGraph;
        Random rng;
        public MainPage()
        {
            this.InitializeComponent();

            rng = new Random();

            
        }

        private void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            drawableGraph.Draw(args.DrawingSession);
        }

        private void MyCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            // Generate a random graph
            graph = Graph.GenerateRandomGraph(rng);
            drawableGraph = DrawableGraph.LayoutDGraphRandom(true, rng, MyCanvas, graph);
        }

        private void generateBtn_Click(Object sender, RoutedEventArgs e)
        {
            graph = Graph.GenerateRandomGraph(rng);
            drawableGraph = DrawableGraph.LayoutDGraphRandom(true, rng, MyCanvas, graph);
            MyCanvas.Invalidate();
        }
    }
}
