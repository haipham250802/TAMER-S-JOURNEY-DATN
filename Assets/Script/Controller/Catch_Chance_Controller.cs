using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch_Chance_Controller : MonoBehaviour
{
    private static Catch_Chance_Controller instance;
    public static Catch_Chance_Controller Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Catch_Chance_Controller();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        //test();
    }
/*    void test()
    {
        Dictionary<Element,float> list = new Dictionary<Element, float>();
       
        list.Add(2, 80f);
        Debug.Log(GetRandomByPercent(list));
    }*/
    public static T GetRandomByPercent<T>(Dictionary<T, float> dicPercent)
    {
        T result = From<T>(dicPercent);
        return result;
    }
    private static T From<T>(Dictionary<T, float> spawnRate)
    {
        WeightedRandomBag<T> bag = new WeightedRandomBag<T>();
        foreach (var item in spawnRate)
        {
            bag.AddEntry(item.Key, item.Value);
        }
        return bag.GetRandom();
        //return new WeightedRandomizer<T>(spawnRate);
    }
}
public class WeightedRandomBag<T>
{
    private struct Entry
    {
        public float accumulatedWeight;
        public T item;
    }

    private List<Entry> entries = new List<Entry>();
    private float accumulatedWeight;
    private System.Random rand = new System.Random();

    public void AddEntry(T item, float weight)
    {
        this.accumulatedWeight += weight;
        entries.Add(new Entry { item = item, accumulatedWeight = weight });
    }

    public T GetRandom()
    {
        //double r = rand.NextDouble() * accumulatedWeight;

        //foreach (Entry entry in entries)
        //{
        //    if (entry.accumulatedWeight >= r)
        //    {
        //        return entry.item;
        //    }
        //}

        var randomPoint = UnityEngine.Random.value * this.accumulatedWeight;

        for (int i = 0; i < entries.Count; i++)
        {
            if (randomPoint < entries[i].accumulatedWeight)
                return entries[i].item;
            else
                randomPoint -= entries[i].accumulatedWeight;
        }
        return default(T); //should only happen when there are no entries
    }
}
