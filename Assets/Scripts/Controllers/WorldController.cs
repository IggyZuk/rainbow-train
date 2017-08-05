using UnityEngine;

public class WorldController : MonoBehaviour
{
    World model = new World();
    int step = 0;

    void Awake()
    {
        Camera.main.transform.position = new Vector3(
            Config.Width / 2f,
            Camera.main.transform.position.y,
            Config.Height / 2f
        );

        ColorService.ResetFreeColors(model);
        PathService.GeneratePath(model, Config.Width, Config.Height);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Next Step"))
        {
            PathService.ConnectPathFromPreviousNodeColors(model, Config.Width, ++step);
        }
        if (GUI.Button(new Rect(100, 0, 100, 50), "Reset"))
        {
            model = new World();
            ColorService.ResetFreeColors(model);
            PathService.GeneratePath(model, Config.Width, Config.Height);
            step = 0;
        }

    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        foreach (var p in model.points.Values)
        {
            Gizmos.color = UnityEngine.Color.white;
            Gizmos.DrawSphere(p.pos.Vector3(), 0.1f);

            float y = 0f;
            foreach (var c in p.colors)
            {
                y += 0.2f;
                Gizmos.color = ColorService.GetUnityColor(c);
                Gizmos.DrawSphere(p.pos.Vector3() + new Vector3(0.2f, 0f, y - 0.2f), 0.1f);
            }
        }

        foreach (var c in model.connections.Values)
        {
            Gizmos.color = ColorService.GetUnityColor(c.color);
            Point fromPoint = PointService.GetPointWithId(model, c.fromPointId);
            Point toPoint = PointService.GetPointWithId(model, c.toPointId);
            Gizmos.DrawLine(fromPoint.pos.Vector3(), toPoint.pos.Vector3());
        }
    }
}
