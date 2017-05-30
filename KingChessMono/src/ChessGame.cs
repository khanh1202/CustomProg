using System;
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
			_players[1].Opponent = _players[0];
            _playerInTurnIndex = 1;
            gameBoard = new Board ();
            _state = GameState.Selecting;
		}

        public void LoadResources()
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
        }

        public void Draw()
        {
            gameBoard.Draw ();
            _players [0].DrawPieces ();
            _players [1].DrawPieces ();
            _message.Draw (this);
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

		public void SetUpGame()
		{
            _players [0].SetupPlayer (gameBoard);
            _players [1].SetupPlayer (gameBoard);
			_playerInTurnIndex = 1;
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

        public void TakeTheTurn(Point2D point)
        {
			_playerwaitingIndex = (_playerInTurnIndex + 1) % 2;
            if (_state == GameState.Selecting)
            {
                Cell chosen = FetchCell (point);
                if (chosen != null && chosen.Piece != null && _players[_playerInTurnIndex].Pieces.Contains (chosen.Piece))
                {
                    _state = GameState.Moving;
                    chosen.Piece.Select ();
                    _chosenPiece = chosen.Piece;
                }
            }
            else if (_state == GameState.Moving)
            {
				
				Cell chosen = FetchCell (point);
				if (chosen.Piece != null && chosen.Piece.isSelected) 
				{
					_state = GameState.Selecting;
					chosen.Piece.Deselect ();
				} 
				else if (chosen.isPossibleMoveOf (_chosenPiece, gameBoard)) 
				{
					_state = GameState.Moved;
					gameBoard.Move (_players [_playerInTurnIndex], _chosenPiece, chosen.X, chosen.Y);
                    if (_chosenPiece.GetType () == typeof (King))
                        MoveRookInCastle (_players [_playerInTurnIndex].Team, chosen);
                    _chosenPiece.Deselect ();
                    _playerInTurnIndex = _playerwaitingIndex;
					_playerwaitingIndex = (_playerInTurnIndex + 1) % 2;
				}
            }

        }

        public void MoveRookInCastle(TeamColor team, Cell cellKingMovedTo)
        {
            if (team == TeamColor.White)
            {
                if (cellKingMovedTo == gameBoard.Cells [2, 0])
                    gameBoard.MoveWithoutChecking (_players [_playerInTurnIndex], gameBoard.Cells [0, 0].Piece, 3, 0);
                if (cellKingMovedTo == gameBoard.Cells [6, 0])
                    gameBoard.MoveWithoutChecking (_players [_playerInTurnIndex], gameBoard.Cells [7, 0].Piece, 5, 0);
            }
            if (team == TeamColor.Black)
            {
				if (cellKingMovedTo == gameBoard.Cells [2, 7])
					gameBoard.MoveWithoutChecking (_players [_playerInTurnIndex], gameBoard.Cells [0, 7].Piece, 3, 7);
				if (cellKingMovedTo == gameBoard.Cells [6, 7])
					gameBoard.MoveWithoutChecking (_players [_playerInTurnIndex], gameBoard.Cells [7, 7].Piece, 5, 7);
            }
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
