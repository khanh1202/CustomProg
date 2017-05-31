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

		public Cell[,] Cells
		{
			get
			{
				return cells;
			}
		}

		public int Height
		{
			get
			{
				return _HEIGHT;
			}
		}

		public int Width
		{
			get
			{
				return _WIDTH;
			}
		}

		public List<Move> Moves
		{
			get
			{
				return _moves;
			}
		}

        private Bitmap MyBitmap()
        {
            return SwinGame.BitmapNamed ("ChessBoard");
        }

        public void Draw()
        {
            SwinGame.DrawBitmap (MyBitmap (), 0, 0);
        }

		public void Move(Player player, Piece p, int x, int y)
		{
			if (Cells[x, y].isPossibleMoveOf(p, this))
            {
                Piece temp = p;
				_moves.Add(new Move(player, p, Cells[x, y].Piece, p.Cell, Cells[x, y]));
                player.RemovePiece (p.Cell);
                player.Opponent.RemovePiece (Cells[x, y]);
                player.AddPiece (temp, Cells[x, y]);
			}
		}

        public void MoveWithoutChecking(Player player, Piece p, int x, int y)
        {
			Piece temp = p;
			_moves.Add (new Move (player, p, Cells [x, y].Piece, p.Cell, Cells [x, y]));
			player.RemovePiece (p.Cell);
			player.Opponent.RemovePiece (Cells [x, y]);
			player.AddPiece (temp, Cells [x, y]);
        }

		public bool isKingMovedBefore (TeamColor team)
		{
			foreach (Move m in _moves) {
				if (m.PieceMove.GetType () == typeof (King) && m.PieceMove.Team == team)
					return true;
			}
			return false;
		}

        public bool isRookMovedBefore(TeamColor team, int ID)
        {
            foreach (Move m in _moves)
            {
                if (m.PieceMove.GetType () == typeof (Rook) && m.PieceMove.Team == team && m.PieceMove.ID == ID)
                    return true;
            }
            return false;
        }

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

        public void ReverseMove(ChessGame game)
        {
            if (_moves.Count == 0)
                return;
            Move lastMove = _moves [_moves.Count - 1];
            MoveWithoutChecking (lastMove.PlayerMove, lastMove.PieceMove, lastMove.CellFrom.X, lastMove.CellFrom.Y);
            if (lastMove.PieceCaptured != null)
                lastMove.PlayerMove.Opponent.AddPiece (lastMove.PieceCaptured, lastMove.CellTo);
            _moves.Remove (_moves [_moves.Count - 1]);
            _moves.Remove (lastMove);
            game.ChangeTurn ();
        }

        public void Save (string filename, Player[] players)
        {
            StreamWriter writer;
            writer = new StreamWriter (filename);
            Move lastmove = _moves[_moves.Count - 1]; 
            writer.WriteLine (lastmove.PlayerMove.Opponent.Team);
            writer.WriteLine (players[1].Pieces.Count + players[0].Pieces.Count);
            foreach (Piece p in players [0].Pieces)
                p.Save (writer);
            foreach (Piece p in players [1].Pieces)
                p.Save (writer);
            writer.Close ();

        }

        public void ReleaseMove()
        {
            _moves.Clear ();
        }

	}
}
