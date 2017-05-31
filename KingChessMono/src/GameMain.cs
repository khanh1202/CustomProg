using System;
using SwinGameSDK;

namespace KingChess
{
    public class GameMain
    {
        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            Screen screen   = new Screen ();
            screen.LoadResources ();
            //Run the game loop
            while(false == SwinGame.WindowCloseRequested() && screen.ViewingScreen != ViewingScreen.QUITTING)
            {
                screen.HandleUserInput ();

                //Clear the screen and draw the framerate
                SwinGame.ClearScreen (Color.White);
                
                //Draw onto the screen
                screen.Draw ();
                SwinGame.RefreshScreen(60);
            }
            screen.FreeResources ();
        }
    }
}