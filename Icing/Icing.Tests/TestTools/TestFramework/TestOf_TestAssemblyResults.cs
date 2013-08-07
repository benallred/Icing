using System;
using System.Collections.Generic;
using System.Linq;

using Icing.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.TestFramework
{
	[TestClass]
	public class TestOf_TestAssemblyResults
	{
		[TestMethod]
		public void Constructor()
		{
			TestAssemblyResults testAssemblyResults = new TestAssemblyResults(null, TimeSpan.FromSeconds(42));

			Assert.IsNotNull(testAssemblyResults.TestClassResults);
			Assert.IsFalse(testAssemblyResults.TestClassResults.Any());
			Assert.AreEqual(42, testAssemblyResults.ExecutionTime.TotalSeconds);
			Assert.AreEqual(TestResultStatus.Passed, testAssemblyResults.Status);

			//////////////////////////////

			List<TestClassResults> testClassResults = new List<TestClassResults>()
			{
				new TestClassResults("SomeClassName", null, new TimeSpan()) { Status = TestResultStatus.Passed }
			};

			testAssemblyResults = new TestAssemblyResults(testClassResults, new TimeSpan());

			Assert.IsNotNull(testAssemblyResults.TestClassResults);
			Assert.AreEqual(1, testAssemblyResults.TestClassResults.Count());
			Assert.AreEqual("SomeClassName", testAssemblyResults.TestClassResults.Single().ClassName);
			Assert.AreEqual(TestResultStatus.Passed, testAssemblyResults.Status);

			//////////////////////////////

			testClassResults.Insert(0, new TestClassResults("SomeClassName2", null, new TimeSpan()) { Status = TestResultStatus.Inconclusive });
			testAssemblyResults = new TestAssemblyResults(testClassResults, new TimeSpan());
			Assert.AreEqual(TestResultStatus.Inconclusive, testAssemblyResults.Status);

			//////////////////////////////

			testClassResults.Insert(1, new TestClassResults("SomeClassName3", null, new TimeSpan()) { Status = TestResultStatus.Failed });
			testAssemblyResults = new TestAssemblyResults(testClassResults, new TimeSpan());
			Assert.AreEqual(TestResultStatus.Failed, testAssemblyResults.Status);
		}
	}
}