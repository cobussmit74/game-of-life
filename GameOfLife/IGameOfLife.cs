namespace GameOfLife
{
  public interface IGameOfLife
  {
    void Reset(int width, int height);
    void Tick();
    ICell[,] Cells { get; }
  }
}