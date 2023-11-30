using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace NetWorking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NASALibrary : ContentPage
    {
        private readonly static string My_API = "efNjLdEED5GcZdRvaEtObuFR91CEdhhfumCuv9dL";
        static readonly HttpClient client = new HttpClient();
        public NASALibrary()
        {
            InitializeComponent();
        }
        
        public async void MakeRequest()
        {
            string url = $"https://images-api.nasa.gov/search?center=KSC&photographer=NASA/SpaceX";

            string year_start = start_year.Text;
            string year_end = end_year.Text; 
            string datePicked = one_year.Text; 

            if (chkbx.IsChecked)    // посылаем период
                url += $"&year_start={year_start}&year_end={year_end}";
            else                    // посылаем один год
                url += $"&year_start={datePicked}&year_end={datePicked}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                string PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "text2.txt");


                System.IO.File.WriteAllText(PATH, responseBody);
                string json = System.IO.File.ReadAllText(PATH);

                Root? myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseBody);
                List<Item> items = myDeserializedClass.collection.items;
                taskList.ItemsSource = items;

                //try
                //{
                //    List<Amod> lists = JsonConvert.DeserializeObject<List<Amod>>(json);
                //}
                //catch 
                //{
                //    Amod amod = JsonConvert.DeserializeObject<Amod>(json);
                //}

                //foreach (Item item in items)
                //{
                //    //Console.WriteLine("Название: " + item.data[0].title);
                //    //Console.WriteLine("Ссылка: " + item.links[0].href);
                //    //Console.WriteLine();
                //}

                //Console.WriteLine(myDeserializedClass.collection.items[0].links[0].href);

            }

            catch (HttpRequestException e){}
        }

        private void Button_Clicked(object sender, EventArgs e) => MakeRequest();

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (chkbx.IsChecked)
            {
                labeltxt.Text = "Укажите нужный период";
                one_year.IsVisible = false;
                start_year.IsVisible = true;
                end_year.IsVisible = true;
            }
            else
            {
                labeltxt.Text = "Укажите нужный год";
                one_year.IsVisible = true;
                start_year.IsVisible = false;
                end_year.IsVisible = false;
            }

        }
    }

    public class Collection
    {
        public string version { get; set; }
        public string href { get; set; }
        public List<Item> items { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Datum
    {
        public string center { get; set; }
        public DateTime date_created { get; set; }
        public string description { get; set; }
        public List<string> keywords { get; set; }
        public string location { get; set; }
        public string media_type { get; set; }
        public string nasa_id { get; set; }
        public string photographer { get; set; }
        public string title { get; set; }
        public List<string> album { get; set; }
    }

    public class Item
    {
        public string href { get; set; }
        public List<Datum> data { get; set; }
        public List<Link> links { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string render { get; set; }
    }

    public class Metadata
    {
        public int total_hits { get; set; }
    }

    public class Root
    {
        public Collection collection { get; set; }
    }


}