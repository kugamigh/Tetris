using System;
using SplashKitSDK;

public enum BlockType
{
    Wall,
    Empty,
    Filled,
    Ghost
}

public class Block
{
    public Color _Colour = Color.Black;
    public int _Size = 32;
    public int X = 0;
    public int Y { get; set; }
    public BlockType Type { get; set; }

    public Block(Color colour, int x, int y)
    {
        _Colour = colour;
        X = x;
        Y = y;
    }

    public Block Clone()
    {
        Block other = (Block)this.MemberwiseClone();
        return other;
    }

    public void Draw()
    {
        if (Type == BlockType.Filled)
        {
            SplashKit.FillRectangle(_Colour, X * _Size, Y * _Size, _Size, _Size);
            SplashKit.DrawRectangle(Color.Black, X * _Size, Y * _Size, _Size, _Size);
        }

        if (Type == BlockType.Ghost)
        {
            SplashKit.DrawRectangle(_Colour, X * _Size, Y * _Size, _Size, _Size);
        }

        if (Type == BlockType.Wall)
        {
            SplashKit.FillRectangle(Color.DarkSlateBlue, X * _Size, Y * _Size, _Size, _Size);
            SplashKit.DrawRectangle(Color.Black, X * _Size, Y * _Size, _Size, _Size);
        }
        if (Type == BlockType.Empty)
        {
            SplashKit.DrawRectangle(Color.Black, X * _Size, Y * _Size, _Size, _Size);
        }
    }
}