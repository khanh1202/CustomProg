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
			int tempx = Cell.X - 1;
			while (tempx >= 0)
			{
				if (board.Cells[tempx, Cell.Y].Piece == null)
					possiblemoves.Add(board.Cells[tempx, Cell.Y]);
				else if (board.Cells[tempx, Cell.Y].Piece.Team == this.Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[tempx, Cell.Y]);
					break;
				}
				tempx--;
			}
			tempx = Cell.X + 1;
			while (tempx < 8)
			{
				if (board.Cells[tempx, Cell.Y].Piece == null)
					possiblemoves.Add(board.Cells[tempx, Cell.Y]);
				else if (board.Cells[tempx, Cell.Y].Piece.Team == this.Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[tempx, Cell.Y]);
					break;
				}
				tempx++;
			}
			int tempy = Cell.Y - 1;
			while (tempy >= 0)
			{
				if (board.Cells[Cell.X, tempy].Piece == null)
					possiblemoves.Add(board.Cells[Cell.X, tempy]);
				else if (board.Cells[Cell.X, tempy].Piece.Team == this.Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[Cell.X, tempy]);
					break;
				}
				tempy--;
			}
			tempy = Cell.Y + 1;
			while (tempy < 8)
			{
				if (board.Cells[Cell.X, tempy].Piece == null)
					possiblemoves.Add(board.Cells[Cell.X, tempy]);
				else if (board.Cells[Cell.X, tempy].Piece.Team == this.Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[Cell.X, tempy]);
					break;
				}
				tempy++;
			}
			return possiblemoves;
		}
	}
}
