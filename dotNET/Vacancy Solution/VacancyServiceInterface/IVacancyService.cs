using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VacancyServiceInterface
{
	[ServiceContract]
	public interface IVacancyService
	{
		[OperationContract]
		Vacancy[] GetOnlineVacancies(string searchText);

		[OperationContract]
		Vacancy[] GetLocalVacancies(string searchText);
	}
}
