using ClassLibrary;
using Library_Solarsystem;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace   WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<string> Solarsystems;

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
                    foreach (var item in await response.Content.ReadAsAsync<string[]>())
                    {
                        if (!Solarsystems.Contains(item))
                            Solarsystems.Add(item);
                    }

                }


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grid_Loaded(null, null);
        }

        private async void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            using (var c = new HttpClient() { })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri($"http://localhost:59306/api/values/{combo.SelectedValue}"));
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(" comboChanged1 | " + tBName.Text);
                    this.DataContext = await response.Content.ReadAsAsync<Solarsystem>();   //Data Binding funktioniert, hier wird jedoch der Text nicht in Name sondern in "" geändert - siehe Debug
                    Debug.WriteLine(" comboChanged2 | " + tBName.Text);
                }

                treeView.Items.Clear();
                CreateTreeView();
            }
        }

        public void CreateTreeView()
        {
            TreeViewItem treeViewItem = new TreeViewItem();
            treeViewItem.Header = combo.SelectedValue.ToString();
            treeView.Items.Add(treeViewItem);
        }
    }
}
