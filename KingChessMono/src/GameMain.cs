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
            ChessGame game = new ChessGame ();
            game.LoadResources ();
            game.SetUpGame ();
            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

                if (SwinGame.MouseClicked (MouseButton.LeftButton))
                {
                    game.TakeTheTurn (SwinGame.MousePosition ());
                    game.HandleReverseMove (SwinGame.MousePosition ());
                    game.HandleReplay (SwinGame.MousePosition ());
                }
                    
				game.Update ();

                //Clear the screen and draw the framerate
                SwinGame.ClearScreen (Color.White);
                game.Draw ();
                
                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}