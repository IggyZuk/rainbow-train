using UnityEngine;

public class GraphicsWorldView : MonoBehaviour
{
    public void Tick(World model)
    {
        IMDraw.Axis3D(Vector3.zero, Quaternion.identity, 1000f, 0.2f);
        IMDraw.Grid3D(Vector3.zero, Quaternion.identity, 5f, 5f, 10, 10, new Color(1f, 1f, 1f, 0.5f));

        foreach (var p in model.points.Values)
        {
            if (model.debugSettings.isShowingNodes)
            {
                GraphicsService.DrawCube(p.pos.Vector3(), Vector3.one * 0.25f, Config.Colors.Blue);
                GraphicsService.DrawLabel(p.pos.Vector3(), Config.Colors.White, p.id.ToString() + "\n" + p.pos.ToString());
            }

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
            var color = ColorService.GetUnityColor(c.color);
            color.a = 0.75f;
            GraphicsService.DrawLine3D(fromPoint.pos.Vector3(), toPoint.pos.Vector3(), 0.1f, color);

            if (model.debugSettings.isShowingConnectionInfo)
            {
                GraphicsService.DrawLabel(
                    Vector.Lerp(fromPoint.pos, toPoint.pos, 0.5f).Vector3(),
                    Config.Colors.White,
                    c.id.ToString()
                );
            }
        }

        Color trainColor = ColorService.GetUnityColor(model.train.selectedColor);
        GraphicsService.DrawCube(model.train.pos.Vector3(), Vector3.one * 0.5f, trainColor);
        GraphicsService.DrawLabel(model.train.pos.Vector3(), trainColor, "TRAIN" + "\n" + model.train.toGridPos);
    }
}
