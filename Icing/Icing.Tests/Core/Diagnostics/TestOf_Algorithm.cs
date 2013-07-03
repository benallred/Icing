using System;
using System.Threading;

using Icing.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.Core.Diagnostics
{
	[TestClass]
	public class TestOf_Algorithm
	{
		[TestMethod]
		public void Constructor()
		{
			Algorithm algorithm;
			Action action;
			
			action = () => { };
			algorithm = new Algorithm("asdf", action);
			Assert.AreEqual("asdf", algorithm.Name);
			Assert.AreEqual(action, algorithm.Action);

			action = () => { 2.ToString(); };
			algorithm = new Algorithm(action);
			Assert.AreEqual("Algorithm 2", algorithm.Name);
			Assert.AreEqual(action, algorithm.Action);

			action = () => { };
			algorithm = new Algorithm("qwer", action);
			Assert.AreEqual("qwer", algorithm.Name);
			Assert.AreEqual(action, algorithm.Action);

			algorithm = new Algorithm(null, action);
			Assert.AreEqual("Algorithm 4", algorithm.Name);
			Assert.AreEqual(action, algorithm.Action);

			algorithm = new Algorithm("", action);
			Assert.AreEqual("Algorithm 5", algorithm.Name);
			Assert.AreEqual(action, algorithm.Action);

			algorithm = new Algorithm(" ", action);
			Assert.AreEqual("Algorithm 6", algorithm.Name);
			Assert.AreEqual(action, algorithm.Action);
		}

		[TestMethod]
		public void BenchmarkAndCacheExecutionTime()
		{
			Algorithm algorithm = new Algorithm(() => Thread.Sleep(10));

			Stats executionTime = algorithm.BenchmarkAndCacheExecutionTime(100, false);

			Assert.AreEqual(100, executionTime.TotalIterations);
			Assert.IsTrue(1000 < executionTime.Total && executionTime.Total < 1200);
			Assert.AreEqual(executionTime.Min, executionTime.Average);
			Assert.AreEqual(executionTime.Average, executionTime.Max);
		}

		[TestMethod]
		public void BenchmarkAndCacheExecutionTime_ReportIndividualIterations()
		{
			Algorithm algorithm = new Algorithm(() => Thread.Sleep(10));

			Stats executionTime = algorithm.BenchmarkAndCacheExecutionTime(100, true);

			Assert.AreEqual(100, executionTime.TotalIterations);
			Assert.IsTrue(1000 < executionTime.Total && executionTime.Total < 1200);
			Assert.IsTrue(executionTime.Min < executionTime.Average);
			Assert.IsTrue(executionTime.Average < executionTime.Max);
		}
		
		[TestMethod]
		public void BenchmarkAndCacheExecutionTime_Formatter()
		{
			Stats executionTime = new Algorithm(() => {}).BenchmarkAndCacheExecutionTime(1, false);

			executionTime.Total = 1000 * 60 * 60 * 24 + 1000 * 60 * 60 + 1000 * 60 + 1000 + 1;
			executionTime.TotalIterations = (int)executionTime.Total;
			executionTime.ExpectedIterations = executionTime.TotalIterations;
			executionTime.Min = 1000 * 60;
			executionTime.Max = 1000 * 60 * 60;

			Assert.AreEqual("Total: 01.01:01:01.001; Min: 01:00.000; Max: 01:00:00.000; Avg: 00.001; Expected: 01.01:01:01.001", executionTime.ToString());
		}
	}
}