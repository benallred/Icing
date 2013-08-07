using System;
using System.Linq;
using System.Reflection;

using Icing.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.TestFramework
{
	[TestClass]
	[JustATest_TestClass]
	public class TestOf_SimpleTestFramework
	{
		[TestMethod]
		public void RunTestsInAssembly_Passed()
		{
			SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_PassedAttribute, JustATest_Exception> simpleTestFramework
				= new SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_PassedAttribute, JustATest_Exception>();

			TestAssemblyResults testAssemblyResults = simpleTestFramework.RunTestsInAssembly(Assembly.GetExecutingAssembly());

			Assert.AreEqual(TestResultStatus.Passed, testAssemblyResults.Status);
			Assert.AreEqual(                      1, testAssemblyResults.TestClassResults.Count());

			TestClassResults testClassResults = testAssemblyResults.TestClassResults.Single();
			Assert.AreEqual(TestResultStatus.Passed     , testClassResults.Status);
			Assert.AreEqual("TestOf_SimpleTestFramework", testClassResults.ClassName);
			Assert.AreEqual(                           1, testClassResults.TestMethodResults.Count());

			TestMethodResult testMethodResult = testClassResults.TestMethodResults.Single();
			Assert.AreEqual(TestResultStatus.Passed, testMethodResult.Status);
			Assert.AreEqual("JustATest_Passed"     , testMethodResult.MethodName);
			Assert.IsNull  (                         testMethodResult.Exception);
		}

		[TestMethod]
		public void RunTestsInAssembly_Failed()
		{
			SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_FailedAttribute, JustATest_Exception> simpleTestFramework
				= new SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_FailedAttribute, JustATest_Exception>();

			TestAssemblyResults testAssemblyResults = simpleTestFramework.RunTestsInAssembly(Assembly.GetExecutingAssembly());

			Assert.AreEqual(TestResultStatus.Failed, testAssemblyResults.Status);
			Assert.AreEqual(                      1, testAssemblyResults.TestClassResults.Count());

			TestClassResults testClassResults = testAssemblyResults.TestClassResults.Single();
			Assert.AreEqual(TestResultStatus.Failed     , testClassResults.Status);
			Assert.AreEqual("TestOf_SimpleTestFramework", testClassResults.ClassName);
			Assert.AreEqual(                           1, testClassResults.TestMethodResults.Count());

			TestMethodResult testMethodResult = testClassResults.TestMethodResults.Single();
			Assert.AreEqual (TestResultStatus.Failed, testMethodResult.Status);
			Assert.AreEqual ("JustATest_Failed"     , testMethodResult.MethodName);
			Assert.IsNotNull(                         testMethodResult.Exception);
			Assert.IsInstanceOfType(testMethodResult.Exception, typeof(JustATest_Exception));
		}

		[TestMethod]
		public void RunTestsInAssembly_Inconclusive()
		{
			SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_InconclusiveAttribute, JustATest_Exception> simpleTestFramework
				= new SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_InconclusiveAttribute, JustATest_Exception>();

			TestAssemblyResults testAssemblyResults = simpleTestFramework.RunTestsInAssembly(Assembly.GetExecutingAssembly());

			Assert.AreEqual(TestResultStatus.Inconclusive, testAssemblyResults.Status);
			Assert.AreEqual(                            1, testAssemblyResults.TestClassResults.Count());

			TestClassResults testClassResults = testAssemblyResults.TestClassResults.Single();
			Assert.AreEqual(TestResultStatus.Inconclusive, testClassResults.Status);
			Assert.AreEqual("TestOf_SimpleTestFramework" , testClassResults.ClassName);
			Assert.AreEqual(                            1, testClassResults.TestMethodResults.Count());

			TestMethodResult testMethodResult = testClassResults.TestMethodResults.Single();
			Assert.AreEqual (TestResultStatus.Inconclusive, testMethodResult.Status);
			Assert.AreEqual ("JustATest_Inconclusive"     , testMethodResult.MethodName);
			Assert.IsNotNull(                               testMethodResult.Exception);
			Assert.IsInstanceOfType(testMethodResult.Exception, typeof(AssertFailedException));
		}

		[TestMethod]
		public void RunTestsInAssembly_Inconclusive_BadParamCount()
		{
			SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_Inconclusive_BadParamCountAttribute, JustATest_Exception> simpleTestFramework
				= new SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_Inconclusive_BadParamCountAttribute, JustATest_Exception>();

			TestAssemblyResults testAssemblyResults = simpleTestFramework.RunTestsInAssembly(Assembly.GetExecutingAssembly());

			Assert.AreEqual(TestResultStatus.Inconclusive, testAssemblyResults.Status);
			Assert.AreEqual(                            1, testAssemblyResults.TestClassResults.Count());

			TestClassResults testClassResults = testAssemblyResults.TestClassResults.Single();
			Assert.AreEqual(TestResultStatus.Inconclusive, testClassResults.Status);
			Assert.AreEqual("TestOf_SimpleTestFramework" , testClassResults.ClassName);
			Assert.AreEqual(                            1, testClassResults.TestMethodResults.Count());

			TestMethodResult testMethodResult = testClassResults.TestMethodResults.Single();
			Assert.AreEqual (TestResultStatus.Inconclusive         , testMethodResult.Status);
			Assert.AreEqual ("JustATest_Inconclusive_BadParamCount", testMethodResult.MethodName);
			Assert.IsNotNull(                                        testMethodResult.Exception);
			Assert.IsInstanceOfType(testMethodResult.Exception, typeof(TargetParameterCountException));
		}

		[TestMethod]
		public void RunTestsInAssembly_Events()
		{
			bool classStarted   = false;
			bool methodStarted  = false;
			bool methodFinished = false;
			bool classFinished  = false;
			
			SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_PassedAttribute, JustATest_Exception> simpleTestFramework
				= new SimpleTestFramework<JustATest_TestClassAttribute, JustATest_TestMethod_PassedAttribute, JustATest_Exception>();

			simpleTestFramework.TestClassStarted   += (sender, eventArgs) => { classStarted   = true; };
			simpleTestFramework.TestMethodStarted  += (sender, eventArgs) => { methodStarted  = true; };
			simpleTestFramework.TestMethodFinished += (sender, eventArgs) => { methodFinished = true; };
			simpleTestFramework.TestClassFinished  += (sender, eventArgs) => { classFinished  = true; };

			simpleTestFramework.RunTestsInAssembly(Assembly.GetExecutingAssembly());

			Assert.IsTrue(classStarted  , "TestClassStarted"   + " was never invoked");
			Assert.IsTrue(methodStarted , "TestMethodStarted"  + " was never invoked");
			Assert.IsTrue(methodFinished, "TestMethodFinished" + " was never invoked");
			Assert.IsTrue(classFinished , "TestClassFinished"  + " was never invoked");
		}

		#region Helper Methods

		[JustATest_TestMethod_Passed]
		public void JustATest_Passed()
		{

		}

		[JustATest_TestMethod_Failed]
		public void JustATest_Failed()
		{
			throw new JustATest_Exception();
		}

		[JustATest_TestMethod_Inconclusive]
		public void JustATest_Inconclusive()
		{
			throw new AssertFailedException();
		}

		[JustATest_TestMethod_Inconclusive_BadParamCount]
		public void JustATest_Inconclusive_BadParamCount(string s)
		{
		}

		[JustATest_TestMethod_Passed]
		private void JustATest_Private()
		{
			// Should not be found. RunTestsInAssembly_Passed() checks the count of the test methods that were run.
		}

		[JustATest_TestClassAttribute]
		private class JustATest_TestClass_Private
		{
			[TestMethod]
			public void JustATest_TestClass_Private_WithPublicMethod()
			{
				// Should not be found. RunTestsInAssembly_Passed() checks the count of the test methods that were run.
			}
		}

		#endregion

		#region Helper Classes

		public class JustATest_TestClassAttribute : Attribute
		{
		}

		public class JustATest_TestMethod_PassedAttribute : Attribute
		{
		}

		public class JustATest_TestMethod_FailedAttribute : Attribute
		{
		}

		public class JustATest_TestMethod_InconclusiveAttribute : Attribute
		{
		}

		public class JustATest_TestMethod_Inconclusive_BadParamCountAttribute : Attribute
		{
		}

		public class JustATest_Exception : Exception
		{
		}

		#endregion
	}
}