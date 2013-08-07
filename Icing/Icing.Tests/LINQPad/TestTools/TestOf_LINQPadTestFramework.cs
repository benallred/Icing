using Icing.LINQPad.TestTools;
using Icing.Tests.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.LINQPad.TestTools
{
	[TestClass]
	public class TestOf_LINQPadTestFramework
	{
		[TestMethod]
		public void RunTests_Passed()
		{
			LINQPadTestFramework<TestOf_SimpleTestFramework.JustATest_TestClassAttribute, TestOf_SimpleTestFramework.JustATest_TestMethod_PassedAttribute, TestOf_SimpleTestFramework.JustATest_Exception> linqPadTestFramework
				= new LINQPadTestFramework<TestOf_SimpleTestFramework.JustATest_TestClassAttribute, TestOf_SimpleTestFramework.JustATest_TestMethod_PassedAttribute, TestOf_SimpleTestFramework.JustATest_Exception>();

			linqPadTestFramework.RunTests();
		}

		[TestMethod]
		public void RunTests_Failed()
		{
			LINQPadTestFramework<TestOf_SimpleTestFramework.JustATest_TestClassAttribute, TestOf_SimpleTestFramework.JustATest_TestMethod_FailedAttribute, TestOf_SimpleTestFramework.JustATest_Exception> linqPadTestFramework
				= new LINQPadTestFramework<TestOf_SimpleTestFramework.JustATest_TestClassAttribute, TestOf_SimpleTestFramework.JustATest_TestMethod_FailedAttribute, TestOf_SimpleTestFramework.JustATest_Exception>();

			linqPadTestFramework.RunTests();
		}

		[TestMethod]
		public void RunTests_Inconclusive()
		{
			LINQPadTestFramework<TestOf_SimpleTestFramework.JustATest_TestClassAttribute, TestOf_SimpleTestFramework.JustATest_TestMethod_InconclusiveAttribute, TestOf_SimpleTestFramework.JustATest_Exception> linqPadTestFramework
				= new LINQPadTestFramework<TestOf_SimpleTestFramework.JustATest_TestClassAttribute, TestOf_SimpleTestFramework.JustATest_TestMethod_InconclusiveAttribute, TestOf_SimpleTestFramework.JustATest_Exception>();

			linqPadTestFramework.RunTests();
		}

		[TestMethod]
		public void RunTestsInAssembly_Inconclusive_BadParamCount()
		{
			LINQPadTestFramework<TestOf_SimpleTestFramework.JustATest_TestClassAttribute, TestOf_SimpleTestFramework.JustATest_TestMethod_Inconclusive_BadParamCountAttribute, TestOf_SimpleTestFramework.JustATest_Exception> linqPadTestFramework
				= new LINQPadTestFramework<TestOf_SimpleTestFramework.JustATest_TestClassAttribute, TestOf_SimpleTestFramework.JustATest_TestMethod_Inconclusive_BadParamCountAttribute, TestOf_SimpleTestFramework.JustATest_Exception>();

			linqPadTestFramework.RunTests();
		}
	}
}