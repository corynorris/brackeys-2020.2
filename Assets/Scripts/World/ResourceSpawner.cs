using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner
{

    [Tooltip("The distance after which to increase the amount of resource spawned")]
    public float distanceBeforeIncrease = 70f;

    [Tooltip("If checked will randomize the item in the resource")]
     public bool randomizeItemType = true;

    private Item.ItemType GetRandomItemType()
    {
        Array values = Enum.GetValues(typeof(Item.ItemType));
        System.Random random = new System.Random();
        return (Item.ItemType)values.GetValue(random.Next(values.Length));
    }

    override protected void AfterSpawn(GameObject gameObject, Vector3 spawnPosition) {

        float spawnDistance = SpawnDistance(spawnPosition);
        Resource resource = gameObject.GetComponent<Resource>();
        resource.item.amount = Mathf.CeilToInt(spawnDistance / distanceBeforeIncrease);
        
        if (randomizeItemType) { 
             resource.item.itemType = GetRandomItemType();
        }
    }

}
