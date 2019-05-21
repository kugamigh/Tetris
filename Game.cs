using System;
using System.Collections.Generic;
using SplashKitSDK;

public class Game
{
    private Playfield TetrisGrid = new Playfield(20, 10);
    public Tetromino _CurrentShape;
    public Tetromino _CollisionTester;
    private int _Level = 1;
    private int _Lines = 0;
    public bool Quit = false;
    public Timer gameTimer;
    public List<Tetromino> BagOfPieces = new List<Tetromino>();

    public Game(Window gameWindow, int level)
    {
        FillBagOfPieces();
        _CurrentShape = ChooseNewShape();
        _CollisionTester = _CurrentShape.Clone();
        _Level = level;
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
        if (gameTimer.Ticks > 0 && gameTimer.Ticks % 17 < 1)
        {
            Console.WriteLine(gameTimer.Ticks);
            MoveDown();
        }
        // Console.WriteLine(gameTimer.Ticks);
    }

    public void DrawGame()
    {
        TetrisGrid.DrawBlocks();
        _CurrentShape.DrawShape();
    }
}