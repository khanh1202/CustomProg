using System;
using SwinGameSDK;
namespace KingChess
{
    public class Screen
    {
        private ViewingScreen _viewing;
        private ChessGame _game;
        private Menu _menu;
        public Screen ()
        {
            _viewing = ViewingScreen.MENUSCREEN;
            _menu = new Menu ();
        }

        public ViewingScreen ViewingScreen
        {
            get
            {
                return _viewing;
            }
            set
            {
                _viewing = value;    
            }
        }

        public Menu Menu
        {
            get
            {
                return _menu;
            }
        }

        public ChessGame Game
        {
            get
            {
                return _game;
            }
        }

        public void CreateGame(bool AI)
        {
            _game = new ChessGame (AI);
        }

		public void LoadResources ()
		{
			SwinGame.LoadBitmapNamed ("ChessBoard", "ChessBoard.png");
			SwinGame.LoadBitmapNamed ("WhitePawn", "White_Pawn.png");
			SwinGame.LoadBitmapNamed ("WhiteRook", "White_Rook.png");
			SwinGame.LoadBitmapNamed ("WhiteKnight", "White_Knight.png");
			SwinGame.LoadBitmapNamed ("WhiteBishop", "White_Bishop.png");
			SwinGame.LoadBitmapNamed ("WhiteQueen", "White_Queen.png");
			SwinGame.LoadBitmapNamed ("WhiteKing", "White_King.png");
			SwinGame.LoadBitmapNamed ("BlackPawn", "Black_Pawn.png");
			SwinGame.LoadBitmapNamed ("BlackRook", "Black_Rook.png");
			SwinGame.LoadBitmapNamed ("BlackKnight", "Black_Knight.png");
			SwinGame.LoadBitmapNamed ("BlackBishop", "Black_Bishop.png");
			SwinGame.LoadBitmapNamed ("BlackQueen", "Black_Queen.png");
			SwinGame.LoadBitmapNamed ("BlackKing", "Black_King.png");
			SwinGame.LoadBitmapNamed ("Background", "Background.png");
			SwinGame.LoadBitmapNamed ("Background1", "Background1.png");
			SwinGame.LoadBitmapNamed ("Undo_active", "Undo_active.png");
			SwinGame.LoadBitmapNamed ("Undo_inactive", "Undo_inactive.png");
			SwinGame.LoadBitmapNamed ("Replay_active", "Replay_active.png");
			SwinGame.LoadBitmapNamed ("Replay_inactive", "Replay_inactive.png");
			SwinGame.LoadBitmapNamed ("Save1", "Save1.png");
            SwinGame.LoadBitmapNamed ("Mainmenu", "Mainmenu.png");
            SwinGame.LoadBitmapNamed ("Newgame", "New_game.png");
            SwinGame.LoadBitmapNamed ("Loadgame", "Load_game.png");
            SwinGame.LoadBitmapNamed ("AI", "AI.png");
            SwinGame.LoadBitmapNamed ("Quit", "Quit.png");
			SwinGame.LoadFontNamed ("Chelsea", "Chelsea.ttf", 15);
		}

		public void FreeResources ()
		{
			SwinGame.FreeFont (SwinGame.FontNamed ("Chelsea"));
		}

        public void HandleGame()
        {
            if (SwinGame.MouseClicked (MouseButton.LeftButton))
            {
				_game.TakeTheTurn (SwinGame.MousePosition ());
				_game.HandleReverseMove (SwinGame.MousePosition ());
				_game.HandleReplay (SwinGame.MousePosition ());
				_game.HandleSaving (SwinGame.MousePosition ());
				_game.HandleBackScreen (SwinGame.MousePosition (), this);
            }
        }

        public void HandleUserInput()
        {
            SwinGame.ProcessEvents ();
            switch (_viewing)
            {
                case ViewingScreen.NEWGAMESCREEN:
	                HandleGame ();
                    _game.Update ();
                    break;
                case ViewingScreen.MENUSCREEN:
	                if (SwinGame.MouseClicked (MouseButton.LeftButton))
	                    _menu.HandleToGame (SwinGame.MousePosition (), this);
                    break;
                case ViewingScreen.LOADGAMESCREEN:
                    _game.HandleLoading ();
                    _viewing = ViewingScreen.NEWGAMESCREEN;
                    break;
	            case ViewingScreen.GAMEVSAISCREEN:
                    HandleGame ();
                    _game.Update ();
                    break;
                    
            }
        }

        public void Draw()
        {
            switch (_viewing)
            {
                case ViewingScreen.NEWGAMESCREEN:
					_game.Draw ();
					break;
                case ViewingScreen.LOADGAMESCREEN:
                    _game.Draw ();
                    break;
                case ViewingScreen.GAMEVSAISCREEN:
	                _game.Draw ();
	                break;
                case ViewingScreen.MENUSCREEN:
                    _menu.Draw ();
                    break;
            }
        }

        public void ChangeScreenViewing(ViewingScreen viewing)
        {
            _viewing = viewing;
        }
    }
}
