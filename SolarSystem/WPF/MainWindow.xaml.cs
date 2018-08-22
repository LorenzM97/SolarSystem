using Library_Solarsystem;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    public partial class MainWindow : Window
    {

        static public ObservableCollection<string> Solarsystems;

        public MainWindow()
        {
            Solarsystems = new ObservableCollection<string>();

            InitializeComponent();

            combo.DataContext = Solarsystems;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSystemList();
            using (var c = new HttpClient() { })
            {
                while (true)
                {
                    //System.Threading.Thread.Sleep(500);

                    await Task.Yield();

                    HttpResponseMessage response = await c.GetAsync(new Uri(@"http://localhost:59306/api/values"));
                    if (response.IsSuccessStatusCode)
                    {
                        foreach (var item in await response.Content.ReadAsAsync<Solarsystem[]>())
                        {
                            if (!Solarsystems.Contains(item.Name))
                                Solarsystems.Add(item.Name);
                        }
                    }

                    await Task.Yield();
                }
            }
        }

        private async void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SaveSystemList();
            

            using (var c = new HttpClient() { })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri($"http://localhost:59306/api/values/{combo.SelectedValue}"));
                if (response.IsSuccessStatusCode)
                {
                    this.DataContext = await response.Content.ReadAsAsync<Solarsystem>();
                }
            }

            LoadSystemList();
        }

        public async void SaveSystemList()
        {
            using (var c = new HttpClient() { })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri($"http://localhost:59306/api/values"));
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("../../../", "jsonSolarsystems.txt")))
                        {
                            outputFile.Write(await response.Content.ReadAsStringAsync());
                        }
                    }
                    catch (Exception e)
                    {
                        SaveSystemList();
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
            }
        }

        private void AddMoon(object sender, RoutedEventArgs e)
        {
            //var tVitemPos = treeView.Items.CurrentPosition;
            //var tVItem = treeView.Items.GetItemAt(tVitemPos) as TreeViewItem;
            
            
            //string name = tVItem.Name;
        }

        private void AddSystem(object sender, RoutedEventArgs e)
        {
        }

        private void AddPlanet(object sender, RoutedEventArgs e)
        {
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            SaveSystemList();
        }
    }
}