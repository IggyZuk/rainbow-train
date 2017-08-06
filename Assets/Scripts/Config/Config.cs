using UnityEngine;

public class GraphicsColors
{
    public Color White = new Color32(255, 255, 255, 255);
    public Color Red = new Color32(255, 127, 127, 255);
    public Color Orange = new Color32(255, 180, 127, 255);
    public Color Yellow = new Color32(223, 203, 111, 255);
    public Color Lime = new Color32(187, 223, 111, 255);
    public Color Green = new Color32(142, 223, 111, 255);
    public Color Cyan = new Color32(111, 223, 220, 255);
    public Color Blue = new Color32(111, 166, 223, 255);
    public Color Purple = new Color32(168, 143, 223, 255);
    public Color Pink = new Color32(223, 154, 222, 255);
}

public static class Config
{
    public const int Width = 5;
    public const int Height = 10;

    public static GraphicsColors Colors = new GraphicsColors();
}
