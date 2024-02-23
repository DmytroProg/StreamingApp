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
using System.Windows.Shapes;

namespace StreamingApp.WPF.Windows
{
    /// <summary>
    /// Interaction logic for DrawWindow.xaml
    /// </summary>
    public partial class DrawWindow : Window
    {
        private Rectangle rectangle;
        private bool isDrawing;
        public DrawWindow()
        {
            InitializeComponent();
            rectangle = new Rectangle();
            rectangle.Stroke = Brushes.Red;
            rectangle.StrokeThickness = 3;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            canvas.Opacity = .4;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
            popup.HorizontalOffset = (Width - popup.Width) / 2;
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            canvas.Children.Add(rectangle);
            var point = e.GetPosition(canvas);
            Canvas.SetLeft(rectangle, point.X);
            Canvas.SetTop(rectangle, point.Y);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            var point = e.GetPosition(canvas);
            rectangle.Width = Math.Abs(Canvas.GetLeft(rectangle) - point.X);
            rectangle.Height = Math.Abs(Canvas.GetTop(rectangle) - point.Y);
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            rectangle = new Rectangle();
            rectangle.Stroke = Brushes.Red;
            rectangle.StrokeThickness = 3;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            canvas.Opacity = 0;
        }
    }
}
