using System;

namespace Icing.TestTools.TestFramework
{
	/// <summary>The result from running a test method.</summary>
	public class TestMethodResult
	{
		/// <summary>Gets or sets the name of the test method.</summary>
		/// <value>The name of the test method.</value>
		public string MethodName { get; set; }

		/// <summary>Gets or sets the status of the test.</summary>
		/// <value>The status of the test.</value>
		public TestResultStatus Status { get; set; }

		/// <summary>Gets or sets the exception thrown (if any) during the test.</summary>
		/// <value>The exception thrown (if any) during the test.</value>
		public Exception Exception { get; set; }

		/// <summary>Gets or sets the execution time of the test.</summary>
		/// <value>The execution time of the test.</value>
		public TimeSpan ExecutionTime { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TestMethodResult"/> class.
		/// </summary>
		/// <param name="methodName">The name of the test method.</param>
		/// <param name="status">The status of the test.</param>
		/// <param name="exception">The exception thrown (if any) during the test.</param>
		/// <param name="executionTime">The execution time of the test.</param>
		public TestMethodResult(string methodName, TestResultStatus status, Exception exception, TimeSpan executionTime)
		{
			MethodName = methodName;
			Status = status;
			Exception = exception;
			ExecutionTime = executionTime;
		}
	}
}