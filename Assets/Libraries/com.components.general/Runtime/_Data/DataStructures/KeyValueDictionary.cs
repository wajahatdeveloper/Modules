using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyValueDictionary_Item
{
    public KeyValueDictionary_Item()
    {
    }

    public KeyValueDictionary_Item(string key, string value)
    {
        this.key = key;
        this.value = value;
    }

    public string key;
    public string value;
}

[System.Serializable]
public class KeyValueDictionary : IEnumerable<KeyValueDictionary_Item>, ICollection<KeyValueDictionary_Item>, IEnumerable, IList<KeyValueDictionary_Item>
{
    public List<KeyValueDictionary_Item> items;

    public int Count => ((ICollection<KeyValueDictionary_Item>)items).Count;

    public bool IsReadOnly => ((ICollection<KeyValueDictionary_Item>)items).IsReadOnly;

    public KeyValueDictionary_Item this[int index] { get => ((IList<KeyValueDictionary_Item>)items)[index]; set => ((IList<KeyValueDictionary_Item>)items)[index] = value; }
    public string this[string key]
    {
        get => GetValue(key);
        set => SetValue(key, value);
    }

    private void SetValue(string key, string value)
    {
        items.Find(x => x.key == key).value = value;
    }

    private string GetValue(string key)
    {
        return items.Find(x => x.key == key).value;
    }

    public IEnumerator<KeyValueDictionary_Item> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(KeyValueDictionary_Item item)
    {
        ((ICollection<KeyValueDictionary_Item>)items).Add(item);
    }

    public void Clear()
    {
        ((ICollection<KeyValueDictionary_Item>)items).Clear();
    }

    public bool Contains(KeyValueDictionary_Item item)
    {
        return ((ICollection<KeyValueDictionary_Item>)items).Contains(item);
    }

    public void CopyTo(KeyValueDictionary_Item[] array, int arrayIndex)
    {
        ((ICollection<KeyValueDictionary_Item>)items).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValueDictionary_Item item)
    {
        return ((ICollection<KeyValueDictionary_Item>)items).Remove(item);
    }

    public int IndexOf(KeyValueDictionary_Item item)
    {
        return ((IList<KeyValueDictionary_Item>)items).IndexOf(item);
    }

    public void Insert(int index, KeyValueDictionary_Item item)
    {
        ((IList<KeyValueDictionary_Item>)items).Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        ((IList<KeyValueDictionary_Item>)items).RemoveAt(index);
    }
}
