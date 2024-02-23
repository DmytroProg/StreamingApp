using StreamingApp.WPF.Windows;
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
    /// Interaction logic for ScreenShareControl.xaml
    /// </summary>
    public partial class ScreenShareControl : UserControl
    {
        private DrawWindow _drawWindow;
        public ScreenShareControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _drawWindow = new DrawWindow();
            _drawWindow.Show();
        }

        ~ScreenShareControl()
        {
            _drawWindow.Close();
        }
    }
}
