using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static Vector3 GetRandomDir()
    {
        return UnityEngine.Random.insideUnitCircle.normalized;
    }
}
