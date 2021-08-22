
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class RandomTerrainType : TerrainType, IComparable
{
    [VectorRange(0, 1, 0, 1, true)]
    public Vector2 noiseRange;

    [Range(0.0f, 1f)]
    public float spawnChance = 1;

    public int CompareTo(object obj)
    {
        RandomTerrainType other = obj as RandomTerrainType;

        if (other != null)
        {
            return this.noiseRange.x.CompareTo(other.noiseRange.x);
        }

        return 0;
    }
}