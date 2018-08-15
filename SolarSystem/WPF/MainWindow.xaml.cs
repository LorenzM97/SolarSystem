using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem tVItem = treeView.SelectedItem as TreeViewItem;
            string name = tVItem.Name;
            if (name.Contains("planet")) {
                lblDistance.Visibility = Visibility.Visible;
                tBDistance.Visibility = Visibility.Visible;
                buttonAdd.Visibility = Visibility.Visible;
                buttonAdd.Content = "Add Moon";
            }
            else if (name.Contains("system")) {
                buttonAdd.Visibility = Visibility.Visible;
                buttonAdd.Content = "Add Planet";
                lblDistance.Visibility = Visibility.Hidden;
                tBDistance.Visibility = Visibility.Hidden;
            }
            else if (name.Contains("sun")) {
                buttonAdd.Content = "Add Planet";
                lblDistance.Visibility = Visibility.Hidden;
                tBDistance.Visibility = Visibility.Hidden;
                buttonAdd.Visibility = Visibility.Hidden;
            }
            else if (name.Contains("moon")) {
                lblDistance.Visibility = Visibility.Visible;
                tBDistance.Visibility = Visibility.Visible;
                buttonAdd.Visibility = Visibility.Hidden;
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem tVItem = treeView.SelectedItem as TreeViewItem;
            string name = tVItem.Name;
            if (name.Contains("planet"))
            {
                var item = new TreeViewItem();
                item.Name = "moon";
                item.Header = "new Moon";
                item.MouseLeftButtonUp += TreeViewItem_MouseDoubleClick;
                tVItem.Items.Add(item);
            }
            else if (name.Contains("system"))
            {
                var item = new TreeViewItem();
                item.Name = "planet";
                item.Header = "new Planet";
                item.MouseLeftButtonUp += TreeViewItem_MouseDoubleClick;
                tVItem.Items.Add(item);
            }
        }
    }
}
