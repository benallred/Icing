using System;

using Icing.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.TestFramework
{
	[TestClass]
	public class TestOf_TestClassFinishedEventArgs
	{
		[TestMethod]
		public void Constructor()
		{
			TestClassFinishedEventArgs eventArgs = new TestClassFinishedEventArgs(new TestClassResults("SomeClassName", null, new TimeSpan()));

			Assert.IsNotNull(eventArgs.Results);
			Assert.AreEqual("SomeClassName", eventArgs.Results.ClassName);
		}
	}
}