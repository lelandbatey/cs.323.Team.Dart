//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SadCL.MissileLauncher;

//namespace SadCL_UnitTests
//{
//	[TestClass]
//	public class TestMissileController
//	{
//		[TestMethod]
//		//Does Phi or Theta ever move to Phi / Theta beyond threshhold?
//		public void MoveByTest(){
//			MissileLauncherController testMan = new MissileLauncherController();

//			double initialPhi = 3000.0;
//			double initialTheta = 0.0;

//			double expectedPhi = 4567.89;
//			double expectedTheta = 123.45;

//			Assert.AreEqual(initialPhi, testMan.currentPhi);
//			Assert.AreEqual(initialTheta, testMan.currentTheta);

//			double testPhi = 1567.89;
//			double testTheta = 123.45;

//			testMan.moveBy(testPhi, testTheta);

//			double actualPhi = testMan.currentPhi;
//			double actualTheta = testMan.currentTheta;

//			Assert.AreEqual(expectedPhi, actualPhi);
//			Assert.AreEqual(expectedTheta, actualTheta);
//		}

//		[TestMethod]
//		//Does Phi or Theta ever move to Phi / Theta beyond threshhold?
//		public void MoveToTest() {
//			MissileLauncherController testMan = new MissileLauncherController();

//			double initialPhi = 3000.0;
//			double initialTheta = 0.0;

//			double expectedPhi = 1567.89;
//			double expectedTheta = 123.45;

//			Assert.AreEqual(initialPhi, testMan.currentPhi);
//			Assert.AreEqual(initialTheta, testMan.currentTheta);

//			double testPhi = 1567.89;
//			double testTheta = 123.45;

//			testMan.move(testPhi, testTheta);

//			double actualPhi = testMan.currentPhi;
//			double actualTheta = testMan.currentTheta;

//			Assert.AreEqual(expectedPhi, actualPhi);
//			Assert.AreEqual(expectedTheta, actualTheta);
//		}
//	}
//}
