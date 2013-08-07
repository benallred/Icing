using System;

namespace Icing.TestTools.TestFramework
{
	/// <summary>Provides data for the event that is raised when a test class is started.</summary>
	public class TestClassStartedEventArgs : EventArgs
	{
		/// <summary>Gets or sets the name of the test class.</summary>
		/// <value>The name of the test class.</value>
		public string ClassName { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TestClassStartedEventArgs"/> class.
		/// </summary>
		/// <param name="className">The name of the test class.</param>
		public TestClassStartedEventArgs(string className)
		{
			ClassName = className;
		}
	}
}