namespace Icing.TestTools.TestFramework
{
	/// <summary>Provides data for the event that is raised when a test class is finished.</summary>
	public class TestClassFinishedEventArgs
	{
		/// <summary>The results from running the tests in the class.</summary>
		/// <value>The results from running the tests in the class.</value>
		public TestClassResults Results { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TestClassFinishedEventArgs"/> class.
		/// </summary>
		/// <param name="results">The results from running the tests in the class.</param>
		public TestClassFinishedEventArgs(TestClassResults results)
		{
			Results = results;
		}
	}
}