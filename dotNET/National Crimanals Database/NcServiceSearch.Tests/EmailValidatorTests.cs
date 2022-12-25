using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalCriminalDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace NationalCriminalDatabase.Tests
{
    [TestClass()]
    public class EmailValidatorTests
    {
        [TestMethod()]
        public void IsValidEmailTest()
        {
            var obj = new EmailValidator();

            var emailAddresses = new Dictionary<string, bool>()
            {
                {"david.jones@proseware.com",       true},
                {"d.j@server1.proseware.com",       true},
                {"jones@ms1.proseware.com",         true},
                {"j.@server1.proseware.com",        false},
                {"j@proseware.com9",                true},
                {"js#internal@proseware.com",       true},
                {"j_9@[129.126.118.1]",             true},
                {"j..s@proseware.com",              false},
                {"js*@proseware.com",               false},
                {"js@proseware..com",               false},
                {"js@proseware.com9",               true},
                {"j.s@server1.proseware.com",       true},
                {"\"j\\\"s\\\"\"@proseware.com",    true},
                {"js@contoso.中国",                 true}
            };

            foreach (var emailAddress in emailAddresses)
            {
                Assert.AreEqual(obj.IsValidEmail(emailAddress.Key), emailAddress.Value,
                    "email \"{0}\" is not {1}", emailAddress.Key,
                    emailAddress.Value ? "valid" : "invalid");
            }
        }

        [TestMethod]
        [DeploymentItem("NcServiceSearch.TestData.sdf")]
        [DataSource(@"System.Data.SqlServerCe.4.0", @"Data Source=NcServiceSearch.TestData.sdf", "Emails", DataAccessMethod.Sequential)]
        public void IsValidEmailRowTest_sdf()
        {
            var email = TestContext.DataRow["Email"] as string;
            var expected = TestContext.DataRow["Valid"] as bool?;

            var actual = (new EmailValidator()).IsValidEmail(email);

            Assert.AreEqual(expected, actual,
                    "email \"{0}\" is not {1}", email, expected == true ? "valid" : "invalid");
        }

        public TestContext TestContext { get; set; }
    }
}
