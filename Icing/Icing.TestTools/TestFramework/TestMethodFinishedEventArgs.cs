namespace Icing.TestTools.TestFramework
{
	/// <summary>Provides data for the event that is raised when a test method is finished.</summary>
	public class TestMethodFinishedEventArgs
	{
		/// <summary>The result from running the test method.</summary>
		/// <value>The result from running the test method.</value>
		public TestMethodResult Result { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TestMethodFinishedEventArgs"/> class.
		/// </summary>
		/// <param name="result">The result from running the test method.</param>
		public TestMethodFinishedEventArgs(TestMethodResult result)
		{
			Result = result;
		}
	}
}