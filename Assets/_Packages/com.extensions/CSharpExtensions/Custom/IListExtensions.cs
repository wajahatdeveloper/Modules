using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public static class IListExtensions
    {
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