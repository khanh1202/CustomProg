///<summary>
/// Pawn class represents the Pawn pieces for
/// the chess game
/// </summary>

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

        /// <summary>
        /// Gets the type
        /// </summary>
        /// <value>PieceType.Pawn</value>
        public override PieceType Type
        {
            get
            {
                return PieceType.Pawn;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>100 for black pawn and -100 for white one</value>
        public override int Value 
        {
			get 
            {
				if (Team == TeamColor.Black)
					return 100;
				return -100;
			}
		}

        /// <summary>
        /// Gets the possible moves
        /// </summary>
        /// <returns>The possible moves.</returns>
        /// <param name="board">the game Board</param>
		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			if (Team == TeamColor.White)
			{
                //if the 1st cell infront of the Pawn is not occupied
				if (board.Cells[X, Y + 1].Piece == null)
				{
					possiblemoves.Add(board.Cells[X, Y + 1]);
                    //if the Pawn is standing at its starting place
					if (Y == 1)
					{
						if (board.Cells[X, Y + 2].Piece == null)
							possiblemoves.Add(board.Cells[X, Y + 2]);
					}
				}

                //if the 1st cell in north-west of the pawn is standing a opponent piece
				if ((X - 1 >= 0) && (Y + 1 < 8) && (board.Cells[X - 1, Y + 1].Piece != null) && (board.Cells[X - 1, Y + 1].Piece.Team != Team))
					possiblemoves.Add(board.Cells[X - 1, Y + 1]);
				//if the 1st cell in north-east of the pawn is standing a opponent piece
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
