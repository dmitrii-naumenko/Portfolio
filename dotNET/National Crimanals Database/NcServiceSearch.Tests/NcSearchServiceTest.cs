using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NationalCriminalDatabase.Tests
{
    [TestClass]
    public class NcSearchServiceTest
    {

        [TestMethod]
        public void SearchTest_ValidateParametrNullFileds()
        {
            var srv = new NcSearchService();
            var par = new SearchParameter();
            Assert.IsFalse(srv.Search(par, "1@1.ru", 10), "Must return false if all search filds is null");

            par.Nationality = new string[0];
            Assert.IsFalse(srv.Search(par, "1@1.ru", 10), "Must return false if all search filds is null and Nationality is emmpty");
        }

        [TestMethod]
        public void SearchTest_ValidateNormal()
        {
            var srv = new NcSearchService();
            var par = new SearchParameter() { FirstName = "test" };
            Assert.IsTrue(srv.Search(par, "1@1.ru", 10), "SearchParameter = [{0}]", par);
        }

        [TestMethod]
        public void SearchTest_ValidateEmail()
        {
            var srv = new NcSearchService();
            var par = new SearchParameter() { FirstName = "test" };
            Assert.IsFalse(srv.Search(par, "11.ru", 10), "SearchParameter = [{0}]", par);
        }

        [TestMethod]
        public void SearchTest_ValidateMaxResultCount()
        {
            var srv = new NcSearchService();
            var par = new SearchParameter() { FirstName = "test" };
            var result_count = 1000;
            Assert.IsFalse(srv.Search(par, "11.ru", result_count), "MaxResultCount = {0}", result_count);
        }

        [TestMethod]
        public void SearchTest_ValidateAge()
        {
            const int min = 0;
            const int max = 120;
            SearchParameter par;

            var srv = new NcSearchService();

            par = new SearchParameter() { FirstName = "test", AgeFrom = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", AgeBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", AgeFrom = min + 2, AgeBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", AgeFrom = min - 1, AgeBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", AgeFrom = min + 1, AgeBefore = max + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", AgeFrom = min + 1, AgeBefore = min + 2 };
            Assert.IsTrue(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

        }


        [TestMethod]
        public void SearchTest_ValidateHeigth()
        {
            const int min = 100;
            const int max = 220;
            SearchParameter par;

            var srv = new NcSearchService();

            par = new SearchParameter() { FirstName = "test", HeightFrom = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", HeightBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", HeightFrom = min + 2, HeightBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", HeightFrom = min - 1, HeightBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", HeightFrom = min + 1, HeightBefore = max + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", HeightFrom = min + 1, HeightBefore = min + 2 };
            Assert.IsTrue(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

        }


        [TestMethod]
        public void SearchTest_ValidateWeigth()
        {
            const int min = 30;
            const int max = 150;
            SearchParameter par;

            var srv = new NcSearchService();

            par = new SearchParameter() { FirstName = "test", WeightFrom = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", WeightBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", WeightFrom = min + 2, WeightBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", WeightFrom = min - 1, WeightBefore = min + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", WeightFrom = min + 1, WeightBefore = max + 1 };
            Assert.IsFalse(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

            par = new SearchParameter() { FirstName = "test", WeightFrom = min + 1, WeightBefore = min + 2 };
            Assert.IsTrue(srv.Search(par, "1@1.ru", 100), "SearchParameter = [{0}]", par);

        }

    }
}