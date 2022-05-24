using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CollectionExtensions
{
    /// <summary>
    		/// Returns new array without element at index
    		/// </summary>
    		public static T[] RemoveAt<T>(this T[] array, int index)
    		{
    			if (index < 0)
    			{
    				Debug.LogError("Index is less than zero. Array is not modified");
    				return array;
    			}
    
    			if (index >= array.Length)
    			{
    				Debug.LogError("Index exceeds array length. Array is not modified");
    				return array;
    			}
    
    			T[] newArray = new T[array.Length - 1];
    			int index1 = 0;
    			for (int index2 = 0; index2 < array.Length; ++index2)
    			{
    				if (index2 == index) continue;
    
    				newArray[index1] = array[index2];
    				++index1;
    			}
    
    			return newArray;
    		}
    
    		/// <summary>
    		/// Returns new array with inserted empty element at index
    		/// </summary>
    		public static T[] InsertAt<T>(this T[] array, int index)
    		{
    			if (index < 0)
    			{
    				Debug.LogError("Index is less than zero. Array is not modified");
    				return array;
    			}
    
    			if (index > array.Length)
    			{
    				Debug.LogError("Index exceeds array length. Array is not modified");
    				return array;
    			}
    
    			T[] newArray = new T[array.Length + 1];
    			int index1 = 0;
    			for (int index2 = 0; index2 < newArray.Length; ++index2)
    			{
    				if (index2 == index) continue;
    
    				newArray[index2] = array[index1];
    				++index1;
    			}
    
    			return newArray;
    		}
    
    
    		/// <summary>
    		/// Returns random element from collection
    		/// </summary>
    		public static T GetRandom<T>(this T[] collection)
    		{
    			return collection[UnityEngine.Random.Range(0, collection.Length)];
    		}
    
    		/// <summary>
    		/// Returns random element from collection
    		/// </summary>
    		public static T GetRandom<T>(this IList<T> collection)
    		{
    			return collection[UnityEngine.Random.Range(0, collection.Count)];
    		}
    
    		/// <summary>
    		/// Returns random element from collection
    		/// </summary>
    		public static T GetRandom<T>(this IEnumerable<T> collection)
    		{
    			return collection.ElementAt(UnityEngine.Random.Range(0, collection.Count()));
    		}
            
            /// <summary>
            		/// Get next index for circular array. <br />
            		/// -1 will result with last element index, Length + 1 is 0. <br />
            		/// If step is more that 1, you will get correct offset <br />
            		/// 
            		/// <code>
            		/// Example (infinite loop first->last->first):
            		/// i = myArray.NextIndex(i++);
            		/// var nextItem = myArray[i];
            		/// </code>
            		/// </summary>
            		public static int NextIndexInCircle<T>(this T[] array, int desiredPosition)
            		{
            			if (array.IsNullOrEmpty())
            			{
            				Debug.LogError("NextIndexInCircle Caused: source array is null or empty");
            				return -1;
            			}
            
            			var length = array.Length;
            			if (length == 1) return 0;
            
            			return (desiredPosition % length + length) % length;
            		}
            
            
            		/// <returns>
            		/// Returns -1 if none found
            		/// </returns>
            		public static int IndexOfItem<T>(this IEnumerable<T> collection, T item)
            		{
            			if (collection == null)
            			{
            				Debug.LogError("IndexOfItem Caused: source collection is null");
            				return -1;
            			}
            
            			var index = 0;
            			foreach (var i in collection)
            			{
            				if (Equals(i, item)) return index;
            				++index;
            			}
            
            			return -1;
            		}
            
            		/// <summary>
            		/// Is Elements in two collections are the same
            		/// </summary>
            		public static bool ContentsMatch<T>(this IEnumerable<T> first, IEnumerable<T> second)
            		{
            			if (first.IsNullOrEmpty() && second.IsNullOrEmpty()) return true;
            			if (first.IsNullOrEmpty() || second.IsNullOrEmpty()) return false;
            
            			var firstCount = first.Count();
            			var secondCount = second.Count();
            			if (firstCount != secondCount) return false;
            
            			foreach (var x1 in first)
            			{
            				if (!second.Contains(x1)) return false;
            			}
            
            			return true;
            		}
                    
                    /// <summary>
		/// Is Keys in MyDictionary is the same as some collection
		/// </summary>
		public static bool ContentsMatchKeys<T1, T2>(this IDictionary<T1, T2> source, IEnumerable<T1> check)
		{
			if (source.IsNullOrEmpty() && check.IsNullOrEmpty()) return true;
			if (source.IsNullOrEmpty() || check.IsNullOrEmpty()) return false;

			return source.Keys.ContentsMatch(check);
		}

		/// <summary>
		/// Is Values in MyDictionary is the same as some collection
		/// </summary>
		public static bool ContentsMatchValues<T1, T2>(this IDictionary<T1, T2> source, IEnumerable<T2> check)
		{
			if (source.IsNullOrEmpty() && check.IsNullOrEmpty()) return true;
			if (source.IsNullOrEmpty() || check.IsNullOrEmpty()) return false;

			return source.Values.ContentsMatch(check);
		}

		/// <summary>
		/// Adds a key/value pair to the IDictionary&lt;TKey,TValue&gt; if the
		/// key does not already exist. Returns the new value, or the existing
		/// value if the key exists.
		/// </summary>
		public static TValue GetOrAdd<TKey, TValue>(
			this IDictionary<TKey, TValue> source,
			TKey key,
			TValue value)
		{
			if (!source.ContainsKey(key)) source[key] = value;
			return source[key];
		}

		/// <summary>
		/// Adds a key/value pair to the IDictionary&lt;TKey,TValue&gt; by using
		/// the specified function if the key does not already exist. Returns
		/// the new value, or the existing value if the key exists.
		/// </summary>
		public static TValue GetOrAdd<TKey, TValue>(
			this IDictionary<TKey, TValue> source,
			TKey key,
			System.Func<TKey, TValue> valueFactory)
		{
			if (!source.ContainsKey(key)) source[key] = valueFactory(key);
			return source[key];
		}

		/// <summary>
		/// Adds a key/value pair to the IDictionary&lt;TKey,TValue&gt; by using
		/// the specified function and an argument if the key does not already
		/// exist, or returns the existing value if the key exists.
		/// </summary>
		public static TValue GetOrAdd<TKey, TValue, TArg>(
			this IDictionary<TKey, TValue> source,
			TKey key,
			System.Func<TKey, TArg, TValue> valueFactory,
			TArg factoryArgument)
		{
			if (!source.ContainsKey(key))
				source[key] = valueFactory(key, factoryArgument);
			return source[key];
		}
		
		/// <summary>
		/// Fills a collection with values generated using a factory function that
		/// passes along their index numbers.
		/// </summary>
		public static IList<T> FillBy<T>(this IList<T> source,
			Func<int, T> valueFactory)
		{
			for (int i = 0; i < source.Count; ++i) source[i] = valueFactory(i);
			return source;
		}
		
		/// <summary>
		/// Fills an array with values generated using a factory function that
		/// passes along their index numbers.
		/// </summary>
		public static T[] FillBy<T>(this T[] source,
			Func<int, T> valueFactory)
		{
			for (int i = 0; i < source.Length; ++i) source[i] = valueFactory(i);
			return source;
		}
		
		/// <summary>
		/// Swaps 2 elements at the specified index positions in place.
		/// </summary>
		public static IList<T> SwapInPlace<T>(this IList<T> source,
			int index1,
			int index2)
		{
			var e1 = source[index1];
			source[index1] = source[index2];
			source[index2] = e1;
			return source;
		}

		/// <summary>
		/// Shuffles a collection in place using the Knuth algorithm.
		/// </summary>
		public static IList<T> Shuffle<T>(this IList<T> source)
		{
			for (int i = 0; i < source.Count - 1; ++i)
			{
				var indexToSwap = UnityEngine.Random.Range(i, source.Count);
				source.SwapInPlace(i, indexToSwap);
			}
			return source;
		}
}
