using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SadCL_UnitTests
{
    /// <summary>
    /// Summary description for TestTargetManager
    /// </summary>
    [TestClass]
    public class TestTargetManager
    {
        public TestTargetManager() {
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

        [TestMethod]
        public void findPrey_CheckAllMembersPresent() {
            // Tests to see if all the proper named targets are found via TargetManager

            string targPath = "validTargets.ini";
            
            Target.TargetManager tMan = Target.TargetManager.Instance;
            tMan.load(GetTestData.getTestFilePath(targPath));

            string[] targNames = new string[] { "target2", "ronni", "patrick", "lionel", "elnora", "mai" }; // All these are names of targets that are in fact in the file

            foreach (var item in targNames) {
                Assert.IsNotNull(tMan.find(item).Count);
            }
        }

        [TestMethod]
        public void findPrey_TestShootEnemies() {
            // Checks that findPrey and find return the same objects
            string targPath = "validTargets.ini";

            Target.TargetManager tMan = Target.TargetManager.Instance;
            tMan.load(GetTestData.getTestFilePath(targPath));
            string[] enemyNames = new string[] { "target2", "ronni", "patrick", "elnora", "mai" }; // A list of all the targets in the file who have "Friend=False" as their attributes

            foreach (var item in enemyNames) {
                Assert.AreEqual(tMan.find(item)[0], tMan.findPrey(item));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void findPrey_TestShootFriend() {
            // Checks that attempting to shoot a friend will raise an error
            string targPath = "validTargets.ini";

            Target.TargetManager tMan = Target.TargetManager.Instance;
            tMan.load(GetTestData.getTestFilePath(targPath));
            string[] friendNames = new string[] { "Lionel" };

            foreach (var item in friendNames) {
                tMan.findPrey(item);
            }
        }


        [TestMethod]
        public void takeAim_TestReturnData() {
            // Basic sanity check that the data being returned is the correct data, and is in the correct order (X, Y, Z)
            string targPath = "validTargets.ini";

            Target.TargetManager tMan = Target.TargetManager.Instance;
            tMan.load(GetTestData.getTestFilePath(targPath));

            Tuple<double, double, double> testTuple = Tuple.Create<double, double, double>(9.51254531005, 0.617818836685, 6.72060384345);

            Assert.AreEqual(testTuple, tMan.takeAim("ronni"));

        }
    }
}
