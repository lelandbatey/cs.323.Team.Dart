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
        public void takeAim_CheckAllMembersPresent() {
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
        public void takeAim_TestShootEnemies() {
            // Checks that takeAim and find return the same objects
            string targPath = "validTargets.ini";

            Target.TargetManager tMan = Target.TargetManager.Instance;
            tMan.load(GetTestData.getTestFilePath(targPath));
            string[] enemyNames = new string[] { "target2", "ronni", "patrick", "elnora", "mai" }; // A list of all the targets in the file who have "Friend=False" as their attributes

            foreach (var item in enemyNames) {
                Assert.AreEqual(tMan.find(item)[0], tMan.takeAim(item));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void takeAim_TestShootFriend() {
            // Checks that attempting to shoot a friend will raise an error
            string targPath = "validTargets.ini";

            Target.TargetManager tMan = Target.TargetManager.Instance;
            tMan.load(GetTestData.getTestFilePath(targPath));
            string[] friendNames = new string[] { "Lionel" };

            foreach (var item in friendNames) {
                tMan.takeAim(item);
            }
        }

        [TestMethod]
        public void Target_TestConverion() {
            string targPath = "validTargets.ini";

            Target.TargetManager tMan = Target.TargetManager.Instance;
            tMan.load(GetTestData.getTestFilePath(targPath));

            Target.Target j1 = tMan.find("mai")[0];
            Target.mutableTarget m1 = new Target.mutableTarget(j1);
            Target.Target j2 = new Target.Target(m1);
            Target.mutableTarget m2 = new Target.mutableTarget(j2);

            Console.WriteLine(j1);
            Console.WriteLine(j2);
            Console.WriteLine();
            Console.WriteLine(m1);
            Console.WriteLine(m2);

            Assert.AreEqual(j1.ToString(), j2.ToString());
            Assert.AreEqual(m1.ToString(), m2.ToString());
        }

    }
}
