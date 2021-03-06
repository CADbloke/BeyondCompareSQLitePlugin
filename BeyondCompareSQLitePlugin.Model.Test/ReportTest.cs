﻿using System.IO;
using NUnit.Framework;

namespace BeyondCompareSQLitePlugin.Model.Test
{
    [TestFixture]
    public class ReportTest : TestBase
    {
        [Test]
        [TestCase(SampleSqlite, 15_688)]
        [TestCase(SampleSqliteSecond, 15_689)]
        [TestCase(EmptySqlite, 15)]
        [TestCase(SampleSqliteEscapeNeeded, 15_688)]
        public void CreateTextReport_String(string name, int length)
        {
            #region Arrange

            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, name);

            #endregion

            #region Act

            var databaseContent = DbContext.GetTableContent(path);
            var stringContent = Report.CreateTextReport(databaseContent);

            #endregion

            #region Assert

            Assert.That(stringContent, Has.Length.EqualTo(length));

            #endregion
        }

        [Test]
        [TestCase(SampleSqlite, 1_939_768)]
        [TestCase(SampleSqliteSecond, 1_933_376)]
        [TestCase(EmptySqlite, 871)]
        [TestCase(SampleSqliteEscapeNeeded, 1_940_165)]
        public void CreateTextReport_File(string name, int length)
        {
            #region Arrange

            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, name);

            #endregion

            #region Act

            var databaseContent = DbContext.GetTableContent(path);
            Report.CreateTextReport(databaseContent, TestFile1);

            #endregion

            #region Assert

            Assert.That(new FileInfo(TestFile1), Has.Length.EqualTo(length));

            #endregion
        }
    }
}
