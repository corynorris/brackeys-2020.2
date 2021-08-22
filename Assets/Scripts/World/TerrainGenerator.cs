using System;

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainGenerator : MonoBehaviour
{

    private Tilemap groundTileMap;
    private Tilemap obstacleTileMap;

    [Tooltip("The settings of our map")]
    public MapSettings mapSetting;

    [Tooltip("The default type when no other option matches")]
    public TerrainType baseType;

    [Tooltip("The type to draw around map edges")]
    public TerrainType edgeType;

    [Tooltip("The types of terrain to generate")]
    public RandomTerrainType[] terrainTypes;

    public void OnEnable()
    {
        groundTileMap = transform.Find("Grid").Find("Ground").GetComponent<Tilemap>();
        obstacleTileMap = transform.Find("Grid").Find("Obstacles").GetComponent<Tilemap>();
    }

    public void ClearLayer()
    {
        if (groundTileMap == null) groundTileMap = transform.Find("Grid").Find("Ground").GetComponent<Tilemap>();
        if (obstacleTileMap == null) obstacleTileMap = transform.Find("Grid").Find("Obstacles").GetComponent<Tilemap>();
        groundTileMap.ClearAllTiles();
        obstacleTileMap.ClearAllTiles();
    }

    public void GenerateMapInEditor()
    {
        ClearLayer();
        Generate(mapSetting.width, mapSetting.height);
    }

    public void Generate(int width, int height)
    {
 
        float[,] noiseMap = GenerateNoiseMap(width, height);

    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (DrawEdge(x,y,width,height))
                {
                    SetTerrainType(x,y, edgeType);
                } 
                else
                {
                    float noise = noiseMap[x, y];
                    TerrainType terrainType = SelectTerrainFromNoise(noise);
                    SetTerrainType(x, y, terrainType);
                }
                           
            }
        }
    }

    private void SetTerrainType(int x, int y, TerrainType type) 
    {
        if (type.isWalkable)
        {
            groundTileMap.SetTile(new Vector3Int(x, y, 0), type.tile);
        }
        else
        {
            obstacleTileMap.SetTile(new Vector3Int(x, y, 0), type.tile);
        }
    }

    private bool DrawEdge(int x, int y, int width, int height)
    {
        return x == 0 || y == 0 || y == height - 1 || x == width - 1;
    }

    private float[,] GenerateNoiseMap(int width, int height)
    {
        float seed = mapSetting.randomSeed ? Time.time * terrainTypes.Length : mapSetting.seed;
        float[,] noiseMap = new float[width, height];
        noiseMap = NoiseMap.PerlinNoise(width + 1, height + 1, seed);
        return noiseMap;
    }



    private TerrainType SelectTerrainFromNoise(float noise)
    {
        // Get all the types with their weights for a given range

        WeightedRandomBag<TerrainType> choices = new WeightedRandomBag<TerrainType>();

        float totalSpawnChance = 0;

        noise = Mathf.Clamp(noise, 0, 1);

        for (int i = 0; i < terrainTypes.Length; i++)
        {
            if (terrainTypes[i].noiseRange.Contains(noise))
            {
                totalSpawnChance += terrainTypes[i].spawnChance;
                choices.Add(terrainTypes[i], terrainTypes[i].spawnChance);
            }
        }


        // if total < 1, fill in the rest with the base tile
        if (totalSpawnChance < 1f)
        {
            Debug.Log($"Noise {noise}, Spawn chance: {totalSpawnChance}");
            
            choices.Add(baseType, 1 - totalSpawnChance);
        }

        // Return a weighted random selection 
        return choices.GetRandom();
    }


}
#if UNITY_EDITOR
[CustomEditor(typeof(TerrainGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Reference to our script
        TerrainGenerator terrainGenerator = (TerrainGenerator)target;

        //Only show the mapsettings UI if we have a reference set up in the editor
        if (terrainGenerator.mapSetting != null)
        {
            Editor mapSettingEditor = CreateEditor(terrainGenerator.mapSetting);
            mapSettingEditor.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                terrainGenerator.GenerateMapInEditor();
            }

            if (GUILayout.Button("Clear"))
            {
                terrainGenerator.ClearLayer();
            }
        }
    }

}
#endif