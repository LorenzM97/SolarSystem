using Library_Solarsystem;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

       

        private async void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                       
            using (var c = new HttpClient() { })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri($"http://localhost:59306/api/values/{combo.SelectedValue}"));
                if (response.IsSuccessStatusCode)
                {                   

                    this.DataContext = await response.Content.ReadAsAsync<Solarsystem>();
                }
                
            }
        }

        
    

        private void AddMoon(object sender, RoutedEventArgs e)
        {
            TreeViewItem tVItem = treeView.SelectedItem as TreeViewItem;
            



            //using (var c = new HttpClient() { })
            //{
            //    HttpResponseMessage response = await c.GetAsync(new Uri($"http://localhost:59306/api/values/{treeView.SelectedItem}"));
            //    if (response.IsSuccessStatusCode)
            //    {


            //        this.DataContext = await response.Content.ReadAsAsync<Solarsystem>();



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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
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
            var solarsystemsFile = JsonConvert.DeserializeObject<ObservableCollection<Solarsystem>>(jsonText);
            

            using (var c = new HttpClient() { })
            {
                var response = await c.PostAsJsonAsync(new Uri($"http://localhost:59306/api/values"), solarsystemsFile);
                //solarsystemsFile = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Solarsystem>>(await response.Content.ReadAsStringAsync());
            }
        }

        public void CreateTreeView()
        {
            TreeViewItem treeViewItem = new TreeViewItem();
            treeViewItem.Header = combo.SelectedValue.ToString();
            treeView.Items.Add(treeViewItem);
                while (true)
                {
                    System.Threading.Thread.Sleep(500);

                    await Task.Yield();

                    HttpResponseMessage response = await c.GetAsync(new Uri(@"http://localhost:59306/api/values"));
                    if (response.IsSuccessStatusCode)
                    {
                        foreach (var item in await response.Content.ReadAsAsync<string[]>())
                        {
                            if (!Solarsystems.Contains(item))
                                Solarsystems.Add(item);
                        }
                        return;
                    }

                    await Task.Yield();
                }
            }
        }
    }
}
