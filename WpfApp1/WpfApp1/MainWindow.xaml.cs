using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using WpfApp1.Entity_Data_Modells;

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

        PersonEntryEntities db = new PersonEntryEntities();
        CollectionViewSource personViewSource;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            personViewSource =
                ((CollectionViewSource)(this.FindResource("personEntryViewSource")));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.PersonEntry.Local.Concat(db.PersonEntry.ToList());
            personViewSource.Source = db.PersonEntry.Local;
            System.Windows.Data.CollectionViewSource personEntryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personEntryViewSource")));
            
            // Załaduj dane poprzez ustawienie właściwości CollectionViewSource.Source:
            // personEntryViewSource.Źródło = [ogólne źródło danych]
        }

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            int tmp;
            if (!(Photo_Name.Source is null) && (int.TryParse(ageTextBox.Text, out tmp)))
            {
                people.Add(new Person { Age = tmp, Name = nameTextBox.Text, Photo = Photo_Name.Source });
            }
            
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newEntry = new PersonEntry()
            {
                Id = int.Parse(idTextBox.Text),
                Wzrost = int.Parse(wzrostTextBox.Text),
                Waga = int.Parse(wagaTextBox.Text),
                Typ_budowy = typ_budowyTextBox.Text
            };
            db.PersonEntry.Local.Add(newEntry);
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                db.PersonEntry.Local.Remove(newEntry);
                Debug.WriteLine("Error, id is not unique!");
            }
            idTextBox.Clear();
            wzrostTextBox.Clear();
            wagaTextBox.Clear();
            typ_budowyTextBox.Clear();
        }
    }
}