using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Security;

public class StringHashSet
{
    private HashSet<string> mStringSet = new HashSet<string>();

    public StringHashSet(string combineString)
    {
        if (string.IsNullOrEmpty(combineString)) return;

        var strArray = combineString.Split('|');
        foreach (var d in strArray)
        {
            mStringSet.Add(d);
        }
    }

    public StringHashSet()
    {

    }

    public bool IsEmpty()
    {
        return mStringSet.Count == 0;
    }

    public void AddString(string item)
    {
        mStringSet.Add(item);
    }

    public void RemoveString(string item)
    {
        mStringSet.Remove(item);
    }

    public HashSet<string> GetStringSet()
    {
        return mStringSet;
    }
}