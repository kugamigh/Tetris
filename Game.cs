using System;
using System.Collections.Generic;
using SplashKitSDK;

public class Game
{
    private Playfield TetrisGrid = new Playfield(20, 10);
    public Tetromino _CurrentShape;
    public Tetromino _CollisionTester;
    public bool Quit = false;
    public bool _GameOver = false;
    public Timer gameTimer;
    public List<Tetromino> BagOfPieces = new List<Tetromino>();

    public Game(Window gameWindow)
    {
        FillBagOfPieces();
        _CurrentShape = ChooseNewShape();
        _CollisionTester = _CurrentShape.Clone();
        gameTimer = SplashKit.CreateTimer("gameTicks");
        gameTimer.Start();
        gameTimer.Pause();
    }

    private void MoveLeft()
    {
        if (gameTimer.IsPaused)
        {
            gameTimer.Start();
        }

        Console.WriteLine("Moving left");

        if (_CollisionTester.MoveShape(Direction.Left) && !_CollisionTester.CheckOverlaps())
        {
            _CurrentShape.MoveShape(Direction.Left);
        }
        else
        {
            _CollisionTester.MoveShape(Direction.Right);
        }
    }

    private void MoveRight()
    {
        if (gameTimer.IsPaused)
        {
            gameTimer.Start();
        }

        Console.WriteLine("Moving right");

        if (_CollisionTester.MoveShape(Direction.Right) && !_CollisionTester.CheckOverlaps())
        {
            _CurrentShape.MoveShape(Direction.Right);
        }
        else
        {
            _CollisionTester.MoveShape(Direction.Left);
        }
    }

    private void MoveDown()
    {
        if (gameTimer.IsPaused)
        {
            gameTimer.Start();
        }

        Console.WriteLine("Moving down");

        if (_CollisionTester.MoveShape(Direction.Down) && !_CollisionTester.CheckOverlaps())
        {
            _CurrentShape.MoveShape(Direction.Down);
        }
        else
        {
            _CollisionTester.MoveShape(Direction.Up);
            _CurrentShape.AssignToGrid();
            TetrisGrid.ClearLines();
            _CurrentShape = ChooseNewShape();
            _CollisionTester = _CurrentShape.Clone();
            _CurrentShape.GenerateShape();
            _CollisionTester.GenerateShape();
        }
    }

    private void RotatePiece()
    {
        if (gameTimer.IsPaused)
        {
            gameTimer.Start();
        }

        Console.WriteLine("Rotating shape");

        _CollisionTester.RotateClockwise();
        if (!_CollisionTester.CheckOverlaps())
        {
            _CurrentShape.RotateClockwise();
        }
        else
        {
            _CollisionTester.RotateCounterClockwise();
        }
    }

    public void HandleInput()
    {
        SplashKit.ProcessEvents();

        // Manual block generation for debugging/cheating
        if (SplashKit.KeyTyped(KeyCode.IKey))
        {
            Console.WriteLine("Generating I");
            _CurrentShape = new IShape(3, 0, TetrisGrid._Grid);
            _CollisionTester = new IShape(3, 0, TetrisGrid._Grid);
        }
        if (SplashKit.KeyTyped(KeyCode.OKey))
        {
            Console.WriteLine("Generating O");
            _CurrentShape = new OShape(3, 0, TetrisGrid._Grid);
            _CollisionTester = new OShape(3, 0, TetrisGrid._Grid);
        }
        if (SplashKit.KeyTyped(KeyCode.TKey))
        {
            Console.WriteLine("Generating T");
            _CurrentShape = new TShape(3, 0, TetrisGrid._Grid);
            _CollisionTester = new TShape(3, 0, TetrisGrid._Grid);
        }
        if (SplashKit.KeyTyped(KeyCode.SKey))
        {
            Console.WriteLine("Generating S");
            _CurrentShape = new SShape(3, 0, TetrisGrid._Grid);
            _CollisionTester = new SShape(3, 0, TetrisGrid._Grid);
        }
        if (SplashKit.KeyTyped(KeyCode.ZKey))
        {
            Console.WriteLine("Generating Z");
            _CurrentShape = new ZShape(3, 0, TetrisGrid._Grid);
            _CollisionTester = new ZShape(3, 0, TetrisGrid._Grid);
        }
        if (SplashKit.KeyTyped(KeyCode.JKey))
        {
            Console.WriteLine("Generating J");
            _CurrentShape = new JShape(3, 0, TetrisGrid._Grid);
            _CollisionTester = new JShape(3, 0, TetrisGrid._Grid);
        }
        if (SplashKit.KeyTyped(KeyCode.LKey))
        {
            Console.WriteLine("Generating L");
            _CurrentShape = new LShape(3, 0, TetrisGrid._Grid);
            _CollisionTester = new LShape(3, 0, TetrisGrid._Grid);
        }

        // Movement and rotation
        if (SplashKit.KeyTyped(KeyCode.DownKey))
        {
            MoveDown();
        }

        if (SplashKit.KeyTyped(KeyCode.LeftKey))
        {
            MoveLeft();
        }

        if (SplashKit.KeyTyped(KeyCode.RightKey))
        {
            MoveRight();
        }

        if (SplashKit.KeyTyped(KeyCode.UpKey))
        {
            RotatePiece();
        }

        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            Quit = true;
        }
    }

    public void FillBagOfPieces()
    {
        BagOfPieces.Add(new IShape(3, 0, TetrisGrid._Grid));
        BagOfPieces.Add(new OShape(3, 0, TetrisGrid._Grid));
        BagOfPieces.Add(new TShape(3, 0, TetrisGrid._Grid));
        BagOfPieces.Add(new SShape(3, 0, TetrisGrid._Grid));
        BagOfPieces.Add(new ZShape(3, 0, TetrisGrid._Grid));
        BagOfPieces.Add(new JShape(3, 0, TetrisGrid._Grid));
        BagOfPieces.Add(new LShape(3, 0, TetrisGrid._Grid));
    }

    public Tetromino ChooseNewShape()
    {
        int rndIndex = SplashKit.Rnd(BagOfPieces.Count);
        Tetromino selectedShape;
        selectedShape = BagOfPieces[rndIndex];
        BagOfPieces.Remove(BagOfPieces[rndIndex]);

        if (BagOfPieces.Count == 0)
        {
            FillBagOfPieces();
        }

        return selectedShape;
    }

    public void UpdateGame()
    {
        // Determine if we have any blocks in the row second from the top of screen (spawn area)
        for (int col = 1; col < TetrisGrid._Grid.GetLongLength(1) - 1; col++)
        {
            if (TetrisGrid._Grid[1, col].Type == BlockType.Filled)
            {
                _GameOver = true;
            }
        }

        if (_GameOver == true)
        {
            // Restart the game and print your score when you lose
            Console.WriteLine($"{TetrisGrid.clearedCounter} lines cleared");
            TetrisGrid.clearedCounter = 0;
            gameTimer.Reset();
            gameTimer.Pause();
            TetrisGrid = new Playfield(20, 10);
            _GameOver = false;
        }

        // Since we refresh at 60 frames per second, our ticks are incremented by 16 or 17 due to floating point rounding
        // Due to this rounding, there may be "double" movements or "skipped" movements.  I could find a solution.
        if (gameTimer.Ticks > 0 && gameTimer.Ticks % 500 < 17)
        {
            MoveDown();
        }
    }

    public void DrawGame()
    {
        TetrisGrid.DrawBlocks();
        _CurrentShape.DrawShape();
    }
}