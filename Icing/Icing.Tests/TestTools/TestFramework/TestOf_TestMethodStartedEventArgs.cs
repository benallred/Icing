using Icing.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.TestFramework
{
	[TestClass]
	public class TestOf_TestMethodStartedEventArgs
	{
		[TestMethod]
		public void Constructor()
		{
			TestMethodStartedEventArgs eventArgs = new TestMethodStartedEventArgs("SomeMethodName");

			Assert.AreEqual("SomeMethodName", eventArgs.MethodName);
		}
	}
}