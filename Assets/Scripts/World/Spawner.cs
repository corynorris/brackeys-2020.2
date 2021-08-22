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
            Instantiate(item,
                 availablePlaces[Random.Range(0, availablePlaces.Count)],
                 Quaternion.identity);
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

    //[Tooltip("References to the instantiated objects")]
    //private  List<Transform> spawnedItems = new List<Transform>();

    //[Tooltip("The settings of our map")]
    //public MapSettings mapSettings;

    //[Tooltip("The game objects to instantiate")]
    //public GameObject itemToSpawn;

    //public int amount = 50;

    //public bool cluster = false;

    //public LayerMask obstacleLayers;

    //private int spawned = 0;

    //public Tilemap tilemap;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    int totalSquaresOnMap = mapSettings.width * mapSettings.height;
    //    Vector2Int[] positionsOnMap = new Vector2Int[totalSquaresOnMap];

    //    for (int x = 0; x < mapSettings.width;x++)
    //    {
    //        for (int y = 0; y < mapSettings.height; y++)
    //        {
    //            positionsOnMap[x +(y* mapSettings.width)] = new Vector2Int(x, y);
    //        }
    //    }

    //    Vector2Int[] randomSpaces = positionsOnMap.OrderBy(x => Random.Range(0, totalSquaresOnMap)).ToArray();


    //    for (int i = 0; i < totalSquaresOnMap; i++)
    //    {
    //        // try and place at randomSpaces
    //        Transform transform = Instantiate(itemToSpawn, randomSpaces[i], Quaternion.identity);
    //    }
    //}


    //// Update is called once per frame
    //void Update()
    //{

    //}

    //private Vector3 PointOnMap(int)
}
