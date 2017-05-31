using System;
using SwinGameSDK;
namespace KingChess
{
    public class Menu
    {
        private const int TOP_BUTTON_X = 100;
        private const int TOP_BUTTON_Y = 100;
        private const int MIDDLE_BUTTON_X = 350;
        private const int MIDDLE_BUTTON_Y = 100;
        private const int BUTTON_WIDTH = 200;
        private const int BUTTON_HEIGHT = 100;

        public void HandleToGame(Point2D point, Screen screen)
        {
            if (SwinGame.PointInRect (point, TOP_BUTTON_X, TOP_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
                screen.ChangeScreenViewing (ViewingScreen.NEWGAMESCREEN);

            if (SwinGame.PointInRect (point, MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
                screen.ChangeScreenViewing (ViewingScreen.LOADGAMESCREEN);
        }

        public void Draw()
        {
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Background"), 0, 0);
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Newgame"), TOP_BUTTON_X, TOP_BUTTON_Y);
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Loadgame"), MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y);
        }

    }
}
