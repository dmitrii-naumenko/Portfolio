using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VacancyService.Properties;
using VacancyServiceInterface;

namespace VacancyService
{
	public class VacancyService : IVacancyService
	{
		public VacancyServiceInterface.Vacancy[] GetOnlineVacancies(string searchText)
		{
			var vacancies = GetVacanciesFromHeadHunter(searchText);

			if (vacancies != null && vacancies.Length > 0)
				Task.Run(() => AddVacanciesToDatabase(vacancies));

			return vacancies;
		}

		private void AddVacanciesToDatabase(VacancyServiceInterface.Vacancy[] vacancies)
		{
			try
			{
				using (var db = new VacancyDataClassesDataContext(Settings.Default.KaVacancyConnectionString))
				{
					var inserted = vacancies.Select(
						x => new Vacancy()
						{
							Name = x.Name?.LimitLength(256),
							Url = x.Url?.LimitLength(256),
							Requirement = x.Requirement?.LimitLength(2048),
							Responsibility = x.Responsibility?.LimitLength(2048)
						});
					inserted = inserted.Where(x => !db.Vacancies.Any(y => x.Url == y.Url));
					db.Vacancies.InsertAllOnSubmit(inserted);
					db.SubmitChanges();
				}
			}
			catch (Exception e)
			{
				// failure to connect to the database should not destroy online service
				// So, need add some information to logs and show warning 
				Console.WriteLine($"Database error \"{e.Message}\"");
			}
		}

		private VacancyServiceInterface.Vacancy[] GetVacanciesFromHeadHunter(string searchText)
		{
			var webRequest =
				(HttpWebRequest)
					WebRequest.Create(string.Format(@"https://api.hh.ru/vacancies?text={0}&per_page=50",
						WebUtility.HtmlDecode(searchText)));
			webRequest.Method = "GET";
			webRequest.UserAgent = @"Mozilla/5.0 (Windows NT 5.1; rv:28.0) Gecko/20100101 Firefox/28.0";

			HttpWebResponse webResponse;
			try
			{
				webResponse = (HttpWebResponse) (webRequest.GetResponse());
			}
			catch (Exception e)
			{
				throw new Exception("Can't load vacancies from Head Hunter", e);
			}

			try
			{
				using (var reader = new StreamReader(webResponse.GetResponseStream()))
				{
					var response = reader.ReadToEnd();
					return
						JsonConvert.DeserializeObject<HhResponse>(response)
							.items.Select(
								x =>
									new VacancyServiceInterface.Vacancy()
									{
										Name = x.name,
										Url = x.apply_alternate_url,
										Requirement = x.snippet.requirement,
										Responsibility = x.snippet.responsibility
									})
							.ToArray();
				}
			}
			catch (Exception e)
			{
				throw new Exception("Can't deserialize vacancies", e);
			}
		}

		public VacancyServiceInterface.Vacancy[] GetLocalVacancies(string searchText)
		{
			try
			{
				using (var db = new VacancyDataClassesDataContext(Settings.Default.KaVacancyConnectionString))
				{
					var q = from v in db.Vacancies
							join fts in db.FTS_VACANTION(searchText) on v.ID equals fts.key
							orderby fts.rank descending
							select new VacancyServiceInterface.Vacancy()
							{
								Name = v.Name,
								Url = v.Url,
								Requirement = v.Requirement,
								Responsibility = v.Responsibility
							};
					return q.Take(50).ToArray();
				}
			}
			catch (Exception e)
			{
				throw new Exception($"Database error \"{e.Message}\"", e);
			}
		}


		public class HhResponse
		{
			public HhItemType[] items { get; set; }
			public class HhItemType
			{
				public string name { get; set; }
				public string apply_alternate_url { get; set; }
				public HhSnippetType snippet { get; set; }
			}

			public class HhSnippetType
			{
				public string requirement { get; set; }
				public string responsibility { get; set; }
			}
		}
	}
}
