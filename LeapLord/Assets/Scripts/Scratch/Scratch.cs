using UnityEngine;
using System.Collections.Generic;


public class ListTools
{
    public static List<int>? GetUniqueRandomIntsFromRange(int _number, int _min, int _max)
    // return a list of _number unique random integers between _min and _max
    {
        // yes I know this part is ugly. Not concerned with that right now
        int span = Mathf.Abs(_max - _min);
        if (span <= 1)
        {
            Debug.LogWarning("Invalid min and max values");
            return null;
        }
        if (_max < _min)
        {
            Debug.LogWarning("Max must be greater than min.");
            return null;
        }

        if (_number >= span)
        {
            Debug.LogWarning($"Can't return more than {span}");
            return null;
        }

        // here's where I want help...
        List<int> chosenInts = new List<int>();

        // Do all this stuff _number times...
        for (int i = 0; i <= _number; i++)
        {
            // create an empty list that will contain "eligible" (i.e. not already chosen) values
            List<int> eligibleInts = new List<int>();
            
            // for each value in the range
            for (int j = _min; j < _max; j++)
            {
                // if the number is not already chosen
                if (!chosenInts.Contains(j))
                {
                    // add it to the eligible list
                    eligibleInts.Add(j);
                }
            }
            // grab a random value from the eligible list, and add it to chosen
            int aRando = Random.Range(0, eligibleInts.Count);
            chosenInts.Add(eligibleInts[aRando]);
        }


        // For sanity checking...
        /*
        string debugString = "";
        foreach (int _value in chosenInts)
        {
            debugString += $" {_value } ";
        }
        Debug.Log(debugString);
        */

        return chosenInts;
    }
}