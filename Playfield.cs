using System;
using SplashKitSDK;

public class Playfield
{
    private int _Rows;
    private int _Columns;
    public Block[,] _Grid;
    private int clearedCounter = 0;

    public Playfield(int rows, int columns)
    {
        Block[,] grid = new Block[rows, columns];

        _Rows = rows;
        _Columns = columns;

        // Making a 2D array of blocks
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Defining wall blocks
                if ((i == rows - 1) || (j == 0) || (j == columns - 1))
                {
                    // Default colour should never show
                    grid[i, j] = new Block(Color.Black, j, i);
                    grid[i, j].Type = BlockType.Wall;
                }
                else
                {
                    grid[i, j] = new Block(Color.Gray, j, i);
                    grid[i, j].Type = BlockType.Empty;
                }
            }
        }
        _Grid = grid;
    }

    public void DrawBlocks()
    {
        foreach (Block block in _Grid)
        {
            if (block != null)
            {
                block.Draw();
            }
        }
    }

    private void swapRows(int row1, int row2)
    {
        for (int k = 0; k < _Columns - 1; k++)
        {
            if (!((k == 0) || (k == _Columns - 1)))
            {
                Block temp = _Grid[row1, k];
                _Grid[row1, k] = _Grid[row2, k];
                _Grid[row2, k] = temp;
            }
        }
    }

    private void deleteRow(int row)
    {
        for (int k = 0; k < _Columns - 1; k++)
        {
            if (!((k == 0) || (k == _Columns - 1)))
            {
                _Grid[row, k].Type = BlockType.Empty;
                swapRows(row, row - 1);
            }
        }
    }

    // Checks through the current playfield, bottom to top, for matching lines, then clears the blocks
    public void ClearLines()
    {
        int numFilled = 0;

        for (int i = _Rows - 1; i > 0; i--)
        {
            numFilled = 0;
            for (int j = 0; j < _Columns - 1; j++)
            {
                // Ignore walls
                if (!((i == _Rows - 1) || (j == 0) || (j == _Columns - 1)))
                {
                    if (_Grid[i, j].Type == BlockType.Filled)
                    {
                        numFilled++;
                        // When number of filled blocks is the width of the playfield minus walls, we clear the line
                        if (numFilled == _Columns - 2)
                        {
                            deleteRow(i);
                        }
                    }
                }
            }
        }
        DrawBlocks();
    }

    public void UpdateGrid()
    {
        int numEmpty = 0;

        for (int i = _Rows - 1; i > 0; i--)
        {
            numEmpty = 0;
            for (int j = 0; j < _Columns - 1; j++)
            {
                // Ignore walls
                if (!((i == _Rows - 1) || (j == 0) || (j == _Columns - 1)))
                {
                    if (_Grid[i, j].Type == BlockType.Empty)
                    {
                        numEmpty++;
                        // When number of filled blocks is the width of the playfield minus walls, we clear the line
                        if (numEmpty == _Columns - 2)
                        {
                            swapRows(i, i - 1);
                        }
                    }
                }
            }
        }
        DrawBlocks();
    }
}