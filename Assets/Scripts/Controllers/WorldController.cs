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
        model.train.fromGridPos = startPoint.gridPos;
        model.train.toGridPos = startPoint.gridPos;
        model.train.color = startPoint.colors[0];
        model.train.nextColor = model.train.color;

        PathService.ConnectPathFromPreviousNodeColors(model, Config.Width, Config.Height);
    }

    void Update()
    {
        TrainService.TickTrain(model, Time.deltaTime);
        view.Tick(model);

        Vector3 camPos = model.train.pos.Vector3();
        Camera.main.transform.position = camPos;
        Camera.main.transform.localPosition += -Camera.main.transform.forward * 10f;
    }

    void OnGUI()
    {
        DrawDebugButtons();
        DrawColorButtons();
    }

    void DrawDebugButtons()
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
        if (GUI.Button(new Rect(0, 150, 100, 50), "Toggle Nodes"))
        {
            model.debugSettings.isShowingNodes = !model.debugSettings.isShowingNodes;
        }
        if (GUI.Button(new Rect(100, 150, 100, 50), "Toggle Connection Info"))
        {
            model.debugSettings.isShowingConnectionInfo = !model.debugSettings.isShowingConnectionInfo;
        }
    }

    void DrawColorButtons()
    {
        int width = Screen.width / 8;
        int height = 200;

        GUI.backgroundColor = Color.red;
        if (GUI.Button(new Rect(width * 0, Screen.height - height, width, height), "Red"))
        {
            model.train.nextColor = ColorType.Red;
        }
        GUI.backgroundColor = Color.green;

        if (GUI.Button(new Rect(width * 1, Screen.height - height, width, height), "Green"))
        {
            model.train.nextColor = ColorType.Green;
        }
        GUI.backgroundColor = Color.blue;
        if (GUI.Button(new Rect(width * 2, Screen.height - height, width, height), "Blue"))
        {
            model.train.nextColor = ColorType.Blue;
        }
        GUI.backgroundColor = Color.cyan;
        if (GUI.Button(new Rect(width * 3, Screen.height - height, width, height), "Cyan"))
        {
            model.train.nextColor = ColorType.Cyan;
        }
        GUI.backgroundColor = Color.magenta;

        if (GUI.Button(new Rect(width * 4, Screen.height - height, width, height), "Magenta"))
        {
            model.train.nextColor = ColorType.Magenta;
        }
        GUI.backgroundColor = Color.yellow;
        if (GUI.Button(new Rect(width * 5, Screen.height - height, width, height), "Yellow"))
        {
            model.train.nextColor = ColorType.Yellow;
        }
        GUI.backgroundColor = Color.black;
        if (GUI.Button(new Rect(width * 6, Screen.height - height, width, height), "Black"))
        {
            model.train.nextColor = ColorType.Black;
        }
        GUI.backgroundColor = Color.gray;
        if (GUI.Button(new Rect(width * 7, Screen.height - height, width, height), "Gray"))
        {
            model.train.nextColor = ColorType.Gray;
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
