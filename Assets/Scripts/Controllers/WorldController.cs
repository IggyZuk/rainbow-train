using System.Collections;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    World model = new World();

    void Awake()
    {
        Camera.main.transform.position = new Vector3(
            Config.Width / 2f,
            Camera.main.transform.position.y,
            Config.Height / 2f
        );

        PathService.GeneratePath(model, Config.Width, Config.Height);
        StartCoroutine(Connect_Coroutine());
    }

    IEnumerator Connect_Coroutine()
    {
        int life = 0;
        while (life <= Config.Height)
        {
            PathService.Connect(model, Config.Width, life);
            life++;
            yield return new WaitForSeconds(1);
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        foreach (var p in model.points.Values)
        {
            Gizmos.color = UnityEngine.Color.white;
            Gizmos.DrawSphere(p.pos.Vector3(), 0.2f);

            float y = 0f;
            foreach (var c in p.colors)
            {
                y += 0.2f;
                Gizmos.color = GetColor(c);
                Gizmos.DrawSphere(p.pos.Vector3() + new Vector3(0.2f, 0f, y - 0.2f), 0.1f);
            }
        }

        foreach (var c in model.connections.Values)
        {
            Gizmos.color = GetColor(c.color);
            Point fromPoint = PointService.GetPointWithId(model, c.fromPointId);
            Point toPoint = PointService.GetPointWithId(model, c.toPointId);
            Gizmos.DrawLine(fromPoint.pos.Vector3(), toPoint.pos.Vector3());
        }
    }

    public UnityEngine.Color GetColor(Color color)
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
        }
        return UnityEngine.Color.blue;
    }
}
