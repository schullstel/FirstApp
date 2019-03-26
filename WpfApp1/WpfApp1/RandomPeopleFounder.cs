using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Lab01;

namespace WpfApp1
{
	class RandomPeopleFounder
	{
		public static object PersonParseReader { get; private set; }

		static public async Task<Person> getRandomPerson()
		{
			return await getRandomPersonFromSite();
		}


		private static async Task<Person> getRandomPersonFromSite()
		{
			Person person = await downloadRandomPersonAsync("https://randomuser.me/api/?format=xml&fbclid=IwAR21SkL7KfGt4MD8tRY2q2yljzhNNGsydkXfrRQ95P4F1aYcl6gTPhbhdCk");
			var imageSrc = await downloadImageAsync("https://picsum.photos/200/300/?image");
			person.Photo = imageSrc;
			return person;
		}

		private static async Task<Person> downloadRandomPersonAsync(string URL)
		{
			string rawXML;
			using (WebClient client = new WebClient())
			{
				Stream data = client.OpenRead(URL);
				StreamReader reader = new StreamReader(data);
				rawXML = await reader.ReadToEndAsync();
			}

			Person person;
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(rawXML)))
			{
				person = ParsePersonReader.parse(stream);		//	TUTAJ MOZNA WYBRAC PARSER 
															// ParsePersonLINQ lub ParsePersonReader
			}
			return person;
		}

		private static async Task<ImageSource> downloadImageAsync(string URL)
		{
			string imageNumber = await generateRandomImageNumber(); 
			string PATH = "C:\\Users\\karol\\Documents\\Image" + imageNumber;
			URL += imageNumber;
			try
			{
				using (WebClient client = new WebClient())
				{
					client.DownloadFileAsync(new Uri(URL), @PATH);
				}
				await Task.Delay(1000);
			}
			catch (Exception e)
			{
			}
			return await getSourcePathFromString(PATH);
		}

		private static async Task<ImageSource> getSourcePathFromString(string path)
		{
			BitmapImage src = new BitmapImage();
			src.BeginInit();
			src.UriSource = new Uri(path, UriKind.Absolute);
			src.EndInit();
			await Task.Delay(0);
			return src;
		}

		private static async Task<string> generateRandomImageNumber()
		{
			Random rnd = new Random();
			var myRandom = rnd.Next(1, 300);
			await Task.Delay(0);
			return myRandom.ToString() + ".jpg";
		}

	}
}
