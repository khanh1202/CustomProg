using System;
using System.Collections.Generic;
namespace KingChess
{
	public class Knight : Piece
	{
		public Knight(TeamColor color): base(color, 1)
		{
		}

        public override PieceType Type
        {
            get
            {
                return PieceType.Knight;
            }
        }
		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			int[] possiblePosX = { X - 1, X - 1, X + 1, X + 1, X + 2, X + 2, X - 2, X - 2 };
			int[] possiblePosY = { Y + 2, Y - 2, Y + 2, Y - 2, Y + 1, Y - 1, Y + 1, Y - 1 };
			for (int i = 0; i < 8; i++)
			{
				if (possiblePosX[i] >= 0 && possiblePosX[i] < 8 && possiblePosY[i] >= 0 && possiblePosY[i] < 8)
				{
					if (board.Cells[possiblePosX[i], possiblePosY[i]].Piece == null || board.Cells[possiblePosX[i], possiblePosY[i]].Piece.Team != Team)
					{
						possiblemoves.Add(board.Cells[possiblePosX[i], possiblePosY[i]]);
					}
				}
			}
			return possiblemoves;
		}
	}
}
