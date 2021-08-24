using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SpawnManager))]
public class Spawner : MonoBehaviour
{


    [Header("Item Details")]
    public GameObject item;

    [Tooltip("If set, more enemies will spawn at each iteration")]
    public float spawnTime = 0f;

    [Tooltip("Number of items to spawn at each iteration")]
    public float itemsPerSpawn = 1f;


    [Header("Spawn Position")]
    [Tooltip("The distance from the spawner")]
    public Vector2 spawnRange;

    [Tooltip("Where to calculate distance from")]
    public GameObject spawnPosition;



    [Tooltip("The walkable tilemap to spawn items on")]
    public Tilemap tileMap;


    private List<Vector3> availablePlaces;
    private SpawnManager spawnManager;




    private void Start()
    {
        tileMap = transform.Find("Grid").Find("Ground").GetComponent<Tilemap>();
        spawnManager = GetComponent<SpawnManager>();
        SpawnItems();
    }

    internal void SpawnItems()
    {
        StopAllCoroutines();
        CalculateAvailableSpaces();
        StartSpawner();
    }

    private void StartSpawner()
    {
        if (item != null)
        {
            if (spawnTime > 0)
            {

                StartCoroutine("SpawnInterval");
            }
            else
            {
                SpawnNow();
            }
        }
    }

    public Vector3 GetSpawnPosition()
    {
        if (spawnPosition != null)
        {
            return spawnPosition.transform.position;
        }

        return tileMap.cellBounds.center;
    }

    private void CalculateAvailableSpaces()
    {
        //Position von jedem Floortile ermitteln:
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {

                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                float distance = Vector3.Distance(GetSpawnPosition(), place);

                if (tileMap.HasTile(localPlace) && distance > spawnRange.x && distance < spawnRange.y)
                {
                    availablePlaces.Add(place);
                }
            }
        }


        if (availablePlaces.Count == 0)
        {
            Debug.LogWarning("No available spaces to spawn for " + item.name);
        }
    }

    private void SpawnNow()
    {
        for (int i = 0; i < itemsPerSpawn; i++)
        {
            if (availablePlaces.Count == 0) break;
            int selectionIdx = Random.Range(0, availablePlaces.Count);
            Vector3 position = availablePlaces[selectionIdx];
            spawnManager.Spawn(item, position);
            availablePlaces.RemoveAt(selectionIdx);

        }
    }


IEnumerator SpawnInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnNow();
        }
    }


}
