using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientAPP
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow()
        {
            InitializeComponent();
            List<MessageModel> msgList = new List<MessageModel>();
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44361/" + "/Home/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = client.GetAsync("api/GetMessageList/").Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var resourceJsonString = response.Content.ReadAsStringAsync().Result;
                            var deserialized = JsonConvert.DeserializeObject(resourceJsonString, typeof(List<MessageModel>));
                        msgList = (List<MessageModel>)deserialized;
                        }
                    }
                }
                catch (Exception ex) 
                       {
                      }
            if(msgList.Count <= 0)
            {
                MessageModel ms = new MessageModel();
                ms.Message = "Data didnt fetch from server";
                msgList.Add(ms);
            }

            this.dgMessage.ItemsSource = msgList;
        }
    }
}
