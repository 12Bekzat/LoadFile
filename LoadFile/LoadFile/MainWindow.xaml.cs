using System;
using System.Collections.Generic;
using System.IO;
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
using System.Threading;
using System.Windows.Threading;

namespace LoadFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Action action;
        public MainWindow()
        {
            InitializeComponent();
            action = new Action(() => { loadingAtDownload.IsIndeterminate = false; });
        }

        private void LoadFile(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(url.Text))
            {
                MessageBox.Show("Адресс пустой!");
            }
            else
            {
                loadingAtDownload.IsIndeterminate = true;
                Thread thread = new Thread(Load);
                thread.IsBackground = true;
                thread.Start(url.Text);
            }
        }

        private void Load(object url)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(new Uri(url.ToString()), @"Z:\Test\book.txt");
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                   (ThreadStart)delegate ()
                   {
                       loadingAtDownload.IsIndeterminate = false;
                   });
            MessageBox.Show("Done!");
        }
    }
}
