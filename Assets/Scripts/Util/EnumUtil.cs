using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class EnumUtil{

    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static string GetString<T>(T e)
    {
        return Enum.GetName(typeof(T), e);
    }

    public static T GetEnum<T>(String s)
    {
        if (Enum.IsDefined(typeof(T), s))
        {
            return (T)Enum.Parse(typeof(T), s);
        }
        else
        {
            Debug.LogError("\"" + s + "\" Was not found in enum \"" + typeof(T).FullName + "\". Default value was returned instead");
            return default(T);
        }
    }
}
