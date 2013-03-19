using System;

using Icing.TestTools.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.MSTest
{
	[TestClass]
	public class TestOf_ExceptionAssertEx
	{
		#region Const, Var, and Properties

		const string FailedExpected = "ExceptionAssertEx.Throws failed. Expected:<{0}>. Actual:<{1}>. ";

		#endregion

		[TestMethod]
		public void Throws()
		{
			// None of these should throw an exception
			ExceptionAssertEx.Throws<Exception             >(() => { throw new Exception             (); });
			ExceptionAssertEx.Throws<NullReferenceException>(() => { throw new NullReferenceException(); });
			ExceptionAssertEx.Throws<ArgumentException     >(() => { throw new ArgumentException     (); });
			ExceptionAssertEx.Throws<ArgumentNullException >(() => { throw new ArgumentNullException (); });
			ExceptionAssertEx.Throws<FormatException       >(() =>   int.Parse("not an int")              );
		}

		[TestMethod]
		public void Throws_Fail()
		{
			bool failed = false;

			try
			{
				ExceptionAssertEx.Throws<Exception>(() => { });
			}
			catch (AssertFailedException ex)
			{
				Assert.AreEqual(string.Format(FailedExpected, typeof(Exception), "[none]"), ex.Message, "The expected failure message does not match the actual failure message.");
				failed = true;
			}

			if (!failed)
			{
				Assert.Fail("Should throw exception for: ExceptionAssertEx.Throws<Exception>(() => { });");
			}

			//////////////////////////////

			failed = false;

			try
			{
				ExceptionAssertEx.Throws<NullReferenceException>(() => { throw new Exception(); });
			}
			catch (AssertFailedException ex)
			{
				Assert.AreEqual(string.Format(FailedExpected, typeof(NullReferenceException), typeof(Exception)), ex.Message, "The expected failure message does not match the actual failure message.");
				failed = true;
			}

			if (!failed)
			{
				Assert.Fail("Should throw exception for: ExceptionAssertEx.Throws<NullReferenceException>(() => { throw new Exception(); });");
			}

			//////////////////////////////

			failed = false;

			try
			{
				ExceptionAssertEx.Throws<ArgumentException>(() => { throw new NullReferenceException(); });
			}
			catch (AssertFailedException ex)
			{
				Assert.AreEqual(string.Format(FailedExpected, typeof(ArgumentException), typeof(NullReferenceException)), ex.Message, "The expected failure message does not match the actual failure message.");
				failed = true;
			}

			if (!failed)
			{
				Assert.Fail("Should throw exception for: ExceptionAssertEx.Throws<Exception>(() => { throw new NullReferenceException(); });");
			}

			//////////////////////////////

			failed = false;

			try
			{
				ExceptionAssertEx.Throws<ArgumentNullException>(() => { throw new ArgumentException(); });
			}
			catch (AssertFailedException ex)
			{
				Assert.AreEqual(string.Format(FailedExpected, typeof(ArgumentNullException), typeof(ArgumentException)), ex.Message, "The expected failure message does not match the actual failure message.");
				failed = true;
			}

			if (!failed)
			{
				Assert.Fail("Should throw exception for: ExceptionAssertEx.Throws<ArgumentNullException>(() => { throw new ArgumentException(); });");
			}

			//////////////////////////////

			failed = false;

			try
			{
				ExceptionAssertEx.Throws<FormatException>(() => { throw new ArgumentNullException(); });
			}
			catch (AssertFailedException ex)
			{
				Assert.AreEqual(string.Format(FailedExpected, typeof(FormatException), typeof(ArgumentNullException)), ex.Message, "The expected failure message does not match the actual failure message.");
				failed = true;
			}

			if (!failed)
			{
				Assert.Fail("Should throw exception for: ExceptionAssertEx.Throws<FormatException>(() => { throw new ArgumentNullException(); });");
			}

			//////////////////////////////

			failed = false;

			try
			{
				ExceptionAssertEx.Throws<InvalidOperationException>(() => int.Parse("not an int"));
			}
			catch (AssertFailedException ex)
			{
				Assert.AreEqual(string.Format(FailedExpected, typeof(InvalidOperationException), typeof(FormatException)), ex.Message, "The expected failure message does not match the actual failure message.");
				failed = true;
			}

			if (!failed)
			{
				Assert.Fail("Should throw exception for: ExceptionAssertEx.Throws<InvalidOperationException>(() => int.Parse(\"not an int\"));");
			}
		}
	}
}