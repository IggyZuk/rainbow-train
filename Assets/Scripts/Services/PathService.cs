using System.Collections.Generic;

public static class PathService
{
    public static void GeneratePath(World model, int width, int height)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Point p = PointService.CreatePoint(model);
                p.pos = new Vector(x, y);
                p.gridPos.x = x;
                p.gridPos.y = y;
            }
        }

        for (int i = 0; i < (int)Color.Count; i++)
        {
            int randomId = PointService.GetPointInGrid(model, MathService.RandomRange(0, width), 0).id;
            Color randomColor = (Color)i;
            PointService.AddColorToPointWithId(model, randomId, randomColor);
        }
    }

    public static void Connect(World model, int width, int height)
    {
        for (int y = 1; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Point fromPoint = PointService.GetPointInGrid(model, x, y - 1);

                for (int i = fromPoint.colors.Count - 1; i >= 0; i--)
                {
                    Color color = fromPoint.colors[i];

                    Point toPoint = PointService.GetPointInGrid(
                        model,
                        MathService.RandomRange(
                            MathService.Clamp(x - 1, 0, width),
                            MathService.Clamp(x + 2, 0, width)),
                        y
                    );
                    ConnectionService.CreateConnection(model, fromPoint.id, toPoint.id, color);
                    PointService.AddColorToPointWithId(model, toPoint.id, color);
                    fromPoint.colors.Remove(color);
                }
            }
        }
    }
}