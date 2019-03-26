using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
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
using WpfApp1;

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
        };

        public ObservableCollection<Person> Items
        {
            get => people;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            int tmp;
                if (!(Photo_Name.Source is null) && (int.TryParse(ageTextBox.Text, out tmp)))
                    people.Add(new Person { Age = tmp, Name = nameTextBox.Text, Photo = Photo_Name.Source });
            
            nameTextBox.Text = "";
            ageTextBox.Text = "";
            Photo_Name.Source = null;
        }

        private void LoadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg)|*.jpeg|PNG files(*.png)|*.png|All files(*.*)|*.*";

                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                if (openFileDialog.ShowDialog() == true)
                {
                    BitmapImage src = new BitmapImage();
                    src.BeginInit();
                    src.UriSource = new Uri(openFileDialog.FileName, UriKind.Absolute);
                    src.EndInit();
                    Photo_Name.Source = src;

                    Photo_Name.Stretch = Stretch.Uniform;
                    Photo_Name.Height = 120;
                    Photo_Name.Width = 120;
                }
        }

		private async void autoFillButton_Click(object sender, RoutedEventArgs e)
		{
			while (true)
			{
				people.Add(await RandomPeopleFounder.getRandomPerson());
			}
		}
    }
}