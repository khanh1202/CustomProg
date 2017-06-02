///<summary>
/// Rook is a derived class that represents a minor piece 
/// in the chess game
/// </summary>

using System.Collections.Generic;
namespace KingChess
{
	public class Rook : Piece
	{
        public Rook(TeamColor team, int ID) : base(team, ID)
		{
		}

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>PieceType.Rook</value>
        public override PieceType Type
        {
            get
            {
                return PieceType.Rook;
            }
        }

        /// <summary>
        /// Gets the value for calculating next move for AI.
        /// </summary>
        /// <value>500 for black Rook and -500 for White one</value>
        public override int Value 
        {
			get 
            {
				if (Team == TeamColor.Black)
					return 500;
				return -500;
			}
		}

        /// <summary>
        /// Gets the possible moves of the Rook
        /// </summary>
        /// <returns>List of cells that a rook can move to</returns>
        /// <param name="board">the game board</param>
		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			int tempx = X - 1;
            //check on the left of the rook
			while (tempx >= 0)
			{
				if (board.Cells[tempx, Y].Piece == null)
					possiblemoves.Add(board.Cells[tempx, Cell.Y]);
                //if a cell already holds another piece in the same team
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
            //check the right of the rook
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
            //check behind the rook
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
            //check infront of the rook
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
