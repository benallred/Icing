using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Icing
{
	/// <summary><see cref="String"/> extensions.</summary>
	public static class StringExtensions
	{
		#region Const, Var, and Properties

		/// <summary>The group name for the replacement token used in FormatRegex.</summary>
		private const string FormatTokenGroupName = "token";

		/// <summary>The regular expression used when calling Format.</summary>
		private static readonly Regex FormatRegex = new Regex(@"(?<!\{)\{(?<" + FormatTokenGroupName + @">\w+)\}(?!\})", RegexOptions.Compiled);

		#endregion

		#region Methods

		/// <summary>
		/// Indicates whether the current string is null or an System.String.Empty string.
		/// </summary>
		/// <param name="source">The string to test.</param>
		/// <returns><c>true</c> if the current string is null or an System.String.Empty string; otherwise, <c>false</c>.</returns>
		public static bool IsNullOrEmpty(this string source)
		{
			return string.IsNullOrEmpty(source);
		}

		/// <summary>
		/// Indicates whether the current string is not null and not an System.String.Empty string.
		/// </summary>
		/// <param name="source">The string to test.</param>
		/// <returns><c>true</c> if the current string is not null and not an System.String.Empty string; otherwise, <c>false</c>.</returns>
		public static bool IsNotNullOrEmpty(this string source)
		{
			return !string.IsNullOrEmpty(source);
		}

		/// <summary>
		/// Indicates whether the current string is null, empty, or consists only of white-space characters.
		/// </summary>
		/// <param name="source">The string to test.</param>
		/// <returns><c>true</c> if the current string is null, empty, or consists only of white-space characters; otherwise, <c>false</c>.</returns>
		public static bool IsNullOrWhiteSpace(this string source)
		{
			return string.IsNullOrWhiteSpace(source);
		}

		/// <summary>
		/// Indicates whether the current string is not null, not empty, and does not consist only of white-space characters.
		/// </summary>
		/// <param name="source">The string to test.</param>
		/// <returns><c>true</c> if the current string is not null, not empty, and does not consist only of white-space characters; otherwise, <c>false</c>.</returns>
		public static bool IsNotNullOrWhiteSpace(this string source)
		{
			return !string.IsNullOrWhiteSpace(source);
		}

		/// <summary>
		/// Determines whether the end of this string instance does not match the specified string.
		/// </summary>
		/// <param name="source">The string to test.</param>
		/// <param name="value">The string to compare to the substring at the end of this instance.</param>
		/// <returns><c>true</c> if the end of this string instance does not match the specified string; otherwise, <c>false</c>.</returns>
		public static bool DoesNotEndWith(this string source, string value)
		{
			return !source.EndsWith(value);
		}

		/// <summary>
		/// Returns the current string instance with the specified value appended, if the current string instance does not already end with the specified value.
		/// </summary>
		/// <param name="source">The source string.</param>
		/// <param name="value">The value to append.</param>
		/// <returns>The current string instance with the specified value appended, if the current string instance does not already end with the specified value.</returns>
		public static string EnsureEndsWith(this string source, string value)
		{
			return source + (source.EndsWith(value) ? "" : value);
		}

		/// <summary>
		/// <para>Returns a new string in which all occurrences of the specified tokens in the current instance are replaced with the provided replacement values.</para>
		/// <para>A token is designated in a string by surrounding it with curly braces (example: {MyToken}).</para>
		/// <para>A token must consist of one or more valid "word characters" (think valid variable name) and is case-sensitive.</para>
		/// <para>Curly braces can be escaped by using double braces.  The double braces will be
		///		replaced with single braces (example: "{{something}}" becomes "{something}").</para>
		/// <para>Any token without a replacement value specified will be untouched and appear in the returned string exactly as in the source.</para>
		/// </summary>
		/// <param name="source">The source string.</param>
		/// <param name="replacements">The replacement values - must be an anonymous type.</param>
		/// <returns>A new string in which all occurrences of the specified tokens in the current instance are replaced with the provided replacement values.</returns>
		public static string Format(this string source, object replacements)
		{
			if (source.IsNullOrWhiteSpace() || replacements == null)
			{
				return source;
			}

			IDictionary<string, string> replacementsDictionary = new Dictionary<string, string>();

			foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(replacements))
			{
				string token = propertyDescriptor.Name;
				object value = propertyDescriptor.GetValue(replacements);

				replacementsDictionary.Add(token, (value != null ? value.ToString() : String.Empty));
			}

			return Format(source, replacementsDictionary);
		}

		/// <summary>
		/// <para>Returns a new string in which all occurrences of the specified tokens in the current instance are replaced with the provided replacement values.</para>
		/// <para>A token is designated in a string by surrounding it with curly braces (example: {MyToken}).</para>
		/// <para>A token must consist of one or more valid "word characters" (think valid variable name) and is case-sensitive.</para>
		/// <para>Curly braces can be escaped by using double braces.  The double braces will be
		///		replaced with single braces (example: "{{something}}" becomes "{something}").</para>
		/// <para>Any token without a replacement value specified will be untouched and appear in the returned string exactly as in the source.</para>
		/// </summary>
		/// <param name="source">The source string.</param>
		/// <param name="replacements">The replacement values.</param>
		/// <returns>A new string in which all occurrences of the specified tokens in the current instance are replaced with the provided replacement values.</returns>
		public static string Format(this string source, IDictionary<string, string> replacements)
		{
			if (source.IsNullOrWhiteSpace() || replacements == null)
			{
				return source;
			}

			string replaced = replacements.Aggregate(source,
				(current, pair) =>
					FormatRegex.Replace(current,
						new MatchEvaluator(match =>
							(match.Groups[FormatTokenGroupName].Value == pair.Key
								? pair.Value : match.Value))));

			return replaced.Replace("{{", "{").Replace("}}", "}");
		}

		/// <summary>
		/// Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character,
		/// for a total length equal to that of the length of the maximum number to match.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="maxNumberToMatch">The maximum number to match.</param>
		/// <param name="paddingChar">A Unicode padding character.</param>
		/// <returns>A new string that is equivalent to this instance, but right-aligned and padded on the left with as many paddingChar characters
		/// as needed to create a length equal to that of the length of the maximum number to match. However, if the length of the maximum number
		/// to match is less than the length of this instance, the method returns a reference to the existing instance. If the length of the
		/// maximum number to match is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxNumberToMatch"/> is less than zero.</exception>
		public static string PadNumber(this string source, int maxNumberToMatch, char paddingChar = '0')
		{
			if (maxNumberToMatch == 0)
			{
				maxNumberToMatch = 1;
			}
			else if (maxNumberToMatch < 0)
			{
//				maxNumberToMatch = Math.Abs(maxNumberToMatch);
				throw new ArgumentOutOfRangeException("maxNumberToMatch");
			}

			return source.PadLeft(((int)Math.Log10(maxNumberToMatch)) + 1, paddingChar);
		}

		/// <summary>
		/// Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character,
		/// for a total length equal to that of the length of the maximum number to match.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="maxNumberToMatch">The maximum number to match.</param>
		/// <param name="paddingChar">A Unicode padding character.</param>
		/// <returns>A new string that is equivalent to this instance, but right-aligned and padded on the left with as many paddingChar characters
		/// as needed to create a length equal to that of the length of the maximum number to match. However, if the length of the maximum number
		/// to match is less than the length of this instance, the method returns a reference to the existing instance. If the length of the
		/// maximum number to match is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxNumberToMatch"/> is less than zero.</exception>
		public static string PadNumber(this string source, uint maxNumberToMatch, char paddingChar = '0')
		{
			if (maxNumberToMatch == 0)
			{
				maxNumberToMatch = 1;
			}

			return source.PadLeft(((int)Math.Log10(maxNumberToMatch)) + 1, paddingChar);
		}

		/// <summary>
		/// Converts the current string representation of a number to its 32-bit signed integer equivalent.
		/// </summary>
		/// <param name="source">The source containing a number to convert.</param>
		/// <returns>A 32-bit signed integer equivalent to the number contained in <paramref name="source"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
		/// <exception cref="FormatException"><paramref name="source"/> is not in the correct format.</exception>
		/// <exception cref="OverflowException"><paramref name="source"/> represents a number less than <see cref="Int32.MinValue"/> or greater than <see cref="Int32.MaxValue"/>.</exception>
		public static int ToInt32(this string source)
		{
			return Int32.Parse(source);
		}

		/// <summary>
		/// Removes all leading and trailing occurrences of the given string from the current <see cref="String"/> object.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="trim">The string to remove.</param>
		/// <returns>The string that remains after all occurrences of the <paramref name="trim"/> parameter are removed
		/// from the start and end of the current string.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="trim"/> is null or empty.</exception>
		public static string Trim(this string source, string trim)
		{
			if (trim.IsNullOrEmpty())
			{
				throw new ArgumentNullException("trim");
			}

			string trimmed = source;
			int lengthOfTrim = trim.Length;

			while (trimmed.StartsWith(trim))
			{
				trimmed = trimmed.Remove(0, lengthOfTrim);
			}

			while (trimmed.EndsWith(trim))
			{
				trimmed = trimmed.Remove(trimmed.Length - lengthOfTrim);
			}

			return trimmed;
		}

		/// <summary>
		/// Reverses the sequence of the characters in the entire string.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>The reverse of the current string.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
		public static string Reverse(this string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			char[] chars = source.ToCharArray();
			Array.Reverse(chars);
			return new string(chars);
		}

		#endregion
	}
}