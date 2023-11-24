using System;
using System.Collections;
using System.Collections.Generic;

public class CustomDictionary<TKey, TValue> : IList<KeyValuePair<TKey, TValue>>
{
    private List<TKey> keys;
    private List<TValue> values;

    public CustomDictionary()
    {
        keys = new List<TKey>();
        values = new List<TValue>();
    }

    public void Add(TKey key, TValue value)
    {
        TKey[] newKeys = new TKey[keys.Count + 1];
        TValue[] newValues = new TValue[values.Count + 1];

        for (int i = 0; i < keys.Count; i++)
        {
            newKeys[i] = keys[i];
            newValues[i] = values[i];
        }

        newKeys[keys.Count] = key; // adding the elements at the end of the array
        newValues[values.Count] = value;

        keys = new List<TKey>(newKeys);
        values = new List<TValue>(newValues);

    }

    public bool Remove(TKey key)
    {
        int index = keys.IndexOf(key);

        if (index != -1)
        {
            TKey[] newKeys = new TKey[keys.Count - 1];
            TValue[] newValues = new TValue[values.Count - 1];

            for (int i = 0, j = 0; i < keys.Count; i++)
            {
                if (i != index)
                {
                    newKeys[j] = keys[i];
                    newValues[j] = values[i];
                    j++;
                }
            }

            keys = new List<TKey>(newKeys);
            values = new List<TValue>(newValues);

            return true;
        }

        return false;
    }

    public TValue GetValue(TKey key)
    {
        int index = keys.IndexOf(key);
        if (index != -1)
        {
            return values[index];
        }
        throw new KeyNotFoundException();
    }

    public IEnumerable<TKey> Keys => keys;

    public IEnumerable<TValue> Values => values;

    // Implementing the IEnumerable interface for both keys and values
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            yield return new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }
    }

    // Implementing the IDictionary interface
    public int Count => keys.Count;

    public bool IsReadOnly => false;

    public ICollection<TKey> KeysCollection => keys;

    public ICollection<TValue> ValuesCollection => values;

    public TValue this[TKey key]
    {
        get => GetValue(key);
        set => Add(key, value);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    public void Clear()
    {
        keys.Clear();
        values.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return keys.Contains(item.Key) && values.Contains(item.Value);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            array[arrayIndex + i] = new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return Remove(item.Key);
    }

    public bool ContainsKey(TKey key)
    {
        return keys.Contains(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = keys.IndexOf(key);
        if (index != -1)
        {
            value = values[index];
            return true;
        }


        value = default(TValue);
        return false;
    }

    // Implementing the IList interface
    public int IndexOf(KeyValuePair<TKey, TValue> item)
    {
        int index = keys.IndexOf(item.Key);
        if (index != -1 && EqualityComparer<TValue>.Default.Equals(values[index], item.Value))
        {
            return index;
        }

        return -1;
    }

    public void Insert(int index, KeyValuePair<TKey, TValue> item)
    {
        keys.Insert(index, item.Key);
        values.Insert(index, item.Value);
    }

    public void RemoveAt(int index)
    {
        keys.RemoveAt(index);
        values.RemoveAt(index);
    }

    public KeyValuePair<TKey, TValue> this[int index]
    {
        get => new KeyValuePair<TKey, TValue>(keys[index], values[index]);
        set
        {
            keys[index] = value.Key;
            values[index] = value.Value;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        CustomDictionary<string, string> customDict = new CustomDictionary<string, string>();

        Console.WriteLine("For 3 elements:\n");

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("Enter the key: ");
            string key = Console.ReadLine();

            Console.WriteLine("Enter the value: ");
            string value = Console.ReadLine();

            customDict.Add(key, value);
        }

        Console.WriteLine("\nDictionary:");
        foreach (var kvp in customDict)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        Console.WriteLine("\nEnter the key to remove: ");
        string keyToRemove = Console.ReadLine();

        customDict.Remove(keyToRemove);

        Console.WriteLine("\nAfter removal of the element:");
        foreach (var kvp in customDict)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
