///<summary>
/// Derived class representing Queen pieces
/// </summary>

using System.Collections.Generic;
namespace KingChess
{
	public class Queen : Piece
	{
		public Queen(TeamColor color) : base(color, 1)
		{
		}

        public Queen(TeamColor color, int ID) : base(color, ID)
        {
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>PieceType.Queen</value>
        public override PieceType Type
        {
            get
            {
                return PieceType.Queen;
            }
        }

        /// <summary>
        /// Gets the value for calculating next move for AI
        /// </summary>
        /// <value>900 for black Queen and -900 for White one</value>
        public override int Value
        {
            get
            {
                if (Team == TeamColor.Black)
                    return 900;
                return -900;
            }
        }

        /// <summary>
        /// Gets the possible moves of a Queen
        /// </summary>
        /// <returns>List of cells a Queen can move to</returns>
        /// <param name="board">the game Board</param>
		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			int tempx = X - 1;
            //check on the left
			while (tempx >= 0)
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
				tempx--;
			}

			tempx = X + 1;
            //check on the right
			while (tempx< 8)
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
            //check behind
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
            //check infront of the Queen
			while (tempy< 8)
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

            //check south-west 
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
            //check north east
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

			tempx = X + 1;
			tempy = Y - 1;
            //check south east
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

			tempx = X - 1;
			tempy = Y + 1;
            //check north-west
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
