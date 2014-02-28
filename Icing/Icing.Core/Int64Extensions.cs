using System;

namespace Icing
{
	/// <summary><see cref="Int64"/> extensions.</summary>
	public static class Int64Extensions
	{
		/// <summary>
		/// Returns a string that right-aligns the digits in this number by padding them on the left with a specified Unicode character,
		/// for a total length equal to that of the length of the maximum number to match.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="maxNumberToMatch">The maximum number to match.</param>
		/// <param name="paddingChar">A Unicode padding character.</param>
		/// <returns>A string representation of this number, but right-aligned and padded on the left with as many paddingChar characters
		/// as needed to create a length equal to that of the length of the maximum number to match.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="source"/> is less than zero.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxNumberToMatch"/> is less than zero.</exception>
		public static string PadNumber(this long source, long maxNumberToMatch, char paddingChar = '0')
		{
			if (source < 0)
			{
				throw new ArgumentOutOfRangeException("source");
			}

			return source.ToString().PadNumber(maxNumberToMatch, paddingChar);
		}
	}
}