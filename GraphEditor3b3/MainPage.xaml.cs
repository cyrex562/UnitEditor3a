using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

namespace GraphEditor3b3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Graph currentGraph;
        Random rng;
        Int32 minVertices;
        Int32 maxVertices;
        Double edgeProbability;
        ObservableCollection<GraphVertex> observableVertices;
        ObservableCollection<GraphEdge> observableEdges;

        public MainPage()
        {
            this.InitializeComponent();
            this.rng = new Random();
            this.currentGraph = new Graph();
            this.minVertices = Defines.MIN_NUM_VERTS;
            this.maxVertices = Defines.MAX_NUM_VERTS;
            this.edgeProbability = Defines.EDGE_PROBABILITY;
            this.MinVertSlider.Minimum = Defines.MIN_NUM_VERTS;
            this.MinVertSlider.Maximum = Defines.MAX_NUM_VERTS;
            this.MaxVertSlider.Minimum = Defines.MIN_NUM_VERTS;
            this.MaxVertSlider.Maximum = Defines.MAX_NUM_VERTS;
            this.MinVertSlider.Value = this.minVertices;
            this.MaxVertSlider.Value = this.maxVertices;
            this.EdgeProbSlider.Value = this.edgeProbability;
            this.observableVertices = new ObservableCollection<GraphVertex>();
            this.observableEdges = new ObservableCollection<GraphEdge>();
        }

        //private void SetCurrentGraph(Graph inGraph)
        //{
        //    Debug.WriteLine("setting current graph");
        //    if (inGraph != null)
        //    {
        //        Debug.WriteLine("clearing observable edges and vertices");
        //        this.observableEdges.Clear();
        //        this.observableVertices.Clear();
        //        this.currentGraph = inGraph;

        //        Debug.WriteLine("adding new graph observable edges and vertices");
        //        foreach(KeyValuePair<Guid, GraphVertex> kvp in inGraph.Vertices) {
        //            this.observableVertices.Add(kvp.Value);
        //        }

        //        foreach(KeyValuePair<Guid, GraphEdge> kvp in inGraph.Edges)
        //        {
        //            this.observableEdges.Add(kvp.Value);
        //        }

        //        Debug.WriteLine("setting new graph edges and vertices changed listeners");
        //        this.currentGraph.EdgesChanged += this.EdgesChanged;
        //        this.currentGraph.VerticesChanged += this.VerticesChanged;

        //        Debug.WriteLine("setting node and vertex counts");
        //        this.VertexCountTxtBlock.Text = String.Format("# Of Vertices: {0}", this.currentGraph.Vertices.Count);
        //        this.EdgeCountTxtBlock.Text = String.Format("# of Edges: {0}", this.currentGraph.Edges.Count);
        //    } else
        //    {
        //        this.VertexCountTxtBlock.Text = "# Of Vertices: (No Graph)";
        //        this.EdgeCountTxtBlock.Text = "# of Edges: (No Graph)";
        //    }
        //}

        private void InitGraphSupport()
        {
            Debug.WriteLine("setting current graph");
            if (this.currentGraph != null)
            {
                Debug.WriteLine("clearing observable edges and vertices");
                this.observableEdges.Clear();
                this.observableVertices.Clear();
                //this.currentGraph = inGraph;

                Debug.WriteLine("adding new graph observable edges and vertices");
                foreach (KeyValuePair<Guid, GraphVertex> kvp in this.currentGraph.Vertices)
                {
                    this.observableVertices.Add(kvp.Value);
                }

                foreach (KeyValuePair<Guid, GraphEdge> kvp in this.currentGraph.Edges)
                {
                    this.observableEdges.Add(kvp.Value);
                }

                Debug.WriteLine("setting new graph edges and vertices changed listeners");
                this.currentGraph.EdgesChanged += this.EdgesChanged;
                this.currentGraph.VerticesChanged += this.VerticesChanged;

                Debug.WriteLine("setting node and vertex counts");
                this.VertexCountTxtBlock.Text = String.Format("# Of Vertices: {0}", this.currentGraph.Vertices.Count);
                this.EdgeCountTxtBlock.Text = String.Format("# of Edges: {0}", this.currentGraph.Edges.Count);
            }
            else
            {
                this.VertexCountTxtBlock.Text = "# Of Vertices: (No Graph)";
                this.EdgeCountTxtBlock.Text = "# of Edges: (No Graph)";
            }
        }

        /// <summary>
        /// Draw the stuff on the canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            Debug.WriteLine("canvas draw call");
            var ds = args.DrawingSession;
            if (this.currentGraph != null)
            {
                this.currentGraph.Draw(args.DrawingSession);
            } else
            {
                Single x = (Single)(this.MainCanvas.ActualWidth / 2);
                Single y = (Single)(this.MainCanvas.ActualHeight / 2);

                CanvasTextFormat format = new CanvasTextFormat
                {
                    FontSize = 30.0f,
                    WordWrapping = CanvasWordWrapping.NoWrap
                };

                CanvasTextLayout textLayout = new CanvasTextLayout(ds, "Nothing to see here.", format, 0.0f, 0.0f);
                var bounds = textLayout.LayoutBounds;
                Single newX = x - (Single)(bounds.Width / 2);
                Single newY = y - (Single)(bounds.Height / 2);

                ds.DrawTextLayout(textLayout, newX, newY, Colors.White);

                //args.DrawingSession.DrawText("Nothing To Display", new 
            }
        }

        private void MyCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            Debug.WriteLine("canvas create resources");
            // Generate a random graph
            //this.currentGraph = Graph.GenerateRandomGraph(this.rng);
            //this.drawableGraph = DrawableGraph.LayoutDGraphRandom(true, this.rng, this.MainCanvas, this.currentGraph);
        }

        // TODO: look at making async
        private void GenerateBtn_Click(Object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("generate new random graph button clicked");
            this.currentGraph.RegenerateRandomGraph(this.minVertices, this.maxVertices, this.edgeProbability);
            InitGraphSupport();
            //SetCurrentGraph();
            this.currentGraph.RelayoutGraphRandom(this.MainCanvas);
            this.MainCanvas.Invalidate();
            this.StatusTextBlock.Text = "Random graph generated";
        }

        /// <summary>
        /// Click handler for save graph button. Saves current graph to file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveGraphBtn_Click(Object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("save graph button clicked");
            String jsonString = Graph.GraphToJson(this.currentGraph);
            var savePicker = new Windows.Storage.Pickers.FileSavePicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
            };
            savePicker.FileTypeChoices.Add("JSON", new List<String>() { ".json" });
            savePicker.SuggestedFileName = "New Graph";
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteTextAsync(file, jsonString);
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    String message = String.Format("Graph saved to file {0}", file.Path);
                    Debug.WriteLine(message);
                    this.StatusTextBlock.Text = message; 
                }
                else
                {
                    String message = String.Format("Graph not saved");
                    this.StatusTextBlock.Text = message;
                    Debug.WriteLine(message);
                }
            }
            else
            {
                Debug.WriteLine("save operation canceled");
                this.StatusTextBlock.Text = "save operation canceled.";
            }
        }

        private async void LoadGraphFromFile(Windows.Storage.StorageFile file)
        {
            // TODO: fix format to include only a list of vertices and edges
            Debug.WriteLine("loading graph from file");
            Windows.Storage.CachedFileManager.DeferUpdates(file);
            String jsonString = await Windows.Storage.FileIO.ReadTextAsync(file);
            //Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            //if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
            //{
            //    String message = String.Format("file {0} read", file.Path);
            //    this.StatusTextBlock.Text = message;
            //    Debug.WriteLine(message);

            //    Graph loadedGraph = Graph.JsonToGraph(jsonString);

            //    SetCurrentGraph(loadedGraph);
            //    // TODO: fire event to update graph;
            //    this.drawableGraph = DrawableGraph.LayoutDGraphRandom(true, this.rng, this.MainCanvas, this.currentGraph);
            //    this.MainCanvas.Invalidate();
            //}
            //else
            //{
            //    this.StatusTextBlock.Text =
            //        String.Format("file {0} not read", file.Name);
            //}
        }

        private async void LoadGraphBtn_Click(Object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Load Graph button clicked");
            var loadPicker = new Windows.Storage.Pickers.FileOpenPicker();
            loadPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            loadPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            loadPicker.FileTypeFilter.Add(".json");

            Windows.Storage.StorageFile file = await loadPicker.PickSingleFileAsync();
            if (file != null)
            {
                String message = String.Format("selected file: {0}", file.Name);
                this.StatusTextBlock.Text = message;
                Debug.WriteLine(message);
                LoadGraphFromFile(file);
            }
            else
            {
                String message = String.Format("operation cancelled.");
                this.StatusTextBlock.Text = message;
                Debug.WriteLine(message);
            }
        }

        private void LayoutGraphBtn_Click(Object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Layout graph button clicked");
            if (this.currentGraph != null)
            {
                //this.drawableGraph = DrawableGraph.LayoutDGraphRandom(true, this.rng, this.MainCanvas, this.currentGraph);
                this.currentGraph.RelayoutGraphRandom(this.MainCanvas);
                this.MainCanvas.Invalidate();
            }
        }

        private void FitToViewCheckBox_Checked(Object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Fit to view checkbox checked");
            if (this.currentGraph != null)
            {
                this.currentGraph.FitToView = true;
                this.currentGraph.RelayoutGraphRandom(this.MainCanvas);
                this.MainCanvas.Invalidate();
            }
            
        }

        private void FitToViewCheckBox_Unchecked(Object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Fit to view checkbox un-checked");
            if (this.currentGraph != null)
            {
                this.currentGraph.FitToView = false;
                this.currentGraph.RelayoutGraphRandom(this.MainCanvas);
                this.MainCanvas.Invalidate();
            }
        }

        private void ClearGraphBtn_Click(
            Object sender, 
            RoutedEventArgs e)
        {
            Debug.WriteLine("Clear graph button clicked");
            // TODO: add confirmation dialog box, with prompt to remember choice as default
            //this.currentGraph = null;
            //SetCurrentGraph(null);
            //this.drawableGraph = null;
            this.currentGraph.ClearGraph();
            this.MainCanvas.Invalidate();
        }

        private void MinVertSLider_ValueChanged(
            Object sender, 
            RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine("Min vert slider moved");
            this.minVertices = (Int32)e.NewValue;
            if (this.minVertices > this.maxVertices)
            {
                this.maxVertices += 1;
                if (this.MaxVertSlider != null)
                {
                    this.MaxVertSlider.Value = this.maxVertices;
                }
                
            }
        }

        private void MaxVertSlider_ValueChanged(
            Object sender, 
            RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine("Max vert slider moved");
            this.maxVertices = (Int32)e.NewValue;
            if (this.maxVertices < this.minVertices)
            {
                this.minVertices -= 1;
                if (this.MinVertSlider != null)
                {
                    this.MinVertSlider.Value = this.minVertices;
                }
            }
        }

        private void EdgeProbSlider_ValueChanged(Object sender, RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine("Edge prob slider moved");
            this.edgeProbability = e.NewValue;
        }

        private void CanvasPointerMoved(
            Object sender,
            PointerRoutedEventArgs e)
        {
            Debug.WriteLine("Canvas pointer moved");
            Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;
            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                // get extended pointer details
                //Windows.UI.Input.PointerPoint ptrPoint = e.GetCurrentPoint(MainCanvas);
                //Point ptrPos = ptrPoint.Position;
                //if (ptrPoint.Properties.IsLeftButtonPressed)
                //{
                //    Debug.WriteLine(String.Format("left mouse button: {0}", ptrPoint.PointerId));
                //    foreach (KeyValuePair<Guid, DrawableVertex> kvp in this.drawableGraph.DrawableVertices)
                //    {
                //        Rect bounds = kvp.Value.Circle.ComputeBounds();
                //    }
                //}
            }
        }

        private Int32 GetEdgeListIndex(Guid edgeId)
        {
            for (Int32 i = 0; i < this.observableEdges.Count; i++)
            {
                if (this.observableEdges[(int)i].EdgeId == edgeId)
                {
                    return i;
                }
            }
            return -1;
        }

        private Int32 GetVertexListIndex(Guid vertexId)
        {
            Debug.WriteLine("Getting vertex list index");
            for (Int32 i = 0; i < this.observableVertices.Count; i++)
            {
                if (this.observableVertices[(int)i].VertexId == vertexId)
                {
                    return i;
                }
            }

            return -1;
        }

        private void ToggleSelectEdge(GraphEdge de)
        {
            Debug.WriteLine("Toggle select edge");
            Int32 listIndex = GetEdgeListIndex(de.EdgeId);
            if (de.Selected)
            {
                this.EdgeList.SelectRange(new ItemIndexRange(listIndex, 1));
                de.DeSelect();
            } else
            {
                this.EdgeList.DeselectRange(new ItemIndexRange(listIndex, 1));
                de.Select();
            }
        }

        private void ToggleSelectVertex(GraphVertex dv)
        {
            Debug.WriteLine("Toggle select vertex");
            Int32 listIndex = GetVertexListIndex(dv.VertexId);
            if (dv.Selected)
            {
                this.VertexList.SelectRange(new ItemIndexRange(listIndex, 1));
                dv.Deselect();
            }
            else
            {
                this.VertexList.SelectRange(new ItemIndexRange(listIndex, 1));
                dv.Select();
            }
        }

        private void CanvasPointerPressed(
            Object sender,
            PointerRoutedEventArgs e)
        {
            Debug.WriteLine("Mouse clicked on canvas");
            Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;
            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                Windows.UI.Input.PointerPoint pointerPoint = e.GetCurrentPoint(this.MainCanvas);
                Point pointerPosition = pointerPoint.Position;
                String mouseButton = "";

                if (pointerPoint.Properties.IsLeftButtonPressed)
                {
                    mouseButton = "Left";
                }
                else if (pointerPoint.Properties.IsRightButtonPressed)
                {
                    mouseButton = "Right";
                } 
                else if (pointerPoint.Properties.IsMiddleButtonPressed)
                {
                    mouseButton = "Center";
                }

                Boolean shapeFound = false;
                foreach (KeyValuePair<Guid, GraphVertex> kvp in this.currentGraph.Vertices)
                {
                    Rect bounds = kvp.Value.Circle.ComputeBounds();
                    if (bounds.Contains(pointerPosition) == true)
                    {
                        Debug.WriteLine("vertex clicked!");
                        this.StatusTextBlock.Text = "Vertex clicked";
                        ToggleSelectVertex(kvp.Value);
                        shapeFound = true;
                        break;
                    }
                }

                if (shapeFound == false)
                {
                    foreach (KeyValuePair<Guid, GraphEdge> kvp in this.currentGraph.Edges)
                    {
                        Rect bounds = kvp.Value.Line.ComputeBounds();
                        if (bounds.Contains(pointerPosition) == true)
                        {
                            Debug.WriteLine("edge clicked!");
                            this.StatusTextBlock.Text = "Edge clicked";
                            ToggleSelectEdge(kvp.Value);
                            break;
                        }
                    }
                }

                if (shapeFound == true)
                {
                    //this.MainCanvas.Invalidate();
                }
            }
        }

        public void VerticesChanged(Object sender, VertexChangedEventArgs e)
        {
            Debug.WriteLine("Vertices changed");
           if (((Graph)sender).LastVertexChange == ChangeType.Added)
            {
                this.observableVertices.Add(e.ChangedVertex);
            }
           else if (((Graph)sender).LastVertexChange == ChangeType.Modified)
            {
                foreach(GraphVertex ov in this.observableVertices)
                {
                    if (ov.VertexId == e.ChangedVertex.VertexId)
                    {
                        ov.Value = e.ChangedVertex.Value;
                    }
                }
            }
           else if (((Graph)sender).LastVertexChange == ChangeType.Removed)
            {
                this.observableVertices.Remove(e.ChangedVertex);
            }
        }

        public void EdgesChanged(Object sender, EdgeChangedEventArgs e)
        {
            Debug.WriteLine("Edges Changed");
            if (((Graph)sender).LastEdgeChange == ChangeType.Added)
            {
                this.observableEdges.Add(e.ChangedEdge);
            }
            else if (((Graph)sender).LastEdgeChange == ChangeType.Modified)
            {
                foreach(GraphEdge oe in this.observableEdges)
                {
                    if (oe.EdgeId == e.ChangedEdge.EdgeId)
                    {
                        oe.Value = e.ChangedEdge.Value;
                    }
                }
            }
            else if (((Graph)sender).LastEdgeChange == ChangeType.Removed)
            {
                this.observableEdges.Remove(e.ChangedEdge);
            }
        }

        private void EdgeList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Edge list selection changed");
            if (this.currentGraph != null)
            {
                foreach (GraphEdge addedEdge in e.AddedItems)
                {
                    this.currentGraph.Edges[addedEdge.EdgeId].Select();
                }

                foreach (GraphEdge removedEdge in e.RemovedItems)
                {
                    this.currentGraph.Edges[removedEdge.EdgeId].DeSelect();
                }
            }

            this.MainCanvas.Invalidate();
        }

        private void VertexList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Vertex list selection changed");
            foreach (GraphVertex addedVertex in e.AddedItems)
            {
                this.currentGraph.Vertices[addedVertex.VertexId].Select();
            }

            foreach (GraphVertex removedVertex in e.RemovedItems)
            {
                this.currentGraph.Vertices[removedVertex.VertexId].Deselect();
            }
            this.MainCanvas.Invalidate();
        }
    } // end of class
} // end of namespace
