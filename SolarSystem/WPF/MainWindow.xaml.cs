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

        public void SaveSystemList(string index)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("../../../", "jsonSolarsystems.txt")))
                {
                    solarsystemsFile[combo.SelectedIndex] = (Solarsystem) DataContext;
                    Debug.WriteLine("DataContexSolar: " + JsonConvert.SerializeObject(DataContext));
                    outputFile.Write(JsonConvert.SerializeObject(solarsystemsFile));
                }   //DataBinding läuft net Distance nicht in DataContext
                using (StreamWriter newTask = new StreamWriter("../../../Index.txt", false))
                {
                    newTask.WriteLine(index);
                }
            }
            catch (Exception)
            {
                SaveSystemList(index);
            }

            //Debug.WriteLine("ComboIndex: " + solarsystemsFile[combo.SelectedIndex].ListPlanets[0].Name);
        }

        ObservableCollection<Solarsystem> solarsystemsFile;

        public async void LoadSystemList()
        {
            try
            {
                string jsonText = File.ReadAllText("../../../jsonSolarsystems.txt");
                solarsystemsFile = JsonConvert.DeserializeObject<ObservableCollection<Solarsystem>>(jsonText);
            }
            catch (Exception)
            {
                string jsonText = File.ReadAllText("../../../jsonSolarsystemsSicherung.txt");
                solarsystemsFile = JsonConvert.DeserializeObject<ObservableCollection<Solarsystem>>(jsonText);
            }

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
            var tVItem = treeView.SelectedItem as SpaceObject;
            if (tVItem.Type != "moon")
            {
                var spaceObj = new SpaceObject("planet", "new Planet", 0, 0, 0);
                solarsystemsFile[combo.SelectedIndex].ListPlanets.Add(spaceObj);

                //TreeViewItem treeViewItem = treeView.SelectedItem as TreeViewItem;
                var item = new TreeViewItem();
                item.Header = spaceObj.Name;
                //treeView.Items.Add(item);
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            SaveSystemList("" + combo.SelectedIndex);
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            btnMoon.IsEnabled = true;
            var tVItem = treeView.SelectedItem as SpaceObject;
            if (tVItem.Type == "moon")
            {
                btnMoon.IsEnabled = false;
            }
        }

        private void StartMonogame_Click(object sender, RoutedEventArgs e)
        {
            Monogame.Program.Main();
        }
    }
}