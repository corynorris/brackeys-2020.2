using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Probably some global spawn settings would go here
    private Dictionary<Vector3, GameObject> objectsSpawned = new Dictionary<Vector3, GameObject>();
    private List<Spawner> spawners = new List<Spawner>();

    private void Start()
    {
        spawners.AddRange(GetComponents<Spawner>());
    }

    public void Spawn(GameObject item, Vector3 position)
    {
        if (!objectsSpawned.ContainsKey(position))
        {
            GameObject spawned = Instantiate(item, position, Quaternion.identity);
            objectsSpawned.Add(position, spawned);
        }

    }

    public void RestartSpawners()
    {
        DestroyAllObjects();
        foreach (Spawner spawner in spawners)
        {
            spawner.SpawnItems();
        }
    }


    private void DestroyAllObjects()
    {
        foreach (KeyValuePair<Vector3, GameObject> entry in objectsSpawned)
        {
            if (entry.Value != null)
            {
                Destroy(entry.Value);
            }
        }

        objectsSpawned.Clear();
    }

}
