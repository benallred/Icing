using System;
using System.Collections.Generic;

namespace Icing.TestTools.TestFramework
{
	/// <summary>The results from running the tests in an assembly.</summary>
	public class TestAssemblyResults
	{
		/// <summary>Gets or sets the status of the test.</summary>
		/// <value>The status of the test.</value>
		public TestResultStatus Status { get; set; }

		/// <summary>Gets or sets the test class results.</summary>
		/// <value>The test class results.</value>
		public IEnumerable<TestClassResults> TestClassResults { get; set; }

		/// <summary>Gets or sets the total execution time of all tests in the assembly.</summary>
		/// <value>The total execution time of all tests in the assembly.</value>
		public TimeSpan ExecutionTime { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TestAssemblyResults" /> class.
		/// </summary>
		/// <param name="testClassResults">The test class results.</param>
		/// <param name="executionTime">The total execution time of all tests in the assembly.</param>
		public TestAssemblyResults(IEnumerable<TestClassResults> testClassResults, TimeSpan executionTime)
		{
			TestClassResults = testClassResults ?? new List<TestClassResults>();
			ExecutionTime = executionTime;

			Status = TestResultStatus.Passed;

			foreach (TestClassResults testClassResult in TestClassResults)
			{
				if (testClassResult.Status == TestResultStatus.Failed)
				{
					Status = TestResultStatus.Failed;
					break;
				}
				else if (testClassResult.Status == TestResultStatus.Inconclusive)
				{
					Status = TestResultStatus.Inconclusive;
				}
			}
		}
	}
}