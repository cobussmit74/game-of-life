namespace GameOfLife
{
  public class Cell: ICell
  {
    public Cell(int x, int y)
    {
      X = x;
      Y = y;
    }
    public int X { get; }
    public int Y { get; }
    public bool CurrentState { get; set; }
    public bool NextState { get; set; }
  }
}