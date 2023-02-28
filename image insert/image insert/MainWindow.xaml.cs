using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp2
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        // this will upload the image in the xaml 
        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                imagePicture.Source = new BitmapImage(new Uri(openDialog.FileName));
            }
        }
        MatrixTransform transform;
        Point StartPoint;
        Point CurrentPoint;
        double[] Dimensions = new double[2];
        Rect rect = new Rect();
        bool IsResizeMode;
        bool IsDragAndDropMode;

        private bool button1WasClicked = false;

        private void rectangle(object sender, EventArgs e){
            try{  // makes sure that it will not print the rectangle unless there is picture loaded in system.
                if (imagePicture!= null){
                    button1WasClicked = true;
                    Rectangle blueRectangle = new Rectangle();
                    blueRectangle.Height = 100;
                    blueRectangle.Width = 200;
                    // Create a blue and a black Brush  
                    SolidColorBrush blueBrush = new SolidColorBrush();
                    blueBrush.Color = Colors.Blue;
                    SolidColorBrush blackBrush = new SolidColorBrush();
                    blackBrush.Color = Colors.Black;
                    // Set Rectangle's width and color  
                    blueRectangle.StrokeThickness = 4;
                    blueRectangle.Stroke = blackBrush;
                    // Fill rectangle with blue color  
                    blueRectangle.Fill = blueBrush;
                    // Add Rectangle to the Grid.  
                    LayoutRoot.Children.Add(blueRectangle);
                }
            } 
            catch (Exception invalid){
                MessageBox.Show(invalid.Message);
            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try{        // makes sure picture is loaded in system.
                if (imagePicture.Source != null){
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
                    if (saveDialog.ShowDialog() == true){
                        // create BitmapEncoder and set it.
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imagePicture.Source));

                        // save the image using users selected path
                        using (FileStream stream = new FileStream(saveDialog.FileName, FileMode.Create)){
                            encoder.Save(stream);
                        }
                    }
                }
            }
            catch (Exception invalid){
                MessageBox.Show(invalid.Message);
            }
        }

        bool captured = false;
        double x_shape, x_canvas, y_shape, y_canvas;

        UIElement source = null;
        private void add_remove(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (imagePicture.Source != null)
                {

                    if (e.OriginalSource is Rectangle)
                    {
                        Rectangle activeRec = (Rectangle)e.OriginalSource;
                        canvas.Children.Remove(activeRec);
                    }

                    else
                    {
                        Rectangle newRec = new Rectangle
                        {
                            Width = 100,
                            Height = 100,
                            StrokeThickness = 3,
                            Fill = Brushes.LightCoral,
                            Stroke = Brushes.LightCoral
                        };

                        Canvas.SetLeft(newRec, Mouse.GetPosition(canvas).X);
                        Canvas.SetTop(newRec, Mouse.GetPosition(canvas).Y);

                        canvas.Children.Add(newRec);
                    }
                }
            }
            catch (Exception invalid)
            {
                MessageBox.Show(invalid.Message);
            }
        
        
        }
    }
}