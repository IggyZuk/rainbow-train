public class Train
{
    public Vector pos = new Vector();
    public GridPos fromGridPos = new GridPos();
    public GridPos toGridPos = new GridPos();
    public ColorType color = ColorType.Gray;
    public ColorType nextColor = ColorType.Gray;
    public float progress = 0f;
}
