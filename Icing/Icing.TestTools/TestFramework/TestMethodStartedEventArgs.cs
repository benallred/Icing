using System;

namespace Icing.TestTools.TestFramework
{
	/// <summary>Provides data for the event that is raised when a test method is started.</summary>
	public class TestMethodStartedEventArgs : EventArgs
	{
		/// <summary>Gets or sets the name of the test method.</summary>
		/// <value>The name of the test method.</value>
		public string MethodName { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TestMethodStartedEventArgs"/> class.
		/// </summary>
		/// <param name="methodName">The name of the test method.</param>
		public TestMethodStartedEventArgs(string methodName)
		{
			MethodName = methodName;
		}
	}
}