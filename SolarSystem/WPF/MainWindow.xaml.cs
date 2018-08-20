using ClassLibrary;
using Library_Solarsystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
//using System.Web.Script.Serialization;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static public ObservableCollection<string> Solarsystems;

        public MainWindow()
        {
            Solarsystems = new ObservableCollection<string>();

            InitializeComponent();

            combo.DataContext = Solarsystems;
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            using (var c = new HttpClient() { })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri(@"http://localhost:59306/api/values"));
                if (response.IsSuccessStatusCode)
                {
                    //Galaxy galaxy1 = await response.Content.ReadAsAsync<Galaxy>();
                    foreach (var item in await response.Content.ReadAsAsync<Solarsystem[]>())
                    {
                        if (!Solarsystems.Contains(item.Name))
                            Solarsystems.Add(item.Name);
                    }
                }
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadSystemList();
            Grid_Loaded(null, null);
            
        }

        private async void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveSystemList();

            using (var c = new HttpClient() { })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri($"http://localhost:59306/api/values/{combo.SelectedValue}"));// /{combo.SelectedValue}
                if (response.IsSuccessStatusCode)
                {
                    //Galaxy galaxy2 = await response.Content.ReadAsAsync<Galaxy>();

                    //Solarsystem selectedSystem = new Solarsystem("Not in your System");
                    //foreach (var item1 in galaxy2.ListSystems)
                    //{
                    //    if (item1.Name.Equals(combo.SelectedValue))
                    //        selectedSystem = item1;
                    //}

                    this.DataContext = await response.Content.ReadAsAsync<Solarsystem>();//selectedSystem;/*galaxy2.ListSystems[galaxy2.ListSystems.IndexOf(item)];

                    //using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("C:/Users/A675952/Lync Recordings", "jsonSolarsystem.txt")))
                    //{
                    //    outputFile.Write(await response.Content.ReadAsStringAsync());
                    //}
                }

                //treeView.Items.Clear();
                //CreateTreeView();
            }
        }

        public async void SaveSystemList()
        {
            using (var c = new HttpClient() { })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri($"http://localhost:59306/api/values"));
                if (response.IsSuccessStatusCode)
                {
                    using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("../../../", "jsonSolarsystems.txt")))
                    {
                        outputFile.Write(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public async void LoadSystemList()
        {
            string jsonText = File.ReadAllText("../../../jsonSolarsystems.txt");
            //JObject jObject = JObject.Parse(jsonText);
            var solarsystemsFile = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Solarsystem>>(jsonText);

            using (var c = new HttpClient() { })
            {
                var response = await c.PostAsJsonAsync(new Uri($"http://localhost:59306/api/values"), solarsystemsFile);

                //StringContent queryString = new StringContent(jsonText);
                //HttpResponseMessage response = await c.PostAsync(new Uri($"http://localhost:59306/api/values"), queryString);
            }
        }

        public void CreateTreeView()
        {
            TreeViewItem treeViewItem = new TreeViewItem();
            treeViewItem.Header = combo.SelectedValue.ToString();
            treeView.Items.Add(treeViewItem);


            //public static ObservableCollection GetData()
            //{
            //    ObservableCollection items = new ObservableCollection();

            //}
        }
    }
}
