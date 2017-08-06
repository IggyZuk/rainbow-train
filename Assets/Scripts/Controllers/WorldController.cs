using UnityEngine;

public class WorldController : MonoBehaviour
{
    World model;
    GraphicsWorldView view;

    int step;

    void Awake()
    {
        InitWorld();
        view = new GraphicsWorldView();
    }

    void InitWorld()
    {
        model = new World();
        Random.InitState(MathService.RandomRange(0, 1000));

        ColorService.ResetFreeColors(model);
        PathService.GeneratePath(model, Config.Width, Config.Height);

        Point startPoint = PointService.GetStartingPoint(model, Config.Width);
        model.train.pos = startPoint.pos;
        model.train.gridPos = startPoint.gridPos;
        model.train.selectedColor = startPoint.colors[0];
    }

    void Update()
    {
        view.Tick(model);
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
            InitWorld();
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

    public string GetModelJson()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(model);
    }

    public void Save()
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
        PlayerPrefs.SetString("world", json);

        Debug.Log("Saved: " + json);
    }

    public void Load()
    {
        string json = PlayerPrefs.GetString("world");
        World loadedModel = Newtonsoft.Json.JsonConvert.DeserializeObject<World>(json);
        model = loadedModel;
        Random.InitState(model.seed);

        Debug.Log("Loaded: " + json);

        step = 0;
    }
}
