using System;
using System.Collections.Generic;
using System.Linq;
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
private Point startPoint;
private Rectangle rectSelectArea;

namespace WpfApp1._1
{
    private void imgCamera_MouseDown(object sender, MouseButtonEventArgs e)
    {
        startPoint = e.GetPosition(cnvImage);

        // Remove the drawn rectanglke if any.
        // At a time only one rectangle should be there
        if (rectSelectArea != null)
            cnvImage.Children.Remove(rectSelectArea);

        // Initialize the rectangle.
        // Set border color and width
        rectSelectArea = new Rectangle
        {
            Stroke = Brushes.LightBlue,
            StrokeThickness = 2
        };

        Canvas.SetLeft(rectSelectArea, startPoint.X);
        Canvas.SetTop(rectSelectArea, startPoint.X);
        cnvImage.Children.Add(rectSelectArea);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void imgCamera_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Released || rectSelectArea == null)
            return;

        var pos = e.GetPosition(cnvImage);

        // Set the position of rectangle
        var x = Math.Min(pos.X, startPoint.X);
        var y = Math.Min(pos.Y, startPoint.Y);

        // Set the dimenssion of the rectangle
        var w = Math.Max(pos.X, startPoint.X) - x;
        var h = Math.Max(pos.Y, startPoint.Y) - y;

        rectSelectArea.Width = w;
        rectSelectArea.Height = h;

        Canvas.SetLeft(rectSelectArea, x);
        Canvas.SetTop(rectSelectArea, y);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void imgCamera_MouseUp(object sender, MouseButtonEventArgs e)
    {
        rectSelectArea = null;
    }
}
