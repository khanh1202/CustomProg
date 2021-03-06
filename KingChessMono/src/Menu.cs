﻿///<summary>
/// Menu class represents the main menu
/// of the game
/// </summary>

using System.IO;
using SwinGameSDK;
namespace KingChess
{
    public class Menu
    {
        private const int TOP_BUTTON_X = 50;
        private const int TOP_BUTTON_Y = 100;
        private const int MIDDLE_BUTTON_X = 300;
        private const int MIDDLE_BUTTON_Y = 100;
        private const int BELOW_MIDDLE_X = 300;
        private const int BELOW_MIDDLE_Y = 250;
        private const int BOTTOM_BUTTON_X = 550;
        private const int BOTTOM_BUTTON_Y = 100;
        private const int BUTTON_WIDTH = 200;
        private const int BUTTON_HEIGHT = 100;

        /// <summary>
        /// Handles the input to make appropriate reaction to the game
        /// </summary>
        /// <param name="point">Mouse Position</param>
        /// <param name="screen">the Screen</param>
        public void HandleToGame(Point2D point, Screen screen)
        {
            //If the "Play new game" button is clicked
            if (SwinGame.PointInRect (point, TOP_BUTTON_X, TOP_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT)) 
            {
                screen.CreateGame (false);
                screen.Game.ReleaseGame ();
                screen.Game.SetUpGame ();
                screen.ChangeScreenViewing (ViewingScreen.NEWGAMESCREEN);
            }
            //If the "Load game" button is clicked
            if (SwinGame.PointInRect (point, MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                StreamReader reader = new StreamReader ("Users/mac/Desktop/chess.txt");
                string opponent = reader.ReadLine ();
                if (opponent == "Human")
                    screen.CreateGame (false);
                else
                    screen.CreateGame (true);
                reader.Close ();
                screen.ChangeScreenViewing (ViewingScreen.LOADGAMESCREEN);
            }

            //If the "Player vs Com" button is clicked
            if (SwinGame.PointInRect (point, BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT)) 
            {
                screen.CreateGame (true);
                screen.Game.SetUpGame ();
                screen.ChangeScreenViewing (ViewingScreen.GAMEVSAISCREEN);
            }
            //If quit button is clicked
            if (SwinGame.PointInRect (point, BELOW_MIDDLE_X, BELOW_MIDDLE_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
                screen.ChangeScreenViewing (ViewingScreen.QUITTING);
        }

        /// <summary>
        /// Draw the menu
        /// </summary>
        public void Draw()
        {
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Background"), 0, 0);
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Newgame"), TOP_BUTTON_X, TOP_BUTTON_Y);
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Loadgame"), MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y);
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Quit"), BELOW_MIDDLE_X, BELOW_MIDDLE_Y);
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("AI"), BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y);
        }

    }
}
