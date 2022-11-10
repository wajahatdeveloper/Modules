using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyValueDictionary_Item<K, V>
{
	public KeyValueDictionary_Item()
	{
	}

	public KeyValueDictionary_Item(K key, V value)
	{
		this.key = key;
		this.value = value;
	}

	public K key;
	public V value;
}

[System.Serializable]
public class KeyValueDictionary<K, V> : IEnumerable<KeyValueDictionary_Item<K, V>>, ICollection<KeyValueDictionary_Item<K, V>>, IList<KeyValueDictionary_Item<K, V>>
{
	public List<KeyValueDictionary_Item<K, V>> items;

	public int Count => ((ICollection<KeyValueDictionary_Item<K, V>>)items).Count;

	public bool IsReadOnly => ((ICollection<KeyValueDictionary_Item<K, V>>)items).IsReadOnly;

	public KeyValueDictionary_Item<K, V> this[int index] { get => ((IList<KeyValueDictionary_Item<K, V>>)items)[index]; set => ((IList<KeyValueDictionary_Item<K, V>>)items)[index] = value; }
	public V this[K key]
	{
		get => GetValue(key);
		set => SetValue(key, value);
	}

	private void SetValue(K key, V value)
	{
		items.Find(x => x.key.Equals(key)).value = value;
	}

	private V GetValue(K key)
	{
		return items.Find(x => x.key.Equals(key)).value;
	}


	public void Add(KeyValueDictionary_Item<K, V> item)
	{
		((ICollection<KeyValueDictionary_Item<K, V>>)items).Add(item);
	}

	public void Clear()
	{
		((ICollection<KeyValueDictionary_Item<K, V>>)items).Clear();
	}

	public bool Contains(KeyValueDictionary_Item<K, V> item)
	{
		return ((ICollection<KeyValueDictionary_Item<K, V>>)items).Contains(item);
	}

	public void CopyTo(KeyValueDictionary_Item<K, V>[] array, int arrayIndex)
	{
		((ICollection<KeyValueDictionary_Item<K, V>>)items).CopyTo(array, arrayIndex);
	}

	public bool Remove(KeyValueDictionary_Item<K, V> item)
	{
		return ((ICollection<KeyValueDictionary_Item<K, V>>)items).Remove(item);
	}

	public int IndexOf(KeyValueDictionary_Item<K, V> item)
	{
		return ((IList<KeyValueDictionary_Item<K, V>>)items).IndexOf(item);
	}

	public void Insert(int index, KeyValueDictionary_Item<K, V> item)
	{
		((IList<KeyValueDictionary_Item<K, V>>)items).Insert(index, item);
	}

	public void RemoveAt(int index)
	{
		((IList<KeyValueDictionary_Item<K, V>>)items).RemoveAt(index);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)items).GetEnumerator();
	}

	public IEnumerator<KeyValueDictionary_Item<K, V>> GetEnumerator()
	{
		return ((IEnumerable<KeyValueDictionary_Item<K, V>>)items).GetEnumerator();
	}
}
