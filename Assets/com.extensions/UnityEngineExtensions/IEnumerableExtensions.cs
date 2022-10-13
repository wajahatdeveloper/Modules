using System;
using System.Collections.Generic;

public static class IEnumerableExtensions
{
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
