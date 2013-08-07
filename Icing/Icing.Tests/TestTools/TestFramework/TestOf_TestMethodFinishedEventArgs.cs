using System;

using Icing.TestTools.TestFramework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.TestFramework
{
	[TestClass]
	public class TestOf_TestMethodFinishedEventArgs
	{
		[TestMethod]
		public void Constructor()
		{
			TestMethodFinishedEventArgs eventArgs = new TestMethodFinishedEventArgs(new TestMethodResult("SomeMethodName", TestResultStatus.Passed, null, new TimeSpan()));

			Assert.IsNotNull(eventArgs.Result);
			Assert.AreEqual("SomeMethodName", eventArgs.Result.MethodName);
		}
	}
}