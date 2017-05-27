using System;
using System.Collections.Generic;
namespace KingChess
{
	public class Queen : Piece
	{
		public Queen(TeamColor color) : base(color)
		{
		}

        public override PieceType Type
        {
            get
            {
                return PieceType.Queen;
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
			while (tempx< 8)
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
			while (tempy< 8)
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

			tempx = Cell.X - 1;
			tempy = Cell.Y - 1;
			while (tempx >= 0 && tempy >= 0)
			{
				if (board.Cells[tempx, tempy].Piece == null)
					possiblemoves.Add(board.Cells[tempx, tempy]);
				else if (board.Cells[tempx, tempy].Piece.Team == Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[tempx, tempy]);
					break;
				}
				tempx--;
				tempy--;
			}

			tempx = Cell.X + 1;
			tempy = Cell.Y + 1;
			while (tempx< 8 && tempy< 8)
			{
				if (board.Cells[tempx, tempy].Piece == null)

					possiblemoves.Add(board.Cells[tempx, tempy]);
				else if (board.Cells[tempx, tempy].Piece.Team == Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[tempx, tempy]);
					break;
				}
				tempx++;
				tempy++;
			}

			tempx = Cell.X + 1;
			tempy = Cell.Y - 1;
			while (tempx< 8 && tempy >= 0)
			{
				if (board.Cells[tempx, tempy].Piece == null)

					possiblemoves.Add(board.Cells[tempx, tempy]);
				else if (board.Cells[tempx, tempy].Piece.Team == Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[tempx, tempy]);
					break;
				}
				tempx++;
				tempy--;
			}

			tempx = Cell.X - 1;
			tempy = Cell.Y + 1;
			while (tempx >= 0 && tempy< 8)
			{
				if (board.Cells[tempx, tempy].Piece == null)

					possiblemoves.Add(board.Cells[tempx, tempy]);
				else if (board.Cells[tempx, tempy].Piece.Team == Team)
					break;
				else
				{
					possiblemoves.Add(board.Cells[tempx, tempy]);
					break;
				}
				tempx--;
				tempy++;
			}
			return possiblemoves;
		}
	}
}
