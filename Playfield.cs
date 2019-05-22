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
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Defining wall blocks
                if ((row == rows - 1) || (col == 0) || (col == columns - 1))
                {
                    // Default colour should never show
                    grid[row, col] = new Block(Color.Black, col, row);
                    grid[row, col].Type = BlockType.Wall;
                }
                else
                {
                    grid[row, col] = new Block(Color.Gray, col, row);
                    grid[row, col].Type = BlockType.Empty;
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

    private void deleteRow(int rowToDelete)
    {
        for (var row = rowToDelete; row > 0; row--)
        {
            copyRow(row - 1, row);
        }

        for (var col = 1; col < _Columns - 1; col++)
        {
            _Grid[0, col].Type = BlockType.Empty;
        }
    }

    private void copyRow(int fromRow, int toRow)
    {
        // Starting at 1 and finshing n - 1 to ignore walls
        for (var col = 1; col < _Columns - 1; col++)
        {
            // Copy the row downwards and redraw it one position lower
            _Grid[toRow, col] = _Grid[fromRow, col].Clone();
            _Grid[toRow, col].Y++;
        }
    }

    public void ClearLines()
    {
        for (int row = _Rows - 1; row > 0; row--)
        {
            while (isRowFilled(row)) deleteRow(row);
        }
        DrawBlocks();
    }

    private bool isRowFilled(int rowNum)
    {
        for (int col = 1; col < _Columns - 1; col++)
        {
            if (_Grid[rowNum, col].Type != BlockType.Filled)
            {
                return false;
            }
        }
        return true;
    }
}