using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.TestTools.MSTest
{
	/// <summary>
	/// <para>Verifies true/false propositions associated with strings in unit tests.</para>
	/// <para>Extends the <see cref="StringAssert"/> class.</para>
	/// </summary>
	public class StringAssertEx : AssertEx
	{
		/// <summary>
		/// Verifies that the specified string is empty.  The assertion fails if it is not empty.  Optionally displays a message if the assertion fails, and applies the specified formatting to it.
		/// </summary>
		/// <param name="value">The string to verify is empty.</param>
		/// <param name="message">The message to display if the assertion fails.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		public static void IsEmpty(string value, string message = null, params object[] parameters)
		{
			if (!string.Empty.Equals(value))
			{
				throw CreateExceptionWithExpectedActual("AssertEx.IsEmpty", string.Empty, value, message, parameters);
			}
		}

		/// <summary>
		/// Verifies that the specified string is not empty.  The assertion fails if it is empty.  Optionally displays a message if the assertion fails, and applies the specified formatting to it.
		/// </summary>
		/// <param name="value">The string to verify is not empty.</param>
		/// <param name="message">The message to display if the assertion fails.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		public static void IsNotEmpty(string value, string message = null, params object[] parameters)
		{
			if (string.Empty.Equals(value))
			{
				throw CreateExceptionWithExpectedActual("AssertEx.IsNotEmpty", "[non-empty string]", value, message, parameters);
			}
		}

		/// <summary>
		/// Verifies that the specified string is null or empty.  The assertion fails if it is not null or empty.  Optionally displays a message if the assertion fails, and applies the specified formatting to it.
		/// </summary>
		/// <param name="value">The string to verify is null or empty.</param>
		/// <param name="message">The message to display if the assertion fails.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		public static void IsNullOrEmpty(string value, string message = null, params object[] parameters)
		{
			if (!string.IsNullOrEmpty(value))
			{
				throw CreateExceptionWithExpectedActual("AssertEx.IsNullOrEmpty", new string[] { "[null]", string.Empty }, value, message, parameters);
			}
		}

		/// <summary>
		/// Verifies that the specified string is not null and not empty.  The assertion fails if it is null or empty.  Optionally displays a message if the assertion fails, and applies the specified formatting to it.
		/// </summary>
		/// <param name="value">The string to verify is not null and not empty.</param>
		/// <param name="message">The message to display if the assertion fails.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		public static void IsNotNullOrEmpty(string value, string message = null, params object[] parameters)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw CreateExceptionWithExpectedActual("AssertEx.IsNotNullOrEmpty", new string[] { "[not null]", "[non-empty string]" }, value, message, parameters);
			}
		}

		/// <summary>
		/// Verifies that the specified string is null or white space.  The assertion fails if it is not null or white space.  Optionally displays a message if the assertion fails, and applies the specified formatting to it.
		/// </summary>
		/// <param name="value">The string to verify is null or white space.</param>
		/// <param name="message">The message to display if the assertion fails.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		public static void IsNullOrWhiteSpace(string value, string message = null, params object[] parameters)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				throw CreateExceptionWithExpectedActual("AssertEx.IsNullOrWhiteSpace", new string[] { "[null]", "[whitespace]" }, value, message, parameters);
			}
		}

		/// <summary>
		/// Verifies that the specified string is not null and not white space.  The assertion fails if it is null or white space.  Optionally displays a message if the assertion fails, and applies the specified formatting to it.
		/// </summary>
		/// <param name="value">The string to verify is not null and not white space.</param>
		/// <param name="message">The message to display if the assertion fails.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		public static void IsNotNullOrWhiteSpace(string value, string message = null, params object[] parameters)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw CreateExceptionWithExpectedActual("AssertEx.IsNotNullOrWhiteSpace", new string[] { "[not null]", "[non-whitespace string]" }, value, message, parameters);
			}
		}

		#region Helper Methods

		/// <summary>
		/// <para>StringAssertEx methods call this method, when they fail, to package and throw an AssertFailedException.</para>
		/// <para>This method packages the expected and actual values and passes them on to CreateException.</para>
		/// </summary>
		/// <param name="assertionName">Name of the assertion.</param>
		/// <param name="expected">The first objects to compare.  Any one of these is the object the unit test expects.</param>
		/// <param name="actual">The second object to compare.  This is the object the unit test produced.</param>
		/// <param name="message">The message to display.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		protected internal static AssertFailedException CreateExceptionWithExpectedActual(string assertionName, string[] expected, string actual, string message, params object[] parameters)
		{
			return CreateException(assertionName, "Expected:" + string.Join(" or ", expected.Select(s => "<" + s + ">")) + ". Actual:<" + actual + ">. " + (message ?? string.Empty), parameters);
		}

		#endregion
	}
}