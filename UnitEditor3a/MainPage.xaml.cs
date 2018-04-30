using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Diagnostics;

namespace UnitEditor3a
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        private AppContext appCtx;

        /// <summary>
        /// 
        /// </summary>
        public MainPage()
        {
            Debug.WriteLine("MainPage()");
            this.appCtx = new AppContext();
            Debug.WriteLine("Init Complete...");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <param name="appContext"></param>
        private void CanvasDraw(
            CanvasControl sender, 
            CanvasDrawEventArgs args)
        {
            Debug.WriteLine("CanvasDraw()");
            if (this.appCtx.CurrentGraph != null)
            {
                DrawingUtils.DrawGraph(args.DrawingSession, this.appCtx.DrawableVertices, this.appCtx.DrawableEdges);
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        //private void CanvasCreateResources(
        //    CanvasControl sender, 
        //    CanvasCreateResourcesEventArgs args)
        //{
        //    Debug.WriteLine("CanvasCreateResources");
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateRandomGraphBtn_OnClick(
            Object sender, 
            RoutedEventArgs e)
        {
            Debug.WriteLine("Generate button clicked");
            // generate the graph
            this.appCtx.CurrentGraph = GraphUtils.GenerateRandomGraph(this.appCtx);
            // update the appcontext graph state
            this.appCtx.CurrentGraphState = GraphState.New;
            // layout the graph
            //DrawingUtils.LayoutDGraphRandom(this.appCtx, this.MainDrawingCanvas, this.appCtx.DrawableEdges, this.appCtx.DrawableVertices);
            //this.MainDrawingCanvas.Invalidate();
            this.statusTextBlock.Text = "Random graph generated.";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FitGraphToViewChkBx_Checked(
            Object sender, 
            RoutedEventArgs e)
        {
            Debug.WriteLine("Fit Graph to View Checkbox Checked");
            this.appCtx.FitGraphToView = true;
            if (this.statusTextBlock != null) {
                this.statusTextBlock.Text = "Fit graph to view enabled.";
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FitGraphToViewChkBx_Unchecked(
            Object sender, 
            RoutedEventArgs e)
        {
            this.appCtx.FitGraphToView = false;
            Debug.WriteLine("Fit Graph to Vew Checkbox Un-Checked");
            this.statusTextBlock.Text = "Fit graph to view disabled.";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomLayoutBtn_OnClick(
            Object sender, 
            RoutedEventArgs e)
        { 
            Debug.WriteLine("Random Layout button clicked");

            //DrawingUtils.LayoutDGraphRandom(this.appCtx, this.MainDrawingCanvas, this.appCtx.DrawableEdges, this.appCtx.DrawableVertices);
            this.statusTextBlock.Text = "Random layout generated";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPointerMoved(
            Object sender, 
            PointerRoutedEventArgs e)
        {
            //Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;
            //if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            //{
            //    // To get mouse state, we need extended pointer details.
            //    // We get the pointer info through the getCurrentPoint method
            //    // of the event argument. 
            //    //Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(MainDrawingCanvas);
            //    Point ptrPos = ptrPt.Position;
            //    if (ptrPt.Properties.IsLeftButtonPressed)
            //    {
            //        Debug.WriteLine(string.Format("left mouse btn: {0}", ptrPt.PointerId));
                    

            //        //foreach (KeyValuePair<Guid, DrawableVertex> kvp in this.drawableVertices)
            //        //{
            //        //    Rect bounds = kvp.Value.
            //        //}
            //    }
            //    if (ptrPt.Properties.IsMiddleButtonPressed)
            //    {
            //        Debug.WriteLine(string.Format("middle mouse btn: {0}", ptrPt.PointerId));
            //    }
            //    if (ptrPt.Properties.IsRightButtonPressed)
            //    {
            //        Debug.WriteLine(string.Format("right mouse btn: {0}", ptrPt.PointerId));
            //    }
            //}

            //// Prevent most handlers along the event route from handling the same event again.
            //e.Handled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPointerPressed(
            Object sender, 
            PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;
            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                // To get mouse state, we need extended pointer details.
                // We get the pointer info through the getCurrentPoint method
                // of the event argument. 
                //Windows.UI.Input.PointerPoint ptrPt = 
                //    e.GetCurrentPoint(this.MainDrawingCanvas);
                //Point ptrPos = ptrPt.Position;
                //String mouseBtn = "";
                //if (ptrPt.Properties.IsLeftButtonPressed)
                //{
                //    mouseBtn = "Left";

                //    bool shapeFound = false;
                //    foreach (KeyValuePair<Guid, DrawableVertex> kvp in this.appCtx.DrawableVertices)
                //    {
                //        Rect bounds = kvp.Value.Circle.ComputeBounds();
                //        if (bounds.Contains(ptrPos) == true) {
                //            Debug.WriteLine("vertex clicked!");
                //            shapeFound = true;
                //            break;
                //        }
                //    }

                //    if (shapeFound == false)
                //    {
                //        foreach(KeyValuePair<Guid, DrawableEdge> kvp in this.appCtx.DrawableEdges)
                //        {
                //            Rect bounds = kvp.Value.Line.ComputeBounds();
                //            if (bounds.Contains(ptrPos) == true)
                //            {
                //                Debug.WriteLine("edge clicked!");
                //                break;
                //            }
                //        }
                //    }
                //}
                //else if (ptrPt.Properties.IsRightButtonPressed)
                //{
                //    mouseBtn = "Right";
                //}
                //else if (ptrPt.Properties.IsMiddleButtonPressed)
                //{
                //    mouseBtn = "Middle";
                //}
                //Debug.WriteLine(String.Format("{2} mouse btn id {0} pressed at {1}", ptr.PointerId, ptrPos, mouseBtn));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveGraphToFileAsync(Object sender, RoutedEventArgs e) {
            String jsonString = GraphUtils.GraphToJson(this.appCtx.CurrentGraph);

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
                    this.statusTextBlock.Text = "File" + file.Name + " saved.";
                } else
                {
                    this.statusTextBlock.Text = "File" + file.Name + " not saved.";
                }
            } else
            {
                this.statusTextBlock.Text = "Operation canceled.";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        private async void LoadGraphFromFile(Windows.Storage.StorageFile file)
        {
            Windows.Storage.CachedFileManager.DeferUpdates(file);
            string jsonString = await Windows.Storage.FileIO.ReadTextAsync(file);
            Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
            {
                this.statusTextBlock.Text = string.Format("file {0} read", file.Name);

                UGraph loadedGraph = GraphUtils.JsonToGraph(jsonString);
                //ms.Close();
                this.appCtx.CurrentGraph = loadedGraph;
                // TODO: fire event to update graph;
                //DrawingUtils.LayoutDGraphRandom(this.appCtx, this.MainDrawingCanvas, this.appCtx.DrawableEdges, this.appCtx.DrawableVertices);
            }
            else
            {
                this.statusTextBlock.Text = 
                    string.Format("file {0} not read", file.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoadGraphFromFileAsync(
            Object sender, 
            RoutedEventArgs e)
        {
            var loadPicker = new Windows.Storage.Pickers.FileOpenPicker();
            loadPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            loadPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            loadPicker.FileTypeFilter.Add(".json");

            Windows.Storage.StorageFile file = await loadPicker.PickSingleFileAsync();
            if (file != null)
            {
                this.statusTextBlock.Text = string.Format("selected file: {0}", file.Name);
                LoadGraphFromFile(file);
            } else
            {
                this.statusTextBlock.Text = string.Format("operation cancelled.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitBtn_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGraphBtn_Click(Object sender, RoutedEventArgs e)
        {
            // TODO: If graph has been modified, prompt to save
            this.appCtx.CurrentGraph = new UGraph();
            this.appCtx.DrawableEdges = new Dictionary<Guid, DrawableEdge>();
            this.appCtx.DrawableVertices = new Dictionary<Guid, DrawableVertex>();
            //this.MainDrawingCanvas.Invalidate();
            this.appCtx.CurrentGraphState = GraphState.New;
        }
    }
}
