public static class ConnectionService
{
    public static Connection CreateConnection(World model, int fromPointId, int toPointId, Color color)
    {
        Point fromPoint = PointService.GetPointWithId(model, fromPointId);
        Point toPoint = PointService.GetPointWithId(model, toPointId);

        Connection c = new Connection();
        c.id = model.nextConnectionId;
        c.fromPointId = fromPointId;
        c.toPointId = toPointId;
        c.color = color;

        model.nextConnectionId++;
        model.connections.Add(c.id, c);

        return c;
    }
}