using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		static public async Task<Person> getRandomPerson()
		{
			return await getRandomPersonFromSite(
				"https://randomuser.me/api/?format=xml&fbclid=IwAR21SkL7KfGt4MD8tRY2q2yljzhNNGsydkXfrRQ95P4F1aYcl6gTPhbhdCk");
		}


		private static async Task<Person> getRandomPersonFromSite(string URL)
		{
			using (WebClient client = new WebClient())
			{
				Stream data = client.OpenRead(URL);
				StreamReader reader = new StreamReader(data);
				string s = reader.ReadToEnd();
			}
			var imageSrc = await DownloadImage("https://picsum.photos/200/300/?image=");
			var age = 2;
			string name = "Imie";
			return (new Person { Age = age, Name = name, Photo = (ImageSource)imageSrc });
		}

		private static async Task<ImageSource> DownloadImage(string URL)
		{
			string imageName = await generateRandomImageName(); 
			string PATH = "C:\\Users\\papoj\\source\\Commit\\WpfApp1\\WpfApp1\\" + imageName;
			URL += imageName;
			try
			{
				using (WebClient client = new WebClient())
				{
					client.DownloadFile(new Uri(URL), @PATH);
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
			await Task.Delay(1000);
			return src;
		}

		private static async Task<string> generateRandomImageName()
		{
			Random rnd = new Random();
			var myRandom = rnd.Next(1, 300);
			return "Image" + myRandom.ToString() + ".jpg";
		}

	}
}
