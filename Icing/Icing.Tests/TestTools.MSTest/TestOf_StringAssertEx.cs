using System;

using Icing.TestTools.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.TestTools.MSTest
{
	[TestClass]
	public class TestOf_StringAssertEx
	{
		[TestMethod]
		public void IsEmpty()
		{
			StringAssertEx.IsEmpty("", "\"\" should succeed.");
			StringAssertEx.IsEmpty(String.Empty, "String.Empty should succeed.");

			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsEmpty("a" ), "\"a\" should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsEmpty(" " ), "\" \" should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsEmpty(null),  "null should fail.");
		}

		[TestMethod]
		public void IsNotEmpty()
		{
			StringAssertEx.IsNotEmpty("a" , "\"a\" should succeed.");
			StringAssertEx.IsNotEmpty(" " , "\" \" should succeed.");
			StringAssertEx.IsNotEmpty(null,  "null should succeed.");

			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotEmpty(""), "\"\" should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotEmpty(String.Empty), "String.Empty should fail.");
		}

		[TestMethod]
		public void IsNullOrEmpty()
		{
			StringAssertEx.IsNullOrEmpty("", "\"\" should succeed.");
			StringAssertEx.IsNullOrEmpty(String.Empty, "String.Empty should succeed.");
			StringAssertEx.IsNullOrEmpty(null, "null should succeed.");

			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNullOrEmpty("a"), "\"a\" should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNullOrEmpty(" "), "\" \" should fail.");
		}

		[TestMethod]
		public void IsNotNullOrEmpty()
		{
			StringAssertEx.IsNotNullOrEmpty("a", "\"a\" should succeed.");
			StringAssertEx.IsNotNullOrEmpty(" ", "\" \" should succeed.");

			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotNullOrEmpty(""), "\"\" should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotNullOrEmpty(String.Empty), "String.Empty should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotNullOrEmpty(null), "null should fail.");
		}

		[TestMethod]
		public void IsNullOrWhiteSpace()
		{
			StringAssertEx.IsNullOrWhiteSpace("", "\"\" should succeed.");
			StringAssertEx.IsNullOrWhiteSpace(String.Empty, "String.Empty should succeed.");
			StringAssertEx.IsNullOrWhiteSpace(" ", "\" \" should succeed.");
			StringAssertEx.IsNullOrWhiteSpace(null, "null should succeed.");

			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNullOrWhiteSpace("a"), "\"a\" should fail.");
		}

		[TestMethod]
		public void IsNotNullOrWhiteSpace()
		{
			StringAssertEx.IsNotNullOrWhiteSpace("a", "\"a\" should succeed.");

			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotNullOrWhiteSpace(""), "\"\" should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotNullOrWhiteSpace(String.Empty), "String.Empty should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotNullOrWhiteSpace(" "), "\" \" should fail.");
			ExceptionAssertEx.Throws<AssertFailedException>(() => StringAssertEx.IsNotNullOrWhiteSpace(null), "null should fail.");
		}
	}
}