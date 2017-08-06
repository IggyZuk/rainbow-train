using UnityEngine;

public class WorldController : MonoBehaviour
{
    World model;
    int step;

    void Awake()
    {
        Camera.main.transform.position = new Vector3(
            Config.Width / 2f,
            Camera.main.transform.position.y,
            Config.Height / 2f
        );

        InitWorld(new World());
    }

    void InitWorld(World newModel)
    {
        model = newModel;
        ColorService.ResetFreeColors(model);
        PathService.GeneratePath(model, Config.Width, Config.Height);

        Point startPoint = PointService.GetStartingPoint(model, Config.Width);
        model.train.pos = startPoint.pos;
        model.train.gridPos = startPoint.gridPos;
        model.train.selectedColor = startPoint.colors[0];
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Next Step"))
        {
            PathService.ConnectPathFromPreviousNodeColors(model, Config.Width, ++step);
        }
        if (GUI.Button(new Rect(100, 0, 100, 50), "Complete"))
        {
            PathService.ConnectPathFromPreviousNodeColors(model, Config.Width, Config.Height);
        }
        if (GUI.Button(new Rect(0, 50, 100, 50), "Move"))
        {
            TrainService.MoveTrainForward(model);
        }
        if (GUI.Button(new Rect(100, 50, 100, 50), "Reset"))
        {
            InitWorld(new World());
            step = 0;
        }
        if (GUI.Button(new Rect(0, 100, 100, 50), "Save"))
        {
            Save();
        }
        if (GUI.Button(new Rect(100, 100, 100, 50), "Load"))
        {
            Load();
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

        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawSphere(model.train.pos.Vector3(), 0.2f);
    }

    public string GetModelJson()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(model);
    }

    public void Save()
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
        PlayerPrefs.SetString("world", json);
        Debug.Log("Saved: " + json);
        //json
    }

    public void Load()
    {
        string json = PlayerPrefs.GetString("world");
        World loadedModel = Newtonsoft.Json.JsonConvert.DeserializeObject<World>(json);
        InitWorld(loadedModel);
        Debug.Log("Loaded: " + json);
    }
}
