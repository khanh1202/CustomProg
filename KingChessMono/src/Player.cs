using System;
using System.IO;
using SwinGameSDK;
using System.Collections.Generic;
namespace KingChess
{
	public class Player
	{
		private TeamColor _color;
		private List<Piece> _pieces = new List<Piece>();
		private Player _opponent;
		private Board _board;
		public Player(TeamColor color)
		{
			_color = color;
		}

		public TeamColor Team
		{
			get
			{
				return _color;
			}
		}

		public List<Piece> Pieces
		{
			get
			{
				return _pieces;
			}
		}

		public King King
		{
			get
			{
				Piece result = null;
				foreach (Piece p in _pieces)
					if (p.GetType() == typeof(King))
						result = p;
				return result as King;
			}
		}

		public Player Opponent
		{
			get
			{
				return _opponent;
			}
			set
			{
				_opponent = value;
			}
		}

		public Board Board
		{
			get
			{
				return _board;
			}
            set
            {
                _board = value;
            }
		}

        public void SetupPlayer()
        {
            StartGameDeployment ();
        }

		public void StartGameDeployment()
		{
			if (_color == TeamColor.White)
			{
				for (int i = 0; i < 8; i++)
	                AddPiece(new Pawn(TeamColor.White), _board.Cells[i, 1]);
	            AddPiece(new Rook(TeamColor.White, 1), _board.Cells [0, 0]);
	            AddPiece (new Rook (TeamColor.White, 2), _board.Cells [7, 0]);
	            AddPiece (new Knight (TeamColor.White), _board.Cells [1, 0]);
	            AddPiece (new Knight (TeamColor.White), _board.Cells [6, 0]);
	            AddPiece (new Bishop (TeamColor.White), _board.Cells [2, 0]);
	            AddPiece (new Bishop (TeamColor.White), _board.Cells [5, 0]);
	            AddPiece (new Queen (TeamColor.White), _board.Cells [3, 0]);
	            AddPiece (new King (TeamColor.White), _board.Cells [4, 0]);

			}
			else
			{
                for (int i = 0; i < 8; i++)
                    AddPiece (new Pawn (TeamColor.Black), _board.Cells [i, 6]);
                AddPiece (new Rook (TeamColor.Black, 1), _board.Cells [0, 7]);
                AddPiece (new Rook (TeamColor.Black, 2), _board.Cells [7, 7]);
                AddPiece (new Knight (TeamColor.Black), _board.Cells [1, 7]);
                AddPiece (new Knight (TeamColor.Black), _board.Cells [6, 7]);
                AddPiece (new Bishop (TeamColor.Black), _board.Cells [2, 7]);
                AddPiece (new Bishop (TeamColor.Black), _board.Cells [5, 7]);
                AddPiece (new Queen (TeamColor.Black), _board.Cells [3, 7]);
                AddPiece (new King (TeamColor.Black), _board.Cells [4, 7]);
			}
		}

        public void ShuffleList(List<Move> moves)
        {
            int n = moves.Count;
            Random rnd = new Random ();
            while (n > 1)
            {
                int k = rnd.Next (0, n - 1);
                n--;
                Move temp = moves [k];
                moves [k] = moves [n - 1];
                moves [n - 1] = temp;
            }
        }

        public List<Move> GeneratedMoves()
        {
            List<Move> _possiblemoves = new List<Move> ();
            foreach (Piece p in _pieces)
            {
                foreach (Cell c in p.GetPossibleMoves (_board))
                    _possiblemoves.Add (new Move (this, p, c.Piece, p.Cell, c));
            }
            ShuffleList (_possiblemoves);
            return _possiblemoves;
        }

		public void AddPiece(Piece p, Cell c)
		{
            c.Piece = p;
            _pieces.Add (p);
		}

        public void RemovePiece (Cell c)
        {
            _pieces.Remove (c.Piece);
            c.RemovePiece (this);
        }

        public void ReleasePiece()
        {
            for (int i = _pieces.Count - 1; i >= 0; i--)
                RemovePiece (_pieces [i].Cell);
        }

        public void DrawPieces()
        {
			foreach (Piece p in Pieces) {
				p.Draw ();
				if (p.isSelected)
					HighlightCells (p);
			}
			foreach (Piece p in Pieces) {
				p.Draw ();
				if (p.isSelected)
					HighlightCells (p);
			}
        }

		public void HighlightCells (Piece chosen)
		{
			chosen.Cell.DrawOutline ();
            foreach (Cell c in chosen.GetPossibleMoves (_board))
				c.DrawOutline ();
		}

