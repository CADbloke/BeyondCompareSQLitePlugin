﻿using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace BeyondCompareSQLitePlugin.Test
{
    public class TestBase
    {
        internal const string SampleSqlite = "Chinook_Sqlite.sqlite";
        internal const string SampleSqliteSecond = "Chinook_Sqlite_second.sqlite";

        internal string SampleSqlitePath => Path.Combine(TestContext.CurrentContext.TestDirectory, SampleSqlite);

        internal string SampleSqliteSecondPath =>
            Path.Combine(TestContext.CurrentContext.TestDirectory, SampleSqliteSecond);

        [OneTimeSetUp]
        public void Setup()
        {
            WriteSqlLiteDatabaseToDisk(SampleSqlite);
            WriteSqlLiteDatabaseToDisk(SampleSqliteSecond);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DeleteFromDisk(SampleSqlite);
            DeleteFromDisk(SampleSqliteSecond);
        }

        public string TestFile1 => Path.Combine(TestContext.CurrentContext.TestDirectory, "testfile_1");

        [TearDown]
        public void TearDown()
        {
            if(File.Exists(TestFile1))
                File.Delete(TestFile1);
        }

        private void DeleteFromDisk(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);
        }

        private static void WriteSqlLiteDatabaseToDisk(string filename)
        {
            var name = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .FirstOrDefault(x => x.EndsWith(filename));
            using (Stream input = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            using (Stream output = File.Create(Path.Combine(TestContext.CurrentContext.TestDirectory, filename)))
            {
                CopyStream(input, output);
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            // Insert null checking here for production
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
    }
}