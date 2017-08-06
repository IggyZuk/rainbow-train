public class GridPos
{
    public int x;
    public int y;

    public Vector Vector()
    {
        return new Vector(x, y);
    }

    public override string ToString()
    {
        return string.Format("x:{0}, y:{1}", x, y);
    }
}