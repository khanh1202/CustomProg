using System;
using System.Collections.Generic;
namespace KingChess
{
	public class Rook : Piece
	{
		public Rook(TeamColor team) : base(team)
		{
		}

        public override PieceType Type
        {
            get
            {
                return PieceType.Rook;
            }
        }
		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			int tempx = X - 1;
			while (tempx >= 0)
			{
				if (board.Cells[tempx, Y].Piece == null)
					possiblemoves.Add(board.Cells[tempx, Cell.Y]);
				else if (board.Cells[tempx, Y].Piece.Team == this.Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[tempx, Y]);
					break;
				}
				tempx--;
			}
			tempx = X + 1;
			while (tempx < 8)
			{
				if (board.Cells[tempx, Y].Piece == null)
					possiblemoves.Add(board.Cells[tempx, Y]);
				else if (board.Cells[tempx, Y].Piece.Team == this.Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[tempx, Y]);
					break;
				}
				tempx++;
			}
			int tempy = Y - 1;
			while (tempy >= 0)
			{
				if (board.Cells[X, tempy].Piece == null)
					possiblemoves.Add(board.Cells[X, tempy]);
				else if (board.Cells[X, tempy].Piece.Team == this.Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[X, tempy]);
					break;
				}
				tempy--;
			}
			tempy = Y + 1;
			while (tempy < 8)
			{
				if (board.Cells[X, tempy].Piece == null)
					possiblemoves.Add(board.Cells[X, tempy]);
				else if (board.Cells[X, tempy].Piece.Team == this.Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[X, tempy]);
					break;
				}
				tempy++;
			}
			return possiblemoves;
		}
	}
}
