using System;
using System.Collections.Generic;
using System.Linq;

using Icing.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.Core.Diagnostics
{
	[TestClass]
	public class TestOf_Stats
	{
		private const int ExpectedIterations = 10;

		[TestMethod]
		public void Int32Stats()
		{
			Stats.Formatter format = d => d.ToString("n");
			Stats.Formatter formatAvg = format;

			Stats stats = new Stats(ExpectedIterations, format, formatAvg);

			for (int i = 0; i < ExpectedIterations; i++)
			{
				string report = stats.ReportAndGetSummary(i);
				IList<int> reported = Enumerable.Range(0, i + 1).ToList();
				Assert.AreEqual(	"Current: "  + format   (i                                      ) + "; " +
										"Total: "    + format   (reported.Sum()                         ) + "; " +
										"Min: "      + format   (0                                      ) + "; " +
										"Max: "      + format   (i                                      ) + "; " +
										"Avg: "      + formatAvg(reported.Average()                     ) + "; " +
										"Expected: " + formatAvg(reported.Average() * ExpectedIterations),
										report);
			}
		}

		[TestMethod]
		public void TimeSpanStats()
		{
			Stats.Formatter format    = d => TimeSpan.FromMilliseconds(d).ToString(    @"ss\.fffffff");
			Stats.Formatter formatAvg = d => TimeSpan.FromMilliseconds(d).ToString(@"mm\:ss\.fffffff");

			Stats stats = new Stats(ExpectedIterations, format, formatAvg);

			for (int i = 0; i < ExpectedIterations; i++)
			{
				string report = stats.ReportAndGetSummary(TimeSpan.FromSeconds(i).TotalMilliseconds);
				IList<int> reported = Enumerable.Range(0, i + 1).ToList();
				Assert.AreEqual(	"Current: "  + format   (TimeSpan.FromSeconds(i                                      ).TotalMilliseconds) + "; " +
										"Total: "    + format   (TimeSpan.FromSeconds(reported.Sum()                         ).TotalMilliseconds) + "; " +
										"Min: "      + format   (TimeSpan.FromSeconds(0                                      ).TotalMilliseconds) + "; " +
										"Max: "      + format   (TimeSpan.FromSeconds(i                                      ).TotalMilliseconds) + "; " +
										"Avg: "      + formatAvg(TimeSpan.FromSeconds(reported.Average()                     ).TotalMilliseconds) + "; " +
										"Expected: " + formatAvg(TimeSpan.FromSeconds(reported.Average() * ExpectedIterations).TotalMilliseconds),
										report);
			}
		}
	}
}