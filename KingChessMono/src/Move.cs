///<summary>
/// Move class represents a move made by a piece
/// </summary>
using System;
using System.Collections.Generic;
namespace KingChess
{
	public class Move
	{
		private Piece _pieceMove;
		private Piece _pieceCaptured;
		private Cell _cellFrom;
		private Cell _cellTo;
        private Player _playerMove;
		public Move(Player playerMove, Piece pieceMove, Piece pieceCaptured, Cell cellFrom, Cell cellTo)
		{
			_pieceMove = pieceMove;
			_pieceCaptured = pieceCaptured;
			_cellFrom = cellFrom;
			_cellTo = cellTo;
            _playerMove = playerMove;
		}

        /// <summary>
        /// Gets the piece making move
        /// </summary>
        /// <value>The piece move.</value>
		public Piece PieceMove
		{
			get
			{
				return _pieceMove;
			}
		}

        /// <summary>
        /// Gets the piece captured in that move
        /// </summary>
        /// <value>The piece captured or null</value>
		public Piece PieceCaptured
		{
			get
			{
				return _pieceCaptured;
			}
		}

        /// <summary>
        /// Gets the Cell the piece move from
        /// </summary>
        /// <value>The Cell</value>
		public Cell CellFrom
		{
			get
			{
				return _cellFrom;
			}
		}

        /// <summary>
        /// Gets the cell the piece move to
        /// </summary>
        /// <value>The cell to.</value>
		public Cell CellTo
		{
			get
			{
				return _cellTo;
			}
		}

        /// <summary>
        /// Gets the player making the move
        /// </summary>
        /// <value>The player move.</value>
        public Player PlayerMove
        {
            get
            {
                return _playerMove;
            }
        }

        /// <summary>
        /// Converts the cell to string representation.
        /// </summary>
        /// <returns>String representation of a Cell.</returns>
        /// <param name="c">A cell</param>
        public string ConvertCellToString(Cell c)
        {
            string [] yEquivalent = new string [] { "8", "7", "6", "5", "4", "3", "2", "1" };
            string [] xEquivalent = new string [] { "H", "G", "F", "E", "D", "C", "B", "A" };
            return xEquivalent [c.X] + yEquivalent [c.Y];
        }

        /// <summary>
        /// Converts the piece to string representation.
        /// </summary>
        /// <returns>string representation of a piece</returns>
        /// <param name="p">The Piece</param>
        public string ConvertPieceToString(Piece p)
        {
            Dictionary<Type, string> pieceEquivalent = new Dictionary<Type, string> ();
            pieceEquivalent.Add (typeof (Pawn), "Pawn");
            pieceEquivalent.Add (typeof (Rook), "Rook");
            pieceEquivalent.Add (typeof (Knight), "Knight");
            pieceEquivalent.Add (typeof (Bishop), "Bishop");
            pieceEquivalent.Add (typeof (Queen), "Queen");
            pieceEquivalent.Add (typeof (King), "King");
            return pieceEquivalent [p.GetType ()];
        }
	}
}
