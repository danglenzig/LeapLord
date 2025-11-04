using UnityEngine;
using System;
using System.Collections.Generic;

namespace MiscTools
{
    public class RandomTools
    {
        public static List<T>? GetRandomUniqueElements<T>(List<T> inList, int count)
        {
            List<T> outList = new List<T>();

            if (count < 0 || count > inList.Count)
            {
                Debug.LogError("Invalid parameters");
                return null;
            }

            // make a copy of inList
            List<T> inListCopy = new List<T>(inList);

            // Fisher-Yates shuffle
            for (int i = inListCopy.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (inListCopy[i], inListCopy[j]) = (inListCopy[j], inListCopy[i]);
            }

            // grab the first N (count) elements of the shuffled list
            outList = inListCopy.GetRange(0, count);

            return outList;
        }
    }
}

