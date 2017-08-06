public static class ColorService
{
    public static void ResetFreeColors(World model)
    {
        foreach (ColorType color in System.Enum.GetValues(typeof(ColorType)))
        {
            if (color == ColorType.Gray) continue;

            model.freeColors.Add(color);
        }
    }

    public static ColorType GetFreeColor(World model)
    {
        if (model.freeColors.Count == 0) return ColorType.Gray;

        ColorType freeRandomColor = model.freeColors[MathService.RandomRange(0, model.freeColors.Count)];
        model.freeColors.Remove(freeRandomColor);
        return freeRandomColor;
    }

    public static void ReleaseColor(World model, ColorType color)
    {
        if (!model.freeColors.Contains(color))
        {
            model.freeColors.Add(color);
        }
    }

    public static UnityEngine.Color GetUnityColor(ColorType color)
    {
        switch (color)
        {
            case ColorType.Red:
            return UnityEngine.Color.red;
            case ColorType.Green:
            return UnityEngine.Color.green;
            case ColorType.Blue:
            return UnityEngine.Color.blue;
            case ColorType.Cyan:
            return UnityEngine.Color.cyan;
            case ColorType.Magenta:
            return UnityEngine.Color.magenta;
            case ColorType.Yellow:
            return UnityEngine.Color.yellow;
            case ColorType.Black:
            return UnityEngine.Color.black;
        }
        return UnityEngine.Color.gray;
    }
}