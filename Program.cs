using System;
using System.Collections.Generic;


public class CustomDictionary<TKey, TValue>
{
    private List<TKey> keys;
    private List<TValue> values;

    public CustomDictionary()
    {
        keys = new List<TKey>();
        values = new List<TValue>();
    }

    //public void Add(TKey key, TValue value)
    //{
    //    keys.Add(key);
    //    values.Add(value);
    //}

    public void Add(TKey key, TValue value)
    {
        TKey[] newKeys = new TKey[keys.Count + 1];
        TValue[] newValues = new TValue[values.Count + 1];

        for (int i = 0; i < keys.Count; i++)
        {
            newKeys[i] = keys[i];
            newValues[i] = values[i];
        }

        newKeys[keys.Count] = key;
        newValues[values.Count] = value;

        keys = new List<TKey>(newKeys);
        values = new List<TValue>(newValues);

    }


    //public bool Remove(TKey key)
    //{
    //    int index = keys.IndexOf(key);

    //    if (index != -1)
    //    {
    //        keys.RemoveAt(index);
    //        values.RemoveAt(index);

    //        return true;
    //    } return false;
    //}

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

    public IEnumerable<TKey> Keys
    {
        get
        {
            return keys;
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            return values;
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
        foreach (var key in customDict.Keys)
        {
            var value = customDict.GetValue(key);
            Console.WriteLine($"Key: {key}, Value: {value}");
        }

        Console.WriteLine("\nEnter the key to remove: ");
        string keyToRemove = Console.ReadLine();

        customDict.Remove(keyToRemove);

        Console.WriteLine("\nAfter removal of the element:");
        foreach (var key in customDict.Keys)
        {
            var value = customDict.GetValue(key);
            Console.WriteLine($"Key: {key}, Value: {value}");
        }
    }
}
