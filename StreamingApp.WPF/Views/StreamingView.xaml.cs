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

namespace StreamingApp.WPF.Views
{
    public partial class StreamingView : UserControl
    {
        private Polyline linia;
        private Brush color;
        private int lineWidth;
        public StreamingView()
        {
            InitializeComponent();
            color = Brushes.White;
            lineWidth = 2;
        }

        private void PlayStream(string videoFilePath)
        {
            try
            {
                mediaElement.Source = new Uri(videoFilePath);
                mediaElement.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void polotno_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            linia = new Polyline() { Stroke = color, StrokeThickness = lineWidth };
            polotno.Children.Add(linia);
        }

        private void polotno_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && linia != null)
                linia.Points.Add(e.GetPosition(polotno));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string videoFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\v2.mp4");
            PlayStream(videoFilePath);
        }
    }
}
