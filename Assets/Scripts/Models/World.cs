using System.Collections.Generic;

public class World
{
    public int seed = 0;
    public Dictionary<int, Point> points = new Dictionary<int, Point>();
    public int nextPointId = 0;
    public Dictionary<int, Connection> connections = new Dictionary<int, Connection>();
    public int nextConnectionId = 0;
    public List<ColorType> freeColors = new List<ColorType>();
    public Train train = new Train();
}