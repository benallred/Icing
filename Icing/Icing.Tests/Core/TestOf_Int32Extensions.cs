using System;

using Icing.TestTools.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.Core
{
	[TestClass]
	public class TestOf_Int32Extensions
	{
		[TestMethod]
		public void PadNumber()
		{
			Assert.AreEqual(  "1",   1.PadNumber(0, '0'));
			Assert.AreEqual( "11",  11.PadNumber(0, '0'));
			Assert.AreEqual("111", 111.PadNumber(0, '0'));

			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => Int32Extensions.PadNumber( 1, -1, '0'));
			ExceptionAssertEx.Throws<ArgumentOutOfRangeException>(() => Int32Extensions.PadNumber(-1,  1, '0'));

			Assert.AreEqual( "0", 0.PadNumber( 1, 'a'));
			Assert.AreEqual("a0", 0.PadNumber(10, 'a'));

			for (int power = 1; power < 4; power++)
			{
				for (int i = (int)Math.Pow(10, power - 1); i < Math.Pow(10, power); i++)
				{
					Assert.AreEqual(  1.ToString().PadLeft(power, '0'),   1.PadNumber(i, '0'));
					Assert.AreEqual( 11.ToString().PadLeft(power, '0'),  11.PadNumber(i, '0'));
					Assert.AreEqual(111.ToString().PadLeft(power, '0'), 111.PadNumber(i, '0'));

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