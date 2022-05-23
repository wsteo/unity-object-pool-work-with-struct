using System;
using System.Collections.Generic;
using MonsterLove.Collections;
using UnityEngine;
using System.Collections;

public class EventBus : MonoBehaviour
{
	public float delay = 1;

	private ObjectPool<ExampleStruct> exampleStructPool;
	private List<ExampleStruct> exampleStructList;

	public List<long> seedList = new List<long>();
	public int seedIndex = 0;
	public int initialPoolSize = 5;

	public List<ExampleStruct> testCaseList = new List<ExampleStruct>();

	void Awake () 
	{
		System.Random random = new System.Random();

		testCaseList.Add(GenerateTestCase("ItemId1","Item Name 1",0));
		testCaseList.Add(GenerateTestCase("ItemId2", "Item Name 2", 1));
		testCaseList.Add(GenerateTestCase("ItemId3", "Item Name 3", 2));
		testCaseList.Add(GenerateTestCase("ItemId4", "Item Name 4", 3));
		testCaseList.Add(GenerateTestCase("ItemId5", "Item Name 5", 4));

		while (seedList.Count < initialPoolSize)
        {
			long seed = random.Next(1000, 9999 + initialPoolSize);
			
            if (!seedList.Contains(seed))
            {
				seedList.Add(seed);
			}
        }

		DateTimeOffset dateTimeOffset = new DateTimeOffset();
		long createdTime = dateTimeOffset.ToUnixTimeMilliseconds();
        exampleStructPool = new ObjectPool<ExampleStruct>(() => new ExampleStruct(() => GetSeed()), 5);
        exampleStructList = new List<ExampleStruct>();
	}

	public long GetSeed()
    {
		if (seedIndex < seedList.Count - 1)
		{
			seedIndex = ++seedIndex;
			return seedList[seedIndex];
		}
		else
		{
			seedIndex = 0;
			return seedList[0];
		}
    }

	void Update()
	{
		Debug.Log("");
		
		SpawnEvent();

		Debug.Log("");
	}

	private void SpawnEvent()
	{
		exampleStructList.Clear();
		for (int i = 0; i < testCaseList.Count; i++)
		{
			ExampleStruct exampleStructSingle = exampleStructPool.GetItem();

			exampleStructSingle.itemId = testCaseList[i].itemId;
			exampleStructSingle.itemName = testCaseList[i].itemName;
			exampleStructSingle.num1 = testCaseList[i].num1;

			exampleStructList.Add(exampleStructSingle);
			exampleStructPool.ReleaseItem(exampleStructSingle);
		}
	}

	private ExampleStruct GenerateTestCase(string itemId, string itemName, int num1)
    {
		ExampleStruct exampleStruct = new ExampleStruct();
		exampleStruct.itemId = itemId;
		exampleStruct.itemName = itemName;
		exampleStruct.num1 = num1;
		return exampleStruct;
	}
}
