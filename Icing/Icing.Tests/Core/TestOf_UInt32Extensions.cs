using System;

using Icing.TestTools.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.Core
{
	[TestClass]
	public class TestOf_UInt32Extensions
	{
		[TestMethod]
		public void PadNumber()
		{
			Assert.AreEqual(  "1",   1u.PadNumber(0, '0'));
			Assert.AreEqual( "11",  11u.PadNumber(0, '0'));
			Assert.AreEqual("111", 111u.PadNumber(0, '0'));

//			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => UInt32Extensions.PadNumber( 1u, -1, '0')); // Not valid
//			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => UInt32Extensions.PadNumber(-1u,  1, '0')); // Not valid

			Assert.AreEqual( "0", 0u.PadNumber( 1, 'a'));
			Assert.AreEqual("a0", 0u.PadNumber(10, 'a'));

			for (int power = 1; power < 4; power++)
			{
				for (uint i = (uint)Math.Pow(10, power - 1); i < Math.Pow(10, power); i++)
				{
					Assert.AreEqual(  1.ToString().PadLeft(power, '0'),   1u.PadNumber(i, '0'));
					Assert.AreEqual( 11.ToString().PadLeft(power, '0'),  11u.PadNumber(i, '0'));
					Assert.AreEqual(111.ToString().PadLeft(power, '0'), 111u.PadNumber(i, '0'));

/*
					Assert.AreEqual(  1.ToString().PadLeft(power, '0'),   1.PadNumber(-1 * i, '0'));
					Assert.AreEqual( 11.ToString().PadLeft(power, '0'),  11.PadNumber(-1 * i, '0'));
					Assert.AreEqual(111.ToString().PadLeft(power, '0'), 111.PadNumber(-1 * i, '0'));
*/
				}
			}
		}
	}
}