using System;
using System.Collections.Generic;
namespace KingChess
{
	public class Pawn : Piece
	{
		public Pawn(TeamColor team) : base(team)
		{
		}

        public override PieceType Type
        {
            get
            {
                return PieceType.Pawn;
            }
        }

		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			if (Team == TeamColor.White)
			{
				if (board.Cells[Cell.X, Cell.Y + 1].Piece == null)
				{
					possiblemoves.Add(board.Cells[Cell.X, Cell.Y + 1]);
					if (Cell.Y == 1)
					{
						if (board.Cells[Cell.X, Cell.Y + 2].Piece == null)
							possiblemoves.Add(board.Cells[Cell.X, Cell.Y + 2]);
					}
				}

				if ((Cell.X - 1 >= 0) && (Cell.Y + 1 < 8) && (board.Cells[Cell.X - 1, Cell.Y + 1].Piece != null) && (board.Cells[Cell.X - 1, Cell.Y + 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[Cell.X - 1, Cell.Y + 1]);
				if ((Cell.X + 1 < 8) && (Cell.Y + 1 < 8) && (board.Cells[Cell.X + 1, Cell.Y + 1].Piece != null) && (board.Cells[Cell.X + 1, Cell.Y + 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[Cell.X + 1, Cell.Y + 1]);
			}
			else
			{
				if (board.Cells[Cell.X, Cell.Y - 1].Piece == null)
				{
					possiblemoves.Add(board.Cells[Cell.X, Cell.Y - 1]);
					if (Cell.Y == 6)
					{
						if (board.Cells[Cell.X, Cell.Y - 2].Piece == null)
							possiblemoves.Add(board.Cells[Cell.X, Cell.Y - 2]);
					}
				}

				if ((Cell.X - 1 >= 0) && (Cell.Y - 1 < 8) && (board.Cells[Cell.X - 1, Cell.Y - 1].Piece != null) && (board.Cells[Cell.X - 1, Cell.Y - 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[Cell.X - 1, Cell.Y + 1]);
				if ((Cell.X + 1 < 8) && (Cell.Y - 1 < 8) && (board.Cells[Cell.X + 1, Cell.Y - 1].Piece != null) && (board.Cells[Cell.X + 1, Cell.Y - 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[Cell.X + 1, Cell.Y - 1]);
			}
			return possiblemoves;
		}
	}
}
