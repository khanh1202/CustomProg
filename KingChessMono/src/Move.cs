using System;
namespace KingChess
{
	public class Move
	{
		private Piece _pieceMove;
		private Piece _pieceCaptured;
		private Cell _cellFrom;
		private Cell _cellTo;
		public Move(Piece pieceMove, Piece pieceCaptured, Cell cellFrom, Cell cellTo)
		{
			_pieceMove = pieceMove;
			_pieceCaptured = pieceCaptured;
			_cellFrom = cellFrom;
			_cellTo = cellTo;
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
	}
}
