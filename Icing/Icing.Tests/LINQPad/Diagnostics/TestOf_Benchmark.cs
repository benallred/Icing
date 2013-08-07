using System;
using System.Collections.Generic;
using System.Threading;

using Icing.Diagnostics;
using Icing.LINQPad.Diagnostics;
using Icing.TestTools.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.LINQPad.Diagnostics
{
	[TestClass]
	public class TestOf_Benchmark
	{
		[TestMethod]
		public void CompareExecutionTime_Exceptions()
		{
			ExceptionAssertEx.Throws<ArgumentNullException>(() => Benchmark.CompareExecutionTime(1, false, (List<Algorithm>)null));
			ExceptionAssertEx.Throws<ArgumentNullException>(() => Benchmark.CompareExecutionTime(1, false, new List<Algorithm>()));
			ExceptionAssertEx.Throws<ArgumentNullException>(() => Benchmark.CompareExecutionTime(1, false));
			ExceptionAssertEx.Throws<ArgumentNullException>(() => Benchmark.CompareExecutionTime(1, false, (Algorithm[])null));
		}

		[TestMethod]
		public void CompareExecutionTime()
		{
			IList<Algorithm> ordered = Benchmark.CompareExecutionTime(1, false, new List<Algorithm>() { new Algorithm("Slower", () => Thread.Sleep(100)), new Algorithm("Faster", () => Thread.Sleep(1)) });

			Assert.AreEqual("Faster", ordered[0].Name);
			Assert.AreEqual("Slower", ordered[1].Name);

			ordered = Benchmark.CompareExecutionTime(1, false, new Algorithm("Slower", () => Thread.Sleep(100)), new Algorithm("Faster", () => Thread.Sleep(1)));

			Assert.AreEqual("Faster", ordered[0].Name);
			Assert.AreEqual("Slower", ordered[1].Name);
		}
	}
}