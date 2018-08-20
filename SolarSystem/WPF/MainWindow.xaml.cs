using ClassLibrary;
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
//using System.Web.Script.Serialization;

namespace   WPF
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

            //TreeViewItem tVItem = treeView.SelectedItem as TreeViewItem;
            //string name = tVItem.Name;
            //int count = tVItem.Items.Count;
            //if (name.Contains("planet"))
            //{
            //    var item = new TreeViewItem();
            //    item.Name = "moon" + count;
            //    item.Header = "Moon" + count;
            //    tVItem.Items.Add(item);
            //}
            //else if (name.Contains("system"))
            //{
            //    var item = new TreeViewItem();
            //    item.Name = "planet" + count;
            //    item.Header = "Planet" + count;
            //    item.MouseLeftButtonUp += TreeViewItem_MouseDoubleClick;
            //    tVItem.Items.Add(item);
            //}
        }

        private void AddSystem(object sender, RoutedEventArgs e)
        {
            //TreeViewItem tVItem = treeView.SelectedItem as TreeViewItem;
            //if(tVItem.)
        }

        private void AddPlanet(object sender, RoutedEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var c = new HttpClient() { })
            {
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
