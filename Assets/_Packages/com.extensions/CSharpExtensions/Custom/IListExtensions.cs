using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public static class IListExtensions
    {

		/// <summary>
		/// Shuffles a list's elements using Unity's random number generator.
		/// Source: https://stackoverflow.com/a/1262619
		/// </summary>
		/// <param name="list">The list whose elements should be shuffled.</param>
		/// <typeparam name="T">The element type of the list.</typeparam>
		public static void Shuffle<T>(this IList<T> list)
		{
			list.ThrowIfNull(nameof(list));

			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = UnityEngine.Random.Range(0, n + 1);
				list.Swap(n, k);
			}
		}

		/// <summary>
		/// Swaps two elements in place.
		/// Source: https://stackoverflow.com/a/22668974/892910
		/// </summary>
		/// <param name="list">The list in which to swap the elements.</param>
		/// <param name="i">The index of the first element to swap.</param>
		/// <param name="j">The index of the second element to swap.</param>
		/// <typeparam name="T">The element type of the list.</typeparam>
		public static void Swap<T>(this IList<T> list, int i, int j)
		{
			list.ThrowIfNull(nameof(list));

			if ((i < 0) || (i >= list.Count))
			{
				throw new ArgumentOutOfRangeException(string.Format("Parameter {0} has value {1}, while the list has size {2}.", nameof(i), i, list.Count));
			}
			else if ((j < 0) || (j >= list.Count))
			{
				throw new ArgumentOutOfRangeException(string.Format("Parameter {0} has value {1}, while the list has size {2}.", nameof(j), j, list.Count));
			}

			T temp = list[i];
			list[i] = list[j];
			list[j] = temp;
		}

		/// <summary>
		/// Inserts a value in the list, assuming it is already sorted, preserving the order of elements.
		/// </summary>
		/// <param name="list">The list in which to insert the element.</param>
		/// <param name="value">Value to insert.</param>
		/// <typeparam name="T">The element type of the list.</typeparam>
		public static void SortedInsert<T>(this IList<T> list, T value)
		where T : IComparable<T>
		{
			SortedInsert(list, value, (a, b) => a.CompareTo(b));
		}

		/// <summary>
		/// Inserts a collection of values in the list, assuming it is already sorted, preserving the order of elements.
		/// </summary>
		/// <param name="list">The list in which to insert the elements.</param>
		/// <param name="values">The elements to be added to the list.</param>
		/// <typeparam name="T">The element type of the list.</typeparam>
		public static void SortedInsert<T>(this IList<T> list, IEnumerable<T> values)
		where T : IComparable<T>
		{
			values.ThrowIfNull(nameof(values));
			foreach (T value in values)
			{
				list.SortedInsert(value);
			}
		}

		/// <summary>
		/// Inserts a value in the list, assuming it is already sorted, preserving the order of elements.
		/// </summary>
		/// <param name="list">The list in which to insert the value.</param>
		/// <param name="value">Value to insert.</param>
		/// <param name="comparison">Comparison operator to determine the order of elements.</param>
		/// <typeparam name="T">The element type of the list.</typeparam>
		public static void SortedInsert<T>(this IList<T> list, T value, Comparison<T> comparison)
		{
			list.ThrowIfNull(nameof(list));
			comparison.ThrowIfNull(nameof(comparison));

			// If no elements exist in the list, add it.
			if (list.Count == 0)
			{
				list.Add(value);
				return;
			}

			// Search for the insertion index using binary search.
			int startIndex = 0;
			int endIndex = list.Count;
			while (endIndex > startIndex)
			{
				int windowSize = endIndex - startIndex;
				int middleIndex = startIndex + (windowSize / 2);
				T middleValue = list[middleIndex];
				int compareToResult = comparison(middleValue, value);
				if (compareToResult == 0)
				{
					list.Insert(middleIndex, value);
					return;
				}
				else if (compareToResult < 0)
				{
					startIndex = middleIndex + 1;
				}
				else
				{
					endIndex = middleIndex;
				}
			}

			list.Insert(startIndex, value);
		}

		/// <summary>
		/// Inserts a set of values in the list, assuming it is already sorted, preserving the order of elements.
		/// </summary>
		/// <param name="list">The list in which to insert the values.</param>
		/// <param name="values">Values to insert.</param>
		/// <param name="comparison">Comparison operator to determine the order of elements.</param>
		/// <typeparam name="T">The element type of the list.</typeparam>
		public static void SortedInsert<T>(this IList<T> list, IEnumerable<T> values, Comparison<T> comparison)
		{
			values.ThrowIfNull(nameof(values));
			comparison.ThrowIfNull(nameof(comparison));

			foreach (T value in values)
			{
				list.SortedInsert(value, comparison);
			}
		}

		/// <summary>
		/// Inserts a value in the list, assuming it is already sorted, preserving the order of elements.
		/// </summary>
		/// <param name="list">The list in which to insert the value.</param>
		/// <param name="value">Value to insert.</param>
		public static void SortedInsert(this IList list, IComparable value)
		{
			SortedInsert(list, value, (a, b) => a.CompareTo(b));
		}

		/// <summary>
		/// Inserts a set of values in the list, assuming it is already sorted, preserving the order of elements.
		/// </summary>
		/// <param name="list">The list in which to insert the values.</param>
		/// <param name="values">Values to insert.</param>
		public static void SortedInsert(this IList list, IEnumerable<IComparable> values)
		{
			values.ThrowIfNull(nameof(values));
			foreach (IComparable value in values)
			{
				list.SortedInsert(value);
			}
		}

		/// <summary>
		/// Inserts a value in the list, assuming it is already sorted, preserving the order of elements.
		/// </summary>
		/// <param name="list">The list in which to insert the values.</param>
		/// <param name="value">Value to insert.</param>
		/// <param name="comparison">Comparison operator to determine the order of elements.</param>
		public static void SortedInsert(this IList list, IComparable value, Comparison<IComparable> comparison)
		{
			comparison.ThrowIfNull(nameof(comparison));

			// If no elements exist in the list, add it.
			if (list.Count == 0)
			{
				list.Add(value);
				return;
			}

			int startIndex = 0;
			int endIndex = list.Count;
			while (endIndex > startIndex)
			{
				int windowSize = endIndex - startIndex;
				int middleIndex = startIndex + (windowSize / 2);
				IComparable middleValue = (IComparable)list[middleIndex];
				int compareToResult = comparison(middleValue, value);
				if (compareToResult == 0)
				{
					list.Insert(middleIndex, value);
					return;
				}
				else if (compareToResult < 0)
				{
					startIndex = middleIndex + 1;
				}
				else
				{
					endIndex = middleIndex;
				}
			}

			list.Insert(startIndex, value);
		}

		/// <summary>
		/// Inserts a set of values in the list, assuming it is already sorted, preserving the order of elements.
		/// </summary>
		/// <param name="list">The list in which to insert the values.</param>
		/// <param name="values">Values to insert.</param>
		/// <param name="comparison">Comparison operator to determine the order of elements.</param>
		public static void SortedInsert(this IList list, IEnumerable<IComparable> values, Comparison<IComparable> comparison)
		{
			values.ThrowIfNull(nameof(values));
			comparison.ThrowIfNull(nameof(comparison));

			foreach (IComparable value in values)
			{
				list.SortedInsert(value, comparison);
			}
		}

		/// <summary>
		/// Tries to find the index of the element that matches.
		/// </summary>
		/// <returns>True if an element is present that matches, false otherwise.</returns>
		public static bool TryFindIndex<TElement>(this TElement[] arr, Predicate<TElement> match, out int index)
		{
			arr.ThrowIfNull(nameof(arr));
			index = Array.FindIndex(arr, match);
			return index >= 0;
		}

		/// <summary>
		/// Tries to find the index of the element that matches.
		/// </summary>
		/// <returns>True if an element is present that matches, false otherwise.</returns>
		public static bool TryFindIndex<TElement>(this TElement[] arr, TElement match, out int index)
		{
			arr.ThrowIfNull(nameof(arr));
			for (int i = 0; i < arr.Length; ++i)
			{
				if (arr[i].Equals(match))
				{
					index = i;
					return true;
				}
			}

			index = -1;
			return false;
		}

		/// <summary>
		/// Tries to find the index of the element that matches.
		/// </summary>
		/// <returns>True if an element is present that matches, false otherwise.</returns>
		public static bool TryFindIndex<TElement>(this IReadOnlyList<TElement> l, Predicate<TElement> match, out int index)
		{
			l.ThrowIfNull(nameof(l));
			for (int i = 0; i < l.Count; ++i)
			{
				if (match(l[i]))
				{
					index = i;
					return true;
				}
			}

			index = -1;
			return false;
		}

		/// <summary>
		/// Tries to find the index of the element that matches.
		/// </summary>
		/// <returns>True if an element is present that matches, false otherwise.</returns>
		public static bool TryFindIndex<TElement>(this IReadOnlyList<TElement> l, TElement match, out int index)
		{
			l.ThrowIfNull(nameof(l));
			for (int i = 0; i < l.Count; ++i)
			{
				if (l[i].Equals(match))
				{
					index = i;
					return true;
				}
			}

			index = -1;
			return false;
		}

		/// <summary>
		/// Checks whether the collection is null or empty.
		/// </summary>
		/// <param name="c">The collection to test.</param>
		/// <returns>True if the collection is either null, or has 0 elements in it. False otherwise.</returns>
		public static bool IsNullOrEmpty(this ICollection c)
		{
			return (c == null) || (c.Count == 0);
		}

		/// <summary>
		/// Checks whether the collection is null or empty.
		/// </summary>
		/// <param name="c">The collection to test.</param>
		/// <returns>True if the collection is either null, or has 0 elements in it. False otherwise.</returns>
		public static bool IsNullOrEmpty<TElement>(this ICollection<TElement> c)
		{
			return (c == null) || (c.Count == 0);
		}

		/// <summary>
		/// Checks whether the collection is null or empty.
		/// </summary>
		/// <param name="c">The collection to test.</param>
		/// <returns>True if the collection is either null, or has 0 elements in it. False otherwise.</returns>
		public static bool IsNullOrEmpty<TElement>(this IReadOnlyCollection<TElement> c)
		{
			return (c == null) || (c.Count == 0);
		}

		/// <summary>
		/// Checks whether the array is null or empty.
		/// </summary>
		/// <param name="c">The array to test.</param>
		/// <returns>True if the array is either null, or has 0 elements in it. False otherwise.</returns>
		public static bool IsNullOrEmpty<TElement>(this TElement[] c)
		{
			return (c == null) || (c.Length == 0);
		}

		/// <summary>
		/// Checks whether the list is null or empty.
		/// </summary>
		/// <param name="c">The list to test.</param>
		/// <returns>True if the list is either null, or has 0 elements in it. False otherwise.</returns>
		public static bool IsNullOrEmpty<TElement>(this List<TElement> c)
		{
			return (c == null) || (c.Count == 0);
		}

		/// <summary>
		/// Checks whether the dictionary is null or empty.
		/// </summary>
		/// <param name="d">The dictionary to test.</param>
		/// <returns>True if the dictionary is either null, or has 0 elements in it. False otherwise.</returns>
		public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> d)
		{
			return (d == null) || (d.Count == 0);
		}

		/// <summary>
		/// Checks whether the queue is null or empty.
		/// </summary>
		/// <param name="q">The queue to test.</param>
		/// <returns>True if the queue is either null, or has 0 elements in it. False otherwise.</returns>
		public static bool IsNullOrEmpty<TElement>(this Queue<TElement> q)
		{
			return (q == null) || (q.Count == 0);
		}

		/// <summary>
		/// Checks whether the collection is null or empty.
		/// </summary>
		/// <param name="a">The collection to test.</param>
		/// <returns>True if the collection is either null, or has 0 elements in it. False otherwise.</returns>
		public static bool IsNullOrEmpty(this ArrayList a)
		{
			return (a == null) || (a.Count == 0);
		}

		/// <summary>
        /// Pops element by <paramref name="index"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="list">List with elements.</param>
        /// <param name="index">Index of element to pop.</param>
        /// <returns>The popped element.</returns>
        public static T Pop<T>(this IList<T> list, int index)
        {
            var element = list[index];
            list.RemoveAt(index);

            return element;
        }

        /// <summary>
        /// Pops elements by <paramref name="indexes"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="list">List with elements.</param>
        /// <param name="indexes">Indexes of elements to be popped.</param>
        /// <returns>The popped element.</returns>
        public static List<T> Pop<T>(this IList<T> list, params int[] indexes)
        {
            var popped = new List<T>();

            foreach (var index in indexes)
                popped.Add(list.Pop(index));

            return popped;
        }

        /// <summary>
        /// Pops random element from <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="list">List with elements.</param>
        /// <returns>Tuple with popped element and it's index.</returns>
        public static (T element, int index) PopRandom<T>(this IList<T> list)
        {
            var index = UnityEngine.Random.Range(0, list.Count);
            return (list.Pop(index), index);
        }

        /// <summary>
        /// Pops random elements from list.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="list">List with elements.</param>
        /// <param name="count">Count of elements to be popped.</param>
        /// <returns>List of tuples with popped elements and it's indexes.</returns>
        public static List<(T element, int index)> PopRandoms<T>(this IList<T> list, int count)
        {
            var popped = new List<(T element, int index)>();

            for (int i = 0; i < count; i++)
                popped.Add(list.PopRandom());

            return popped;
        }

        /// <summary>
        /// Pops random elements from list with specified probability.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="list">List with elements.</param>
        /// <param name="probabilities">Probabilities, must match in count with enumerable.</param>
        /// <returns>Popped element.</returns>
        public static (T element, int index) PopRandomElementWithProbability<T>(this IList<T> list, params float[] probabilities)
        {
            return PopRandomElementWithProbability(list, (IEnumerable<float>)probabilities);
        }

        /// <summary>
        /// Pops random elements from list with specified probability.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="list">List with elements.</param>
        /// <param name="probabilities">Probabilities, must match in count with enumerable.</param>
        /// <returns>Popped elements.</returns>
        public static (T element, int index) PopRandomElementWithProbability<T>(this IList<T> list, IEnumerable<float> probabilities)
        {
            var random = list.GetRandomElementWithProbability(probabilities);
            Pop(list, random.index);

            return random;
        }

        /// <summary>
        /// Pops random elements from list with specified probability selector.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="list">List with elements.</param>
        /// <param name="probabilitiesSelector">Probabilities selector.</param>
        /// <returns>Popped elements.</returns>
        public static (T element, int index) PopRandomElementWithProbability<T>(this IList<T> list, Func<T, float> probabilitiesSelector)
        {
            var random = list.GetRandomElementWithProbability(probabilitiesSelector);
            Pop(list, random.index);

            return random;
        }

        /// <summary>
        /// An ICollection&lt;T&gt; extension method that swaps item only when it exists in a collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public static void Swap<T>(this IList<T> @this, T oldValue, T newValue)
        {
            var oldIndex = @this.IndexOf(oldValue);
            while (oldIndex > 0)
            {
                @this.RemoveAt(oldIndex);
                @this.Insert(oldIndex, newValue);
                oldIndex = @this.IndexOf(oldValue);
            }
        }

        /// <summary>
        /// Removes all elements starts from <paramref name="index"/>.
        /// </summary>
        /// <typeparam name="T">Elements type.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">From what index need starts removing?</param>
        public static void RemoveRange<T>(this IList<T> list, int index)
        {
            for (int i = list.Count - 1; i >= index; i++)
                list.RemoveAt(i);
        }

        public static T Next<T>(this IList<T> list, T item)
        {
            return list[NextPosition(list, item)];
        }

        public static T Previous<T>(this IList<T> list, T item)
        {
            return list[PreviousPosition(list, item)];
        }

        public static int NextPosition<T>(this IList<T> list, T item)
        {
            return (list.IndexOf(item) + 1) == list.Count ? 0 : (list.IndexOf(item) + 1);
        }

        public static int PreviousPosition<T>(this IList<T> list, T item)
        {
            return (list.IndexOf(item) - 1) < 0 ? list.Count - 1 : (list.IndexOf(item) - 1);
        }
    }