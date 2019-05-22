using System;
using System.Collections.Generic;
using SplashKitSDK;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

// I, O, S, Z, T, J, L
public abstract class Tetromino
{
    protected Block[,] _MainGrid { get; set; }
    protected Block[,] _Blocks;
    protected Block[,] _CollisionBlocks;
    protected List<int[,]> _Rotations;
    protected Color BlockColor;
    protected int currentRotation = 0;
    protected int X { get; set; }
    protected int Y { get; set; }

    public Tetromino(int originX, int originY, Block[,] grid)
    {
        X = originX;
        Y = originY;

        _MainGrid = grid;

        _Blocks = new Block[4, 4];

        _Rotations = new List<int[,]>();

        _Rotations.Add(new int[4, 4]
            {
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        GenerateShape();
    }

    public void GenerateShape()
    {
        for (int i = 0; i < _Rotations[currentRotation].GetLength(0); i++)
        {
            for (int j = 0; j < _Rotations[currentRotation].GetLength(1); j++)
            {
                if (_Rotations[currentRotation][i, j] == 1)
                {
                    _Blocks[i, j] = new Block(BlockColor, X + j, Y + i);
                    _Blocks[i, j].Type = BlockType.Filled;
                }
                else
                {
                    _Blocks[i, j] = new Block(Color.Pink, X + j, Y + i);
                    _Blocks[i, j].Type = BlockType.Empty;
                }
            }
        }
    }

    public void AssignToGrid()
    {
        for (int i = 0; i < _Blocks.GetLength(0); i++)
        {
            for (int j = 0; j < _Blocks.GetLength(1); j++)
            {
                if (_Blocks[i, j].Type == BlockType.Filled)
                {
                    _MainGrid[i + Y, j + X] = _Blocks[i, j].Clone();
                }
            }
        }
    }

    public void DrawShape()
    {
        try
        {
            foreach (Block block in _Blocks)
            {
                block.Draw();
            }
        }
        catch
        {
            throw new Exception("No blocks contained in shape");
        }
    }

    public bool MoveShape(Direction dir)
    {
        if (dir == Direction.Down)
        {
            Y += 1;
            GenerateShape();
            return true;
        }

        if (dir == Direction.Up)
        {
            Y -= 1;
            GenerateShape();
            return true;
        }

        if (dir == Direction.Left)
        {
            X -= 1;
            GenerateShape();
            return true;
        }

        if (dir == Direction.Right)
        {
            X += 1;
            GenerateShape();
            return true;
        }

        return false;
    }

    public bool CheckOverlaps()
    {
        for (int i = 0; i < _Blocks.GetLength(0); i++)
        {
            for (int j = 0; j < _Blocks.GetLength(1); j++)
            {
                if ((_Blocks[i, j].Type == BlockType.Filled) && (_MainGrid[i + Y, j + X].Type == BlockType.Filled)
                    || (_Blocks[i, j].Type == BlockType.Filled) && (_MainGrid[i + Y, j + X].Type == BlockType.Wall))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void RotateClockwise()
    {
        currentRotation = ((currentRotation + 1) % _Rotations.Count);
        GenerateShape();
    }

    public void RotateCounterClockwise()
    {
        currentRotation = ((currentRotation - 1) % _Rotations.Count);
        GenerateShape();
    }

    public Tetromino Clone()
    {
        Tetromino other = (Tetromino)this.MemberwiseClone();
        return other;
    }
}

public class IShape : Tetromino
{
    public IShape(int originX, int originY, Block[,] grid) : base(originX, originY, grid)
    {
        _Blocks = new Block[4, 4];
        _Rotations = new List<int[,]>();

        _Rotations.Add(new int[4, 4]
            {
                {1,1,1,1},
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {0,1,0,0},
                {0,1,0,0},
                {0,1,0,0},
                {0,1,0,0}
            }
        );

        BlockColor = Color.Cyan;

        GenerateShape();
    }
}

public class OShape : Tetromino
{
    public OShape(int originX, int originY, Block[,] grid) : base(originX, originY, grid)
    {
        _Blocks = new Block[4, 4];
        _Rotations = new List<int[,]>();

        _Rotations.Add(new int[4, 4]
            {
                {0,1,1,0},
                {0,1,1,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        BlockColor = Color.Yellow;

        GenerateShape();
    }
}

public class TShape : Tetromino
{
    public TShape(int originX, int originY, Block[,] grid) : base(originX, originY, grid)
    {
        _Blocks = new Block[4, 4];
        _Rotations = new List<int[,]>();

        _Rotations.Add(new int[4, 4]
            {
                {1,1,1,0},
                {0,1,0,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {0,1,0,0},
                {1,1,0,0},
                {0,1,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {0,1,0,0},
                {1,1,1,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {1,0,0,0},
                {1,1,0,0},
                {1,0,0,0},
                {0,0,0,0}
            }
        );

        BlockColor = Color.Purple;

        GenerateShape();
    }
}

public class SShape : Tetromino
{
    public SShape(int originX, int originY, Block[,] grid) : base(originX, originY, grid)
    {
        _Blocks = new Block[4, 4];
        _Rotations = new List<int[,]>();

        _Rotations.Add(new int[4, 4]
            {
                {0,1,1,0},
                {1,1,0,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {1,0,0,0},
                {1,1,0,0},
                {0,1,0,0},
                {0,0,0,0}
            }
        );

        BlockColor = Color.Green;

        GenerateShape();
    }
}

public class ZShape : Tetromino
{
    public ZShape(int originX, int originY, Block[,] grid) : base(originX, originY, grid)
    {
        _Blocks = new Block[4, 4];
        _Rotations = new List<int[,]>();

        _Rotations.Add(new int[4, 4]
            {
                {1,1,0,0},
                {0,1,1,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {0,1,0,0},
                {1,1,0,0},
                {1,0,0,0},
                {0,0,0,0}
            }
        );

        BlockColor = Color.Red;

        GenerateShape();
    }
}

public class JShape : Tetromino
{
    public JShape(int originX, int originY, Block[,] grid) : base(originX, originY, grid)
    {
        _Blocks = new Block[4, 4];
        _Rotations = new List<int[,]>();

        _Rotations.Add(new int[4, 4]
            {
                {1,1,1,0},
                {0,0,1,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {0,1,0,0},
                {0,1,0,0},
                {1,1,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {1,0,0,0},
                {1,1,1,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {1,1,0,0},
                {1,0,0,0},
                {1,0,0,0},
                {0,0,0,0}
            }
        );

        BlockColor = Color.Blue;

        GenerateShape();
    }
}

public class LShape : Tetromino
{
    public LShape(int originX, int originY, Block[,] grid) : base(originX, originY, grid)
    {

        _Blocks = new Block[4, 4];
        _Rotations = new List<int[,]>();

        _Rotations.Add(new int[4, 4]
            {
                {1,1,1,0},
                {1,0,0,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {1,1,0,0},
                {0,1,0,0},
                {0,1,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {0,0,1,0},
                {1,1,1,0},
                {0,0,0,0},
                {0,0,0,0}
            }
        );

        _Rotations.Add(new int[4, 4]
            {
                {1,0,0,0},
                {1,0,0,0},
                {1,1,0,0},
                {0,0,0,0}
            }
        );

        BlockColor = Color.Orange;

        GenerateShape();
    }
}