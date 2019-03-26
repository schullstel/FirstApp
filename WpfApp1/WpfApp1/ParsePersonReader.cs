using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Lab01;

namespace WpfApp1
{
	class ParsePersonReader
	{
		public static Person parse(System.IO.Stream stream)
		{
			XmlTextReader reader = new XmlTextReader(stream);
			Person person = new Person()
			{
				Age = 0,
				Name = string.Empty
			};

			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						switch (reader.Name)
						{
							case "name":
								person.Name = "RandomName";
								break;
							case "dob":
								person.Age = 27;
								break;
						}
						break;
				}
			}
			return person;
		}
	}
}
