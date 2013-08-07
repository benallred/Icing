using System;

using Icing.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.TestFramework
{
	[TestClass]
	public class TestOf_TestMethodResult
	{
		[TestMethod]
		public void Constructor()
		{
			TestMethodResult testMethodResult = new TestMethodResult("SomeMethodName", TestResultStatus.Failed, new Exception("SomeException"), TimeSpan.FromSeconds(42));

			Assert.AreEqual("SomeMethodName", testMethodResult.MethodName);
			Assert.AreEqual(TestResultStatus.Failed, testMethodResult.Status);
			Assert.IsNotNull(testMethodResult.Exception);
			Assert.AreEqual("SomeException", testMethodResult.Exception.Message);
			Assert.AreEqual(42, testMethodResult.ExecutionTime.TotalSeconds);
		}
	}
}