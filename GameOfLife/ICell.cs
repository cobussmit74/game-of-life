namespace GameOfLife
{
  public interface ICell
  {
    int X { get; }
    int Y { get; }
    bool CurrentState { get; set; }
    bool NextState { get; set; }
  }
}