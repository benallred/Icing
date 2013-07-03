using System;
using System.Diagnostics;

namespace Icing.Diagnostics
{
	/// <summary>Contains information about an algorithm and its performance.</summary>
	public class Algorithm
	{
		private static int TotalAlgorithms = 0;

		/// <summary>Gets or sets the name of the algorithm.</summary>
		/// <value>The name of the algorithm.</value>
		public string Name { get; set; }

		/// <summary>Gets or sets the action, or the code of the algorithm itself.</summary>
		/// <value>The action, or the code of the algorithm itself.</value>
		public Action Action { get; set; }

		/// <summary>Gets or sets the execution time stats of the algorithm.</summary>
		/// <value>The execution time stats of the algorithm.</value>
		public Stats ExecutionTime { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Algorithm"/> class.
		/// </summary>
		/// <param name="action">The action, or the code of the algorithm itself.</param>
		public Algorithm(Action action)
			: this(null, action)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Algorithm"/> class.
		/// </summary>
		/// <param name="name">The name of the algorithm.</param>
		/// <param name="action">The action, or the code of the algorithm itself.</param>
		public Algorithm(string name, Action action)
		{
			TotalAlgorithms++;

			if (name.IsNullOrWhiteSpace())
			{
				Name = "Algorithm " + TotalAlgorithms;
			}
			else
			{
				Name = name;
			}

			Action = action;
		}

		/// <summary>
		/// Benchmarks and caches (in <see cref="ExecutionTime"/>) the execution time stats of the algorithm.
		/// </summary>
		/// <param name="numberOfIterations">The number of iterations to run.</param>
		/// <param name="reportIndividualIterations">
		/// <para>If set to <c>true</c>, reports individual iteration stats; if <c>false</c>, reports average iteration stats.</para>
		/// <para>If the algorithm runs really fast, the floating point calculations will come out to zero, so you will want to set this to <c>false</c>.</para>
		/// </param>
		/// <returns>The execution time stats of the algorithm.</returns>
		public Stats BenchmarkAndCacheExecutionTime(int numberOfIterations, bool reportIndividualIterations)
		{
//			ExecutionTime = new Stats(numberOfIterations, d => TimeSpan.FromMilliseconds(d).ToString(@"ss\.fffffff") + " sec", d => (d / 1000).ToString() + " sec");
			Stats.Formatter formatter =
				d =>
				{
					string format = (d >= 1000 * 60 ? (d >= 1000 * 60 * 60 ? (d >= 1000 * 60 * 60 * 24 ? @"dd\." : "") + @"hh\:" : "") + @"mm\:" : "") + @"ss\.fff";
					return TimeSpan.FromMilliseconds(d).ToString(format);
				};
			ExecutionTime = new Stats(numberOfIterations, formatter, formatter);

			ExecutionTime = reportIndividualIterations
									? Benchmark_ReportIndividualIterations(numberOfIterations, ExecutionTime)
									: Benchmark                           (numberOfIterations, ExecutionTime);

			return ExecutionTime;
		}

		private Stats Benchmark(int numberOfIterations, Stats stats)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();

			for (int i = 0; i < numberOfIterations; i++)
			{
				Action.Invoke();
			}

			stopwatch.Stop();

			stats.Report(stopwatch.ElapsedMilliseconds);

			stats.TotalIterations = numberOfIterations;
			stats.Min = stats.Max = stats.Average;

			return stats;
		}

		private Stats Benchmark_ReportIndividualIterations(int numberOfIterations, Stats stats)
		{
			for (int i = 0; i < numberOfIterations; i++)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();

				Action.Invoke();
				
				stopwatch.Stop();
				
				stats.Report(stopwatch.ElapsedMilliseconds);
			}

			return stats;
		}
	}
}