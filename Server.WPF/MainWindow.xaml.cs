using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Server.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isCursorOverImage = false;
        public MainWindow()
        {
            InitializeComponent();
            var hostName = Dns.GetHostName();
            var hostAddress = Dns.GetHostAddresses(hostName);
            var ipAddress = hostAddress.Where(ip => ip.AddressFamily
            == System.Net.Sockets.AddressFamily.InterNetwork);

            string text = "";
            foreach(var ip in ipAddress)
            {
                text += ip.ToString() + "\n";
            }

            MessageBox.Show(text);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !isCursorOverImage) DragMove();
        }
    }
}
