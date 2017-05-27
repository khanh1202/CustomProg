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

		public Piece King
		{
			get
			{
				Piece result = null;
				foreach (Piece p in _pieces)
					if (p.GetType() == typeof(King))
						result = p;
				return result;
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

		public void StartGameDeployment()
		{
			if (_color == TeamColor.White)
			{
				for (int i = 0; i < 8; i++)
					_board.Cells[i, 1].Piece = new Pawn(TeamColor.White);
				_board.Cells[0, 0].Piece = new Rook(TeamColor.White);
				_board.Cells[7, 0].Piece = new Rook(TeamColor.White);
				_board.Cells[1, 0].Piece = new Knight(TeamColor.White);
				_board.Cells[6, 0].Piece = new Knight(TeamColor.White);
				_board.Cells[2, 0].Piece = new Bishop(TeamColor.White);
				_board.Cells[5, 0].Piece = new Bishop(TeamColor.White);
				_board.Cells[3, 0].Piece = new Queen(TeamColor.White);
				_board.Cells[4, 0].Piece = new King(TeamColor.White);
			}
			else
			{
				for (int i = 0; i < 8; i++)
					_board.Cells[i, 6].Piece = new Pawn(TeamColor.Black);
				_board.Cells[0, 7].Piece = new Rook(TeamColor.Black);
				_board.Cells[7, 7].Piece = new Rook(TeamColor.Black);
				_board.Cells[1, 7].Piece = new Knight(TeamColor.Black);
				_board.Cells[6, 7].Piece = new Knight(TeamColor.Black);
				_board.Cells[2, 7].Piece = new Bishop(TeamColor.Black);
				_board.Cells[5, 7].Piece = new Bishop(TeamColor.Black);
				_board.Cells[3, 7].Piece = new Queen(TeamColor.Black);
				_board.Cells[4, 7].Piece = new King(TeamColor.Black);
			}
		}

		public void AddPiece(Piece p)
		{
			_pieces.Add(p);
		}

		public void AddPieces()
		{
			if (Team == TeamColor.Black)
			{
				for (int y = 6; y <= 7; y++)
					for (int x = 0; x < 8; x++)
                    {
                        _pieces.Add (_board.Cells [x, y].Piece);
                    }
						
			}
			else
			{
				for (int y = 0; y <= 1; y++)
					for (int x = 0; x < 8; x++)
						_pieces.Add(_board.Cells[x, y].Piece);
			}
		}

		public void RemovePiece(Piece p)
		{
			_pieces.Remove(p);
		}

		public Piece SelectPieceAt(int x, int y)
		{
			if (_pieces.Contains(_board.Cells[x, y].Piece))
			{
				_board.Cells[x, y].Piece.isSelected = true;
				foreach (Piece piece in _pieces)
				{
					if (piece != _board.Cells[x, y].Piece && piece.isSelected)
						DeselectPiece(piece);
				}
				return _board.Cells[x, y].Piece;
			}
			return null;
		}

		public void DeselectPiece(Piece p)
		{
			p.isSelected = false;
		}

	}
}
