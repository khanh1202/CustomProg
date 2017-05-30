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

		public Piece PieceMove
		{
			get
			{
				return _pieceMove;
			}
		}

		public Piece PieceCaptured
		{
			get
			{
				return _pieceCaptured;
			}
		}

		public Cell CellFrom
		{
			get
			{
				return _cellFrom;
			}
		}

		public Cell CellTo
		{
			get
			{
				return _cellTo;
			}
		}

        public Player PlayerMove
        {
            get
            {
                return _playerMove;
            }
        }

        public string ConvertCellToString(Cell c)
        {
            string [] yEquivalent = new string [] { "8", "7", "6", "5", "4", "3", "2", "1" };
            string [] xEquivalent = new string [] { "H", "G", "F", "E", "D", "C", "B", "A" };
            return xEquivalent [c.X] + yEquivalent [c.Y];
        }

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
