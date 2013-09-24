using System;
using System.ComponentModel;

namespace Icing
{
	/// <summary><see cref="Enum"/> extensions.</summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Gets the description for the given enumeration value.
		/// </summary>
		/// <param name="value">The enumeration value.</param>
		/// <returns>The description for the given enumeration value.</returns>
		public static string GetDescription(this Enum value)
		{
			DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
			return (descriptionAttributes != null && descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : null);
		}
	}
}