using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyValueDictionary_Item
{
    public string key;
    public string value;
}

[System.Serializable]
public class KeyValueDictionary : IEnumerable<KeyValueDictionary_Item>
{
    public List<KeyValueDictionary_Item> items;
    
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
}
