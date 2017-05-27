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
        private Piece _chosenPiece;
        private GameState _state;

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
            foreach (Piece p in _players [0].Pieces) 
            {
                p.Draw ();
                if (p.isSelected)
                    HighlightCells (p);
            }
            foreach (Piece p in _players [1].Pieces)
            {
                p.Draw ();
                if (p.isSelected)
                    HighlightCells (p);
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
            int waitingPlayer = (_playerInTurnIndex + 1) % 2;
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
                    _state = GameState.Selecting;
                    _chosenPiece.Deselect ();
                    gameBoard.Move (_players [_playerInTurnIndex], _chosenPiece, chosen.X, chosen.Y);
                    _playerInTurnIndex = waitingPlayer;
                }
            }

        }

        public void HighlightCells (Piece chosen)
        {
            chosen.Cell.DrawOutline ();
            foreach (Cell c in chosen.GetPossibleMoves (gameBoard))
                c.DrawOutline ();
        }


	}
}
