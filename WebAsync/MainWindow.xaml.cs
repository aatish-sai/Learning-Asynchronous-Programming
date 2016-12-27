using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WebAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            int contentLength = await AccessWeb();

            textBox.Text += String.Format("\r\n Length of downloaded String {0}\r\n", contentLength);
        }

        async Task<int> AccessWeb()
        {
            HttpClient client = new HttpClient();

            Task<string> task = client.GetStringAsync("https://blog.aatish.me");

            OtherWork();

            string urlContent = await task;

            return urlContent.Length;
        }

        void OtherWork()
        {
            textBox.Text += "doing other work\r\n";
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
