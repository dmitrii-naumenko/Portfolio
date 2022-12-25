using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database_UnitTests
{
    [TestClass()]
    public class sp_Search_FTS : SqlDatabaseTestClass
    {

        public sp_Search_FTS()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Search_FTSTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sp_Search_FTS));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Search_FTSTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Search_FTSTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition checksumCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            this.dbo_Search_FTSTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_Search_FTSTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Search_FTSTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Search_FTSTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            checksumCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
            scalarValueCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // dbo_Search_FTSTest_TestAction
            // 
            dbo_Search_FTSTest_TestAction.Conditions.Add(checksumCondition1);
            resources.ApplyResources(dbo_Search_FTSTest_TestAction, "dbo_Search_FTSTest_TestAction");
            // 
            // dbo_Search_FTSTest_PretestAction
            // 
            dbo_Search_FTSTest_PretestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(dbo_Search_FTSTest_PretestAction, "dbo_Search_FTSTest_PretestAction");
            // 
            // dbo_Search_FTSTest_PosttestAction
            // 
            resources.ApplyResources(dbo_Search_FTSTest_PosttestAction, "dbo_Search_FTSTest_PosttestAction");
            // 
            // dbo_Search_FTSTestData
            // 
            this.dbo_Search_FTSTestData.PosttestAction = dbo_Search_FTSTest_PosttestAction;
            this.dbo_Search_FTSTestData.PretestAction = dbo_Search_FTSTest_PretestAction;
            this.dbo_Search_FTSTestData.TestAction = dbo_Search_FTSTest_TestAction;
            // 
            // checksumCondition1
            // 
            checksumCondition1.Checksum = "1431674025";
            checksumCondition1.Enabled = true;
            checksumCondition1.Name = "checksumCondition1";
            // 
            // scalarValueCondition1
            // 
            scalarValueCondition1.ColumnNumber = 2;
            scalarValueCondition1.Enabled = false;
            scalarValueCondition1.ExpectedValue = "Idle";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion


        [TestMethod()]
        public void dbo_Search_FTSTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_Search_FTSTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        private SqlDatabaseTestActions dbo_Search_FTSTestData;
    }
}
