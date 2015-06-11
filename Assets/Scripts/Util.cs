using UnityEngine;
using System.Collections;

public static class Util
{
    public static bool QuickSphereCheck(Vector3 point, Vector3 origin, float radius)
    {
        Vector3 delta = origin - point;
        return delta.sqrMagnitude <= radius * radius;
    }
}
