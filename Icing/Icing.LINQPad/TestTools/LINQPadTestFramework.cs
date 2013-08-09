using System;
using System.Linq;
using System.Reflection;

using Icing.TestTools.TestFramework;

using LINQPad;

namespace Icing.LINQPad.TestTools
{
	/// <summary>A unit test framework for LINQPad.</summary>
	/// <typeparam name="TTestClassAttribute">The attribute that defines a test class.</typeparam>
	/// <typeparam name="TTestMethodAttribute">The attribute that defines a test method.</typeparam>
	/// <typeparam name="TAssertFailedException">The type of exception that is thrown on failed asserts. Any other exception is considered <see cref="TestResultStatus.Inconclusive"/>.</typeparam>
	/// <example>
	/// A sample LINQPad "My Extensions" file might look like this.
	/// <code>
	/// void Main()
	/// {
	///	new LINQPadTestFramework&lt;TestClassAttribute, TestMethodAttribute, AssertFailedException&gt;().RunTests();
	/// }
	/// 
	/// public static class StringExtensions
	/// {
	///	public static string Quoted(this string source, string beginQuote = "\"", string endQuote = null)
	///	{
	///		return beginQuote + source + (endQuote ?? beginQuote);
	///	}
	/// }
	/// 
	/// [TestClass]
	/// public class TestOf_StringExtensions
	/// {
	///	[TestMethod]
	///	public void Quoted()
	///	{
	///		Assert.AreEqual("\"asdf\"", StringExtensions.Quoted("asdf", "\"", null));
	///		Assert.AreEqual("'asdf'", StringExtensions.Quoted("asdf", "\'", null));
	///		Assert.AreEqual("(asdf)", StringExtensions.Quoted("asdf", "(", ")"));
	///	}
	/// }
	/// 
	/// public static class ObjectExtensions
	/// {
	///	public static bool NotEquals(this object current, object obj)
	///	{
	///		return !current.Equals(obj);
	///	}
	/// }
	/// 
	/// [TestClass]
	/// public class TestOf_ObjectExtensions
	/// {
	///	[TestMethod]
	///	public void NotEquals()
	///	{
	///		Assert.IsFalse(ObjectExtensions.NotEquals("test", "test"));
	///		Assert.IsTrue(ObjectExtensions.NotEquals("test", "test2"));
	/// 
	///		object o = new object();
	///		Assert.IsFalse(ObjectExtensions.NotEquals(o, o));
	///		Assert.IsTrue(ObjectExtensions.NotEquals(new object(), new object()));
	///	}
	/// }
	/// </code>
	/// </example>
	public class LINQPadTestFramework<TTestClassAttribute, TTestMethodAttribute, TAssertFailedException>
		where TTestClassAttribute : Attribute
		where TTestMethodAttribute : Attribute
		where TAssertFailedException : Exception
	{
		private SimpleTestFramework<TTestClassAttribute, TTestMethodAttribute, TAssertFailedException> SimpleTestFramework { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LINQPadTestFramework&lt;TTestClassAttribute, TTestMethodAttribute, TAssertFailedException&gt;"/> class.
		/// </summary>
		public LINQPadTestFramework()
		{
			SimpleTestFramework = new SimpleTestFramework<TTestClassAttribute, TTestMethodAttribute, TAssertFailedException>();

			SimpleTestFramework.TestClassStarted += ReportTestClassStarted;
			SimpleTestFramework.TestMethodFinished += ReportTestMethodFinished;
			SimpleTestFramework.TestClassFinished += ReportTestClassFinished;
		}

		/// <summary>
		/// <para>Runs all tests in the assembly compiled from the current .linq file.</para>
		/// <para>Test methods should be <c>public</c>, marked with <typeparamref name="TTestMethodAttribute"/>, and contained in a class marked with <typeparamref name="TTestClassAttribute"/>.</para>
		/// </summary>
		public void RunTests()
		{
			Util.AutoScrollResults = true;

			TestAssemblyResults testAssemblyResults = SimpleTestFramework.RunTestsInAssembly(Assembly.GetCallingAssembly());

			("Total test execution time: " + testAssemblyResults.ExecutionTime.ToString(@"ss\.fffffff")).Dump();

			if (testAssemblyResults.Status != TestResultStatus.Passed)
			{
				Util.HorizontalRun(false, Util.RawHtml("<span style='color:red;font-weight:bold;font-size:200%'>Tests failed</span>")).Dump();
				testAssemblyResults
					.TestClassResults
					.ToList()
					.ForEach(testClassResults => testClassResults
															.TestMethodResults
																.Where(testMethodResult => testMethodResult.Status != TestResultStatus.Passed)
																.ToList()
																.ForEach(testMethodResult => Util.VerticalRun("",
																																Util.HorizontalRun(false,
																																							"\t",
																																							testClassResults.ClassName + "." + testMethodResult.MethodName,
																																							"\t",
																																							testMethodResult.Status,
																																							"\t",
																																							(testMethodResult.Exception is TargetParameterCountException
																																								? "The signature for this test method may contain parameters. Test method signatures should not contain parameters.\r\n"
																																								: "") +
																																								testMethodResult.Exception.Message +
																																								" (" + testMethodResult.Exception.GetType().Name + ")" +
																																								"\r\n" + testMethodResult.Exception.StackTrace)).Dump()));
			}
			else
			{
				Util.HorizontalRun(false, Util.RawHtml("<span style='color:green;font-weight:bold;font-size:200%'>All tests passed</span>")).Dump();
			}
		}

		private static void ReportTestClassStarted(object sender, TestClassStartedEventArgs e)
		{
			(e.ClassName).Dump();
		}

		private static void ReportTestMethodFinished(object sender, TestMethodFinishedEventArgs e)
		{
			Util.HorizontalRun(false, "\t", e.Result.ExecutionTime.ToString(@"ss\.fffffff"), "\t", Util.RawHtml(string.Format("{0}{1}{2}", e.Result.Status != TestResultStatus.Passed ? "<span style='color:red;font-weight:bold'>" : "", e.Result.Status, e.Result.Status != TestResultStatus.Passed ? "</span>" : "")), "\t", e.Result.MethodName, "\t", e.Result.Exception != null ? e.Result.Exception.Message + " (" + e.Result.Exception.GetType().Name + ")" : "").Dump();
/*
			Util.HorizontalRun(false, "\t", e.Result.ExecutionTime.ToString(@"ss\.fffffff"), "\t", Util.RawHtml(string.Format("{0}{1}{2}", e.Result.Status != TestResultStatus.Passed ? "<span style='color:red;font-weight:bold'>" : "", e.Result.Status, e.Result.Status != TestResultStatus.Passed ? "</span>" : "")), "\t", e.Result.MethodName).Dump();
			if (e.Result.Exception != null)
			{
				Util.HorizontalRun(false, new String('\t', 10), e.Result.Exception.Message).Dump();
			}
*/
		}

		private static void ReportTestClassFinished(object sender, TestClassFinishedEventArgs e)
		{
			("\t" + e.Results.ExecutionTime.ToString(@"ss\.fffffff") + "\t(total parallel execution time)").Dump();
			"".Dump();
//			e.Results.TestMethodResults.Select(result => new { result.MethodName, Status = Util.RawHtml(string.Format("{0}{1}{2}", result.Status != TestResultStatus.Passed ? "<span style='color:red;font-weight:bold'>" : "", result.Status, result.Status != TestResultStatus.Passed ? "</span>" : "")), ExecutionTime = result.ExecutionTime.ToString(@"ss\.fffffff"), Exception = result.Exception != null ? result.Exception.Message : "" }).Dump();
		}
	}
}