using System;
using System.Collections.Generic;
using System.Linq;

namespace Icing.TestTools.TestFramework
{
	/// <summary>The results from running the tests in a class.</summary>
	public class TestClassResults
	{
		/// <summary>Gets or sets the name of the test class.</summary>
		/// <value>The name of the test class.</value>
		public string ClassName { get; set; }

		/// <summary>Gets or sets the status of the test.</summary>
		/// <value>The status of the test.</value>
		public TestResultStatus Status { get; set; }

		/// <summary>Gets or sets the test method results.</summary>
		/// <value>The test method results.</value>
		public IEnumerable<TestMethodResult> TestMethodResults { get; set; }

		/// <summary>Gets or sets the total execution time of all tests in the class.</summary>
		/// <value>The total execution time of all tests in the class.</value>
		public TimeSpan ExecutionTime { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TestClassResults" /> class.
		/// </summary>
		/// <param name="className">The name of the test class.</param>
		/// <param name="testMethodResults">The test method results.</param>
		/// <param name="executionTime">The total execution time of all tests in the class.</param>
		public TestClassResults(string className, IEnumerable<TestMethodResult> testMethodResults, TimeSpan executionTime)
		{
			ClassName = className;
			TestMethodResults = testMethodResults ?? new List<TestMethodResult>();
			ExecutionTime = executionTime;

			Status = TestResultStatus.Passed;

			foreach (TestMethodResult testMethodResult in TestMethodResults)
			{
				if (testMethodResult.Status == TestResultStatus.Failed)
				{
					Status = TestResultStatus.Failed;
					break;
				}
				else if (testMethodResult.Status == TestResultStatus.Inconclusive)
				{
					Status = TestResultStatus.Inconclusive;
				}
			}
		}
	}
}