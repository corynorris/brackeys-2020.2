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

    //[Tooltip("If checked will randomize the item in the resource")]
    //public bool randomizeItemType = true;


    //private Item.ItemType GetRandomResource()
    //{
    //    System.Random random = new System.Random();
    //    int idx = random.Next(0, Item.Resources.Length);
    //    return Item.Resources[idx];
    //}

    override protected void AfterSpawn(GameObject gameObject, Vector3 spawnPosition) {

        float spawnDistance = SpawnDistance(spawnPosition);
        Resource resource = gameObject.GetComponent<Resource>();
        resource.item.amount = Mathf.CeilToInt(spawnDistance / distanceBeforeIncrease);
        
        //if (randomizeItemType) { 
        //     resource.item.itemType = GetRandomResource();
        //}
    }

}
