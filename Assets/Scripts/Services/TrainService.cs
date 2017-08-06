using System.Collections.Generic;

public static class TrainService
{
    public static void MoveTrainForward(World model)
    {
        Point trainPoint = PointService.GetPointInGrid(model, model.train.gridPos);
        List<Connection> cons = ConnectionService.GetConnectionsForPointId(model, trainPoint.id);
        Logger.Log(model.train.selectedColor);
        Connection matchingConnection = cons.Find(x => x.color == model.train.selectedColor);
        if (matchingConnection == null)
        {
            Logger.Log("Game Over!");
        }
        else
        {
            Point forwardPoint = PointService.GetPointWithId(model, matchingConnection.toPointId);
            model.train.gridPos = forwardPoint.gridPos;
            model.train.pos = forwardPoint.pos;
        }
    }
}
