using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    private static WayPointManager _instance;
    public static WayPointManager Instance
    {
        get
        {
            return _instance;
        }
        
    }
    //give every enemy different path
    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int>();

    private void Awake() 
    {
        _instance = this;
        int tempCount = rawIndex.Count;
        for(int i = 0; i < tempCount; i++) 
        {
            int tempIndex = Random.Range(0, rawIndex.Count); //random path index
            usingIndex.Add(rawIndex[tempIndex]);//distribute path
            rawIndex.RemoveAt(tempIndex);//delete index(avoid same)
        }
    }

}
