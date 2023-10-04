using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityRandom = UnityEngine.Random;

public static class IEnumerableExtensions
{
	/// <summary>
	///     An IEnumerable&lt;T&gt; extension method that queries if a not is empty.
	/// </summary>
	/// <typeparam name="T">Generic type parameter.</typeparam>
	/// <param name="this">The collection to act on.</param>
	/// <returns>true if a not is t>, false if not.</returns>
	public static bool IsNotEmpty<T>(this IEnumerable<T> @this)
	{
		return @this.Any();
	}

	/// <summary>
	///     An IEnumerable&lt;T&gt; extension method that queries if a not null or is empty.
	/// </summary>
	/// <typeparam name="T">Generic type parameter.</typeparam>
	/// <param name="this">The collection to act on.</param>
	/// <returns>true if a not null or is t>, false if not.</returns>
	public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this)
	{
		return @this != null && @this.Any();
	}

	/// <summary>
	///     Concatenates all the elements of a IEnumerable, using the specified separator between each element.
	/// </summary>
	/// <typeparam name="T">Generic type parameter.</typeparam>
	/// <param name="this">An IEnumerable that contains the elements to concatenate.</param>
	/// <param name="separator">
	///     The string to use as a separator. separator is included in the returned string only if
	///     value has more than one element.
	/// </param>
	/// <returns>
	///     A string that consists of the elements in value delimited by the separator string. If value is an empty array,
	///     the method returns String.Empty.
	/// </returns>
	public static string StringJoin<T>(this IEnumerable<T> @this, string separator)
	{
		return string.Join(separator, @this);
	}

	/// <summary>
	///     Concatenates all the elements of a IEnumerable, using the specified separator between
	///     each element.
	/// </summary>
	/// <typeparam name="T">Generic type parameter.</typeparam>
	/// <param name="this">The @this to act on.</param>
	/// <param name="separator">
	///     The string to use as a separator. separator is included in the
	///     returned string only if value has more than one element.
	/// </param>
	/// <returns>
	///     A string that consists of the elements in value delimited by the separator string. If
	///     value is an empty array, the method returns String.Empty.
	/// </returns>
	public static string StringJoin<T>(this IEnumerable<T> @this, char separator)
	{
		return string.Join(separator.ToString(), @this);
	}


	/// <summary>
        /// Get random element from the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>Random element from enumerable.</returns>
        public static T GetRandomElement<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(UnityRandom.Range(0, enumerable.Count()));

        /// <summary>
        /// Get random elements from the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="count">Count of the random elements.</param>
        /// <returns>Random elements from enumerable.</returns>
        public static List<T> GetRandomElements<T>(this IEnumerable<T> enumerable, int count)
        {
            var poppedIndexes = Enumerable.Range(0, enumerable.Count()).ToList().PopRandoms(count).Select(p => p.index);
            return enumerable.Where((el, i) => poppedIndexes.Contains(i)).ToList();
        }

        /// <summary>
        /// Excepts passed elements from <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="elements">Elements to exclude.</param>
        /// <returns>Enumerable without passed elements.</returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, params T[] elements) => enumerable.Except((IEnumerable<T>)elements);

        /// <summary>
        /// Shuffles <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>Shuffled <paramref name="enumerable"/>.</returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> enumerable) => enumerable.OrderBy(v => UnityRandom.value);

        /// <summary>
        /// Get random element index with probability selector.
        /// </summary>
        /// <typeparam name="T">Enumerable elements type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="probabilities">Probabilities, must match in count with enumerable.</param>
        /// <returns>Tuple with random element and it's index.</returns>
        /// <exception cref="ArgumentException">Throwed when <paramref name="enumerable"/> and <paramref name="probabilities"/> counts are not match.</exception>
        public static (T element, int index) GetRandomElementWithProbability<T>(this IEnumerable<T> enumerable, params float[] probabilities) => GetRandomElementWithProbability(enumerable, (IEnumerable<float>)probabilities);

        /// <summary>
        /// <inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])"/>
        /// </summary>
        /// <typeparam name="T"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])"/></typeparam>
        /// <param name="enumerable"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])" path="/param[@name='enumerable']"/></param>
        /// <param name="probabilities"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])" path="/param[@name='probabilities']"/></param>
        /// <returns>Tuple with random element and it's index.</returns>
        /// <exception cref="ArgumentException">Throwed when <paramref name="enumerable"/> and <paramref name="probabilities"/> counts are not match.</exception>
        public static (T element, int index) GetRandomElementWithProbability<T>(this IEnumerable<T> enumerable, IEnumerable<float> probabilities)
        {
            var count = enumerable.Count();

            if (probabilities.Count() != count)
                throw new ArgumentException($"Count of probabilities and enumerble elements must be equal.");

            if (count == 0)
                throw new ArgumentException($"Enumerable count must be greater than zero");

            var randomValue = UnityRandom.value * probabilities.Sum();
            var sum = 0f;

            var index = -1;
            var enumerator = probabilities.GetEnumerator();

            while (enumerator.MoveNext())
            {
                index += 1;
                var probability = enumerator.Current;

                sum += probability;

                if (randomValue < sum || randomValue.Approximately(sum))
                    return (enumerable.ElementAt(index), index);
            }

            index = probabilities.Count() - 1;
            return (enumerable.ElementAt(index), index);
        }

        /// <summary>
        /// <inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])"/>
        /// </summary>
        /// <typeparam name="T"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])"/></typeparam>
        /// <param name="enumerable"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])" path="/param[@name='enumerable']"/></param>
        /// <param name="probabilitySelector">Probabilities selector.</param>
        /// <returns>Tuple with random element and it's index.</returns>
        /// <exception cref="ArgumentException">Throwed when <paramref name="enumerable"/> and <paramref name="probabilities"/> counts are not match.</exception>
        public static (T element, int index) GetRandomElementWithProbability<T>(this IEnumerable<T> enumerable, Func<T, float> probabilitySelector)
        {
            return GetRandomElementWithProbability(enumerable, enumerable.Select(el => probabilitySelector(el)));
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            int index = 0;
            foreach (T item in enumerable)
            {
                action(item, index++);

                yield return item;
            }
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> enumerable, bool condition, Func<T, bool> predicate)
        {
            return condition ? enumerable.Where(predicate) : enumerable;
        }

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.TakeWhile(item => !predicate(item));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T item)
        {
	        return enumerable.Except(Enumerable.Repeat(item, 1));
        }

        public static void Rewrite<T>(this ICollection<T> collection, IEnumerable<T> newCollection)
        {
	        collection.Clear();
	        foreach (T item in newCollection)
		        collection.Add(item);
        }

        public static IEnumerable<T> Concat<T>(IEnumerable<T> enumerable, T item)
        {
            return enumerable.Concat(Enumerable.Repeat(item, 1));
        }

        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> enumerable)
        {
            T[] array = enumerable.ToArray();

            for (int i = array.Length - 1; i >= 0; i--)
                yield return array[i];
        }

        public static T MinBy<T, TMin>(this IEnumerable<T> enumerable, Func<T, TMin> selector)
            where TMin : IComparable<TMin>
        {
            T min = default;
            bool first = true;

            foreach (T item in enumerable)
            {
                if (first)
                {
                    min = item;
                    first = false;
                }
                else if (selector(item).CompareTo(selector(min)) < 0)
                {
                    min = item;
                }
            }

            return min;
        }

        public static T MaxBy<T, TMax>(this IEnumerable<T> enumerable, Func<T, TMax> selector)
            where TMax : IComparable<TMax>
        {
            T max = default;
            bool first = true;

            foreach (T item in enumerable)
            {
                if (first)
                {
                    max = item;
                    first = false;
                }
                else if (selector(item).CompareTo(selector(max)) > 0)
                {
                    max = item;
                }
            }

            return max;
        }

        public static T FirstOrNull<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.DefaultIfEmpty(null).FirstOrDefault();
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            T[] array = enumerable.ToArray();

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T Random<T>(this IEnumerable<T> enumerable, Func<T, float> chanceSelector)
        {
            T[] orderedArray = enumerable.OrderByDescending(chanceSelector).ToArray();

            float[] chances = orderedArray.Select(chanceSelector).ToArray();
            float totalChance = chances.Sum();
            float chance = UnityEngine.Random.Range(0, totalChance);

            int index = 0;
            for (int i = 0; i < chances.Length; i++)
            {
                if (chance <= chances[i])
                {
                    index = i;

                    break;
                }
                else chance -= chances[i];
            }

            return orderedArray[index];
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> enumerable, int amount)
        {
            return Shuffle(enumerable).Take(amount);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(item => UnityEngine.Random.value);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.IsEmpty();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }

        public static string AsString<T>(this IEnumerable<T> enumerable)
        {
            return AsString(enumerable, x => x?.ToString(), ", ");
        }

        public static string AsString<T>(this IEnumerable<T> enumerable, Func<T, string> selector)
        {
            return AsString(enumerable, selector, ", ");
        }

        public static string AsString<T>(this IEnumerable<T> enumerable, string separator)
        {
            return AsString(enumerable, x => x?.ToString(), separator);
        }

        public static string AsString<T>(this IEnumerable<T> enumerable, Func<T, string> selector, string separator)
        {
            return enumerable.IsEmpty()
                ? string.Empty
                : string.Join(separator, enumerable.Select(x => selector(x) ?? "null"));
        }

	#region UnityConvertAll

	/// <summary>
	/// ConvertAll LINQ extension, which runs on WP8 (WP8 doesn't support .ConvertAll())
	/// </summary>
	/// <typeparam name="InputType"></typeparam>
	/// <typeparam name="OutputType"></typeparam>
	/// <param name="inputList"></param>
	/// <param name="converter"></param>
	/// <returns></returns>
	public static List<OutputType> UnityConvertAll<InputType, OutputType>(this List<InputType> inputList, Func<InputType, OutputType> converter)
	{
		int j = inputList.Count;
		List<OutputType> output = new List<OutputType>(j);
		for (int i = 0; i < j; i++)
			output.Add(converter(inputList[i]));

		return output;
	}

	// UnityConvertAll
	#endregion

	/// <summary>
	/// Returns all elements of the specified sequence separated by the given separator.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <param name="source">The <see cref="IEnumerable{TSource}"/> to intersperse the separator into.</param>
	/// <param name="separator">The separator.</param>
	/// <param name="count">The count of <paramref name="seperator"> between each element. Defaults to 1.</param>
	/// <returns>The sequence containing the interspersed separator.</returns>
	/// <example>
	/// <code>
	/// int[] numbers = { 1, 2, 3, 4 };
	/// IEnumerable&lt;int&gt; interspersed = numbers.Intersperse(0, 2);
	/// </code>
	/// The <c>interspersed</c> variable, when iterated over, will yield the sequence 1, 0, 0, 2, 0, 0, 3, 0, 0, 4.
	/// </example>
	public static IEnumerable<TSource> Intersperse<TSource>(this IEnumerable<TSource> source, TSource separator, int count = 1)
	{
		ThrowIf.Argument.IsNull(source, "source");
		ThrowIf.Argument.IsZeroOrNegative(count, "count");

		return IntersperseIterator(source, separator, count);
	}

	private static IEnumerable<TSource> IntersperseIterator<TSource>(IEnumerable<TSource> source, TSource separator, int count)
	{
		bool isFirst = true;

		foreach (TSource element in source)
		{
			if (!isFirst)
			{
				for (int i = 0; i < count; i++)
				{
					yield return separator;
				}
			}
			else
			{
				isFirst = false;
			}

			yield return element;
		}
	}

	
	/// <summary>
	/// Returns a flattened sequence that contains the concatenation of all the nested sequences' elements.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <param name="source">A sequence of sequences to be flattened.</param>
	/// <returns>The concatenation of all the nested sequences' elements.</returns>
	public static IEnumerable<TSource> Flatten<TSource>(this IEnumerable<IEnumerable<TSource>> source)
	{
		ThrowIf.Argument.IsNull(source, "source");

		return FlattenIterator(source);
	}

	private static IEnumerable<TSource> FlattenIterator<TSource>(IEnumerable<IEnumerable<TSource>> source)
	{
		foreach (IEnumerable<TSource> array in source)
		{
			foreach (TSource element in array)
			{
				yield return element;
			}
		}
	}

	
	/// <summary>
	/// Turns a finite sequence into a circular one, or equivalently,
	/// repeats the original sequence indefinitely.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}"/> to cycle through.</param>
	/// <returns>An infinite sequence cycling through the given sequence.</returns>
	public static IEnumerable<TSource> Cycle<TSource>(this IEnumerable<TSource> source)
	{
		ThrowIf.Argument.IsNull(source, "source");

		return CycleIterator(source);
	}

	private static IEnumerable<TSource> CycleIterator<TSource>(IEnumerable<TSource> source)
	{
		var collection = source as ICollection<TSource>;

		var elementBuffer = collection == null
			? new List<TSource>()
			: new List<TSource>(collection.Count);

		foreach (TSource element in source)
		{
			yield return element;

			// We add this element to a local element buffer so that
			// we don't have to enumerate the sequence multiple times
			elementBuffer.Add(element);
		}

		if (elementBuffer.IsEmpty())
		{
			// If the element buffer is empty, so was the source sequence.
			// In this case, we can stop here and simply return an empty sequence.
			yield break;
		}

		int index = 0;
		while (true)
		{
			yield return elementBuffer[index];
			index = (index + 1) % elementBuffer.Count;
		}
	}

	
	/// <summary>
    /// Splits the given sequence into chunks of the given size.
    /// If the sequence length isn't evenly divisible by the chunk size,
    /// the last chunk will contain all remaining elements.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">The sequence.</param>
    /// <param name="chunkSize">The number of elements per chunk.</param>
    /// <returns>The chunked sequence.</returns>
    public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int chunkSize)
    {
        ThrowIf.Argument.IsNull(source, "source");
        ThrowIf.Argument.IsZeroOrNegative(chunkSize, "chunkSize");

        return ChunkIterator(source, chunkSize);
    }

    private static IEnumerable<TSource[]> ChunkIterator<TSource>(IEnumerable<TSource> source, int chunkSize)
    {
        TSource[] currentChunk = null;
        int currentIndex = 0;

        foreach (var element in source)
        {
            currentChunk = currentChunk ?? new TSource[chunkSize];
            currentChunk[currentIndex++] = element;

            if (currentIndex == chunkSize)
            {
                yield return currentChunk;
                currentIndex = 0;
                currentChunk = null;
            }
        }

        // Do we have an incomplete chunk of remaining elements?
        if (currentChunk != null)
        {
            // This last chunk is incomplete, otherwise it would've been returned already.
            // Thus, we have to create a new, shorter array to hold the remaining elements.
            var lastChunk = new TSource[currentIndex];
            Array.Copy(currentChunk, lastChunk, currentIndex);

            yield return lastChunk;
        }
    }

    public static T MaxElement<T, TCompare>(this IEnumerable<T> collection, Func<T, TCompare> func)
	where TCompare : IComparable<TCompare>
	{
		T maxItem = default(T);
		TCompare maxValue = default(TCompare);

		if (collection == null)
			return maxItem;

		foreach (var item in collection)
		{
			TCompare temp = func(item);

			if (maxItem == null || temp.CompareTo(maxValue) > 0)
			{
				maxValue = temp;
				maxItem = item;
			}
		}
		return maxItem;
	}

	public static T[] RemoveRange<T>(this T[] array, int index, int count)
	{
		if (count < 0)
			throw new ArgumentOutOfRangeException("count", " is out of range");
		if (index < 0 || index > array.Length - 1)
			throw new ArgumentOutOfRangeException("index", " is out of range");

		if (array.Length - count - index < 0)
			throw new ArgumentException("index and count do not denote a valid range of elements in the array", "");

		var newArray = new T[array.Length - count];

		for (int i = 0, ni = 0; i < array.Length; i++)
		{
			if (i < index || i >= index + count)
			{
				newArray[ni] = array[i];
				ni++;
			}
		}

		return newArray;
	}
}

internal static class ThrowIf
{
	public static class Argument
	{
		public static void IsNull(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		public static void IsNegative(int argument, string argumentName)
		{
			if (argument < 0)
			{
				throw new ArgumentOutOfRangeException(argumentName, argumentName + " must not be negative.");
			}
		}

		public static void IsZeroOrNegative(int argument, string argumentName)
		{
			if (argument <= 0)
			{
				throw new ArgumentOutOfRangeException(argumentName, argumentName + " must be positive.");
			}
		}
	}
}