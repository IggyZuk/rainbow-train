public static class ColorService
{
    public static void ResetFreeColors(World model)
    {
        foreach (Color color in System.Enum.GetValues(typeof(Color)))
        {
            if (color == Color.Gray) continue;

            model.freeColors.Add(color);
        }
    }

    public static Color GetFreeColor(World model)
    {
        if (model.freeColors.Count == 0) return Color.Gray;

        Color freeRandomColor = model.freeColors[MathService.RandomRange(0, model.freeColors.Count)];
        model.freeColors.Remove(freeRandomColor);
        return freeRandomColor;
    }

    public static void ReleaseColor(World model, Color color)
    {
        if (!model.freeColors.Contains(color))
        {
            model.freeColors.Add(color);
        }
    }

    public static UnityEngine.Color GetUnityColor(Color color)
    {
        switch (color)
        {
            case Color.Red:
            return UnityEngine.Color.red;
            case Color.Green:
            return UnityEngine.Color.green;
            case Color.Blue:
            return UnityEngine.Color.blue;
            case Color.Cyan:
            return UnityEngine.Color.cyan;
            case Color.Magenta:
            return UnityEngine.Color.magenta;
            case Color.Yellow:
            return UnityEngine.Color.yellow;
            case Color.Black:
            return UnityEngine.Color.black;
        }
        return UnityEngine.Color.gray;
    }
}