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

}
