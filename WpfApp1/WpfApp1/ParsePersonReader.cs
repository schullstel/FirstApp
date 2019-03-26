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
							case "first":
								{
									while (reader.NodeType != XmlNodeType.Text)
										reader.Read();
									person.Name = reader.Value;
									break;
								}
							case "age":
								{
									while (reader.NodeType != XmlNodeType.Text)
										reader.Read();
									person.Age = int.Parse(reader.Value);
									return person;
								}
						}
						break;
				}
			}
			return person;
		}
	}
}
