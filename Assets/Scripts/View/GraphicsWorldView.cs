using UnityEngine;

public class GraphicsWorldView : MonoBehaviour
{
    public void Tick(World model)
    {
        IMDraw.Axis3D(Vector3.zero, Quaternion.identity, 1000f, 0.2f);
        IMDraw.Grid3D(Vector3.zero, Quaternion.identity, 5f, 5f, 10, 10, new Color(1f, 1f, 1f, 0.5f));

        foreach (var p in model.points.Values)
        {
            GraphicsService.DrawCube(p.pos.Vector3(), Vector3.one * 0.25f, Config.Colors.Blue);
            GraphicsService.DrawLabel(p.pos.Vector3(), Config.Colors.White, p.id.ToString());

            float y = 0f;
            foreach (var c in p.colors)
            {
                y += 0.2f;
                GraphicsService.DrawCube(
                    p.pos.Vector3() + new Vector3(0.2f, 0f, y - 0.2f),
                    Vector3.one * 0.1f,
                    ColorService.GetUnityColor(c)
                );
            }
        }

        foreach (var c in model.connections.Values)
        {
            Point fromPoint = PointService.GetPointWithId(model, c.fromPointId);
            Point toPoint = PointService.GetPointWithId(model, c.toPointId);
            GraphicsService.DrawLine3D(fromPoint.pos.Vector3(), toPoint.pos.Vector3(), ColorService.GetUnityColor(c.color));
        }

        GraphicsService.DrawCube(model.train.pos.Vector3(), Vector3.one * 0.25f, Config.Colors.Red);
        GraphicsService.DrawLabel(model.train.pos.Vector3(), Config.Colors.Red, "TRAIN");
    }
}
