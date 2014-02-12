using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SadCL_UnitTests
{
    class GetTestData
    {
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
                    throw new InvalidOperationException("Moved out to the base directory. Very bad.");
                }
            }

            Directory.SetCurrentDirectory("./SadCL_UnitTests/TestData/");
            currentDir = Path.GetFullPath(Directory.GetCurrentDirectory());
            Directory.SetCurrentDirectory(originalDir);
            return currentDir;

        }

        // Given a the name of a test file in the "TestData" folder, returns a StreamReader object of that file
        public static StreamReader getTestFile(string name) {
            string path = Path.Combine(getTestDataLocation(), name);
            StreamReader myFile= new StreamReader(@path);
            return myFile;
        }

        // Convenience method to get the full string representation of the path to any file in the "TestData" folder
        public static string getTestFilePath(string name) {
            return Path.Combine(getTestDataLocation(), name);
        }

        // Method returns list of strings composing all the various paths in "testPaths.txt" in the "TestData" folder
        public static List<string> getAssortedTestPaths() {
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
