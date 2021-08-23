using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{

    [Header("Item")]
    public GameObject item;

    [Tooltip("If set, more enemies will spawn at each iteration")]
    public float spawnTime = 0f;

    [Tooltip("Number of items to spawn at each iteration")]
    public float itemsPerSpawn = 1f;

    [Tooltip("The walkable tilemap to spawn items on")]
    public Tilemap tileMap;

    private List<Vector3> availablePlaces;

    private List<Transform> objectsSpawned = new List<Transform>();



    private void Start()
    {
        //Position von jedem Floortile ermitteln:
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    availablePlaces.Add(place);
                }
            }
        }


        if (item != null)
        {
            if (spawnTime > 0)
            {

                StartCoroutine("StartSpawner");
            } 
            else
            {
                SpawnItem();
            }
        }
    }

    private void SpawnItem()
    {
        for (int i = 0; i < itemsPerSpawn; i++)
        {
            int selectionIdx = Random.Range(0, availablePlaces.Count);
            Transform spawned = Instantiate(item, availablePlaces[selectionIdx], Quaternion.identity).transform;
            availablePlaces.RemoveAt(selectionIdx);
            objectsSpawned.Add(spawned);
        }
    }

    IEnumerator StartSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnItem();
        }
    }

   
}
