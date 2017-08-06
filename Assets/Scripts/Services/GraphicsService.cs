using UnityEngine;

public static class GraphicsService
{
    public static void DrawCube(Vector3 pos, Vector3 size, Color color)
    {
        IMDraw.Box3D(pos, size, new Color(color.r, color.g, color.b, 0.25f));
        IMDraw.WireBox3D(pos, size, color);
    }

    public static void DrawSphere(Vector3 pos, float radius, Color color)
    {
        IMDraw.Sphere3D(pos, radius, new Color(color.r, color.g, color.b, 0.25f));
        IMDraw.WireSphere3D(pos, radius, color);
    }

    public static void DrawLine3D(Vector3 fromPos, Vector3 toPos, float thickness, Color color)
    {
        Vector3 diff = toPos - fromPos;
        Quaternion dir = Quaternion.LookRotation(diff, Vector3.up);
        IMDraw.Box3D(Vector3.Lerp(fromPos, toPos, 0.5f), dir, new Vector3(thickness, thickness, diff.magnitude), color);
    }

    public static void DrawLine(Vector3 fromPos, Vector3 toPos, Color color)
    {
        IMDraw.Line3D(fromPos, toPos, color);
    }

    public static void DrawLabel(Vector3 pos, Color color, string text)
    {
        IMDraw.LabelShadowed(pos, 0f, 0f, color, LabelPivot.MIDDLE_CENTER, LabelAlignment.CENTER, "■");
        IMDraw.LabelShadowed(pos, 25f, 0f, color, LabelPivot.MIDDLE_LEFT, LabelAlignment.LEFT, text);

    }
}