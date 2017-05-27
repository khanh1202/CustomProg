using System;
using System.Collections.Generic;
namespace KingChess
{
	public class King : Piece
	{
		private Piece _pieceChecking;
		public King(TeamColor team): base(team)
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
			int[] possiblePosX = { Cell.X + 1, Cell.X + 1, Cell.X + 1, Cell.X - 1, Cell.X - 1, Cell.X - 1, Cell.X, Cell.X };
			int[] possiblePosY = { Cell.Y - 1, Cell.Y, Cell.Y + 1, Cell.Y - 1, Cell.Y + 1, Cell.Y, Cell.Y + 1, Cell.Y - 1 };
			for (int i = 0; i < 8; i++)
			{
				if (possiblePosX[i] >= 0 && possiblePosX[i] < 8 && possiblePosY[i] >= 0 && possiblePosY[i] < 8)
					if (board.Cells[possiblePosX[i], possiblePosY[i]].Piece == null || board.Cells[possiblePosX[i], possiblePosY[i]].Piece.Team != Team)
					possiblemoves.Add(board.Cells[possiblePosX[i], possiblePosY[i]]);
			}
			return possiblemoves;

		}

		//check if the king is being checked
		public bool isChecked(Player opponent, Board b)
		{
			bool result = false;
			for (int i = 0; i < opponent.Pieces.Count; i++)
			{
				if (Cell.isPossibleMoveOf(opponent.Pieces[i], b))
				{
					_pieceChecking = opponent.Pieces[i];
					result = true;
				}
			}
			return result;
		}

		//check if the King cannot move out of the mate when being checked
		public bool isOutOfMove(Player opponent, Board b)
		{
			bool result = true;
			for (int i = 0; i < GetPossibleMoves(opponent.Board).Count; i++)
			{
				if (!GetPossibleMoves(opponent.Board)[i].isChecked(opponent, b))
					result = false;
			}
			return result;
		}

		//get the path between the King and the piece checking it
		public List<Cell> CheckingPath(Player opponnet)
		{
			List<Cell> checkingPath = new List<Cell>();
			foreach (Cell c in _pieceChecking.GetPossibleMoves(opponnet.Board))
			{
				if (_pieceChecking.Cell.X == Cell.X)
				{
					if (c.X == Cell.X && (_pieceChecking.Cell.Y - Cell.Y) * (_pieceChecking.Cell.Y - c.Y) > 0)
						checkingPath.Add(c);
				}
				else if (_pieceChecking.Cell.Y == Cell.Y)
				{
					if (c.Y == Cell.Y && (_pieceChecking.Cell.X - Cell.X) * (_pieceChecking.Cell.X - c.X) > 0)
						checkingPath.Add(c);
				}
				else if (Math.Abs(_pieceChecking.Cell.X - Cell.X) == Math.Abs(_pieceChecking.Cell.Y - Cell.Y))
				{
					if (Math.Abs(c.X - Cell.X) == Math.Abs(c.Y - Cell.Y) && ((_pieceChecking.Cell.X - Cell.X) * (_pieceChecking.Cell.X - c.X) + (_pieceChecking.Cell.Y - Cell.Y) * (_pieceChecking.Cell.Y - c.Y)) > 0)
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
				foreach (Piece p in opponent.Opponent.Pieces)
				{
					if (p.GetType() != typeof(King) && c.isPossibleMoveOf(p, b))
					{
						Cell temp = p.Cell;
						b.Move(opponent.Opponent, p, c.X, c.Y);
						if (!isChecked(opponent, b))
						{
							b.Move(opponent.Opponent, p, temp.X, temp.Y);
							return true;
						}
						b.Move(opponent.Opponent, p, temp.X, temp.Y);
					}
				}
			}
			return false;
		}

		//check if the king is checkmated
		public bool isCheckmated(Player opponent, Board b)
		{
			if (isChecked(opponent, b))
			{
				if (isOutOfMove(opponent, b) && !CanBlockMate(opponent, b))
					return true;
			}
			return false;
		}
			
	}
}
