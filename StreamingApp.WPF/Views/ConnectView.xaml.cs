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
    /// <summary>
    /// Interaction logic for ConnectView.xaml
    /// </summary>
    public partial class ConnectView : UserControl
    {
        public ConnectView()
        {
            InitializeComponent();
        }

        private void PlayVideo(string videoFilePath)
        {
            try
            {
                bg.Source = new Uri(videoFilePath);
                bg.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string videoFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\v4.mp4");
            PlayVideo(videoFilePath);
        }

        //private void bg_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    bg.Position = TimeSpan.Zero;
        //    bg.Play();
        //}
    }
}
