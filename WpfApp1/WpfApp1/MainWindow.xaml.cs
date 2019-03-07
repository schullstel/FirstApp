using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void List_view_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void buttonLoad_Folder(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg)|*.jpeg|PNG files(*.png)|*.png|All files(*.*)|*.*";
            try
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                if (openFileDialog.ShowDialog() == true)
                {
                    BitmapImage src = new BitmapImage();
                    src.BeginInit();
                    src.UriSource = new Uri(openFileDialog.FileName, UriKind.Absolute);
                    src.EndInit();
                    imageView.Source = src;

                    imageView.Stretch = Stretch.Uniform;
                    imageView.Height = 120;
                    imageView.Width = 120;
                }
            } catch
            {
            }

  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        List<MyData> items = new List<MyData>();

        private void buttonLoad_List(object sender, RoutedEventArgs e)
        {
            items.Add(new MyData() { Name = name.Text, Surname = surname.Text, Photo = imageView});
            list_view.ItemsSource = items;
            name.Text = "";
            surname.Text = "";
            imageView.Source = null;

        }
    }
}
