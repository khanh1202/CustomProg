///<summary>
/// The Player represents the player who controls the piecs and the 
/// whole team to win against the opponent
/// </summary>

using System;
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

        /// <summary>
        /// Gets the team color.
        /// </summary>
        /// <value>The team color</value>
		public TeamColor Team
		{
			get
			{
				return _color;
			}
		}

        /// <summary>
        /// Gets all the pieces it has
        /// </summary>
        /// <value>a list of pieces</value>
		public List<Piece> Pieces
		{
			get
			{
				return _pieces;
			}
		}

        /// <summary>
        /// Gets the king of a Player
        /// </summary>
        /// <value>The king piece</value>
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

        /// <summary>
        /// Gets or sets the opponent of this
        /// </summary>
        /// <value>a Player which is the opponent</value>
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

        /// <summary>
        /// Gets or sets the game board
        /// </summary>
        /// <value>The board.</value>
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

        /// <summary>
        /// Setups the player at the start of the game.
        /// </summary>
        public void SetupPlayer()
        {
            StartGameDeployment ();
        }

        /// <summary>
        /// Setup pieces around the board
        /// </summary>
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

        /// <summary>
        /// Shuffles the order of elements in the Move list to make
        /// sure the AI play differently at each time
        /// </summary>
        /// <param name="moves">Moves.</param>
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

        /// <summary>
        /// Generate all the possible moves for a player in their
        /// turn
        /// </summary>
        /// <returns>The list of Move</returns>
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

        /// <summary>
        /// Adds piece to the list and add piece the board cell
        /// </summary>
        /// <param name="p">The piece to add</param>
        /// <param name="c">The Cell a piece is Added to</param>
		public void AddPiece(Piece p, Cell c)
		{
            c.Piece = p;
            _pieces.Add (p);
		}

        /// <summary>
        /// Removes the piece from the list and remove the 
        /// piece from its cell
        /// </summary>
        /// <param name="c">The cell to remove the piece</param>
        public void RemovePiece (Cell c)
        {
            _pieces.Remove (c.Piece);
            c.RemovePiece (this);
        }

        /// <summary>
        /// Remove all the piece from the list and all the cells
        /// </summary>
        public void ReleasePiece()
        {
            for (int i = _pieces.Count - 1; i >= 0; i--)
                RemovePiece (_pieces [i].Cell);
        }

        /// <summary>
        /// Draws the pieces.
        /// </summary>
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

        /// <summary>
        /// Highlights the cells that are possible moves of a piece
        /// </summary>
        /// <param name="chosen">The chosen piece</param>
		public void HighlightCells (Piece chosen)
		{
			chosen.Cell.DrawOutline ();
            foreach (Cell c in chosen.GetPossibleMoves (_board))
				c.DrawOutline ();
		}

        /// <summary>
        /// Handle input and take a turn for Human Player
        /// </summary>
        /// <param name="point">usually the mouse position</param>
        /// <param name="game">the game in play</param>
        public virtual void TakeTurn(Point2D point, ChessGame game)
        { 
            //if the player is selecting piece to move
            if (game.State == GameState.Selecting)
            {
                Cell chosen = game.FetchCell (point);
                //if the piece chosen is not null and belongs to the player
                if (chosen != null && chosen.Piece != null && _pieces.Contains (chosen.Piece))
                {
                    game.ChangeState (GameState.Moving);
                    chosen.Piece.Select ();
                    game.SetChosenPiece (chosen.Piece);
                }
            } 
            //if the player is selecting a cell to move the piece
            else if (game.State == GameState.Moving)
            {
                Cell chosen = game.FetchCell (point);
                //if the chosen cell is the cell that contains the piece, deselect the piece
                if (chosen.Piece != null && chosen.Piece.isSelected)
                {
                    game.ChangeState (GameState.Selecting);
                    chosen.Piece.Deselect ();
                }
                //if the chosen cell is in possible moves of the chosen piece
                else if (chosen.isPossibleMoveOf (game.ChosenPiece, _board))
                {
                    game.ChangeState (GameState.Moved);
                    _board.Move (new Move (this, game.ChosenPiece, chosen.Piece, game.ChosenPiece.Cell, chosen));
                    //if the king is moved, check if whether it is a castle
                    if (game.ChosenPiece.GetType () == typeof (King) && _board.timesKingMovedBefore (_color) == 1)
                        MoveRookInCastle (_color, chosen);
                    game.ChosenPiece.Deselect ();
                    game.ChangeTurn ();
                    Opponent.TakeTurn (point, game);
                }
            }
        }

        /// <summary>
        /// Moves the rook in castle.
        /// </summary>
        /// <param name="team">team the rook belongs to</param>
        /// <param name="cellKingMovedTo">Cell king moved to.</param>
		public void MoveRookInCastle (TeamColor team, Cell cellKingMovedTo)
		{
			if (team == TeamColor.White) {
                //if the king is moved to F8
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

        /// <summary>
        /// Calculate the value of the board to calculate next
        /// move for the AI
        /// </summary>
        /// <returns>The board value</returns>
		public int BoardValue ()
		{
			int result = 0;
            foreach (Cell c in _board.Cells)
                if (c.Piece != null)
                    result += c.Piece.Value;
			return result;
		}

        /// <summary>
        /// Calculate the best move for AI Player
        /// </summary>
        /// <returns>The greatest board value</returns>
        /// <param name="depth">the number of moves the AI can look ahead</param>
        /// <param name="game">the chess game</param>
        /// <param name="maximising">the player trying to maximize the board value</param>
        /// <param name="beta">maximum value</param>
        /// <param name="alpha">minimum value</param>
		public int AlphaBetaMax (int depth, ChessGame game, Player maximising, int alpha, int beta)
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

		/// <summary>
		/// Calculate the best move for the Human Player
		/// </summary>
		/// <returns>the smallest board value</returns>
		/// <param name="depth">the number of moves the AI can look ahead</param>
		/// <param name="game">the chess game</param>
		/// <param name="minimising">The player trying to minimize the board value</param>
		/// <param name="alpha">the minimum board value</param>
		/// <param name="beta">the maximum board value</param>
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

        /// <summary>
        /// Figure out the best move for the AI
        /// </summary>
        /// <returns>The Move</returns>
        /// <param name="depth">Number of moves the AI can see ahead</param>
        /// <param name="game">the chess game</param>
        /// <param name="p">the player needing best move</param>
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
