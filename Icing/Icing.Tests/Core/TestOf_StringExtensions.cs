using System;
using System.Collections.Generic;

using Icing.TestTools.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.Core
{
	[TestClass]
	public class TestOf_StringExtensions
	{
		#region Const, Var, and Properties

		/// <summary>Test cases for testing null, empty, and whitespace string methods.</summary>
		public static string[] NullEmptyWhitespaceTestCases = { null, "", " ", "  ", "   ", "\t", "\n", " a ", "a", ".", "          .\t\n" };

		#endregion

		#region Test Methods

		[TestMethod]
		public void IsNullOrEmpty()
		{
			foreach (string testCase in NullEmptyWhitespaceTestCases)
			{
				bool bCorrectValue = string.IsNullOrEmpty(testCase);
				Assert.AreEqual(bCorrectValue, testCase.IsNullOrEmpty(), "IsNullOrEmpty() should return " + bCorrectValue + " for the string \"" + testCase + "\".");
			}
		}

		[TestMethod]
		public void IsNotNullOrEmpty()
		{
			foreach (string testCase in NullEmptyWhitespaceTestCases)
			{
				bool bCorrectValue = !string.IsNullOrEmpty(testCase);
				Assert.AreEqual(bCorrectValue, testCase.IsNotNullOrEmpty(), "IsNotNullOrEmpty() should return " + bCorrectValue + " for the string \"" + testCase + "\".");
			}
		}

		[TestMethod]
		public void IsNullOrWhiteSpace()
		{
			foreach (string testCase in NullEmptyWhitespaceTestCases)
			{
				bool bCorrectValue = string.IsNullOrWhiteSpace(testCase);
				Assert.AreEqual(bCorrectValue, testCase.IsNullOrWhiteSpace(), "IsNullOrWhiteSpace() should return " + bCorrectValue + " for the string \"" + testCase + "\".");
			}
		}

		[TestMethod]
		public void IsNotNullOrWhiteSpace()
		{
			foreach (string testCase in NullEmptyWhitespaceTestCases)
			{
				bool bCorrectValue = !string.IsNullOrWhiteSpace(testCase);
				Assert.AreEqual(bCorrectValue, testCase.IsNotNullOrWhiteSpace(), "IsNotNullOrWhiteSpace() should return " + bCorrectValue + " for the string \"" + testCase + "\".");
			}
		}

		[TestMethod]
		public void DoesNotEndWith()
		{
			Assert.IsTrue ("test".DoesNotEndWith("te"));
			Assert.IsFalse("test".DoesNotEndWith("st"));

			Assert.IsTrue ("test".DoesNotEndWith("Something longer than the source string"));
			Assert.IsFalse("test".DoesNotEndWith(""));

			Assert.IsTrue ("".DoesNotEndWith("a"));
			Assert.IsFalse("".DoesNotEndWith("" ));

			Assert.IsTrue ("  ".DoesNotEndWith("a"));
			Assert.IsFalse("  ".DoesNotEndWith(" "));

			ExceptionAssertEx.Throws<NullReferenceException>(() => StringExtensions.DoesNotEndWith(null, "a" ));
			ExceptionAssertEx.Throws<NullReferenceException>(() => StringExtensions.DoesNotEndWith(null, null));
			ExceptionAssertEx.Throws<ArgumentNullException >(() => StringExtensions.DoesNotEndWith("a" , null));
		}

		[TestMethod]
		public void EnsureEndsWith()
		{
			Assert.AreEqual("test", "te"  .EnsureEndsWith("st"));
			Assert.AreEqual("test", "test".EnsureEndsWith("st"));

			Assert.AreEqual(  "test", ""  .EnsureEndsWith("test"));
			Assert.AreEqual("  test", "  ".EnsureEndsWith("test"));

			Assert.AreEqual("test" , "test".EnsureEndsWith("" ));
			Assert.AreEqual("test ", "test".EnsureEndsWith(" "));

			ExceptionAssertEx.Throws<NullReferenceException>(() => StringExtensions.EnsureEndsWith(null  , "test"));
			ExceptionAssertEx.Throws<ArgumentNullException >(() => StringExtensions.EnsureEndsWith("test", null  ));
		}

		[TestMethod]
		public void Format()
		{
			#region List of test cases
			FormatTestCase[] testCases = {
													new FormatTestCase("", null, ""),
													new FormatTestCase("  ", null, "  "),
													new FormatTestCase(null, null, null),
													new FormatTestCase("", new {}, ""),
													new FormatTestCase("  ", new {}, "  "),
													new FormatTestCase(null, new {}, null),
													new FormatTestCase("", new { Test = "value" }, ""),
													new FormatTestCase("  ", new { Test = "value" }, "  "),
													new FormatTestCase(null, new { Test = "value" }, null),
													new FormatTestCase("Test", null, "Test"),
													new FormatTestCase("Test", new {}, "Test"),
													new FormatTestCase("Test", new { Test = "value" }, "Test"),
													new FormatTestCase("{Test}", new { Test = "value" }, "value"),
													new FormatTestCase("{Test}", new { test = "value" }, "{Test}"),
													new FormatTestCase("{test}", new { Test = "value" }, "{test}"),
													new FormatTestCase("{Test} and {test}", new { Test = "value" }, "value and {test}"),
													new FormatTestCase("{Test} and {test}", new { test = "value" }, "{Test} and value"),
													new FormatTestCase("{Test} and {test}", new { Test = "value", test = "value2" }, "value and value2"),
													new FormatTestCase("{{Test}}", new { Test = "value" }, "{Test}"),
													new FormatTestCase("{{{Test}}}", new { Test = "value" }, "{{Test}}"),
													new FormatTestCase("{{{{Test}}}}", new { Test = "value" }, "{{Test}}"),
													new FormatTestCase("{{{{{Test}}}}}", new { Test = "value" }, "{{{Test}}}"),
													new FormatTestCase("{{{{{{Test}}}}}}", new { Test = "value" }, "{{{Test}}}"),
													new FormatTestCase("another {test} and {test}", new { test = "value", yetanother = "value2" }, "another value and value"),
													new FormatTestCase("another {test} and {{test}}", new { test = "value", yetanother = "value2" }, "another value and {test}"),
													new FormatTestCase("another {{test}} and {test}", new { test = "value", yetanother = "value2" }, "another {test} and value"),
													new FormatTestCase("another {test} and {yetanother}", new { test = "value", yetanother = "value2" }, "another value and value2"),
													new FormatTestCase("another {test} and {{yetanother}}", new { test = "value", yetanother = "value2" }, "another value and {yetanother}"),
													new FormatTestCase("another {{test}} and {yetanother}", new { test = "value", yetanother = "value2" }, "another {test} and value2"),
													new FormatTestCase("another {test} and {yet another}", new { test = "value", yetanother = "value2" }, "another value and {yet another}"),
													new FormatTestCase("another {test} and {yet{again}another}", new { test = "value", yetanother = "value2", again = "value3" }, "another value and {yetvalue3another}"),
													new FormatTestCase("another {test} and {yet{again}another}", new { test = "value", yetanother = "value2", again = "value3", yetvalue3another = "value4" }, "another value and value4"),
													new FormatTestCase("another {test} and {yet{again}another}", new { test = "value", yetanother = "value2", yetvalue3another = "value4", again = "value3" }, "another value and {yetvalue3another}"),
													new FormatTestCase("another {test} and {yet{test}another}", new { test = "value", yetanother = "value2" }, "another value and {yetvalueanother}"),
													new FormatTestCase("another {test} and {yet{{test}}another}", new { test = "value", yetanother = "value2" }, "another value and {yet{test}another}"),
													new FormatTestCase("another {test} and {yet.another}", new { test = "value", yetanother = "value2" }, "another value and {yet.another}"),
													new FormatTestCase("another {test} and {yet_another}", new { test = "value", yet_another = "value2" }, "another value and value2"),
													new FormatTestCase("another {test} and {yet-another}", new { test = "value", yetanother = "value2" }, "another value and {yet-another}"),
													new FormatTestCase("another {test} and {yetanother}", new { test = "value", yetanother = "" }, "another value and "),
													new FormatTestCase("another {test} and {yetanother}", new { test = (string)null, yetanother = "" }, "another  and "),
												};
			FormatTestCase[] testCasesDictionary = {
																	new FormatTestCase("", (IDictionary<string, string>)null, ""),
																	new FormatTestCase("  ", (IDictionary<string, string>)null, "  "),
																	new FormatTestCase(null, (IDictionary<string, string>)null, null),
																	new FormatTestCase("Test", (IDictionary<string, string>)null, "Test"),
																	new FormatTestCase("another {0} and {1}", new Dictionary<string, string> { {"0", "value"}, {"1", "value2"} }, "another value and value2"),
																	new FormatTestCase("another {0} and {{1}}", new Dictionary<string, string> { {"0", "value"}, {"1", "value2"} }, "another value and {1}"),
																};
			#endregion

			foreach (FormatTestCase testCase in testCases)
			{
				Assert.AreEqual(testCase.Expected, testCase.Source.Format(testCase.Replacements), "source: \"" + testCase.Source + "\", replacements: \"" + testCase.Replacements + "\"");
			}

			foreach (FormatTestCase testCase in testCasesDictionary)
			{
				Assert.AreEqual(testCase.Expected, testCase.Source.Format((IDictionary<string, string>)testCase.Replacements), "source: \"" + testCase.Source + "\", replacements: \"" + testCase.Replacements + "\"");
			}
		}

		[TestMethod]
		public void PadNumber_Int32()
		{
			Assert.AreEqual(  "1",   "1".PadNumber(0, '0'));
			Assert.AreEqual( "11",  "11".PadNumber(0, '0'));
			Assert.AreEqual("111", "111".PadNumber(0, '0'));

			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => StringExtensions.PadNumber("1" , -1, '0'));
			ExceptionAssertEx.Throws<NullReferenceException     >(() => StringExtensions.PadNumber(null,  1, '0'));

			Assert.AreEqual( "0", "" .PadNumber( 0, '0'));
			Assert.AreEqual( "0", "" .PadNumber( 1, '0'));
			Assert.AreEqual( "a", "a".PadNumber( 0, '0'));
			Assert.AreEqual("0a", "a".PadNumber(10, '0'));
			Assert.AreEqual( "b", "" .PadNumber( 0, 'b'));
			Assert.AreEqual("ba", "a".PadNumber(10, 'b'));

			for (int power = 1; power < 4; power++)
			{
				for (int i = (int)Math.Pow(10, power - 1); i < Math.Pow(10, power); i++)
				{
					Assert.AreEqual(  "1".PadLeft(power, '0'),   "1".PadNumber(i, '0'));
					Assert.AreEqual( "11".PadLeft(power, '0'),  "11".PadNumber(i, '0'));
					Assert.AreEqual("111".PadLeft(power, '0'), "111".PadNumber(i, '0'));

/*
					Assert.AreEqual(  "1".PadLeft(power, '0'),   "1".PadNumber(-1 * i, '0'));
					Assert.AreEqual( "11".PadLeft(power, '0'),  "11".PadNumber(-1 * i, '0'));
					Assert.AreEqual("111".PadLeft(power, '0'), "111".PadNumber(-1 * i, '0'));
*/
				}
			}
		}

		[TestMethod]
		public void PadNumber_Int64()
		{
			Assert.AreEqual(  "1",   "1".PadNumber(0L, '0'));
			Assert.AreEqual( "11",  "11".PadNumber(0L, '0'));
			Assert.AreEqual("111", "111".PadNumber(0L, '0'));

			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => StringExtensions.PadNumber("1" , -1L, '0'));
			ExceptionAssertEx.Throws<NullReferenceException     >(() => StringExtensions.PadNumber(null,  1L, '0'));

			Assert.AreEqual( "0", "" .PadNumber( 0L, '0'));
			Assert.AreEqual( "0", "" .PadNumber( 1L, '0'));
			Assert.AreEqual( "a", "a".PadNumber( 0L, '0'));
			Assert.AreEqual("0a", "a".PadNumber(10L, '0'));
			Assert.AreEqual( "b", "" .PadNumber( 0L, 'b'));
			Assert.AreEqual("ba", "a".PadNumber(10L, 'b'));

			for (int power = 1; power < 4; power++)
			{
				for (long i = (long)Math.Pow(10, power - 1); i < Math.Pow(10, power); i++)
				{
					Assert.AreEqual(  "1".PadLeft(power, '0'),   "1".PadNumber(i, '0'));
					Assert.AreEqual( "11".PadLeft(power, '0'),  "11".PadNumber(i, '0'));
					Assert.AreEqual("111".PadLeft(power, '0'), "111".PadNumber(i, '0'));

/*
					Assert.AreEqual(  "1".PadLeft(power, '0'),   "1".PadNumber(-1 * i, '0'));
					Assert.AreEqual( "11".PadLeft(power, '0'),  "11".PadNumber(-1 * i, '0'));
					Assert.AreEqual("111".PadLeft(power, '0'), "111".PadNumber(-1 * i, '0'));
*/
				}
			}
		}

		[TestMethod]
		public void PadNumber_UInt32()
		{
			Assert.AreEqual(  "1",   "1".PadNumber(0u, '0'));
			Assert.AreEqual( "11",  "11".PadNumber(0u, '0'));
			Assert.AreEqual("111", "111".PadNumber(0u, '0'));

//			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => StringExtensions.PadNumber("1" , -1u, '0')); // Not valid
			ExceptionAssertEx.Throws<NullReferenceException     >(() => StringExtensions.PadNumber(null,  1u, '0'));

			Assert.AreEqual( "0", "" .PadNumber( 0u, '0'));
			Assert.AreEqual( "0", "" .PadNumber( 1u, '0'));
			Assert.AreEqual( "a", "a".PadNumber( 0u, '0'));
			Assert.AreEqual("0a", "a".PadNumber(10u, '0'));
			Assert.AreEqual( "b", "" .PadNumber( 0u, 'b'));
			Assert.AreEqual("ba", "a".PadNumber(10u, 'b'));

			for (int power = 1; power < 4; power++)
			{
				for (uint i = (uint)Math.Pow(10, power - 1); i < Math.Pow(10, power); i++)
				{
					Assert.AreEqual(  "1".PadLeft(power, '0'),   "1".PadNumber(i, '0'));
					Assert.AreEqual( "11".PadLeft(power, '0'),  "11".PadNumber(i, '0'));
					Assert.AreEqual("111".PadLeft(power, '0'), "111".PadNumber(i, '0'));
				}
			}
		}

		[TestMethod]
		public void ToInt32()
		{
			Assert.AreEqual( 1, StringExtensions.ToInt32(  "1"));
			Assert.AreEqual( 1, StringExtensions.ToInt32( "01"));
			Assert.AreEqual(11, StringExtensions.ToInt32( "11"));
			Assert.AreEqual(-1, StringExtensions.ToInt32( "-1"));
			Assert.AreEqual(-1, StringExtensions.ToInt32("-01"));
			Assert.AreEqual( 0, StringExtensions.ToInt32(  "0"));
			Assert.AreEqual( 0, StringExtensions.ToInt32( "-0"));

			ExceptionAssertEx.Throws<ArgumentNullException>(() => StringExtensions.ToInt32(null));
			ExceptionAssertEx.Throws<FormatException>(() => StringExtensions.ToInt32(""   ));
			ExceptionAssertEx.Throws<FormatException>(() => StringExtensions.ToInt32("a"  ));
			ExceptionAssertEx.Throws<FormatException>(() => StringExtensions.ToInt32("-"  ));
			ExceptionAssertEx.Throws<FormatException>(() => StringExtensions.ToInt32("a1" ));
			ExceptionAssertEx.Throws<FormatException>(() => StringExtensions.ToInt32("1a" ));
			ExceptionAssertEx.Throws<FormatException>(() => StringExtensions.ToInt32("1 2"));
			ExceptionAssertEx.Throws<OverflowException>(() => StringExtensions.ToInt32( "2147483648"));
			ExceptionAssertEx.Throws<OverflowException>(() => StringExtensions.ToInt32("-2147483649"));
		}

		[TestMethod]
		public void Trim()
		{
			Assert.AreEqual(""      , "test"         .Trim("test"    ));
			Assert.AreEqual(""      , "testtest"     .Trim("test"    ));
			Assert.AreEqual(""      , "testtesttest" .Trim("test"    ));
			Assert.AreEqual("test"  , "testtesttest" .Trim("testtest"));
			Assert.AreEqual("testa" , "testatesttest".Trim("testtest"));
			Assert.AreEqual("atest" , "testtestatest".Trim("testtest"));
			Assert.AreEqual(""      , ""             .Trim("test"    ));
			Assert.AreEqual(" test ", " test "       .Trim("test"    ));
												 
			Assert.AreEqual("abc", "abc".Trim("test"));
			Assert.AreEqual("abc", "abc".Trim("b"   ));
			Assert.AreEqual("bc" , "abc".Trim("a"   ));
			Assert.AreEqual("ab" , "abc".Trim("c"   ));
			Assert.AreEqual("a"  , "abc".Trim("bc"  ));
			Assert.AreEqual("c"  , "abc".Trim("ab"  ));
			Assert.AreEqual("abc", "abc".Trim("ac"  ));

			Assert.AreEqual("abc"  , "abc"    .Trim(" " ));
			Assert.AreEqual("abc"  , " abc "  .Trim(" " ));
			Assert.AreEqual("abc"  , "  abc  ".Trim(" " ));
			Assert.AreEqual("abc"  , "  abc  ".Trim("  "));
			Assert.AreEqual(" abc ", " abc "  .Trim("  "));

			ExceptionAssertEx.Throws<ArgumentNullException >(() => StringExtensions.Trim("abc", ""   ));
			ExceptionAssertEx.Throws<ArgumentNullException >(() => StringExtensions.Trim("abc", null ));
			ExceptionAssertEx.Throws<NullReferenceException>(() => StringExtensions.Trim(null , "abc"));
		}

		[TestMethod]
		public void Reverse()
		{
			ExceptionAssertEx.Throws<ArgumentNullException>(() => StringExtensions.Reverse(null));
			Assert.AreEqual("", "".Reverse());
			Assert.AreEqual(" ", " ".Reverse());
			Assert.AreEqual("    ", "    ".Reverse());
			Assert.AreEqual("a", "a".Reverse());
			Assert.AreEqual("aaaa", "aaaa".Reverse());
			Assert.AreEqual("asdf", "fdsa".Reverse());
			Assert.AreEqual("ㅑㅏㄷㄴㄱ", "ㄱㄴㄷㅏㅑ".Reverse());
			Assert.AreEqual("용지정", "정지용".Reverse());
			Assert.AreEqual("勇智鄭", "鄭智勇".Reverse());
			Assert.AreEqual("0987654321험시zyxwvutsrqponmlkjihgfedcba", "abcdefghijklmnopqrstuvwxyz시험1234567890".Reverse());
		}

		#endregion

		#region Helper Classes

		/// <summary>Represents a test case for the Format method</summary>
		private class FormatTestCase
		{
			/// <summary>Gets or sets the source string.</summary>
			/// <value>The source string.</value>
			public string Source { get; private set; }

			/// <summary>Gets or sets the replacement values.</summary>
			/// <value>The replacement values.</value>
			public object Replacements { get; private set; }

			/// <summary>Gets or sets the expected string after calling Format.</summary>
			/// <value>The expected string after calling Format.</value>
			public string Expected { get; private set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="FormatTestCase"/> class.
			/// </summary>
			/// <param name="source">The source string.</param>
			/// <param name="replacements">The replacement values.</param>
			/// <param name="expected">The expected string after calling Format.</param>
			public FormatTestCase(string source, object replacements, string expected)
			{
				Source = source;
				Replacements = replacements;
				Expected = expected;
			}
		}

		#endregion
	}
}