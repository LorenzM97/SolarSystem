using ClassLibrary;
using Library_Solarsystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace WPF
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
            using (var c = new HttpClient() {  })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri(@"http://localhost:59306/api/values"));
                if (response.IsSuccessStatusCode)
                {
                    foreach (var item in await response.Content.ReadAsAsync<string[]>())
                    {
                        if(!Solarsystems.Contains(item))
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
                    this.DataContext = await response.Content.ReadAsAsync<SpaceObject>();

                }


            }
        }
    }
}
