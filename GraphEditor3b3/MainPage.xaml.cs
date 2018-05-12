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
        DrawableGraph drawableGraph;
        Random rng;
        Int32 minVertices;
        Int32 maxVertices;
        Double edgeProbability;
        ObservableCollection<Vertex> observableVertices;
        ObservableCollection<Edge> observableEdges;

        public MainPage()
        {
            this.InitializeComponent();
            this.rng = new Random();
            SetCurrentGraph(null);
            this.drawableGraph = null;
            this.minVertices = Defines.MIN_NUM_VERTS;
            this.maxVertices = Defines.MAX_NUM_VERTS;
            this.edgeProbability = Defines.EDGE_PROBABILITY;
            MinVertSlider.Minimum = Defines.MIN_NUM_VERTS;
            MinVertSlider.Maximum = Defines.MAX_NUM_VERTS;
            MaxVertSlider.Minimum = Defines.MIN_NUM_VERTS;
            MaxVertSlider.Maximum = Defines.MAX_NUM_VERTS;
            MinVertSlider.Value = this.minVertices;
            MaxVertSlider.Value = this.maxVertices;
            EdgeProbSlider.Value = this.edgeProbability;
            this.observableVertices = new ObservableCollection<Vertex>();
            this.observableEdges = new ObservableCollection<Edge>();
        }

        private void SetCurrentGraph(Graph inGraph)
        {
            if (inGraph != null)
            {
                this.observableEdges.Clear();
                this.observableVertices.Clear();
                this.currentGraph = inGraph;
                foreach(KeyValuePair<Guid, Vertex> kvp in inGraph.Vertices) {
                    this.observableVertices.Add(kvp.Value);
                }
                foreach(KeyValuePair<Guid, Edge> kvp in inGraph.Edges)
                {
                    this.observableEdges.Add(kvp.Value);
                }
                this.currentGraph.EdgesChanged += EdgesChanged;
                this.currentGraph.VerticesChanged += VerticesChanged;

                this.VertexCountTxtBlock.Text = String.Format("# Of Vertices: {0}", this.currentGraph.Vertices.Count);
                this.EdgeCountTxtBlock.Text = String.Format("# of Edges: {0}", this.currentGraph.Edges.Count);
            } else
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
            var ds = args.DrawingSession;
            if (this.drawableGraph != null)
            {
                this.drawableGraph.Draw(args.DrawingSession);
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

                //args.DrawingSession.DrawText("Nothing To Display", new System.Numerics.Vector2(x, y), Colors.White);

            }
        }

        private void MyCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            // Generate a random graph
            //this.currentGraph = Graph.GenerateRandomGraph(this.rng);
            //this.drawableGraph = DrawableGraph.LayoutDGraphRandom(true, this.rng, this.MainCanvas, this.currentGraph);
        }

        private void GenerateBtn_Click(Object sender, RoutedEventArgs e)
        {
            this.StatusTextBlock.Text = "Generating new graph with random layout";
            SetCurrentGraph(Graph.GenerateRandomGraph(this.rng, this.minVertices, this.maxVertices, this.edgeProbability));
            this.drawableGraph = DrawableGraph.LayoutDGraphRandom(true, this.rng, this.MainCanvas, this.currentGraph);
            this.drawableGraph.Layout = DrawableGraphLayout.Random;
            this.drawableGraph.FitToView = true;
            this.MainCanvas.Invalidate();

        }

        /// <summary>
        /// Click handler for save graph button. Saves current graph to file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveGraphBtn_Click(Object sender, RoutedEventArgs e)
        {
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
                    this.StatusTextBlock.Text = String.Format("Graph saved to file {0}", file.Path);
                }
                else
                {
                    this.StatusTextBlock.Text = String.Format("Graph not saved");
                }
            }
            else
            {
                this.StatusTextBlock.Text = "Operation canceled.";
            }
        }

        private async void LoadGraphFromFile(Windows.Storage.StorageFile file)
        {
            Windows.Storage.CachedFileManager.DeferUpdates(file);
            String jsonString = await Windows.Storage.FileIO.ReadTextAsync(file);
            Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
            {
                this.StatusTextBlock.Text = string.Format("file {0} read", file.Name);

                Graph loadedGraph = Graph.JsonToGraph(jsonString);

                SetCurrentGraph(loadedGraph);
                // TODO: fire event to update graph;
                this.drawableGraph = DrawableGraph.LayoutDGraphRandom(true, this.rng, this.MainCanvas, this.currentGraph);
                this.MainCanvas.Invalidate();
                //DrawingUtils.LayoutDGraphRandom(this.appCtx, this.MainDrawingCanvas, this.appCtx.DrawableEdges, this.appCtx.DrawableVertices);
            }
            else
            {
                this.StatusTextBlock.Text =
                    string.Format("file {0} not read", file.Name);
            }
        }

        private async void LoadGraphBtn_Click(Object sender, RoutedEventArgs e)
        {
            var loadPicker = new Windows.Storage.Pickers.FileOpenPicker();
            loadPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            loadPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            loadPicker.FileTypeFilter.Add(".json");

            Windows.Storage.StorageFile file = await loadPicker.PickSingleFileAsync();
            if (file != null)
            {
                this.StatusTextBlock.Text = string.Format("selected file: {0}", file.Name);
                LoadGraphFromFile(file);
            }
            else
            {
                this.StatusTextBlock.Text = string.Format("operation cancelled.");
            }
        }

        private void LayoutGraphBtn_Click(Object sender, RoutedEventArgs e)
        {
            if (this.currentGraph != null)
            {
                this.drawableGraph = DrawableGraph.LayoutDGraphRandom(true, this.rng, this.MainCanvas, this.currentGraph);
                MainCanvas.Invalidate();
            }
        }

        private void FitToViewCheckBox_Checked(Object sender, RoutedEventArgs e)
        {
            if (this.currentGraph != null)
            {
                this.drawableGraph = DrawableGraph.LayoutDGraphRandom(true, this.rng, this.MainCanvas, this.currentGraph);
                MainCanvas.Invalidate();
            }
            
        }

        private void FitToViewCheckBox_Unchecked(Object sender, RoutedEventArgs e)
        {
            if (this.currentGraph != null)
            {
                this.drawableGraph = DrawableGraph.LayoutDGraphRandom(false, this.rng, this.MainCanvas, this.currentGraph);
                MainCanvas.Invalidate();
            }
        }

        private void ClearGraphBtn_Click(Object sender, RoutedEventArgs e)
        {
            // TODO: add confirmation dialog box, with prompt to remember choice as default
            //this.currentGraph = null;
            SetCurrentGraph(null);
            this.drawableGraph = null;
            MainCanvas.Invalidate();
        }

        private void MinVertSLider_ValueChanged(Object sender, RangeBaseValueChangedEventArgs e)
        {
            this.minVertices = (Int32)e.NewValue;
            if (this.minVertices > this.maxVertices)
            {
                this.maxVertices += 1;
                if (MaxVertSlider != null)
                {
                    MaxVertSlider.Value = this.maxVertices;
                }
                
            }
        }

        private void MaxVertSlider_ValueChanged(Object sender, RangeBaseValueChangedEventArgs e)
        {
            this.maxVertices = (Int32)e.NewValue;
            if (this.maxVertices < this.minVertices)
            {
                this.minVertices -= 1;
                if (MinVertSlider != null)
                {
                    MinVertSlider.Value = this.minVertices;
                }
            }
        }

        private void EdgeProbSlider_ValueChanged(Object sender, RangeBaseValueChangedEventArgs e)
        {
            this.edgeProbability = e.NewValue;
        }

        private void CanvasPointerMoved(
            Object sender,
            PointerRoutedEventArgs e)
        {
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

        private void CanvasPointerPressed(
            Object sender,
            PointerRoutedEventArgs e)
        {
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
                foreach (KeyValuePair<Guid, DrawableVertex> kvp in this.drawableGraph.DrawableVertices)
                {
                    Rect bounds = kvp.Value.Circle.ComputeBounds();
                    if (bounds.Contains(pointerPosition) == true)
                    {
                        Debug.WriteLine("vertex clicked!");
                        this.StatusTextBlock.Text = "Vertex clicked";
                        kvp.Value.ToggleSelect();
                        shapeFound = true;
                        break;
                    }
                }

                if (shapeFound == false)
                {
                    foreach (KeyValuePair<Guid, DrawableEdge> kvp in this.drawableGraph.DrawableEdges)
                    {
                        Rect bounds = kvp.Value.Line.ComputeBounds();
                        if (bounds.Contains(pointerPosition) == true)
                        {
                            Debug.WriteLine("edge clicked!");
                            this.StatusTextBlock.Text = "Edge clicked";
                            kvp.Value.ToggleSelect();
                            shapeFound = true;
                            break;
                        }
                    }
                }

                if (shapeFound == true)
                {
                    MainCanvas.Invalidate();
                }
            }
        }

        public void VerticesChanged(Object sender, VertexChangedEventArgs e)
        {
           if (((Graph)sender).LastVertexChange == ChangeType.Added)
            {
                this.observableVertices.Add(e.ChangedVertex);
            }
           else if (((Graph)sender).LastVertexChange == ChangeType.Modified)
            {
                foreach(Vertex ov in observableVertices)
                {
                    if (ov.VertexId == e.ChangedVertex.VertexId)
                    {
                        ov.Value = e.ChangedVertex.Value;
                    }
                }
            }
           else if (((Graph)sender).LastVertexChange == ChangeType.Removed)
            {
                observableVertices.Remove(e.ChangedVertex);
            }
        }

        public void EdgesChanged(Object sender, EdgeChangedEventArgs e)
        {
            if (((Graph)sender).LastEdgeChange == ChangeType.Added)
            {
                this.observableEdges.Add(e.ChangedEdge);
            }
            else if (((Graph)sender).LastEdgeChange == ChangeType.Modified)
            {
                foreach(Edge oe in observableEdges)
                {
                    if (oe.EdgeId == e.ChangedEdge.EdgeId)
                    {
                        oe.Value = e.ChangedEdge.Value;
                    }
                }
            }
            else if (((Graph)sender).LastEdgeChange == ChangeType.Removed)
            {
                observableEdges.Remove(e.ChangedEdge);
            }
        }
    } // end of class
} // end of namespace
