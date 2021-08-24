using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spawnable
{
    public int amount;
    public float spawnChance;
    public Vector2 range;
    public GameObject spawnTarget;
}
