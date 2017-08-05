public static class PointService
{
    public static Point CreatePoint(World model)
    {
        Point p = new Point();
        p.id = model.nextPointId;
        model.nextPointId++;
        model.points.Add(p.id, p);
        return p;
    }

    public static Point GetPointWithId(World model, int id)
    {
        if (model.points.ContainsKey(id))
        {
            return model.points[id];
        }
        return null;
    }

    public static Point GetPointInGrid(World model, int x, int y)
    {
        foreach (var p in model.points.Values)
        {
            if (p.gridPos.x == x && p.gridPos.y == y)
            {
                return p;
            }
        }
        return null;
    }

    public static void AddColorToPointWithId(World model, int id, Color color)
    {
        Point p = GetPointWithId(model, id);
        if (p != null)
        {
            p.colors.Add(color);
        }
    }
}