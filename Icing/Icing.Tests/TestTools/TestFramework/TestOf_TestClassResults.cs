using System;
using System.Collections.Generic;
using System.Linq;

using Icing.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.TestFramework
{
	[TestClass]
	public class TestOf_TestClassResults
	{
		[TestMethod]
		public void Constructor()
		{
			TestClassResults testClassResults = new TestClassResults("SomeClassName", null, TimeSpan.FromSeconds(42));

			Assert.IsNotNull(testClassResults.TestMethodResults);
			Assert.IsFalse(testClassResults.TestMethodResults.Any());
			Assert.AreEqual(42, testClassResults.ExecutionTime.TotalSeconds);
			Assert.AreEqual(TestResultStatus.Passed, testClassResults.Status);

			//////////////////////////////

			List<TestMethodResult> testMethodResults = new List<TestMethodResult>()
			{
				new TestMethodResult("SomeMethodName", TestResultStatus.Passed, null, new TimeSpan())
			};
			
			testClassResults = new TestClassResults("SomeClassName", testMethodResults, new TimeSpan());

			Assert.AreEqual("SomeClassName", testClassResults.ClassName);
			Assert.IsNotNull(testClassResults.TestMethodResults);
			Assert.AreEqual(1, testClassResults.TestMethodResults.Count());
			Assert.AreEqual("SomeMethodName", testClassResults.TestMethodResults.Single().MethodName);
			Assert.AreEqual(TestResultStatus.Passed, testClassResults.Status);

			//////////////////////////////

			testMethodResults.Insert(0, new TestMethodResult("SomeMethodName2", TestResultStatus.Inconclusive, null, new TimeSpan()));
			testClassResults = new TestClassResults("SomeClassName", testMethodResults, new TimeSpan());
			Assert.AreEqual(TestResultStatus.Inconclusive, testClassResults.Status);

			//////////////////////////////

			testMethodResults.Insert(1, new TestMethodResult("SomeMethodName3", TestResultStatus.Failed, null, new TimeSpan()));
			testClassResults = new TestClassResults("SomeClassName", testMethodResults, new TimeSpan());
			Assert.AreEqual(TestResultStatus.Failed, testClassResults.Status);
		}
	}
}