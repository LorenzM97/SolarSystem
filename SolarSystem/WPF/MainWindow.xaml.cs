﻿using ClassLibrary;
using Library_Solarsystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private int count;
        char[] numberCheck;
        List<Solarsystem> _lstSolarSystems = new List<Solarsystem>();
        public MainWindow()
        {
            InitializeComponent();
            system0.IsSelected = true;
            TreeViewItem_MouseDoubleClick(null, null);
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

                tBX.Visibility = Visibility.Visible;
                tBY.Visibility = Visibility.Visible;
                lblPosition.Visibility = Visibility.Visible;
                lblX.Visibility = Visibility.Visible;
                lblY.Visibility = Visibility.Visible;
            }
            else if (name.Contains("system")) {
                buttonAdd.Visibility = Visibility.Visible;
                buttonAdd.Content = "Add Planet";
                lblDistance.Visibility = Visibility.Hidden;
                tBDistance.Visibility = Visibility.Hidden;

                tBX.Visibility = Visibility.Hidden;
                tBY.Visibility = Visibility.Hidden;
                lblPosition.Visibility = Visibility.Hidden;
                lblX.Visibility = Visibility.Hidden;
                lblY.Visibility = Visibility.Hidden;
            }
            else if (name.Contains("sun")) {
                buttonAdd.Content = "Add Planet";
                lblDistance.Visibility = Visibility.Hidden;
                tBDistance.Visibility = Visibility.Hidden;
                buttonAdd.Visibility = Visibility.Hidden;
                //buttonAdd.Visibility = Visibility.Visible;
                //buttonAdd.Content = "Add Planet";

                tBX.Visibility = Visibility.Visible;
                tBY.Visibility = Visibility.Visible;
                lblPosition.Visibility = Visibility.Visible;
                lblX.Visibility = Visibility.Visible;
                lblY.Visibility = Visibility.Visible;
            }
            else if (name.Contains("moon")) {
                lblDistance.Visibility = Visibility.Visible;
                tBDistance.Visibility = Visibility.Visible;
                buttonAdd.Visibility = Visibility.Hidden;

                tBX.Visibility = Visibility.Visible;
                tBY.Visibility = Visibility.Visible;
                lblPosition.Visibility = Visibility.Visible;
                lblX.Visibility = Visibility.Visible;
                lblY.Visibility = Visibility.Visible;
            }

            tBHeader.Text = tVItem.Header.ToString();
            tBX.Text = "";
            tBY.Text = "";
            tBDistance.Text = "";
            tBDistance.Background = Brushes.White;
            tBX.Background = Brushes.White;
            tBY.Background = Brushes.White;

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem tVItem = treeView.SelectedItem as TreeViewItem;
            string name = tVItem.Name;
            int count = tVItem.Items.Count;
            if (name.Contains("planet"))
            {
                var item = new TreeViewItem();
                item.Name = "moon" + count;
                item.Header = "Moon" + count;
                item.MouseLeftButtonUp += TreeViewItem_MouseDoubleClick;
                tVItem.Items.Add(item);
            }
            else if (name.Contains("system"))
            {
                var item = new TreeViewItem();
                item.Name = "planet" + count;
                item.Header = "Planet" + count;
                item.MouseLeftButtonUp += TreeViewItem_MouseDoubleClick;
                tVItem.Items.Add(item);

                //_lstSolarSystems.Add(new MovingSpaceObject("planet", item.Name, 0, 0, 0));

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem tVItem = treeView.SelectedItem as TreeViewItem;
            if (tBHeader.Text != "")
            {
                tVItem.Header = tBHeader.Text;
            }
            else
            {
                tVItem.Header = tVItem.Name;
            }

            if (NumberCheck(tBDistance))
            {
                tBDistance.Background = Brushes.White;

            }
            else
            {
                tBDistance.Background = Brushes.Red;
            }

            if (NumberCheck(tBX))
            {
                tBX.Background = Brushes.White;

            }
            else
            {
                tBX.Background = Brushes.Red;

            }

            if (NumberCheck(tBY))
            {
                tBY.Background = Brushes.White;

            }
            else
            {
                tBY.Background = Brushes.Red;

            }

            if (!NumberCheck(tBY) && tBY.IsVisible || !NumberCheck(tBX) && tBX.IsVisible || !NumberCheck(tBDistance) && tBDistance.IsVisible)
            {
                MessageBox.Show("Keine Zahl eingegeben!", "Solarsystem Editor", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            SaveInFile();
        }

        private bool NumberCheck(TextBox tb)
        {
            if (int.TryParse(tb.Text, out int n))
            {
                return true;
            }
            else
                return false;
        }

        private void SaveInFile()
        {
            // Create a string array with the lines of text
            string[] lines = { "Erde", "Mars", "Jupiter" };

            // Set a variable to the My Documents path
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt"
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(mydocpath, "WriteLines.txt")))
            {
                foreach (string line in lines)
                    outputFile.Write(line + ",");
            }
        }
    }
}
