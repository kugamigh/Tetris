using System;
using SplashKitSDK;

// https://tetris.wiki/Tetris_Guideline
public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Tetris", 320, 640);
        Game Tetris = new Game(gameWindow, 1);
        do
        {        
            gameWindow.Clear(Color.White);
            
            Tetris.UpdateGame();
            Tetris.DrawGame();
            Tetris.HandleInput();

            gameWindow.Refresh();
        } while ( !SplashKit.QuitRequested() && !Tetris.Quit );
    }
}