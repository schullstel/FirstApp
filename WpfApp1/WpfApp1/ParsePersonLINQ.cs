using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lab01;

namespace WpfApp1
{
	static class ParsePersonLINQ
	{
		public static Person parse(System.IO.Stream stream)
		{
			XElement xml = XElement.Load(stream);
			var name = (from element in xml.Elements()
						let elementName = element.Name
						where (elementName == "name")
						select new
						{
							Name = element.Attributes("first").FirstOrDefault(),
						});
			var age = (from element in xml.Elements()
					   let elementName = element.Name
					   where (elementName == "dob")
					   select new
					   {
						   Age = element.Attributes("age").FirstOrDefault(),
					   });
			return new Person
			{
				Name = name.FirstOrDefault().Name.Value,
				Age = int.Parse(
					age.FirstOrDefault().Age.Value,
					System.Globalization.CultureInfo.InvariantCulture)
			};
		}
	}
}
