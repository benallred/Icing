using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.TestTools.MSTest
{
	/// <summary>
	/// <para>Verifies true/false propositions associated with exceptions in unit tests.</para>
	/// <para>Extends the <see cref="Assert"/> class.</para>
	/// </summary>
	public class ExceptionAssertEx : AssertEx
	{
		/// <summary>
		/// Verifies that the given action throws the specified exception.  The assertion fails if it does not throw the exception.  Optionally displays a message if the assertion fails, and applies the specified formatting to it.
		/// </summary>
		/// <typeparam name="TException">The type of exception that should be thrown.</typeparam>
		/// <param name="action">The action to verify throws the given exception.</param>
		/// <param name="message">The message to display if the assertion fails.  This message can be seen in the unit test results.</param>
		/// <param name="parameters">An array of parameters to use when formatting message.</param>
		public static void Throws<TException>(Action action, string message = null, params object[] parameters)
		{
			Type expectedExceptionType = typeof(TException);

			try
			{
				action();
			}
			catch (Exception actualException)
			{
				if (expectedExceptionType.IsInstanceOfType(actualException))
				{
					return; // SUCCESS
				}

				throw CreateExceptionWithExpectedActual("ExceptionAssertEx.Throws", expectedExceptionType.ToString(), actualException.GetType().ToString(), message, parameters);
			}

			throw CreateExceptionWithExpectedActual("ExceptionAssertEx.Throws", expectedExceptionType.ToString(), "[none]", message, parameters);
		}
	}
}