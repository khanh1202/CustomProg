using System;
using System.Collections.Generic;
namespace KingChess
{
	public class Pawn : Piece
	{
        public Pawn(TeamColor team) : base(team, 1)
		{
		}

        public Pawn(TeamColor team, int ID) : base(team, ID)
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
				if (board.Cells[X, Y + 1].Piece == null)
				{
					possiblemoves.Add(board.Cells[X, Y + 1]);
					if (Y == 1)
					{
						if (board.Cells[X, Y + 2].Piece == null)
							possiblemoves.Add(board.Cells[X, Y + 2]);
					}
				}

				if ((X - 1 >= 0) && (Y + 1 < 8) && (board.Cells[X - 1, Y + 1].Piece != null) && (board.Cells[X - 1, Y + 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[X - 1, Y + 1]);
				if ((X + 1 < 8) && (Y + 1 < 8) && (board.Cells[X + 1, Y + 1].Piece != null) && (board.Cells[X + 1, Y + 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[X + 1, Y + 1]);
			}
			else
			{
				if (board.Cells[X, Y - 1].Piece == null)
				{
					possiblemoves.Add(board.Cells[X, Y - 1]);
					if (Cell.Y == 6)
					{
						if (board.Cells[X, Y - 2].Piece == null)
							possiblemoves.Add(board.Cells[X, Y - 2]);
					}
				}

				if ((X - 1 >= 0) && (Y - 1 >= 0) && (board.Cells[X - 1, Y - 1].Piece != null) && (board.Cells[X - 1, Y - 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[X - 1, Y - 1]);
				if ((X + 1 < 8) && (Y - 1 >= 0) && (board.Cells[X + 1, Y - 1].Piece != null) && (board.Cells[X + 1, Y - 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[X + 1, Y - 1]);
			}
			return possiblemoves;
		}
	}
}
