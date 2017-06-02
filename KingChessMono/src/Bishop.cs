///<summary>
/// Bishop class represents the Bishop 
/// pieces in the chess game
/// </summary>

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

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>PieceType.Bishop</value>
        public override PieceType Type
        {
            get
            {
                return PieceType.Bishop;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>330 for black bishop and -330 for white one</value>
        public override int Value
        {
            get
            {
                if (Team == TeamColor.Black)
                    return 330;
                return -330;
            }
        }

        /// <summary>
        /// Gets the possible moves.
        /// </summary>
        /// <returns>The list of possible moves.</returns>
        /// <param name="board">game board</param>
		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			int tempx, tempy;
            tempx = X - 1;
			tempy = Y - 1;
            //check south-west
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
            //check north-east
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
            //check south-east
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
            //check north-west
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
