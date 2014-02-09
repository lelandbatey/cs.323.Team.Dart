using System;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SadCL_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestMethod1() {
        //}
    }
    [TestClass]
    public class IniTargetFactoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameTest() {
            string testName = "Odd with spaces!";
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniBuilder testFact = new Target.IniBuilder();

            testFact.setName(testName, testTarg);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PointsSetTest() {
            string testPoints = "2  df";
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniBuilder testFact = new Target.IniBuilder();

            testFact.setPoints(testPoints, testTarg);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XSetTest() {
            string testX = "2.r";
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniBuilder testFact = new Target.IniBuilder();

            testFact.setX(testX, testTarg);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void YSetTest() {
            string testY = "4.2z";
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniBuilder testFact = new Target.IniBuilder();

            testFact.setY(testY, testTarg);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZSetTest() {
            string testZ = "1.9k";
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniBuilder testFact = new Target.IniBuilder();

            testFact.setZ(testZ, testTarg);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FriendSetTest() {
            string testFriend = "trooo";
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniBuilder testFact = new Target.IniBuilder();

            testFact.setFlash(testFriend, testTarg);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FlashSetTest() {
            string testFlashRate = "     ";
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniBuilder testFact = new Target.IniBuilder();

            testFact.setFlash(testFlashRate, testTarg);
        }

        [TestMethod]
        public void toStringTest() {
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniBuilder testFact = new Target.IniBuilder();

            Console.WriteLine(testTarg);
        }


    }
    
}
