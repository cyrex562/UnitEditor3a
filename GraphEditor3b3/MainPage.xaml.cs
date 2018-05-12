using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

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
        int minVertices;
        int maxVertices;
        double edgeProbability;

        public MainPage()
        {
            this.InitializeComponent();
            this.rng = new Random();
            SetCurrentGraph(null);
            this.drawableGraph = null;
            this.minVertices = Defines.MIN_NUM_NODES;
            this.maxVertices = Defines.MAX_NUM_NODES;
            this.edgeProbability = Defines.EDGE_PROBABILITY;
            MinVertSlider.Value = this.minVertices;
            MaxVertSlider.Value = this.maxVertices;
            EdgeProbSlider.Value = this.edgeProbability;
        }

        private void SetCurrentGraph(Graph inGraph)
        {
            this.currentGraph = inGraph;
            if (inGraph != null)
            {
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
                float x = (float)(this.MainCanvas.ActualWidth / 2);
                float y = (float)(this.MainCanvas.ActualHeight / 2);

                CanvasTextFormat format = new CanvasTextFormat
                {
                    FontSize = 30.0f,
                    WordWrapping = CanvasWordWrapping.NoWrap
                };

                CanvasTextLayout textLayout = new CanvasTextLayout(ds, "Nothing to see here.", format, 0.0f, 0.0f);
                var bounds = textLayout.LayoutBounds;
                float newX = x - (float)(bounds.Width / 2);
                float newY = y - (float)(bounds.Height / 2);

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
            string jsonString = await Windows.Storage.FileIO.ReadTextAsync(file);
            Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
            {
                this.StatusTextBlock.Text = string.Format("file {0} read", file.Name);

                Graph loadedGraph = Graph.JsonToGraph(jsonString);
                //ms.Close();
                //this.currentGraph = loadedGraph;
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
            this.minVertices = (int)e.NewValue;
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
            this.maxVertices = (int)e.NewValue;
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
    }
}
