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

        public override PieceType Type
        {
            get
            {
                return PieceType.King;
            }
        }

		public Piece PieceChecking
		{
			get
			{
				return _pieceChecking;
			}
		}

		//get the possible moves of the King
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
            if (!board.isKingMovedBefore (Team))
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

		//check if the king is being checked
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

		//check if the King cannot move out of the mate when being checked
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
                        Console.WriteLine ("escaped");
                    }
						
                } else
                {
                    Piece temp = possibleEscapes [i].Piece;
                    opponent.RemovePiece (possibleEscapes[i]);
					if (!GetPossibleMoves (opponent.Board) [i].isChecked (opponent, b))
                    {
                        result = false;
                        Console.WriteLine ("escaped");
                    }
						
                    opponent.AddPiece (temp, possibleEscapes[i]);
                }    
                    
            }
			return result;
		}

		//get the path between the King and the piece checking it
		public List<Cell> CheckingPath(Player opponnet)
		{
			List<Cell> checkingPath = new List<Cell>();
			foreach (Cell c in _pieceChecking.GetPossibleMoves(opponnet.Board))
			{
                if (_pieceChecking.X == X)
				{
					if (c.X == X && (_pieceChecking.Y - c.Y) * (_pieceChecking.Y - c.Y) > 0)
						checkingPath.Add(c);
				}
				else if (_pieceChecking.Y == Y)
				{
					if (c.Y == Y && (_pieceChecking.X - X) * (_pieceChecking.X - c.X) > 0)
						checkingPath.Add(c);
				}
				else if (Math.Abs(_pieceChecking.X - X) == Math.Abs(_pieceChecking.Y - Y))
				{
					if (Math.Abs(c.X - X) == Math.Abs(c.Y - Y) && ((_pieceChecking.X - X) * (_pieceChecking.X - c.X) + (_pieceChecking.Y - Y) * (_pieceChecking.Y - c.Y)) > 0)
						checkingPath.Add(c);
				}
			}
			checkingPath.Add(_pieceChecking.Cell);
			return checkingPath;

		}

		//check if any other pieces can go between the checking path to protect the king
		public bool CanBlockMate(Player opponent, Board b)
		{
			if (_pieceChecking.GetType() == typeof(Knight) || _pieceChecking.GetType() == typeof(Pawn) || _pieceChecking.GetType() == typeof(King))
				return false;
			foreach (Cell c in CheckingPath(opponent))
			{
                Piece tempPiece = c.Piece;
				foreach (Piece p in opponent.Opponent.Pieces)
				{
					if (p.GetType() != typeof(King) && c.isPossibleMoveOf(p, b))
					{
						Cell temp = p.Cell;
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

		//check if the king is checkmated
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
