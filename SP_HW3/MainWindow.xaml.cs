using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace SP_HW3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Path { get; set; } = @"C:\New\";
        private string Url { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFile(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(urlText.Text) || urlText.Text.Contains("Url"))
            {
                MessageBox.Show("Поле пустое!");
                return;
            }

            progressBar.Visibility = Visibility.Visible;
            Url = urlText.Text;

            var thread = new Thread(Download);
            thread.IsBackground = true;
            thread.Start();
        }

        private void UrlTextGotFocus(object sender, RoutedEventArgs e)
        {
            urlText.Text = "";
        }

        private void Download()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(Url, $@"{Directory.GetCurrentDirectory()}\{Url.Substring(Url.Length - 10)}");
                }
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                   (ThreadStart)delegate ()
                   {
                       progressBar.Visibility = Visibility.Hidden;
                   });
                MessageBox.Show("Файл загружен");
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
