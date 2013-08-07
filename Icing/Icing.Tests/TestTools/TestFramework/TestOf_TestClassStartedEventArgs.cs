using Icing.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.TestFramework
{
	[TestClass]
	public class TestOf_TestClassStartedEventArgs
	{
		[TestMethod]
		public void Constructor()
		{
			TestClassStartedEventArgs eventArgs = new TestClassStartedEventArgs("SomeClassName");

			Assert.AreEqual("SomeClassName", eventArgs.ClassName);
		}
	}
}