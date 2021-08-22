
using UnityEngine;

public class NoiseMap
{
    public static float[,] PerlinNoise(int width, int height, float seed)
    {
        float[,] map = new float[width, height];

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                float sampleX = (x + seed) / 10f;
                float sampleY = (y + seed) / 10f;

                map[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
            }
        }

        return map;
    }
}