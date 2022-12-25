using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VacancyServiceInterface
{
	[DataContract]
	public class Vacancy
    {
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Url { get; set; }
		[DataMember]
		public string Requirement { get; set; }
		[DataMember]
		public string Responsibility { get; set; }
	}
}
