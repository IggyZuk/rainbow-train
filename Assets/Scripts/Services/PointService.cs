using System.Collections.Generic;

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

    public static Point GetPointInGrid(World model, GridPos gridPos)
    {
        return GetPointInGrid(model, gridPos.x, gridPos.y);
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

    public static Point GetStartingPoint(World model, int width)
    {
        List<Point> points = new List<Point>();
        for (int i = 0; i < width; i++)
        {
            Point p = GetPointInGrid(model, i, 0);
            if (p.colors.Count > 0)
            {
                points.Add(p);
            }
        }
        if (points.Count > 0)
        {
            return points[MathService.RandomRange(0, points.Count)];
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