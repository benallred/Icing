namespace Icing.Diagnostics
{
	/// <summary>Calculates and formats running totals, averages, etc, for an arbitrary statistic.</summary>
	public class Stats
	{
		/// <summary>Gets or sets the number of expected iterations.</summary>
		/// <value>The number of expected iterations.</value>
		public int ExpectedIterations { get; set; }

		/// <summary>Gets or sets the total iterations.</summary>
		/// <value>The total iterations.</value>
		public int TotalIterations { get; set; }

		/// <summary>Gets or sets the total.</summary>
		/// <value>The total.</value>
		public double Total { get; set; }

		/// <summary>Gets or sets the smallest value reported so far.</summary>
		/// <value>The smallest value reported so far.</value>
		public double Min { get; set; }

		/// <summary>Gets or sets the largest value reported so far.</summary>
		/// <value>The largest value reported so far.</value>
		public double Max { get; set; }

		/// <summary>Gets the average.</summary>
		/// <value>The average.</value>
		public double Average
		{
			get
			{
				return Total / TotalIterations;
			}
		}

		/// <summary>A function that defines how a value should be formatted.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The formatted value.</returns>
		public delegate string Formatter(double value);

		/// <summary>Gets or sets the value formatter.</summary>
		/// <value>The value formatter.</value>
		public Formatter Format { get; set; }

		/// <summary>Gets or sets the average value formatter.</summary>
		/// <value>The average value formatter.</value>
		public Formatter FormatAverage { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Stats"/> class.
		/// </summary>
		/// <param name="expectedIterations">The number of expected iterations.</param>
		/// <param name="formatter">The value formatter.</param>
		/// <param name="averageFormatter">The average value formatter.</param>
		public Stats(int expectedIterations, Formatter formatter, Formatter averageFormatter)
		{
			ExpectedIterations = expectedIterations;
			TotalIterations = 0;
			Total = 0;
			Min = double.MaxValue;
			Max = double.MinValue;
			Format = formatter;
			FormatAverage = averageFormatter;
		}

		/// <summary>
		/// Adds the current value to the statistics.
		/// </summary>
		/// <param name="current">The current value.</param>
		public void Report(double current)
		{
			TotalIterations++;
			Total += current;
			if (current < Min) { Min = current; }
			if (current > Max) { Max = current; }
		}

		/// <summary>
		/// Adds the current value to the statistics and returns a summary of the current state.
		/// </summary>
		/// <param name="current">The current value.</param>
		/// <returns>A summary of the current state.</returns>
		public string ReportAndGetSummary(double current)
		{
			Report(current);
			return	"Current: " + Format        (current                     ) + "; " + ToString();
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return	"Total: "    + Format       (Total                       ) + "; " +
						"Min: "      + Format       (Min                         ) + "; " +
						"Max: "      + Format       (Max                         ) + "; " +
						"Avg: "      + FormatAverage(Average                     ) + "; " +
						"Expected: " + FormatAverage(Average * ExpectedIterations);
		}
	}
}