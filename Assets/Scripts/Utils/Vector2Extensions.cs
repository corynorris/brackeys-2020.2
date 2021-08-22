using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions
{
    public static bool Contains(this Vector2 vec, float value)
    {
        return value >= vec.x && value <= vec.y;
    }
}
