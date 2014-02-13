using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SadCL_UnitTests
{
    /// <summary>
    /// Tests the 'TargetFactory' class.
    /// </summary>
    [TestClass]
    public class TestTargetFactory
    {
        public TestTargetFactory() {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

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
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        ///  Used to find the absolute path to our folder full of test-data.
        /// </summary>
        /// <returns>string constituting full path to folder of test-data.</returns>
        public static string getTestDataLocation() {
            string currentDir = Directory.GetCurrentDirectory();
            string originalDir = currentDir;
            bool doneFlag = false;
            string dirName = "";

            // Walk up the directory tree till we get to a directory we know, then move down to the directory we want.
            while (!doneFlag) {
                Directory.SetCurrentDirectory("..");
                currentDir = Directory.GetCurrentDirectory();
                dirName = currentDir.Split(Path.DirectorySeparatorChar).Last();
                if (dirName == @"SadCL_Main") {
                    doneFlag = true;
                }
                if (currentDir == @"C:\") {
                    doneFlag = true;
                }
            }

            Directory.SetCurrentDirectory("./SadCL_UnitTests/TestData/");
            currentDir = Path.GetFullPath(Directory.GetCurrentDirectory());
            Directory.SetCurrentDirectory(originalDir);
            return currentDir;

        }

        public static List<string> getTestPaths() {
            // Setting up the variable names
            string testFolder = getTestDataLocation();
            string fileName = "testPaths.txt";
            string path = Path.Combine(testFolder, fileName);
            string line = "";
            StreamReader myFile = new StreamReader(@path);
            List<string> testPaths = new List<string>();


            // We begin reading the file.
            while ((line = myFile.ReadLine()) != null) {
                testPaths.Add(line);
            }

            return testPaths;
        }

    }
}
