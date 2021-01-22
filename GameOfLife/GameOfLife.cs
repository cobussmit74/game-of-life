using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
  public class GameOfLife : IGameOfLife
  {
    private int _width;
    private int _height;
    public void Reset(int width, int height)
    {
      _width = width;
      _height = height;
      Cells = new ICell[width, height];

      for (var w = 0; w < _width; w++)
      {
        for (var h = 0; h < _height; h++)
        {
          Cells[w, h] = new Cell(w,h);
        }
      }
    }

    public void Tick()
    {
      for (var w = 0; w < _width; w++)
      {
        for (var h = 0; h < _height; h++)
        {
          var theCell = Cells[w, h];
          theCell.NextState = theCell.CurrentState;

          var neighbors = GetNeighbors(w, h);

          var liveNeighbors = neighbors.Count(n => n.CurrentState);
          if (liveNeighbors < 2)
          {
            theCell.NextState = false;
          }
          if (liveNeighbors == 3)
          {
            theCell.NextState = true;
          }
          if (liveNeighbors > 3)
          {
            theCell.NextState = false;
          }
        }
      }

      for (var w = 0; w < _width; w++)
      {
        for (var h = 0; h < _height; h++)
        {
          var theCell = Cells[w, h];
          theCell.CurrentState = theCell.NextState;
        }
      }
    }

    private ICell[] GetNeighbors(int x, int y)
    {
      var neighbors = new List<ICell>();

      if (x > 0 && y > 0)
      {
        neighbors.Add(Cells[x - 1, y - 1]);
      }

      if (y > 0)
      {
        neighbors.Add(Cells[x, y - 1]);
      }

      if (x > 0)
      {
        neighbors.Add(Cells[x - 1, y]);
      }

      if (x < (_width - 1) && y < (_height - 1))
      {
        neighbors.Add(Cells[x + 1, y + 1]);
      }

      if (y < (_height - 1))
      {
        neighbors.Add(Cells[x, y + 1]);
      }

      if (x < (_width - 1))
      {
        neighbors.Add(Cells[x + 1, y]);
      }

      if (x < (_width - 1) && y > 0)
      {
        neighbors.Add(Cells[x + 1, y - 1]);
      }

      if (x > 0 && y < (_height - 1))
      {
        neighbors.Add(Cells[x - 1, y + 1]);
      }

      return neighbors.ToArray();
    }

    public ICell[,] Cells { get; private set; }
  }
}