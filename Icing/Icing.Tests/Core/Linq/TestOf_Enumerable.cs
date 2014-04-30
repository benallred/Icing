using System;
using System.Collections.Generic;
using System.Linq;

using Icing.Linq;
using Icing.TestTools.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.Core.Linq
{
	[TestClass]
	public class TestOf_Enumerable
	{
		[TestMethod]
		public void Union()
		{
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2       }.Union(new int[] {       3    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2       }.Union(new int[] {    2, 3    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2       }.Union(new int[] { 1,    3    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2, 3    }.Union(new int[] { 1, 2, 3    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3, 4 }, new int[] { 1,    3, 4 }.Union(new int[] { 1, 2,    4 }, i => i).ToList());

			CollectionAssert.AreEquivalent(new int[] {   }, new int[] {      }.Union(new int[] {   }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1    }.Union(new int[] {   }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] {      }.Union(new int[] { 1 }, i => i).ToList());

			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1, 1 }.Union(new int[] {      }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1, 1 }.Union(new int[] { 1    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] {      }.Union(new int[] { 1, 1 }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1    }.Union(new int[] { 1, 1 }, i => i).ToList());

			ExceptionAssertEx.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).Union(new int[] { }, i => i              ).ToList());
			ExceptionAssertEx.Throws<ArgumentNullException>(() => new int[] { }           .Union(null         , i => i              ).ToList());
			ExceptionAssertEx.Throws<ArgumentNullException>(() => new int[] { }           .Union(new int[] { }, (Func<int, int>)null).ToList());
		}

/*
		[TestMethod]
		public void Union()
		{
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2       }.Union(new int[] {       3    }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2       }.Union(new int[] {    2, 3    }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2       }.Union(new int[] { 1,    3    }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2, 3    }.Union(new int[] { 1, 2, 3    }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3, 4 }, new int[] { 1,    3, 4 }.Union(new int[] { 1, 2,    4 }, (i, j) => i == j).ToList());

			CollectionAssert.AreEquivalent(new int[] {   }, new int[] {      }.Union(new int[] {   }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1    }.Union(new int[] {   }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] {      }.Union(new int[] { 1 }, (i, j) => i == j).ToList());

			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1, 1 }.Union(new int[] {      }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1, 1 }.Union(new int[] { 1    }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] {      }.Union(new int[] { 1, 1 }, (i, j) => i == j).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1    }.Union(new int[] { 1, 1 }, (i, j) => i == j).ToList());

			ExceptionAssertEx.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).Union(new int[] { }, (i, j) => i == j));
			ExceptionAssertEx.Throws<ArgumentNullException>(() => new int[] { }           .Union(null         , (i, j) => i == j));

			CollectionAssert.AreEquivalent(new int[] { 1,    3    }, new int[] { 1, 2 }.Union(new int[] { 3, 4 }, (i, j) => i + 1 == j    ).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1,    3    }, new int[] { 3, 4 }.Union(new int[] { 1, 2 }, (i, j) => i + 1 == j    ).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2,    4 }, new int[] { 2, 1 }.Union(new int[] { 4, 3 }, (i, j) => i + 1 == j    ).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3, 4 }, new int[] { 4, 3 }.Union(new int[] { 2, 1 }, (i, j) => i + 1 == j    ).ToList());

			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2 }.Union(new int[] { 3, 4 }, (i, j) => i     == j + 1 ).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1,    3, 4 }, new int[] { 3, 4 }.Union(new int[] { 1, 2 }, (i, j) => i     == j + 1 ).ToList());
			CollectionAssert.AreEquivalent(new int[] {    2,    4 }, new int[] { 2, 1 }.Union(new int[] { 4, 3 }, (i, j) => i     == j + 1 ).ToList());
			CollectionAssert.AreEquivalent(new int[] {    2,    4 }, new int[] { 4, 3 }.Union(new int[] { 2, 1 }, (i, j) => i     == j + 1 ).ToList());
		}
*/
		
		[TestMethod]
		public void Intersect()
		{
			CollectionAssert.AreEquivalent(new int[] {            }, new int[] { 1, 2       }.Intersect(new int[] {       3    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] {    2       }, new int[] { 1, 2       }.Intersect(new int[] {    2, 3    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1          }, new int[] { 1, 2       }.Intersect(new int[] { 1,    3    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3    }, new int[] { 1, 2, 3    }.Intersect(new int[] { 1, 2, 3    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1,       4 }, new int[] { 1,    3, 4 }.Intersect(new int[] { 1, 2,    4 }, i => i).ToList());

			CollectionAssert.AreEquivalent(new int[] {   }, new int[] {      }.Intersect(new int[] {   }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] {   }, new int[] { 1    }.Intersect(new int[] {   }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] {   }, new int[] {      }.Intersect(new int[] { 1 }, i => i).ToList());

			CollectionAssert.AreEquivalent(new int[] {   }, new int[] { 1, 1 }.Intersect(new int[] {      }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1, 1 }.Intersect(new int[] { 1    }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] {   }, new int[] {      }.Intersect(new int[] { 1, 1 }, i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1 }, new int[] { 1    }.Intersect(new int[] { 1, 1 }, i => i).ToList());

			ExceptionAssertEx.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).Intersect(new int[] { }, i => i              ).ToList());
			ExceptionAssertEx.Throws<ArgumentNullException>(() => new int[] { }           .Intersect(null         , i => i              ).ToList());
			ExceptionAssertEx.Throws<ArgumentNullException>(() => new int[] { }           .Intersect(new int[] { }, (Func<int, int>)null).ToList());
		}
		
		[TestMethod]
		public void Distinct()
		{
			CollectionAssert.AreEquivalent(new int[] {         }, new int[] {         }.Distinct(i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1       }, new int[] { 1       }.Distinct(i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2    }, new int[] { 1, 2    }.Distinct(i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }.Distinct(i => i).ToList());

			CollectionAssert.AreEquivalent(new int[] { 1    }, new int[] { 1, 1       }.Distinct(i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2 }, new int[] { 1, 2, 1    }.Distinct(i => i).ToList());
			CollectionAssert.AreEquivalent(new int[] { 1, 2 }, new int[] { 1, 2, 2, 1 }.Distinct(i => i).ToList());

			ExceptionAssertEx.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).Distinct(i => i              ).ToList());
			ExceptionAssertEx.Throws<ArgumentNullException>(() => new int[] { }           .Distinct((Func<int, int>)null).ToList());
		}

		[TestMethod]
		public void LongRange()
		{
			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => Icing.Linq.Enumerable.LongRange(0, -1).ToArray());
			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => Icing.Linq.Enumerable.LongRange(Int64.MaxValue - Int32.MaxValue + 2, Int32.MaxValue).ToArray());
			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => Icing.Linq.Enumerable.LongRange(Int64.MaxValue, 2).ToArray());

			Assert.AreEqual(0, Icing.Linq.Enumerable.LongRange( 0, 0).LongCount());
			Assert.AreEqual(1, Icing.Linq.Enumerable.LongRange( 0, 1).LongCount());
			Assert.AreEqual(0, Icing.Linq.Enumerable.LongRange( 1, 0).LongCount());
			Assert.AreEqual(1, Icing.Linq.Enumerable.LongRange( 1, 1).LongCount());
			Assert.AreEqual(0, Icing.Linq.Enumerable.LongRange(-1, 0).LongCount());
			Assert.AreEqual(1, Icing.Linq.Enumerable.LongRange(-1, 1).LongCount());

			CollectionAssert.AreEquivalent(new long[] {    }, Icing.Linq.Enumerable.LongRange( 0, 0).ToArray());
			CollectionAssert.AreEquivalent(new long[] {  0 }, Icing.Linq.Enumerable.LongRange( 0, 1).ToArray());
			CollectionAssert.AreEquivalent(new long[] {    }, Icing.Linq.Enumerable.LongRange( 1, 0).ToArray());
			CollectionAssert.AreEquivalent(new long[] {  1 }, Icing.Linq.Enumerable.LongRange( 1, 1).ToArray());
			CollectionAssert.AreEquivalent(new long[] {    }, Icing.Linq.Enumerable.LongRange(-1, 0).ToArray());
			CollectionAssert.AreEquivalent(new long[] { -1 }, Icing.Linq.Enumerable.LongRange(-1, 1).ToArray());

			CollectionAssert.AreEquivalent(System.Linq.Enumerable.Range(  0,  10).Select(i => (long)i).ToArray(), Icing.Linq.Enumerable.LongRange(  0,  10).ToArray());
			CollectionAssert.AreEquivalent(System.Linq.Enumerable.Range( 10, 123).Select(i => (long)i).ToArray(), Icing.Linq.Enumerable.LongRange( 10, 123).ToArray());
			CollectionAssert.AreEquivalent(System.Linq.Enumerable.Range(-10,  10).Select(i => (long)i).ToArray(), Icing.Linq.Enumerable.LongRange(-10,  10).ToArray());
			CollectionAssert.AreEquivalent(System.Linq.Enumerable.Range(-10,  20).Select(i => (long)i).ToArray(), Icing.Linq.Enumerable.LongRange(-10,  20).ToArray());

			CollectionAssert.AreEquivalent(new long[] { 9876543210, 9876543211, 9876543212 }, Icing.Linq.Enumerable.LongRange(9876543210, 3).ToArray());

			Assert.AreEqual(1, Icing.Linq.Enumerable.LongRange(Int64.MaxValue, 1).LongCount());

			// These two asserts take a long time (no pun intended)
			Assert.AreEqual(Int32.MaxValue, Icing.Linq.Enumerable.LongRange(Int64.MaxValue - Int32.MaxValue, Int32.MaxValue).LongCount());
			Assert.AreEqual(Int32.MaxValue, Icing.Linq.Enumerable.LongRange(Int64.MaxValue - Int32.MaxValue + 1, Int32.MaxValue).LongCount());
		}
	}
}