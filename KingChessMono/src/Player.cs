using System;
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

        public void SetupPlayer(Board board)
        {
            _board = board;
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
	}
}
