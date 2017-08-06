using System.Collections.Generic;

public static class TrainService
{
    public static bool MoveTrainForward(World model)
    {
        Point trainPoint = PointService.GetPointInGrid(model, model.train.toGridPos);
        List<Connection> cons = ConnectionService.GetConnectionsForPointId(model, trainPoint.id);
        Logger.Log(model.train.selectedColor);
        Connection matchingConnection = cons.Find(x => x.color == model.train.selectedColor);
        if (matchingConnection == null)
        {
            Logger.Log("Game Over!");
            return false;
        }
        else
        {
            Point forwardPoint = PointService.GetPointWithId(model, matchingConnection.toPointId);
            model.train.fromGridPos = model.train.toGridPos;
            model.train.toGridPos = forwardPoint.gridPos;
            //model.train.pos = forwardPoint.pos;
            return true;
        }
    }

    public static void TickTrain(World model, float deltaTime)
    {
        model.train.progress += deltaTime;
        if (model.train.progress >= 1f)
        {
            if (MoveTrainForward(model))
            {
                model.train.progress -= 1f;
            }
            else {
                model.train.progress = 1f;
            }
        }
        model.train.pos = Vector.Lerp(
            model.train.fromGridPos.Vector(),
            model.train.toGridPos.Vector(),
            model.train.progress
        );
    }
}
