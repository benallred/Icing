using Icing.TestTools.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.MSTest
{
	[TestClass]
	public class TestOf_AssertEx
	{
		#region Const, Var, and Properties
		
		const string AssertionName = "TestAssertion";
		const string Message = "My Message";
		const string MessageWithParam = "My {0} Message";
		const string TestParam = "testparam";
		const string FailedExpected = "{0} failed. {1}";

		const string FailedExpectedWithExpectedActual = "{0} failed. Expected:<{1}>. Actual:<{2}>. {3}";
		const string Expected = "exp";
		const string Actual = "act";
				
		#endregion
		
		[TestMethod]
		public void CreateException_EmptyParameters()
		{
			AssertFailedException ex = AssertEx.CreateException(AssertionName, Message, new object[] { });
			Assert.IsNotNull(ex);
			Assert.AreEqual(string.Format(FailedExpected, AssertionName, Message), ex.Message, "The expected failure message does not match the actual failure message.");
		}

		[TestMethod]
      public void CreateException_NullParameters()
      {
			AssertFailedException ex = AssertEx.CreateException(AssertionName, Message, null);
			Assert.IsNotNull(ex);
			Assert.AreEqual(string.Format(FailedExpected, AssertionName, Message), ex.Message, "The expected failure message does not match the actual failure message.");
		}

		[TestMethod]
		public void CreateException_WithParameters()
		{
			AssertFailedException ex = AssertEx.CreateException(AssertionName, MessageWithParam, TestParam);
			Assert.IsNotNull(ex);
			Assert.AreEqual(string.Format(FailedExpected, AssertionName, string.Format(MessageWithParam, TestParam)), ex.Message, "The expected failure message does not match the actual failure message.");
		}

		[TestMethod]
		public void CreateExceptionWithExpectedActual_NoParameters()
		{
			AssertFailedException ex = AssertEx.CreateExceptionWithExpectedActual(AssertionName, Expected, Actual, Message);
			Assert.IsNotNull(ex);
			Assert.AreEqual(string.Format(FailedExpectedWithExpectedActual, AssertionName, Expected, Actual, Message), ex.Message, "The expected failure message does not match the actual failure message.");
		}

		[TestMethod]
		public void CreateExceptionWithExpectedActual_NoMessage()
		{
			AssertFailedException ex = AssertEx.CreateExceptionWithExpectedActual(AssertionName, Expected, Actual, null);
			Assert.IsNotNull(ex);
			Assert.AreEqual(string.Format(FailedExpectedWithExpectedActual, AssertionName, Expected, Actual, ""), ex.Message, "The expected failure message does not match the actual failure message.");
		}

		[TestMethod]
		public void ReplaceNulls()
		{
			PrivateType internalAssertEx = new PrivateType(typeof(AssertEx));
			object oNull = null;
			Assert.AreEqual("(null)", internalAssertEx.InvokeStatic("ReplaceNulls", oNull), "AssertEx.ReplaceNulls didn't return the correct value.");
			Assert.AreEqual("teststring", internalAssertEx.InvokeStatic("ReplaceNulls", "teststring"), "AssertEx.ReplaceNulls didn't return the correct value.");
			Assert.AreEqual("test\\0string", internalAssertEx.InvokeStatic("ReplaceNulls", "test\0string"), "AssertEx.ReplaceNulls didn't return the correct value.");
		}
	}
}