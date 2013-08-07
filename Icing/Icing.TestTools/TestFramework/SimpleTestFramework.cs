using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Icing.TestTools.TestFramework
{
	/// <summary>A simple test framework.</summary>
	/// <typeparam name="TTestClassAttribute">The attribute that defines a test class.</typeparam>
	/// <typeparam name="TTestMethodAttribute">The attribute that defines a test method.</typeparam>
	/// <typeparam name="TAssertFailedException">The type of exception that is thrown on failed asserts. Any other exception is considered <see cref="TestResultStatus.Inconclusive"/>.</typeparam>
	public class SimpleTestFramework<TTestClassAttribute, TTestMethodAttribute, TAssertFailedException>
		where TTestClassAttribute : Attribute
		where TTestMethodAttribute : Attribute
		where TAssertFailedException : Exception
	{
		/// <summary>Occurs when a test class is started.</summary>
		public event EventHandler<TestClassStartedEventArgs> TestClassStarted;

		/// <summary>Occurs when a test method is started.</summary>
		public event EventHandler<TestMethodStartedEventArgs> TestMethodStarted;

		/// <summary>Occurs when a test method is finished.</summary>
		public event EventHandler<TestMethodFinishedEventArgs> TestMethodFinished;

		/// <summary>Occurs when a test class is finished.</summary>
		public event EventHandler<TestClassFinishedEventArgs> TestClassFinished;

		/// <summary>
		/// Runs all tests in the given assembly that are <c>public</c>, marked with <typeparamref name="TTestMethodAttribute"/>, and contained in a class marked with <typeparamref name="TTestClassAttribute"/>.
		/// </summary>
		/// <param name="assembly">The assembly where tests can be found.</param>
		/// <returns>The results from the test run.</returns>
		public TestAssemblyResults RunTestsInAssembly(Assembly assembly)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			
			IEnumerable<TestClassResults> testClassResults = assembly
																				.GetTypes()
																				.Where(type => type.IsPublic && type.GetCustomAttributes(typeof(TTestClassAttribute), false).Any())
																				.Select(RunTestsInClass);
			
			stopwatch.Stop();

			return new TestAssemblyResults(testClassResults, stopwatch.Elapsed);
		}

		private TestClassResults RunTestsInClass(Type classType)
		{
			object classInstance = Activator.CreateInstance(classType);

			ConcurrentBag<TestMethodResult> testMethodResults = new ConcurrentBag<TestMethodResult>();

			ParallelQuery<MethodInfo> parallelMethodInfo = classType
																			.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
																			.Where(methodInfo => methodInfo.GetCustomAttributes(typeof(TTestMethodAttribute), false).Any())
																			.AsParallel();

			if (TestClassStarted != null) { TestClassStarted(this, new TestClassStartedEventArgs(classType.Name)); }

			Stopwatch stopwatch = Stopwatch.StartNew();

			parallelMethodInfo.ForAll(methodInfo => testMethodResults.Add(RunTest(methodInfo, classInstance)));

			stopwatch.Stop();

			TestClassResults testClassResults = new TestClassResults(classType.Name, testMethodResults, stopwatch.Elapsed);

			if (TestClassFinished != null) { TestClassFinished(this, new TestClassFinishedEventArgs(testClassResults)); }

			return testClassResults;
		}

		private TestMethodResult RunTest<TClass>(MethodInfo methodInfo, TClass classInstance)
		{
			TestResultStatus status = TestResultStatus.Passed;
			Exception exception = null;

			if (TestMethodStarted != null) { TestMethodStarted(this, new TestMethodStartedEventArgs(methodInfo.Name)); }

			Stopwatch stopwatch = Stopwatch.StartNew();

			try
			{
				methodInfo.Invoke(classInstance, null);
			}
			catch (Exception ex)
			{
				exception = ex.InnerException ?? ex;

				if (exception is TAssertFailedException)
				{
					status = TestResultStatus.Failed;
				}
				else
				{
					status = TestResultStatus.Inconclusive;
				}
			}

			stopwatch.Stop();

			TestMethodResult result = new TestMethodResult(methodInfo.Name, status, exception, stopwatch.Elapsed);

			if (TestMethodFinished != null) { TestMethodFinished(this, new TestMethodFinishedEventArgs(result)); }

			return result;
		}
	}
}