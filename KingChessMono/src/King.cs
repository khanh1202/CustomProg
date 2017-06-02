///<summary>
/// King class represents the King
/// pieces in the chess game
/// </summary>

using System;
using System.Collections.Generic;
namespace KingChess
{
	public class King : Piece
	{
		private Piece _pieceChecking;
		public King(TeamColor team): base(team, 1)
		{
		}

        public King(TeamColor team, int ID) : base(team, ID)
        {
            
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>PieceType.King</value>
        public override PieceType Type
        {
            get
            {
                return PieceType.King;
            }
        }

        /// <summary>
        /// Gets the piece checking it
        /// </summary>
        /// <value>The piece checking</value>
		public Piece PieceChecking
		{
			get
			{
				return _pieceChecking;
			}
		}

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>20000 for Black and -20000 for White</value>
        public override int Value
        {
            get
            {
                if (Team == TeamColor.Black)
                    return 20000;
                return -20000;
            }
        }

		/// <summary>
        /// Gets the possible moves of the King
        /// </summary>
        /// <returns>The possible moves.</returns>
        /// <param name="board">The game board</param>
		public override List<Cell> GetPossibleMoves(Board board)
		{
			List<Cell> possiblemoves = new List<Cell>();
			int[] possiblePosX = { X + 1, X + 1, X + 1, X - 1, X - 1, X - 1, X, X };
			int[] possiblePosY = { Y - 1, Y, Y + 1, Y - 1, Y + 1, Y, Y + 1, Y - 1 };
			for (int i = 0; i < 8; i++)
			{
				if (possiblePosX[i] >= 0 && possiblePosX[i] < 8 && possiblePosY[i] >= 0 && possiblePosY[i] < 8)
					if (board.Cells[possiblePosX[i], possiblePosY[i]].Piece == null || board.Cells[possiblePosX[i], possiblePosY[i]].Piece.Team != Team)
					possiblemoves.Add(board.Cells[possiblePosX[i], possiblePosY[i]]);
			}

            //check for eligibility to do castling
            if (board.timesKingMovedBefore (Team) == 0)
            {
                if (!board.isRookMovedBefore (Team, 1) && board.AreTwoPiecesNotBlocked (Team, 1))
                {
                    if (Team == TeamColor.White)
                        possiblemoves.Add (board.Cells [2, 0]);
                    else
                        possiblemoves.Add (board.Cells [2, 7]);
                }
                if (!board.isRookMovedBefore (Team, 2) && board.AreTwoPiecesNotBlocked (Team, 2)) 
                {
                    if (Team == TeamColor.White)
                        possiblemoves.Add (board.Cells [6, 0]);
                    else
                        possiblemoves.Add (board.Cells [6, 7]);
                }
            }
            return possiblemoves;

		}

		/// <summary>
        /// Check if the King is Checked
        /// </summary>
        /// <returns><c>true</c>, if the king's cell is possible move of any opponent piece, <c>false</c> otherwise.</returns>
        /// <param name="opponent">The opponent player</param>
        /// <param name="b">The game board</param>
		public bool isChecked(Player opponent, Board b)
		{
			bool result = false;
            for (int i = 0; i < opponent.Pieces.Count; i++) {
                    if (Cell.isPossibleMoveOf (opponent.Pieces [i], b)) {
                        _pieceChecking = opponent.Pieces [i];
                        result = true;
                    }
                }
			return result;
		}

        /// <summary>
        /// Check if the King is Checked, not counting Piece p. It is used to check for 
        /// king being checkmated
        /// </summary>
        /// <returns><c>true</c>, if the king is checked by any opponent's pieces excluding p, <c>false</c> otherwise.</returns>
        /// <param name="opponent">Opponent.</param>
        /// <param name="b">the game board</param>
        /// <param name="p">the piece excluding</param>
        public bool isChecked(Player opponent, Board b, Piece p)
        {
            bool result = false;
            for (int i = 0; i < opponent.Pieces.Count; i++)
            {
                if (opponent.Pieces[i] != p && Cell.isPossibleMoveOf (opponent.Pieces[i], b))
                {
                    _pieceChecking = opponent.Pieces [i];
                    result = true;
                }
            }
            return result;
        }

		/// <summary>
        /// Check if the King cannot move anywhere when it is in check
        /// </summary>
        /// <returns><c>true</c>, if all of possible moves of the King are checked,
        ///  <c>false</c> otherwise.
        /// </returns>
        /// <param name="opponent">Opponent player</param>
        /// <param name="b">game board</param>
		public bool isOutOfMove(Player opponent, Board b)
		{
			bool result = true;
            List<Cell> possibleEscapes = GetPossibleMoves (opponent.Board);
            for (int i = 0; i < possibleEscapes.Count; i++) 
            {
                if (possibleEscapes[i].Piece == null)
                {
					if (!GetPossibleMoves (opponent.Board) [i].isChecked (opponent, b))
                    {
                        result = false;
                    }
						
                } 
                else
                {
                    Piece temp = possibleEscapes [i].Piece;
                    opponent.RemovePiece (possibleEscapes[i]);
					if (!GetPossibleMoves (opponent.Board) [i].isChecked (opponent, b))
                    {
                        result = false;
                    }
						
                    opponent.AddPiece (temp, possibleEscapes[i]);
                }    
                    
            }
			return result;
		}

		/// <summary>
        /// Gets the cells lying between the king the the piece checking it
        /// </summary>
        /// <returns>The list of cells in the path</returns>
        /// <param name="opponnet">Opponent player</param>
		public List<Cell> CheckingPath(Player opponnet)
		{
			List<Cell> checkingPath = new List<Cell>();
			foreach (Cell c in _pieceChecking.GetPossibleMoves(opponnet.Board))
			{
                //if the piece checking and the king belongs to the same column
                if (_pieceChecking.X == X)
				{
					if (c.X == X && (_pieceChecking.Y - c.Y) * (_pieceChecking.Y - c.Y) > 0)
						checkingPath.Add(c);
				}
				//if the piece checking and the king belongs to the same row
				else if (_pieceChecking.Y == Y)
				{
					if (c.Y == Y && (_pieceChecking.X - X) * (_pieceChecking.X - c.X) > 0)
						checkingPath.Add(c);
				}
				//if the piece checking and the king are diagonally aligned
				else if (Math.Abs(_pieceChecking.X - X) == Math.Abs(_pieceChecking.Y - Y))
				{
					if (Math.Abs(c.X - X) == Math.Abs(c.Y - Y) && ((_pieceChecking.X - X) * (_pieceChecking.X - c.X) + (_pieceChecking.Y - Y) * (_pieceChecking.Y - c.Y)) > 0)
						checkingPath.Add(c);
				}
			}
			checkingPath.Add(_pieceChecking.Cell);
			return checkingPath;

		}

		/// <summary>
        /// Check if any other teammates can block the checking path to protect the king
        /// </summary>
        /// <returns><c>true</c>, if for any cells in the checking path, there is a
        /// teammate which can move to that cell to protect the king,
        /// <c>false</c> otherwise.
        /// </returns>
        /// <param name="opponent">opponent player.</param>
        /// <param name="b">game board.</param>
		public bool CanBlockMate(Player opponent, Board b)
		{
            //if checking piece is a Pawn, or King, or Knight, return false
			if (_pieceChecking.GetType() == typeof(Knight) || _pieceChecking.GetType() == typeof(Pawn) || _pieceChecking.GetType() == typeof(King))
				return false;
			foreach (Cell c in CheckingPath(opponent))
			{
                Piece tempPiece = c.Piece;
				foreach (Piece p in opponent.Opponent.Pieces)
				{
                    //if the piece mentioned is not the King and the cell in the checking path is
                    //a possible move of that piece
					if (p.GetType() != typeof(King) && c.isPossibleMoveOf(p, b))
					{
						Cell temp = p.Cell;
                        //if the cell in the checking path is not holding any piece
                        if (c.Piece == null)
                        {
                            p.Cell.RemovePiece (opponent.Opponent);
                            c.Piece = p;
                            if (!isChecked (opponent, b))
                            {
                                c.RemovePiece (opponent.Opponent);
                                temp.Piece = p;
                                return true;
                            }
                            c.RemovePiece (opponent.Opponent);
							temp.Piece = p;
                        } 
                        else
                        {
                            tempPiece.Cell.RemovePiece (opponent);
                            p.Cell.RemovePiece (opponent.Opponent);
                            c.Piece = p;
                            if (!isChecked (opponent, b, tempPiece))
                            {
                                c.RemovePiece (opponent.Opponent);
                                c.Piece = tempPiece;
                                temp.Piece = p;
                                return true;
                            }
							c.RemovePiece (opponent.Opponent);
							c.Piece = tempPiece;
							temp.Piece = p;
                        }
					}
				}
			}
			return false;
		}

		/// <summary>
        /// Check if the king is checkmated
        /// </summary>
        /// <returns><c>true</c>, if isChecked and isOutOfMove are true and CanBlockMate
        /// is false, <c>false</c> otherwise.
        /// </returns>
        /// <param name="opponent">opponent player</param>
        /// <param name="b">the board</param>/
		public bool isCheckmated(Player opponent, Board b)
		{
            if (isChecked (opponent, b)) {
                    if (isOutOfMove (opponent, b) && !CanBlockMate (opponent, b))
                        return true;
                }
			return false;
		}


	}
}
