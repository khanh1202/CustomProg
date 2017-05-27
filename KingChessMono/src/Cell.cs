using System;
using SwinGameSDK;
namespace KingChess
{
	public class Cell
	{
        public const int BOTTOM_LEFT_CELL_X = 42;
        public const int BOTTOM_LEFT_CELL_Y = 448;
        public const int CELL_WIDTH = 57;
		private int _x;
		private int _y;
		private Piece _piece = null;
		public Cell(int x, int y, Piece piece)
		{
			_x = x;
			_y = y;
			_piece = piece;
		}

		public int X
		{
			get
			{
				return _x;
			}
		}

		public int Y
		{
			get
			{
				return _y;
			}
		}

		public Piece Piece
		{
			get
			{
				return _piece;
			}
			set
			{
                if (_piece == null)
                {
					_piece = value;
                    if (_piece != null)
                        _piece.Deployed (this); 
                } else
                {
                    throw new InvalidOperationException ("Cannot move a piece here");
                }
			}
		}

        public void RemovePiece(Player p)
        {
            if (_piece != null)
            {
				RemovePieceFromPlayer (p);
				_piece.RemoveCell ();
				_piece = null; 
            }
        }

        public void RemovePieceFromPlayer(Player p)
        {
            if (p.Pieces.Contains (_piece))
                p.Pieces.Remove (_piece);
        }

		public bool isEmpty()
		{
			return _piece == null;
		}

		public bool isPossibleMoveOf(Piece p, Board b)
		{
			return p.GetPossibleMoves(b).Contains(this);
		}

		public bool isChecked(Player opponent, Board b)
		{
			foreach (Piece p in opponent.Pieces)
				if (isPossibleMoveOf(p, b))
					return true;
			return false;
		}

        public void DrawOutline()
        {
            SwinGame.DrawRectangle (Color.Green, BOTTOM_LEFT_CELL_X + X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y - Y*CELL_WIDTH, CELL_WIDTH, CELL_WIDTH);
            SwinGame.DrawRectangle (Color.Green, BOTTOM_LEFT_CELL_X + 1 + X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y - Y*CELL_WIDTH, CELL_WIDTH, CELL_WIDTH);
            SwinGame.DrawRectangle (Color.Green, BOTTOM_LEFT_CELL_X - 1 + X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y - Y*CELL_WIDTH, CELL_WIDTH, CELL_WIDTH);
            SwinGame.DrawRectangle (Color.Green, BOTTOM_LEFT_CELL_X + X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y + 1 - Y * CELL_WIDTH, CELL_WIDTH, CELL_WIDTH);
            SwinGame.DrawRectangle (Color.Green, BOTTOM_LEFT_CELL_X + X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y - 1 - Y * CELL_WIDTH, CELL_WIDTH, CELL_WIDTH);
            SwinGame.DrawRectangle (Color.Green, BOTTOM_LEFT_CELL_X + X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y - Y * CELL_WIDTH, CELL_WIDTH, CELL_WIDTH);
           
        }
	}
}
