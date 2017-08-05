using UnityEngine;

public static class MathService
{
    public static float Random()
    {
        return UnityEngine.Random.value;
    }

    public static float RandomRange(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static int RandomRange(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static int Clamp(int v, int min, int max)
    {
        return Mathf.Clamp(v, min, max);
    }
}