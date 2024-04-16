using UnityEngine;

public static class Utilities
{
    public static float MapValue(float value, float originalMin, float originalMax, float newMin, float newMax, bool clamp)
    {
        float newValue = (value - originalMin) / (originalMax - originalMin) * (newMax - newMin) + newMin;
        if (clamp == true)
            newValue = Mathf.Clamp(newValue, newMin, newMax);
        return newValue;
    }
}