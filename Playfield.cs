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

    public bool isRowFilled(int rowNum)
    {

        int numFilled = 0;
        for (int col = 1; col < _Columns - 1; col++)
        {
            if (_Grid[rowNum, col].Type == BlockType.Filled)
            {
                numFilled++;
            }
        }

        //is numfilled same as number of columns
        return numFilled == _Columns - 2;
    }

    public bool isRowEmpty(int rowNum)
    {

        int numEmpty = 0;
        for (int col = 1; col < _Columns - 1; col++)
        {
            if (_Grid[rowNum, col].Type == BlockType.Empty)
            {
                numEmpty++;
            }
        }

        //is numempty same as number of columns
        return numEmpty == _Columns - 2;
    }

    private void emptyRow(int row)
    {
        for (int col = 0; col < _Columns - 1; col++)
        {
            if (!((col == 0) || (col == _Columns - 1)))
            {
                _Grid[row, col] = _Grid[0, col].Clone();
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
                // swapRows(row, row - 1);
            }
        }
    }

    public void copyDown(int fromRow, int toRow) {

        if (isRowFilled(toRow)) {
            //do the actual copy.
        }

        if (fromRow - 1 > _Rows - 1)
            copyDown(fromRow - 1, toRow - 1);
    }

    // Checks through the current playfield, bottom to top, for matching lines, then clears the blocks
    public void ClearLines()
    {
        for (int i = _Rows - 1; i > 0; i--)
        {
            if (isRowFilled(i))
            {
                deleteRow(i);
            }
        }
        DrawBlocks();
    }

    public void UpdateGrid()
    {
        for (int row = _Rows - 1; row < 0; row--)
        {
            copyDown(0, row);
        }

        DrawBlocks();
    }
}