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

        for (int i = 0; i < 3; i++)
        {
            ColorType randomFreeColor = ColorService.GetFreeColor(model);
            Point randomPoint = PointService.GetPointInGrid(model, MathService.RandomRange(0, width), 0);
            PointService.AddColorToPointWithId(model, randomPoint.id, randomFreeColor);
        }
    }

    public static void ConnectPathFromPreviousNodeColors(World model, int width, int height)
    {
        for (int y = 1; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Point fromPoint = PointService.GetPointInGrid(model, x, y - 1);

                for (int i = fromPoint.colors.Count - 1; i >= 0; i--)
                {
                    ColorType color = fromPoint.colors[i];
                    fromPoint.colors.Remove(color);

                    Point toPoint = PointService.GetPointInGrid(
                        model,
                        MathService.RandomRange(
                            MathService.Clamp(x - 1, 0, width),
                            MathService.Clamp(x + 2, 0, width)),
                        y
                    );

                    ConnectionService.CreateConnection(model, fromPoint.id, toPoint.id, color);

                    if (MathService.Random() < 0.75f)
                    {
                        PointService.AddColorToPointWithId(model, toPoint.id, color);
                    }
                    else
                    {
                        ColorService.ReleaseColor(model, color);
                        for (int j = 0; j < MathService.RandomRange(1, 3); j++)
                        {
                            PointService.AddColorToPointWithId(model, toPoint.id, ColorService.GetFreeColor(model));
                        }
                    }

                }
            }
        }
    }
}