        public virtual void TakeTurn(Point2D point, ChessGame game)
        {
            if (game.State == GameState.Selecting)
            {
                Cell chosen = game.FetchCell (point);
                if (chosen != null && chosen.Piece != null && _pieces.Contains (chosen.Piece))
                {
                    game.ChangeState (GameState.Moving);
                    chosen.Piece.Select ();
                    game.SetChosenPiece (chosen.Piece);
                }
            } 
            else if (game.State == GameState.Moving)
            {
                Cell chosen = game.FetchCell (point);
                if (chosen.Piece != null && chosen.Piece.isSelected)
                {
                    game.ChangeState (GameState.Selecting);
                    chosen.Piece.Deselect ();
                } 
                else if (chosen.isPossibleMoveOf (game.ChosenPiece, _board))
                {
                    game.ChangeState (GameState.Moved);
					//_board.Move (this, game.ChosenPiece, chosen.X, chosen.Y);
                    _board.Move (new Move (this, game.ChosenPiece, chosen.Piece, game.ChosenPiece.Cell, chosen));
                    if (game.ChosenPiece.GetType () == typeof (King) && _board.timesKingMovedBefore (_color) == 1)
                        MoveRookInCastle (_color, chosen);
                    game.ChosenPiece.Deselect ();
                    game.ChangeTurn ();
                    Opponent.TakeTurn (point, game);
                }
            }
        }

		public void MoveRookInCastle (TeamColor team, Cell cellKingMovedTo)
		{
			if (team == TeamColor.White) {
                if (cellKingMovedTo == _board.Cells [2, 0])
                    _board.MoveWithoutChecking (new Move (this, _board.Cells [0, 0].Piece, null, _board.Cells[0, 0], _board.Cells[3, 0]), true);
                if (cellKingMovedTo == _board.Cells [6, 0])
                    _board.MoveWithoutChecking (new Move (this, _board.Cells [7, 0].Piece, null, _board.Cells [7, 0], _board.Cells [5, 0]), true);
			}
			if (team == TeamColor.Black) {
                if (cellKingMovedTo == _board.Cells [2, 7])
                    _board.MoveWithoutChecking (new Move (this, _board.Cells [0, 7].Piece, null, _board.Cells [0, 7], _board.Cells [3, 7]), true);
                if (cellKingMovedTo == _board.Cells [6, 7])
                    _board.MoveWithoutChecking (new Move (this, _board.Cells [0, 0].Piece, null, _board.Cells [0, 0], _board.Cells [3, 0]), true);
			}
		}

		public int BoardValue ()
		{
			int result = 0;
            foreach (Cell c in _board.Cells)
                if (c.Piece != null)
                    result += c.Piece.Value;
			return result;
		}

		public int AlphaBetaMax (int depth, ChessGame game, Player maximising, int beta, int alpha)
		{
			if (depth == 0)
            {
                Console.WriteLine (BoardValue ());
               return BoardValue (); 
            }
				
            List<Move> newGameMoves = maximising.GeneratedMoves ();
            int bestValue = -9999;
			for (int i = 0; i < newGameMoves.Count; i++) {
				game.Board.Move (newGameMoves [i]);
                bestValue = Math.Max(bestValue, AlphaBetaMin (depth - 1, game, maximising.Opponent, alpha, beta));
                alpha = Math.Max (alpha, bestValue);
		      game.Board.ReverseMove (game, false);
                if (beta <= alpha)
                    return bestValue;
			}
            return bestValue;
		}

		public int AlphaBetaMin (int depth, ChessGame game, Player minimising, int alpha, int beta)
		{
			if (depth == 0) 
            {
				Console.WriteLine (BoardValue ());
				return BoardValue ();
			}
            List<Move> newGameMoves = minimising.GeneratedMoves ();
			int bestValue = 9999;
			for (int i = 0; i < newGameMoves.Count; i++) 
            {
				game.Board.Move (newGameMoves [i]);
                bestValue = Math.Min (bestValue, AlphaBetaMax (depth - 1, game, minimising.Opponent, alpha, beta));
                beta = Math.Min (beta, bestValue);
				game.Board.ReverseMove (game, false);
                if (beta <= alpha)
                    return bestValue;
			}
            return bestValue;
		}

        public Move BestMove(int depth, ChessGame game, Player p)
        {
            List<Move> newGameMoves = GeneratedMoves ();
            Console.WriteLine (newGameMoves.Count);
            int bestValue = -9999;
            Move bestMove = null;
            for (int i = 0; i < newGameMoves.Count; i++)
            {
                Move newMove = newGameMoves [i];
                game.Board.Move (newMove);
                int moveValue = AlphaBetaMin (depth - 1, game, p.Opponent, -10000, 10000);
                game.Board.ReverseMove (game, false);
                if (moveValue >= bestValue)
                {
                    bestValue = moveValue;
                    bestMove = newMove;
                }
            }
            return bestMove;
        }
	}
}
