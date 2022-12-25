using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NationalCriminalDatabase.Tests
{
    [TestClass]
    public class SearchParametrTest
    {
        [TestMethod]
        public void ToStringTest_FirstName()
        {
            var par = new SearchParameter() { FirstName = "Test" };
            Assert.AreEqual("FirstName = Test", par.ToString());
        }

        [TestMethod]
        public void ToStringTest_Nationality()
        {
            var par = new SearchParameter() { Nationality = new []{"One, Two"}};
            Assert.AreEqual("Nationality = {One, Two}", par.ToString());
        }

        [TestMethod]
        public void ToStringTest_Full()
        {
            var par = new SearchParameter()
            {
                FirstName = "1",
                LastName = "2",
                Sex = "3",
                AgeFrom = 4,
                AgeBefore = 5,
                HeightFrom = 6,
                HeightBefore = 7,
                WeightFrom = 8,
                WeightBefore = 9,
                FreeText = "10"
            };

            var exspected = "FirstName = 1, LastName = 2, Sex = 3, AgeFrom = 4, AgeBefore = 5, HeightFrom = 6, HeightBefore = 7, WeightFrom = 8, WeightBefore = 9, FreeText = 10";

            Assert.AreEqual(exspected, par.ToString());
        }
    }
}
