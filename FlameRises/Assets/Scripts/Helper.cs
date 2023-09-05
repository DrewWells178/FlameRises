using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Helper
{
    public static float Clamp(float value, float min, float max)  
    {  
        return (value < min) ? min : (value > max) ? max : value;  
    }

    public static bool Close(Vector3 v1, Vector3 v2, float dis)
    {
        float dist = MathF.Sqrt(MathF.Pow(v1.x - v2.x, 2) + MathF.Pow(v1.y - v2.y, 2));
        //Debug.Log(dist);
        if (dist < dis)
        {
            return true;
        }
        return false;
    }
}
