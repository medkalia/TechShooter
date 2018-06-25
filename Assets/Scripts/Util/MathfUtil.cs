using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathfUtil  {

    public static float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    
    #region Time
    public static float MinutesToSeconds (float minutes)
    {
        return minutes * 60;
    }

    public static float HoursToSeconds(float hours)
    {
        return hours * 3600;
    }
    # endregion

}
