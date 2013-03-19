using System.Globalization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.TestTools.MSTest
{
	/// <summary>Extends the <see cref="Assert"/> class.</summary>
	public class AssertEx
	{
		/// <summary>
		/// AssertEx methods call this method, when they fail, to package and throw an AssertFailedException.
		/// </summary>
		/// <param name="assertionName">Name of the assertion.</param>
		/// <param name="message">The message to display.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		protected internal static AssertFailedException CreateException(string assertionName, string message, params object[] parameters)
		{
			string formattedMessage = "";

			if (!string.IsNullOrEmpty(message))
			{
				if (parameters == null)
				{
					formattedMessage = ReplaceNulls(message);
				}
				else
				{
					formattedMessage = string.Format(CultureInfo.CurrentCulture, ReplaceNulls(message), parameters);
				}
			}

			return new AssertFailedException(string.Format("{0} failed. {1}", assertionName, formattedMessage));
		}

		/// <summary>
		/// <para>AssertEx methods call this method, when they fail, to package and throw an AssertFailedException.</para>
		/// <para>This method packages the expected and actual values and passes them on to CreateException.</para>
		/// </summary>
		/// <param name="assertionName">Name of the assertion.</param>
		/// <param name="expected">The first object to compare.  This is the object the unit test expects.</param>
		/// <param name="actual">The second object to compare.  This is the object the unit test produced.</param>
		/// <param name="message">The message to display.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		protected internal static AssertFailedException CreateExceptionWithExpectedActual(string assertionName, string expected, string actual, string message, params object[] parameters)
		{
			return CreateException(assertionName, "Expected:<" + expected + ">. Actual:<" + actual + ">. " + (message ?? string.Empty), parameters);
		}

		/// <summary>
		/// In a string, replaces nulls with a common string.
		/// </summary>
		/// <param name="input">The string in which to search for and replace nulls.</param>
		/// <returns>The given string with nulls replaced with a common string.</returns>
		protected internal static string ReplaceNulls(object input)
		{
			return (input == null ? "(null)" : Assert.ReplaceNullChars(input.ToString()));
		}
	}
}