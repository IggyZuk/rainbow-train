using System.Collections.Generic;

public class World
{
    public Dictionary<int, Point> points = new Dictionary<int, Point>();
    public int nextPointId = 0;
    public Dictionary<int, Connection> connections = new Dictionary<int, Connection>();
    public int nextConnectionId = 0;
}