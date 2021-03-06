﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Icing.Linq
{
	/// <summary>Provides a set of static (Shared in Visual Basic) methods for querying objects that implement <see cref="IEnumerable&lt;T&gt;"/>.</summary>
	public static class Enumerable
	{
		#region Methods

		/// <summary>
		/// Produces the set union of two sequences according to a key.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
		/// <param name="first">An <see cref="IEnumerable&lt;T&gt;"/> whose distinct elements form the first set for the union.</param>
		/// <param name="second">An <see cref="IEnumerable&lt;T&gt;"/> whose distinct elements form the second set for the union.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="first"/>, <paramref name="second"/>, or <paramref name="keySelector"/> is null.</exception>
		public static IEnumerable<TSource> Union<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
		{
//			return first.Union(second, new LambdaEqualityComparer<TSource>((x, y) => keySelector(x).Equals(keySelector(y))));
			return first.Union(second, new KeyEqualityComparer<TSource, TKey>(keySelector));
		}

/*
		/// <summary>
		/// Produces the set union of two sequences by using a predicate to compare.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <param name="keySelector"/>.</typeparam>
		/// <param name="first">An <see cref="IEnumerable&lt;T&gt;"/> whose distinct elements form the first set for the union.</param>
		/// <param name="second">An <see cref="IEnumerable&lt;T&gt;"/> whose distinct elements form the second set for the union.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">The predicate to compare values.</param>
		/// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="first"/>, <paramref name="second"/>, <paramref name="keySelector"/>, or <paramref name="comparer"/> is null</exception>
		public static IEnumerable<TSource> Union<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector, Func<TKey, TKey, bool> comparer)
		{
			return first.Union(second, new LambdaEqualityComparer<TSource>((x, y) => comparer(keySelector(x), keySelector(y))));
		}
*/

		/// <summary>
		/// Produces the set intersection of two sequences according to a key.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
		/// <param name="first">An <see cref="IEnumerable&lt;T&gt;"/> whose distinct elements that also appear in <paramref name="second"/> will be returned.</param>
		/// <param name="second">An <see cref="IEnumerable&lt;T&gt;"/> whose distinct elements that also appear in the first sequence will be returned.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="first"/>, <paramref name="second"/>, or <paramref name="keySelector"/> is null.</exception>
		public static IEnumerable<TSource> Intersect<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
		{
			return first.Intersect(second, new KeyEqualityComparer<TSource, TKey>(keySelector));
		}

		/// <summary>
		/// Returns distinct elements from a sequence according to a key.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains distinct elements from the source sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.Distinct(new KeyEqualityComparer<TSource, TKey>(keySelector));
		}

		/// <summary>
		/// Generates a sequence of integral numbers within a specified range.
		/// </summary>
		/// <param name="start">The value of the first integer in the sequence.</param>
		/// <param name="count">The number of sequential integers to generate.</param>
		/// <returns>An <see cref="IEnumerable&lt;Int64&gt;"/> in C# or IEnumerable(Of Int64) in Visual Basic that contains a range of sequential integral numbers.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0.-or-<paramref name="start"/> + <paramref name="count"/> -1 is larger than <see cref="Int64.MaxValue"/>.</exception>
		public static IEnumerable<long> LongRange(long start, int count)
		{
			long num = start + count - 1L;

			if (count < 0 || num > long.MaxValue || (start > 0 && num < 0)) // last condition checks for overflow/wrapping around to a negative long
			{
				throw new ArgumentOutOfRangeException("count");
			}
			else
			{
				for (long i = 0; i < count; i++)
				{
					yield return start + i;
				}
			}
		}

		#endregion

		#region Helper Classes

		/// <summary>Defines methods to support the comparison of objects for equality.</summary>
		/// <typeparam name="TSource">The type of objects from which to select a key.</typeparam>
		/// <typeparam name="TKey">The type of objects to compare.</typeparam>
		private class KeyEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
		{
			/// <summary>Gets or sets the key selector.</summary>
			/// <value>The key selector.</value>
			private Func<TSource, TKey> KeySelector { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="KeyEqualityComparer&lt;TSource, TKey&gt;"/> class.
			/// </summary>
			/// <param name="keySelector">The key selector.</param>
			public KeyEqualityComparer(Func<TSource, TKey> keySelector)
			{
				if (keySelector == null)
				{
					throw new ArgumentNullException("keySelector");
				}

				KeySelector = keySelector;
			}

			/// <summary>
			/// Determines whether the specified objects are equal.
			/// </summary>
			/// <returns><c>true</c> if the specified objects are equal; otherwise, <c>false</c>.</returns>
			/// <param name="x">The first object of type T to compare.</param>
			/// <param name="y">The second object of type T to compare.</param>
			public bool Equals(TSource x, TSource y)
			{
				return KeySelector(x).Equals(KeySelector(y));
			}

			/// <summary>
			/// Returns a hash code for the specified object.
			/// </summary>
			/// <returns>A hash code for the specified object.</returns>
			/// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param>
			/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
			public int GetHashCode(TSource obj)
			{
				return obj.GetHashCode();
			}
		}

/*
		/// <summary>Defines methods to support the comparison of objects for equality.</summary>
		/// <typeparam name="T">
		///	<para>The type of objects to compare.</para>
		///	<para>This type parameter is contravariant. That is, you can use either the type you specified or any type
		///		that is less derived. For more information about covariance and contravariance, see
		///		<a href="http://msdn.microsoft.com/en-us/library/dd799517.aspx">Covariance and Contravariance in Generics</a>.</para>
		/// </typeparam>
		private class LambdaEqualityComparer<T> : IEqualityComparer<T>
		{
			/// <summary>Gets or sets the equality comparer.</summary>
			/// <value>The equality comparer.</value>
			private Func<T, T, bool> EqualityComparer { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="LambdaEqualityComparer&lt;T&gt;"/> class.
			/// </summary>
			/// <param name="equalityComparer">The equality comparer.</param>
			public LambdaEqualityComparer(Func<T, T, bool> equalityComparer)
			{
				EqualityComparer = equalityComparer;
			}

			/// <summary>
			/// Determines whether the specified objects are equal.
			/// </summary>
			/// <returns><c>true</c> if the specified objects are equal; otherwise, <c>false</c>.</returns>
			/// <param name="x">The first object of type T to compare.</param>
			/// <param name="y">The second object of type T to compare.</param>
			public bool Equals(T x, T y)
			{
				return EqualityComparer(x, y);
			}

			/// <summary>
			/// Returns a hash code for the specified object.
			/// </summary>
			/// <returns>A hash code for the specified object.</returns>
			/// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param>
			/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
			public int GetHashCode(T obj)
			{
				// If the hash codes are different, then Equals never gets called. Make sure Equals is always called by making sure the hash codes are always the same.
				// (Underneath, the .NET code is using a set and the not (!) of a Find method to determine if the set doesn't already contain the item and should be added.
				// Find is not bothering to call Equals unless it finds a hash code that matches.)
//				return obj.GetHashCode();
				return 0;
			}
		}
*/

		#endregion
	}
}