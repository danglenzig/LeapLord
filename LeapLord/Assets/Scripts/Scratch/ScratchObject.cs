using UnityEngine;
using System.Collections.Generic;


public class ScratchObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<int>? someInts = new List<int>();
        someInts = ListTools.GetUniqueRandomIntsFromRange(5, 10, 20);

        if (someInts != null)
        {
            foreach(int _val in someInts)
            {
                Debug.Log(_val);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
