using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;
using Xamarin.Forms.Shapes;
using System.Net.WebSockets;
using System.Collections.Specialized;
using System.Collections;

namespace NetWorking
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string classId = button.ClassId;

            switch (classId)
            {
                case "Library":
                    await Navigation.PushAsync(new NASALibrary());
                    break;
                default:
                    break;
            }
        }
    }
}



//foreach(var img in lists)
//{
//    label1.Text += img.url + " DONE ";
//}