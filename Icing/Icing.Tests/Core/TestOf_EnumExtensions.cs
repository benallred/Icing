using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icing.Tests.Core
{
	[TestClass]
	public class TestOf_EnumExtensions
	{
		[TestMethod]
		public void GetDescription()
		{
			Assert.AreEqual(null, TestEnum.TestValue1.GetDescription());
			Assert.AreEqual("", TestEnum.TestValue2.GetDescription());
			Assert.AreEqual("Test description 3", TestEnum.TestValue3.GetDescription());
		}

		public enum TestEnum : short
		{
			TestValue1,
			[System.ComponentModel.Description]
			TestValue2,
			[System.ComponentModel.Description("Test description 3")]
			TestValue3
		}
	}
}