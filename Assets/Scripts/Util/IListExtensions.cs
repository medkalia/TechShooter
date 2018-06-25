using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    /// <summary>
    /// Return true if the two lists have the same Elements in the same order
    /// </summary>
    public static bool AreEqualAndOrdered<T>(IList<T> list1, IList<T> list2)
    {
       return list1.SequenceEqual(list2); 
    }

    /// <summary>
    /// Return true if the two lists have the same Elements without considering the order
    /// </summary>
    public static bool AreEqual<T>(IList<T> list1, IList<T> list2)
    {
        bool _areEqual = false;
        if (list1.Count == list2.Count)
        {
            var filteredSequence = list1.Where(x => list2.Contains(x));
            if (filteredSequence.Count() == list1.Count)
            {
                _areEqual = true;
            }
        }
        return _areEqual;
    }
}
