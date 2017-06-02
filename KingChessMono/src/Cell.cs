///<summary>
/// the Cell represents the squares on the
/// game board
/// </summary>

using System;
using SwinGameSDK;
namespace KingChess
{
	public class Cell
	{
        public const int BOTTOM_LEFT_CELL_X = 42;
        public const int BOTTOM_LEFT_CELL_Y = 448;
        public const int CELL_WIDTH = 58;
		private int _x;
		private int _y;
		private Piece _piece = null;
		public Cell(int x, int y, Piece piece)
		{
			_x = x;
			_y = y;
			_piece = piece;
		}

        /// <summary>
        /// Gets the column of the cell
        /// </summary>
        /// <value>column index</value>
		public int X
		{
			get
			{
				return _x;
			}
		}

        /// <summary>
        /// Gets the row of the cell
        /// </summary>
        /// <value>row index</value>
		public int Y
		{
			get
			{
				return _y;
			}
		}

        /// <summary>
        /// Gets or sets the piece standing on the cell
        /// </summary>
        /// <value>A Piece</value>
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
                    {
                        _piece.Deployed (this);
					}
                } 
                else
                {
                    throw new InvalidOperationException ("Cannot move a piece here");
                }
			}
		}

        /// <summary>
        /// Removes the piece from the cell
        /// </summary>
        /// <param name="p">the Player that has the removed piece</param>
        public void RemovePiece(Player p)
        {
            if (_piece != null)
            {
				_piece.RemoveCell ();
				_piece = null; 
            }
        }

        /// <summary>
        /// Check if the cell has any piece standing on it
        /// </summary>
        /// <returns><c>true</c>, if the piece is null, <c>false</c> otherwise.</returns>
		public bool isEmpty()
		{
			return _piece == null;
		}

		/// <summary>
        /// Check if it is a possible move of a piece
        /// </summary>
        /// <returns><c>true</c>, if the cell is contained in the possible moves list of the piece, <c>false</c> otherwise.</returns>
        /// <param name="p">the piece checking</param>
        /// <param name="b">The game board</param>
		public bool isPossibleMoveOf(Piece p, Board b)
		{
			return p.GetPossibleMoves(b).Contains(this);
		}

        /// <summary>
        /// check if the cell is checked by any piece of a player
        /// </summary>
        /// <returns><c>true</c>, if the cell is possible move of any piece in a player's pieces, <c>false</c> otherwise.</returns>
        /// <param name="opponent">opponent player</param>
        /// <param name="b">the board</param>
		public bool isChecked(Player opponent, Board b)
		{
			foreach (Piece p in opponent.Pieces)
				if (isPossibleMoveOf(p, b))
					return true;
			return false;
		}

        /// <summary>
        /// Draws the outline of the cell
        /// </summary>
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
