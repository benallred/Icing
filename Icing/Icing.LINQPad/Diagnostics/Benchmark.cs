using System;
using System.Collections.Generic;
using System.Linq;

using Icing.Diagnostics;

using LINQPad;

namespace Icing.LINQPad.Diagnostics
{
	/// <summary>Helpers for benchmarking the performance of algorithms.</summary>
	public static class Benchmark
	{
		/// <summary>
		/// Benchmarks and compares the execution time stats of the given algorithms.
		/// </summary>
		/// <param name="numberOfIterations">The number of iterations to run.</param>
		/// <param name="reportIndividualIterations">
		/// <para>If set to <c>true</c>, reports individual iteration stats; if <c>false</c>, reports average iteration stats.</para>
		/// <para>If the algorithm runs really fast, the floating point calculations will come out to zero, so you will want to set this to <c>false</c>.</para>
		/// </param>
		/// <param name="algorithms">The algorithms to compare.</param>
		/// <returns>The list of algorithms, sorted from fastest to slowest, with their <see cref="Algorithm.ExecutionTime"/> property set.</returns>
		public static IList<Algorithm> CompareExecutionTime(int numberOfIterations, bool reportIndividualIterations, IList<Algorithm> algorithms)
		{
			if (algorithms == null || !algorithms.Any())
			{
				throw new ArgumentNullException("algorithms");
			}
			
			foreach (Algorithm algorithm in algorithms)
			{
				("Running " + algorithm.Name).Dump();
				algorithm.BenchmarkAndCacheExecutionTime(numberOfIterations, reportIndividualIterations);
				"\tDone".Dump();
			}

			"".Dump();
			("Total iterations: " + numberOfIterations.ToString("n0")).Dump();
			"".Dump();
			
			algorithms = algorithms.OrderBy(algorithm => algorithm.ExecutionTime.Average).ToList();
			
			for (int i = 0; i < algorithms.Count; i++)
			{
				DumpAlgorithmStats(algorithms[i], i + 1 < algorithms.Count ? algorithms[i + 1] : null, reportIndividualIterations);
			}
			
			return algorithms;
		}

		/// <summary>
		/// Benchmarks and compares the execution time stats of the given algorithms.
		/// </summary>
		/// <param name="numberOfIterations">The number of iterations to run.</param>
		/// <param name="reportIndividualIterations">
		/// <para>If set to <c>true</c>, reports individual iteration stats; if <c>false</c>, reports average iteration stats.</para>
		/// <para>If the algorithm runs really fast, the floating point calculations will come out to zero, so you will want to set this to <c>false</c>.</para>
		/// </param>
		/// <param name="algorithms">The algorithms to compare.</param>
		/// <returns>The list of algorithms, sorted from fastest to slowest, with their <see cref="Algorithm.ExecutionTime"/> property set.</returns>
		public static IList<Algorithm> CompareExecutionTime(int numberOfIterations, bool reportIndividualIterations, params Algorithm[] algorithms)
		{
			return CompareExecutionTime(numberOfIterations, reportIndividualIterations, (IList<Algorithm>)algorithms);
		}

		private static void DumpAlgorithmStats(Algorithm algorithm, Algorithm compareTo, bool reportIndividualIterations)
		{
			(algorithm.Name + "\n\tTotal time: " + algorithm.ExecutionTime.Format(algorithm.ExecutionTime.Total) + "\tAverage time: " + algorithm.ExecutionTime.FormatAverage(algorithm.ExecutionTime.Average) +
				(compareTo != null ? "\t\t" + (compareTo.ExecutionTime.Total / algorithm.ExecutionTime.Total).ToString("n2") + " times (" + Math.Abs(100 * (algorithm.ExecutionTime.Total - compareTo.ExecutionTime.Total) / compareTo.ExecutionTime.Total).ToString("n2") + "%) faster than " + compareTo.Name : "")).Dump();
		}
	}
}