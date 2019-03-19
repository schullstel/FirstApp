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

        private static string DownloadWebPage(string theURL)
        {
            WebClient client = new WebClient();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            Stream data = client.OpenRead(theURL);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            return s;
        }

        private static string DownloadImage(string URL)
        {
            Random rnd = new Random();
            var myRandom = rnd.Next(1, 300);
            string PATH = "C:\\Users\\papoj\\source\\Commit\\WpfApp1\\WpfApp1\\";
            string name = "Image" + myRandom.ToString() + ".jpg";
            PATH += name;
            URL += myRandom.ToString();

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(URL), @PATH);
            }
            return PATH;
        }

        private string getFromSite(string URL, string beginPhrase, string endPhrase)
        {
            string site = DownloadWebPage(URL);
            var indBegin = site.IndexOf(beginPhrase, 0);
            var indEnd = site.IndexOf(endPhrase, indBegin);
            string result = site.Substring(indBegin + beginPhrase.Length, (indEnd - indBegin - beginPhrase.Length));
            return result;
        }

        private void fillByRandomData()
        {
            string Name = getFromSite(
                "https://www.behindthename.com/random/random.php?number=2&sets=1&gender=both&surname=&usage_eng=1&usage_pol=1", "/name/", "\"");
            string Age = getFromSite("https://en.wikiquote.org/wiki/Special:Random", "<title>", "</title>");
            string imageSource = DownloadImage("https://picsum.photos/200/300/?image=");
            BitmapImage src = new BitmapImage();

            src.BeginInit();
            src.UriSource = new Uri(imageSource, UriKind.Absolute);
            src.EndInit();
            Photo_Name.Source = src;

            people.Add(new Person { Age = Age.Length, Name = Name.ToUpper(), Photo = Photo_Name.Source });
        }

        private void autoFillButton_Click(object sender, RoutedEventArgs e)
        {
            fillByRandomData();
        }
    }
}