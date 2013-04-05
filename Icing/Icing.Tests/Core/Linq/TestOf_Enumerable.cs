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

			ExceptionAssertEx.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).Union(new int[] { }, i => i));
			ExceptionAssertEx.Throws<ArgumentNullException>(() => new int[] { }           .Union(null         , i => i));
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
	}
}