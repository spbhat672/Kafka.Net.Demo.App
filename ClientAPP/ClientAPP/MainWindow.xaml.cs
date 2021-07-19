using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace ClientAPP
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

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(txtBxMessage.Text))
            {
                MessageBox.Show("put message");
                return;
            }
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44361/" + "/Home/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    MessageModel model = new MessageModel() { Message = this.txtBxMessage.Text };
                    HttpResponseMessage response = client.PostAsJsonAsync($"api/PostMessageValue/",model ).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("successfully added msg Value");
                    }
                    else
                        MessageBox.Show("Error Save data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorrr  " + ex.Message);
            }
        }

        private void btnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow window = new MessageWindow();
            window.ShowDialog();
        }
    }
}
