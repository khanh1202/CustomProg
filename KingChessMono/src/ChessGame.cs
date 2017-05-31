using System;
using System.IO;
using SwinGameSDK;
namespace KingChess
{
	public class ChessGame
	{
		public const int MARGIN_LEFT = 39;
		public const int MARGIN_TOP = 39;
		public const int MARGIN_RIGHT = 39;
		public const int MARGIN_BOTTOM = 42;
		public const int CELL_WIDTH = 58;
		private Player[] _players = new Player[2];
        private Board gameBoard;
		private int _playerInTurnIndex;
		private int _playerwaitingIndex;
        private Piece _chosenPiece;
        private GameState _state;
        private Messages _message = new Messages();

		public ChessGame()
		{
			_players[0] = new Player(TeamColor.Black);
			_players[1] = new Player(TeamColor.White);
			_players[0].Opponent = _players[1];
			_players [1].Opponent = _players [0];
            gameBoard = new Board ();
            _state = GameState.Selecting;
            Piece.ClearPieceRegistry ();
			Piece.RegisterPiece ("Pawn", typeof (Pawn));
			Piece.RegisterPiece ("Rook", typeof (Rook));
			Piece.RegisterPiece ("Knight", typeof (Knight));
			Piece.RegisterPiece ("Bishop", typeof (Bishop));
			Piece.RegisterPiece ("Queen", typeof (Queen));
			Piece.RegisterPiece ("King", typeof (King));
		}

        public void Draw()
        {
            DrawBackGround ();
            gameBoard.Draw ();
            _players [0].DrawPieces ();
            _players [1].DrawPieces ();
            _message.Draw (this);
        }

        public void DrawBackGround()
        {
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Background1"), 0, 0);
        }

        public Player PlayerInturn
        {
            get
            {
                return _players [_playerInTurnIndex];
            }
        }

		public Player[] Players
		{
			get
			{
				return _players;
			}
		}

        public Board Board
        {
            get
            {
                return gameBoard;
            }
        }

        public GameState State
        {
            get
            {
                return _state;
            }
        }

        public Piece ChosenPiece
        {
            get
            {
                return _chosenPiece;
            }
        }

        public void SetChosenPiece(Piece p)
        {
            _chosenPiece = p;
        }

		public void SetUpGame()
		{
            _players [0].SetupPlayer (gameBoard);
            _players [1].SetupPlayer (gameBoard);
			_playerInTurnIndex = 1;
            _playerwaitingIndex = 0;
		}

        public void ReleaseGame()
        {
            _players [0].ReleasePiece ();
            _players [1].ReleasePiece ();
            gameBoard.ReleaseMove ();
        }

        public void LoadGame(string filename)
        {
            ReleaseGame ();
            StreamReader reader = new StreamReader (filename);
            string playertomove = reader.ReadLine ();
            if (playertomove == "White")
                _playerInTurnIndex = 1;
            else
                _playerInTurnIndex = 0;
            int numOfPieces = Convert.ToInt32 (reader.ReadLine ());
            for (int i = 0; i < numOfPieces; i++)
                Piece.Load (reader, _players);
            reader.Close ();
        }

        public Cell FetchCell(Point2D point)
        {
            float pointRelativeY = point.Y - MARGIN_TOP;
            float pointRelativeX = point.X - MARGIN_LEFT;
            int row = 7 - (int)Math.Floor (pointRelativeY / CELL_WIDTH);
            int col = (int)Math.Floor (pointRelativeX / CELL_WIDTH);
            if (row >= 0 && row < 8 && col >= 0 && col < 8)
                return gameBoard.Cells [col, row];
            return null;
        }

        public void HandleReverseMove(Point2D point)
        {
            if (SwinGame.PointInRect (point, 600, 350, 30, 30))
            {
                gameBoard.ReverseMove (this);
                if (_state == GameState.Ended)
                    _state = GameState.Selecting;
			}
                
        }

        public void HandleReplay(Point2D point)
        {
            int count = gameBoard.Moves.Count;
            if (SwinGame.PointInRect (point, 730, 350, 30, 30))
            {
				for (int i = 0; i < count; i++)
					gameBoard.ReverseMove (this);
                _state = GameState.Selecting;
            }
        }

        public void HandleSaving(Point2D point)
        {
            if (SwinGame.PointInRect (point, 600, 450, 163, 50))
                gameBoard.Save ("Users/mac/Desktop/chess.txt", _players);      
        }

		public void HandleLoading ()
		{
             LoadGame ("/Users/mac/Desktop/chess.txt");			
		}

        public void TakeTheTurn(Point2D point)
        {
            _players [_playerInTurnIndex].TakeTurn (point, this);
        }

        public void HandleBackScreen(Point2D point, Screen screen)
        {
            if (SwinGame.PointInRect (point, 600, 500, 163, 50))
            {
                screen.ChangeScreenViewing (ViewingScreen.MENUSCREEN);
            }
        }

        public void ChangeTurn()
        {
            _playerInTurnIndex = _playerwaitingIndex;
            _playerwaitingIndex = (_playerInTurnIndex + 1) % 2;
        }

        public void ChangeState(GameState state)
        {
            _state = state;
        }

		public void Update ()
		{
            if (_players [_playerInTurnIndex].King == null)
                _state = GameState.Ended;
            else
            {
				if (_state == GameState.Moved) 
                {
					if (_players [_playerInTurnIndex].King.isChecked (_players [_playerwaitingIndex], gameBoard)) 
                    {
						if (_players [_playerInTurnIndex].King.isCheckmated (_players [_playerwaitingIndex], gameBoard))
							_state = GameState.Ended;
						else
							_state = GameState.Selecting;
					} 
                    else
						_state = GameState.Selecting;

				}
            }
		}
	}
}
