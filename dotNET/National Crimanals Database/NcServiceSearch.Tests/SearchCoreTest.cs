using System;
using System.Collections.Generic;
using System.Fakes;
using System.Globalization.Fakes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NationalCriminalDatabase.Fakes;

namespace NationalCriminalDatabase.Tests
{
    [TestClass]
    public class SearchCoreTest
    {
        [TestMethod]
        public void GetDataTest()
        {
            var fakeDb = GetFakeDb();

            var par = new SearchParameter
            {
                AgeFrom = 40,
                AgeBefore = 60,
                Nationality = new List<string> {"<Undefined>", "Belgian", "English"}
            };

            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2016, 7, 22);

                // MSTest: 350 ms
                ShimSearchCore.AllInstances.GetDb = (@this) => fakeDb;
                var core = new SearchCore();

                // Moq: 1 sec
                //var core = new Mock<SearchCore>();
                //core.Setup(m => m.GetDb()).Returns(fakeDb);

                // override: 330 ms 
                //var core = new TestSearchCore(fakeDb);

                var result = core.GetData(par).ToList();

                Assert.AreEqual(4, result.Count, "Count of result must be 4");
                Assert.IsTrue(result.Exists(p => p.id == 3), "Must exist id = 3");
                Assert.IsTrue(result.Exists(p => p.id == 7), "Must exist id = 7");
                Assert.IsTrue(result.Exists(p => p.id == 8), "Must exist id = 8");
                Assert.IsTrue(result.Exists(p => p.id == 12), "Must exist id = 12");
            }
        }

        [TestMethod]
        public void GetDataTest2()
        {
            var fakeDb = GetFakeDb();

            var par = new SearchParameter
            {
                FirstName = "David"
            };

            using (ShimsContext.Create())
            {
                ShimSearchCore.AllInstances.GetDb = (@this) => fakeDb;
                var core = new SearchCore();

                var result = core.GetData(par).ToList();

                Assert.AreEqual(3, result.Count);
            }
        }

        [TestMethod]
        public void CreatePdfTest()
        {

            var core = new SearchCore();

            var person = new Person
            {
                id = 7,
                FirstName = "David",
                LastName = "James",
                DateOfBirth = new DateTime(1971, 1, 1),
                Sex = 2,
                Sex1 = new Sex() {Alias = "Female"},
                Weight = 60,
                Nationality = 4,
                Nationality1 = new Nationality() {Alias = "Indian"},
                Text = @"Just for example! I was born on the 3rd of June 1991 in Moscow... 
Just for example! I was born on the 3rd of June 1991 in Moscow... 
Just for example! I was born on the 3rd of June 1991 in Moscow... 
Just for example! I was born on the 3rd of June 1991 in Moscow... 
Just for example! I was born on the 3rd of June 1991 in Moscow... "
            };

            var arr = core.CreatePdf(person).ToArray();
            var hash = MD5.Create().ComputeHash(arr, 0, arr.Length - 378); // note: another variant to use SHA hash generator

            //File.WriteAllBytes(@"d:\3.pdf", arr);

            var actual = hash.Aggregate("", (sum, b) => sum + b.ToString("X2"));

            var expected = "44226D94967109C8115993FDF24BFA25";

            Assert.AreEqual(expected, actual, "Hash code of generated pdf not equal expected");
        }

        [TestMethod]
        public void ExecuteTaskTest()
        {
            using (ShimsContext.Create())
            {
                string log = "";
                var files = new List<string>();
                Fakes.ShimSearchCore.AllInstances.SendMailStringInt32StringStringStringStringStringDictionaryOfStringStream =
                    (@this, smtpServer, port, from, password, mailto, caption, message, attach) =>
                    {
                        Assert.IsNotNull(attach, "Mail attachments must be not null");
                        log += string.Format("Email: mailto={0}: caption={1}, message={2}\n",
                            mailto, caption, message);
                        files.AddRange(attach.Select(att => att.Key +", "+ (new StreamReader(att.Value)).ReadToEnd()));
                    };
                Fakes.ShimSearchCore.AllInstances.GetDataSearchParameter = (@this, par) => new List<Person>()
                {
                    new Person() {id = 1, FirstName = "Name_1"},
                    new Person() {id = 2, FirstName = "Name_2"},
                    new Person() {id = 3, FirstName = "Name_3"},
                    new Person() {id = 4, FirstName = "Name_4"},
                    new Person() {id = 5, FirstName = "Name_5"},
                    new Person() {id = 6, FirstName = "Name_6"},
                    new Person() {id = 7, FirstName = "Name_7"},
                    new Person() {id = 8, FirstName = "Name_8"},
                    new Person() {id = 9, FirstName = "Name_9"},
                    new Person() {id = 10, FirstName = "Name_10"},
                    new Person() {id = 11, FirstName = "Name_11"},
                    new Person() {id = 12, FirstName = "Name_12"},
                    new Person() {id = 13, FirstName = "Name_13"},
                    new Person() {id = 14, FirstName = "Name_14"},
                    new Person() {id = 15, FirstName = "Name_15"},
                }.AsQueryable();
                Fakes.ShimSearchCore.AllInstances.CreatePdfPerson = (@this, p) => Encoding.ASCII.GetBytes(p.FirstName);

                var maxResultsCount = 12;
                var task = new SearchTask()
                {
                    Search = new SearchParameter() {FirstName = "name"},
                    Email = "1@1.1",
                    MaxResultCount = maxResultsCount
                };
                var core = new SearchCore();

                core.ExecuteTask(task);

                var expected = "Email: mailto=1@1.1: caption=Search results (1 from 2), message=See attachments\nEmail: mailto=1@1.1: caption=Search results (2 from 2), message=See attachments\n";

                Assert.AreEqual(expected, log);

                Assert.AreEqual(12, files.Count, "Must be 12 files");

                Assert.IsTrue(files.Exists(f => f == "1.pdf, Name_1"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "2.pdf, Name_2"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "3.pdf, Name_3"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "4.pdf, Name_4"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "5.pdf, Name_5"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "6.pdf, Name_6"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "7.pdf, Name_7"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "8.pdf, Name_8"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "9.pdf, Name_9"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "10.pdf, Name_10"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "11.pdf, Name_11"), "No expected file");
                Assert.IsTrue(files.Exists(f => f == "12.pdf, Name_12"), "No expected file");

            }
        }


        [TestMethod]
        public void ExecuteTaskTestNonResults()
        {
            using (ShimsContext.Create())
            {
                string log = "";
                var files = new List<string>();
                Fakes.ShimSearchCore.AllInstances.SendMailStringInt32StringStringStringStringStringDictionaryOfStringStream =
                    (@this, smtpServer, port, from, password, mailto, caption, message, attach) =>
                    {
                        log += string.Format("Email: mailto={0}: caption={1}, message={2}\n",
                            mailto, caption, message);
                        files.AddRange(attach.Select(att => att.Key + ", " + (new StreamReader(att.Value)).ReadToEnd()));
                    };
                Fakes.ShimSearchCore.AllInstances.GetDataSearchParameter = (@this, par) => new List<Person>().AsQueryable();
                Fakes.ShimSearchCore.AllInstances.CreatePdfPerson = (@this, p) => Encoding.ASCII.GetBytes(p.FirstName);

                var maxResultsCount = 12;
                var task = new SearchTask()
                {
                    Search = new SearchParameter() { FirstName = "name" },
                    Email = "1@1.1",
                    MaxResultCount = maxResultsCount
                };
                var core = new SearchCore();

                core.ExecuteTask(task);

                var expected = "Email: mailto=1@1.1: caption=Search results, message=Nothing found\n";

                Assert.AreEqual(expected, log);

                Assert.AreEqual(0, files.Count, "Must be 0 files");

            }
        }





        private static FakeDb GetFakeDb()
        {
            var sexes = new List<Sex>
            {
                new Sex {id = 1, Alias = "Man"},
                new Sex {id = 2, Alias = "Female"}
            };

            var nationalities = new List<Nationality>
            {
                new Nationality {id = 1, Alias = "<Undefined>"},
                new Nationality {id = 2, Alias = "Argentinian"},
                new Nationality {id = 3, Alias = "Australian"},
                new Nationality {id = 4, Alias = "Belgian"},
                new Nationality {id = 5, Alias = "Canadian"},
                new Nationality {id = 6, Alias = "Chinese"},
                new Nationality {id = 7, Alias = "Danish"},
                new Nationality {id = 8, Alias = "English"},
                new Nationality {id = 9, Alias = "French"},
                new Nationality {id = 10, Alias = "German"},
                new Nationality {id = 11, Alias = "Dutch"},
                new Nationality {id = 12, Alias = "Indian"},
                new Nationality {id = 13, Alias = "Japanese"},
                new Nationality {id = 14, Alias = "Mexican"},
                new Nationality {id = 15, Alias = "Norwegian"},
                new Nationality {id = 16, Alias = "Polish"},
                new Nationality {id = 17, Alias = "Russian"},
                new Nationality {id = 18, Alias = "Singaporean"},
                new Nationality {id = 19, Alias = "Thai"},
                new Nationality {id = 20, Alias = "US"},
                new Nationality {id = 20, Alias = "Uzbek"}
            };

            var persons = new List<Person>
            {
                new Person
                {
                    id = 3,
                    FirstName = "Michael",
                    LastName = "Adamson",
                    Sex = 1,
                    DateOfBirth = new DateTime(1972, 1, 1),
                    Nationality = 1
                },
                new Person {id = 4, FirstName = "Emily", LastName = "Adamson", Sex = 2, Nationality = 1},
                new Person {id = 6, FirstName = "Olivia", LastName = "Holiday", Sex = 2, Nationality = 4},
                new Person
                {
                    id = 7,
                    FirstName = "David",
                    LastName = "James",
                    DateOfBirth = new DateTime(1971, 1, 1),
                    Sex = 2,
                    Weight = 60,
                    Nationality = 4,
                    Text = "Just for example! I was born on the 3rd of June 1991 in Moscow"
                },
                new Person
                {
                    id = 8,
                    FirstName = "Daniel",
                    Sex = 1,
                    DateOfBirth = new DateTime(1972, 1, 1),
                    Weight = 65,
                    Nationality = 4
                },
                new Person
                {
                    id = 9,
                    FirstName = "Joseph",
                    Sex = 1,
                    DateOfBirth = new DateTime(1973, 1, 1),
                    Weight = 70,
                    Nationality = 6
                },
                new Person
                {
                    id = 10,
                    FirstName = "Ryan",
                    Sex = 1,
                    Height = 168,
                    DateOfBirth = new DateTime(1974, 1, 1),
                    Weight = 75,
                    Nationality = 6
                },
                new Person
                {
                    id = 11,
                    FirstName = "Tyler",
                    Sex = 1,
                    Height = 169,
                    DateOfBirth = new DateTime(1975, 1, 1),
                    Weight = 80,
                    Nationality = 6
                },
                new Person
                {
                    id = 12,
                    FirstName = "John",
                    Sex = 1,
                    Height = 170,
                    DateOfBirth = new DateTime(1976, 1, 1),
                    Weight = 85,
                    Nationality = 8
                },
                new Person
                {
                    id = 14,
                    FirstName = "Logan",
                    Sex = 1,
                    Height = 171,
                    DateOfBirth = new DateTime(1977, 1, 1),
                    Weight = 90,
                    Nationality = 8,
                    Text = "'Just for example! My name is . I would like to tell you about my s"
                },
                new Person
                {
                    id = 16,
                    FirstName = "Jose",
                    Sex = 1,
                    Height = 172,
                    DateOfBirth = new DateTime(1978, 1, 1),
                    Weight = 95,
                    Nationality = 8
                },
                new Person
                {
                    id = 17,
                    FirstName = "David",
                    Sex = 1,
                    Height = 173,
                    DateOfBirth = new DateTime(1979, 1, 1),
                    Nationality = 20
                },
                new Person
                {
                    id = 18,
                    FirstName = "Luke",
                    Sex = 1,
                    Height = 173,
                    DateOfBirth = new DateTime(1980, 1, 1),
                    Nationality = 20
                },
                new Person {id = 19, FirstName = "Cameron", Sex = 1, Nationality = 2},
                new Person {id = 20, FirstName = "Adam", Sex = 1, Nationality = 1}
            };

            foreach (var person in persons)
            {
                person.Sex1 = sexes[person.Sex - 1];
                person.Nationality1 = nationalities[person.Nationality - 1];
            }

            var fakeDb = new FakeDb
            {
                Sexes = sexes.AsQueryable(),
                Nationalities = nationalities.AsQueryable(),
                Persons = persons.AsQueryable()
            };
            return fakeDb;
        }

        public class FakeDb : SearchCore.IDb
        {
            public IQueryable<Person> Persons { get; set; }
            public IQueryable<Nationality> Nationalities { get; set; }
            public IQueryable<Sex> Sexes { get; set; }
        }

        public class TestSearchCore : SearchCore
        {
            private readonly IDb dbContex;

            public TestSearchCore(IDb db)
            {
                dbContex = db;
            }

            internal override IDb GetDb()
            {
                return dbContex;
            }
        }
    }
}