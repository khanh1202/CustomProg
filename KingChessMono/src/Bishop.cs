using System;
using System.Collections.Generic;
namespace KingChess
{
	public class Bishop : Piece
	{
		public Bishop(TeamColor color):base(color, 1)
		{
		}

        public Bishop(TeamColor color, int ID): base(color, ID)
        {
            
        }

        public override PieceType Type
        {
            get
            {
                return PieceType.Bishop;
            }
        }

        public override int Value
        {
            get
            {
                if (Team == TeamColor.Black)
                    return 330;
                return -330;
            }
        }

		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			int tempx, tempy;
            tempx = X - 1;
			tempy = Y - 1;
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

			tempx = X + 1;
			tempy = Y + 1;
			while (tempx < 8 && tempy < 8)
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

			tempx = X + 1;
			tempy = Y - 1;
			while (tempx < 8 && tempy >= 0)
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

			tempx = X - 1;
			tempy = Y + 1;
			while (tempx >= 0 && tempy < 8)
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
