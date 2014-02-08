using System;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SadCL_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1() {
        }
    }
    [TestClass]
    public class IniTargetFactoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameTest() {
            string testName = "Odd with spaces!";
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniTargetFactory testFact = new Target.IniTargetFactory();

            testFact.setName(testName, testTarg);

        }

        [TestMethod]
        public void toStringTest() {
            Target.mutableTarget testTarg = new Target.mutableTarget();
            Target.IniTargetFactory testFact = new Target.IniTargetFactory();

            Console.WriteLine(testTarg);
            //Console.WriteLine("Testing?");
            //Debug.WriteLine(testTarg);
            //Debug.WriteLine("Testing?");
        }
    }
    
}
