using System;
using System.IO;
using SwinGameSDK;
using System.Collections.Generic;
namespace KingChess
{
	public class Board
	{
		private const int _WIDTH = 8;
		private const int _HEIGHT = 8;
		private List<Move> _moves;
		private Cell[,] cells = new Cell[_WIDTH, _HEIGHT];
		public Board()
		{
			for (int i = 0; i < _WIDTH; i++)
				for (int j = 0; j < _HEIGHT; j++)
					cells[i, j] = new Cell(i, j, null);
            _moves = new List<Move> ();
		}

        /// <summary>
        /// Gets the cells on the board.
        /// </summary>
        /// <value>list of cells</value>
		public Cell[,] Cells
		{
			get
			{
				return cells;
			}
		}

        /// <summary>
        /// Gets the height of the board
        /// </summary>
        /// <value>The height.</value>
		public int Height
		{
			get
			{
				return _HEIGHT;
			}
		}

        /// <summary>
        /// Gets the width of the board
        /// </summary>
        /// <value>The width.</value>
		public int Width
		{
			get
			{
				return _WIDTH;
			}
		}

        /// <summary>
        /// Gets the list of all the moves made before
        /// </summary>
        /// <value>The list of moves.</value>
		public List<Move> Moves
		{
			get
			{
				return _moves;
			}
		}

        /// <summary>
        /// gets the bitmap of the board
        /// </summary>
        /// <returns>The bitmap.</returns>
        private Bitmap MyBitmap()
        {
            return SwinGame.BitmapNamed ("ChessBoard");
        }

        /// <summary>
        /// Draw the board
        /// </summary>
        public void Draw()
        {
            SwinGame.DrawBitmap (MyBitmap (), 0, 0);
        }

        /// <summary>
        /// Move a piece
        /// </summary>
        /// <returns></returns>
        /// <param name="m">A move</param>
		public void Move(Move m)
		{
            if (m.CellTo.isPossibleMoveOf(m.PieceMove, this))
            {
                Piece temp = m.PieceMove;
				_moves.Add(m);
                m.PlayerMove.RemovePiece (m.PieceMove.Cell);
                m.PlayerMove.Opponent.RemovePiece (m.CellTo);
                m.PlayerMove.AddPiece (temp, m.CellTo);
			}
		}

        /// <summary>
        /// Moves a piece the without checking the eligibility of the move
        /// </summary>
        /// <param name="m">A Move</param>
        /// <param name="isACastle">Is it a castle move?</param>
        public void MoveWithoutChecking(Move m, bool isACastle)
        {
			Piece temp = m.PieceMove;
            if (!isACastle)
			    _moves.Add (m);
			m.PlayerMove.RemovePiece (m.PieceMove.Cell);
			m.PlayerMove.Opponent.RemovePiece (m.CellTo);
			m.PlayerMove.AddPiece (temp, m.CellTo);
        }

        /// <summary>
        /// times a king has moved before
        /// </summary>
        /// <returns>The number of times king appears as the pieceMove in a move</returns>
        /// <param name="team">Team.</param>
		public int timesKingMovedBefore (TeamColor team)
		{
            int result = 0;
            foreach (Move m in _moves) 
            {
                if (m.PieceMove.GetType () == typeof (King) && m.PieceMove.Team == team)
                    result++;
            }
            return result;
		}

        /// <summary>
        /// check if the rook has moved before
        /// </summary>
        /// <returns><c>true</c>, if rook does not appear in the history of moves, <c>false</c> otherwise.</returns>
        /// <param name="team">team color</param>
        /// <param name="ID">nam</param>
        public bool isRookMovedBefore(TeamColor team, int ID)
        {
            foreach (Move m in _moves)
            {
                if (m.PieceMove.GetType () == typeof (Rook) && m.PieceMove.Team == team && m.PieceMove.ID == ID)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check if there are any other pieces between them
        /// </summary>
        /// <returns><c>true</c>, if there is no piece standing between them, <c>false</c> otherwise.</returns>
        /// <param name="team">Team.</param>
        /// <param name="rookID">Rook identifier.</param>
        public bool AreTwoPiecesNotBlocked(TeamColor team, int rookID)
        {
            switch (team)
            {
                case TeamColor.White:
					if (rookID == 1) 
                    {
						if (Cells [1, 0].Piece == null && Cells [2, 0].Piece == null && Cells [3, 0].Piece == null)
							return true;
						return false;
					} 
                    else 
                    {
						if (Cells [5, 0].Piece == null && Cells [6, 0].Piece == null)
							return true;
						return false;
					}
                default:
					if (rookID == 1) 
                    {
						if (Cells [1, 7].Piece == null && Cells [2, 7].Piece == null && Cells [3, 7].Piece == null)
							return true;
						return false;
					} 
                    else 
                    {
						if (Cells [5, 7].Piece == null && Cells [6, 7].Piece == null)
							return true;
						return false;
					}
            }

        }

        /// <summary>
        /// Reverses the move.
        /// </summary>
        /// <param name="game">the Game</param>
        /// <param name="isTurnChanged">If set to <c>true</c> the turn is swapped</param>
        public void ReverseMove(ChessGame game, bool isTurnChanged)
        {
            if (_moves.Count == 0)
                return;
            Move lastMove = _moves [_moves.Count - 1];
            MoveWithoutChecking (new Move (lastMove.PlayerMove, lastMove.PieceMove, null, lastMove.CellTo, lastMove.CellFrom), false);
            if (lastMove.PieceCaptured != null)
                lastMove.PlayerMove.Opponent.AddPiece (lastMove.PieceCaptured, lastMove.CellTo);
            _moves.Remove (_moves [_moves.Count - 1]);
            _moves.Remove (lastMove);
            if (isTurnChanged)
                game.ChangeTurn ();
        }

        /// <summary>
        /// Save the board the the pieces to the file 
        /// </summary>
        /// <returns></returns>
        /// <param name="filename">filename</param>
        /// <param name="game">the game</param>
        public void Save (string filename, ChessGame game)
        {
            StreamWriter writer;
            writer = new StreamWriter (filename);
            if (game.isAI)
                writer.WriteLine ("AI");
            else
                writer.WriteLine ("Human");
            writer.WriteLine (game.State);
            Move lastmove = _moves[_moves.Count - 1]; 
            writer.WriteLine (lastmove.PlayerMove.Opponent.Team);
            writer.WriteLine (game.Players[1].Pieces.Count + game.Players[0].Pieces.Count);
            foreach (Piece p in game.Players [0].Pieces)
                p.Save (writer);
            foreach (Piece p in game.Players [1].Pieces)
                p.Save (writer);
            writer.Close ();

        }

        /// <summary>
        /// Clear all moves in the list
        /// </summary>
        public void ReleaseMove()
        {
            _moves.Clear ();
        }

	}
}
