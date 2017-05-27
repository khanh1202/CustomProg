using System;
using SwinGameSDK;
using System.Collections.Generic;
namespace KingChess
{
	public abstract class Piece
	{
        public const int BOTTOM_LEFT_CELL_X = 45;
        public const int BOTTOM_LEFT_CELL_Y = 448;
        public const int CELL_WIDTH = 58;
		private Cell _cell;
        private int _x;
        private int _y;
		private TeamColor _team;
		private bool _isSelected;
		private List<Cell> _possibleMoves = new List<Cell>();

        public Bitmap MyBitmap()
        {
            switch (Type)
            {
                case PieceType.Bishop:
	                if (_team == TeamColor.Black)
	                    return SwinGame.BitmapNamed ("BlackBishop");
	                else
	                    return SwinGame.BitmapNamed ("WhiteBishop");
                case PieceType.King:
					if (_team == TeamColor.Black)
						return SwinGame.BitmapNamed ("BlackKing");
					else
						return SwinGame.BitmapNamed ("WhiteKing");
                case PieceType.Knight:
	                if (_team == TeamColor.Black)
	                    return SwinGame.BitmapNamed ("BlackKnight");
	                else
	                    return SwinGame.BitmapNamed ("WhiteKnight");
	            case PieceType.Pawn:
					if (_team == TeamColor.Black)
						return SwinGame.BitmapNamed ("BlackPawn");
					else
						return SwinGame.BitmapNamed ("WhitePawn");
                case PieceType.Queen:
					if (_team == TeamColor.Black)
						return SwinGame.BitmapNamed ("BlackQueen");
					else
						return SwinGame.BitmapNamed ("WhiteQueen");
                default:
					if (_team == TeamColor.Black)
						return SwinGame.BitmapNamed ("BlackRook");
					else
						return SwinGame.BitmapNamed ("WhiteRook");
            }
        }

        public void Draw()
        {
            SwinGame.DrawBitmap (MyBitmap (), BOTTOM_LEFT_CELL_X + Cell.X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y - Cell.Y * CELL_WIDTH);
        }

        public abstract PieceType Type
        {
            get;
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

		public Piece(TeamColor team)
		{
			_team = team;
		}

		public TeamColor Team
		{
			get
			{
				return _team;
			}
		}

		public bool isSelected
		{
			get
			{
				return _isSelected;
			}
		}

        public void Deployed(Cell c)
        {
            if (c != null)
            {
				_x = c.X;
				_y = c.Y;
				_cell = c; 
            } else
            {
                _x = -1;
                _y = -1;
                _cell = null;
            }

        }

        public Cell Cell
        {
            get
            {
                return _cell;
            }

        }

        public void RemoveCell()
        {
            Deployed (null);
        }

        public void Select()
        {
            _isSelected = true;
        }

        public void Deselect()
        {
            _isSelected = false;
        }

		public abstract List<Cell> GetPossibleMoves(Board board);
	}
}
