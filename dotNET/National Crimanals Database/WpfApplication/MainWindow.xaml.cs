using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Data.ConnectionUI;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlServerCe;
using NationalCriminalDatabase;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseConnectionClick(object sender, RoutedEventArgs e)
        {
            /// Пример использования диалога выбора строки соединения 
            /// Предварительно нужно установить пакет NuGet
            DataConnectionDialog dcd = new DataConnectionDialog();
            DataSource.AddStandardDataSources(dcd);


            //dcd.ConnectionString = Properties.Settings.Default.ConnectionString;
            //DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
            //dcs.LoadConfiguration(dcd);
            ////////if (DataConnectionDialog.Show(dcd) == System.Windows.Forms.DialogResult.OK)
            //    Properties.Settings.Default.ConnectionString = dcd.ConnectionString;
            //dcs.SaveConfiguration(dcd);
            //return connectionString;

            if (DataConnectionDialog.Show(dcd) == System.Windows.Forms.DialogResult.OK)
                MessageBox.Show(dcd.ConnectionString);

            /// Можно сохранить строку соединений, но в этом приложении она неизменная через GUI 
            /// и хранится в файле конфигарации приложения Properties.Settings.Default.ConnectionString

        }


        private DataTable GetNationality()
        {
            DataTable table = new DataTable();
            table.Columns.Add("[Alias]", typeof(string));

            table.Rows.Add("<Undefined>");
            table.Rows.Add("Belgian");
            table.Rows.Add("English");

            return table;

        }



        private void SqlCommandClick(object sender, RoutedEventArgs e)
        {
            using (var sqlConnection  = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "dbo.Search";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.Parameters.AddWithValue("FirstName", /*"David"*/DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("LastName", DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("Sex", DBNull.Value);
                    sqlCommand.Parameters.Add(new SqlParameter("Nationality",GetNationality())
                        {SqlDbType = System.Data.SqlDbType.Structured, TypeName = "dbo.NationalityTypeTable"});
                    sqlCommand.Parameters.AddWithValue("AgeFrom", 40/*DBNull.Value*/);
                    sqlCommand.Parameters.AddWithValue("AgeBefore", 60/*DBNull.Value*/);
                    sqlCommand.Parameters.AddWithValue("HeightFrom", DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("HeightBefore", DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("WeightFrom", DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("WeightBefore", DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("FreeText", DBNull.Value);

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    var s = "";
                    while (reader.Read())
                        s += reader["First name"] + "\n";
                    MessageBox.Show(s);
                }
            }
        }

        private void LinqToSqlClick(object sender, RoutedEventArgs e)
        {
            ///http://professorweb.ru/my/LINQ/linq_sql/level9/9_4.php

            var nat = new List<string>() { "<Undefined>", "Belgian", "English" };

            var res = Queryable(nat);


            //var s = res.Aggregate("", (total, next) =>  total + next + "\n");   - не поддерживается

            string s = "";
            foreach (var row in res)
                s += row.FirstName + "\n";

            /// Полнотекстовой поиск пможено использовать через создание функции,
            /// которая возвращает таблицу по полнотекстовому поиску и далее соединяться с ней
            /// например тут http://stackoverflow.com/questions/224475/is-it-possible-to-use-full-text-search-fts-with-linq

            MessageBox.Show(s);

        }

        public interface IDb
        {
            IQueryable<Person>  Persons { get;  }
            IQueryable<Nationality> Nationalities { get; }
            IQueryable<Sex> Sexes { get; }
            
        }

        public class RealDb :  IDb
        {
            internal NationalCriminalDatabaseDataContext context =
                new NationalCriminalDatabaseDataContext(Properties.Settings.Default.ConnectionString);
            
            public IQueryable<Person> Persons
            {
                get { return context.Persons; }
            }

            public IQueryable<Nationality> Nationalities
            {
                get { return context.Nationalities; }
            }

            public IQueryable<Sex> Sexes
            {
                get { return context.Sexes; }
            }
        }

        public class FakeDb : IDb
        {

            public FakeDb()
            {

                Sexes = new List<Sex>()
                {
                    new Sex(){id=1, Alias = "Man"},
                    new Sex(){id=2, Alias = "Female"}
                }.AsQueryable();

                Nationalities = new List<Nationality>()
                {
                    new Nationality(){id=1, Alias = "<Undefined>"},
                    new Nationality(){id=2, Alias = "Argentinian"},
                    new Nationality(){id=3, Alias = "Australian"},
                    new Nationality(){id=4, Alias = "Belgian"},
                    new Nationality(){id=5, Alias = "Canadian"},
                    new Nationality(){id=6, Alias = "Chinese"},
                    new Nationality(){id=7, Alias = "Danish"},
                    new Nationality(){id=8, Alias = "English"},
                    new Nationality(){id=9, Alias = "French"},
                    new Nationality(){id=10, Alias = "German"},
                    new Nationality(){id=11, Alias = "Dutch"},
                    new Nationality(){id=12, Alias = "Indian"},
                    new Nationality(){id=13, Alias = "Japanese"},
                    new Nationality(){id=14, Alias = "Mexican"},
                    new Nationality(){id=15, Alias = "Norwegian"},
                    new Nationality(){id=16, Alias = "Polish"},
                    new Nationality(){id=17, Alias = "Russian"},
                    new Nationality(){id=18, Alias = "Singaporean"},
                    new Nationality(){id=19, Alias = "Thai"},
                    new Nationality(){id=20, Alias = "US"},
                    new Nationality(){id=20, Alias = "Uzbek"}
                }.AsQueryable();

                Persons = new List<Person>()
                {
                    new Person(){id = 3, FirstName = "Michael", LastName = "Adamson", Sex = 1, 
                        DateOfBirth = DateTime.Parse("01.01.1972"), Nationality = 1},
                    new Person(){id = 4, FirstName = "Emily", LastName = "Adamson", Sex = 2, Nationality = 1},
                    new Person(){id = 6, FirstName = "Olivia", LastName = "Holiday", Sex = 2, Nationality = 4},
                    new Person(){id = 7, FirstName = "David", LastName = "James", DateOfBirth = DateTime.Parse("01.01.1971"), 
                        Sex = 2, Weight = 60, Nationality = 4, Text = "Just for example! I was born on the 3rd of June 1991 in Moscow"},
                    new Person(){id = 8, FirstName = "Daniel", Sex = 1,
                        DateOfBirth = DateTime.Parse("01.01.1972"), Weight = 65, Nationality = 4},
                    new Person(){id = 9, FirstName = "Joseph", Sex = 1,
                        DateOfBirth = DateTime.Parse("01.01.1973"), Weight = 70, Nationality = 6},
                    new Person(){id = 10, FirstName = "Ryan", Sex = 1, Height = 168,
                        DateOfBirth = DateTime.Parse("01.01.1974"), Weight = 75, Nationality = 6},
                    new Person(){id = 11, FirstName = "Tyler", Sex = 1, Height = 169,
                        DateOfBirth = DateTime.Parse("01.01.1975"), Weight = 80, Nationality = 6},
                    new Person(){id = 12, FirstName = "John", Sex = 1, Height = 170,
                        DateOfBirth = DateTime.Parse("01.01.1976"), Weight = 85, Nationality = 8},
                    new Person(){id = 14, FirstName = "Logan", Sex = 1, Height = 171,
                        DateOfBirth = DateTime.Parse("01.01.1977"), Weight = 90, Nationality = 8,
                        Text = "'Just for example! My name is . I would like to tell you about my s"},
                    new Person(){id = 16, FirstName = "Jose", Sex = 1, Height = 172,
                        DateOfBirth = DateTime.Parse("01.01.1978"), Weight = 95, Nationality = 8},
                    new Person(){id = 17, FirstName = "David", Sex = 1, Height = 173,
                        DateOfBirth = DateTime.Parse("01.01.1979"), Nationality = 20},
                    new Person(){id = 18, FirstName = "Luke", Sex = 1, Height = 173,
                        DateOfBirth = DateTime.Parse("01.01.1980"), Nationality = 20},
                    new Person(){id = 19, FirstName = "Cameron", Sex = 1, Nationality = 2},
                    new Person(){id = 20, FirstName = "Adam", Sex = 1, Nationality = 1},
                }.AsQueryable();
              

            }

            public IQueryable<Person> Persons { get; private set; }
            public IQueryable<Nationality> Nationalities { get; private set; }
            public IQueryable<Sex> Sexes { get; private set; }
        }

        private IQueryable<Person> Queryable(List<string> nat)
        {
            //var db = new NationalCriminalDatabaseDataContext(Properties.Settings.Default.ConnectionString);

            //var db = new RealDb();
            //db.context.Log = Console.Out;

            //var db = new NationalCriminalDatabaseDataContext();
            //db.Log = Console.Out;
            
            var db = new FakeDb();

            /*
            var res = from person in db.Persons
                //join n in db.Nationalities on person.Nationality equals n.id
                //where (person.DateOfBirth > new DateTime(1970, 1, 1)) &&
                //      (person.DateOfBirth < new DateTime(2000, 1, 1)) &&
                //      (nat.Contains(n.Alias))
                select person;
            */

            var par = new SearchParameter()
            {
                AgeFrom = 40,
                AgeBefore = 60,
                Nationality = new List<string>() {"<Undefined>", "Belgian", "English"}
            };

            DateTime? dateOfBirthFrom = null;
            DateTime? dateOfBirthBefore = null;
            if (par.AgeFrom != null && par.AgeBefore != null)
            {
                dateOfBirthFrom = DateTime.Now - TimeSpan.FromDays(365.25 * (int)par.AgeBefore);
                dateOfBirthBefore = DateTime.Now - TimeSpan.FromDays(365.25 * (int)par.AgeFrom);
            }

            var res = from person in db.Persons
                      join n in db.Nationalities on person.Nationality equals n.id
                      join s in db.Nationalities on person.Sex equals s.id
                      where (par.Nationality == null || par.Nationality.Count() == 0
                                    || par.Nationality.Contains(n.Alias) &&
                            (par.FirstName == null || person.FirstName == par.FirstName) &&
                            (par.LastName == null || person.LastName == par.LastName) &&
                            (par.Sex == null || s.Alias == par.Sex) &&
                            (dateOfBirthFrom == null ||
                                    (person.DateOfBirth > dateOfBirthFrom && person.DateOfBirth < dateOfBirthBefore)) &&
                            (par.WeightFrom == null || par.WeightBefore == null || (person.Weight > par.WeightFrom && person.Weight < par.WeightBefore)) &&
                            (par.HeightFrom == null || par.HeightBefore == null || (person.Height > par.HeightFrom && person.Height < par.HeightBefore)) &&
                            (par.FreeText == null || person.Text.Contains(par.FreeText))
                      )
                      select person;

            return res;
        }

        private void LinqToSqlTestCe(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlCeConnection(@"Data Source=D:\Dima\Job\Find\Crossover.CShapr\National_Crimanals_Database\Visial studio project\NcServiceSearch.Tests\NcServiceSearch.TestData.sdf"))
            {
                var db = new NationalCriminalDatabaseDataContext(conn);
                var person = db.Persons.First();
            }
            
        }
    }
}
