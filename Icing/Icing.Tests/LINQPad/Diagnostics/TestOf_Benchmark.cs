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
			ExceptionAssertEx.Throws<ArgumentNullException>(() => Benchmark.CompareExecutionTime(1, false, null));
			ExceptionAssertEx.Throws<ArgumentNullException>(() => Benchmark.CompareExecutionTime(1, false, new List<Algorithm>()));
		}

		[TestMethod]
		public void CompareExecutionTime()
		{
			List<Algorithm> ordered = Benchmark.CompareExecutionTime(1, false, new List<Algorithm>() { new Algorithm("Slower", () => Thread.Sleep(10)), new Algorithm("Faster", () => Thread.Sleep(1)) });

			Assert.AreEqual("Faster", ordered[0].Name);
			Assert.AreEqual("Slower", ordered[1].Name);
		}
	}
}