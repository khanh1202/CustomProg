using System;
using System.IO;
using SwinGameSDK;
using System.Collections.Generic;
namespace KingChess
{
	public abstract class Piece
	{
        public const int BOTTOM_LEFT_CELL_X = 45;
        public const int BOTTOM_LEFT_CELL_Y = 448;
        public const int CELL_WIDTH = 58;
        private int _id;
		private Cell _cell;
        private int _x;
        private int _y;
		private TeamColor _team;
		private bool _isSelected;
        private static Dictionary<string, Type> _pieceRegistry = new Dictionary<string, Type> ();

        public Bitmap MyBitmap()
        {
            switch (Type)
            {
                case PieceType.Bishop:
	                if (_team == TeamColor.Black)
	                    return SwinGame.BitmapNamed ("BlackBishop");
	                else
	                    return SwinGame.BitmapNamed ("WhiteBishop");
                case PieceType.King:
					if (_team == TeamColor.Black)
						return SwinGame.BitmapNamed ("BlackKing");
					else
						return SwinGame.BitmapNamed ("WhiteKing");
                case PieceType.Knight:
	                if (_team == TeamColor.Black)
	                    return SwinGame.BitmapNamed ("BlackKnight");
	                else
	                    return SwinGame.BitmapNamed ("WhiteKnight");
	            case PieceType.Pawn:
					if (_team == TeamColor.Black)
						return SwinGame.BitmapNamed ("BlackPawn");
					else
						return SwinGame.BitmapNamed ("WhitePawn");
                case PieceType.Queen:
					if (_team == TeamColor.Black)
						return SwinGame.BitmapNamed ("BlackQueen");
					else
						return SwinGame.BitmapNamed ("WhiteQueen");
                default:
					if (_team == TeamColor.Black)
						return SwinGame.BitmapNamed ("BlackRook");
					else
						return SwinGame.BitmapNamed ("WhiteRook");
            }
        }

        public static void RegisterPiece(string name, Type t)
        {
            _pieceRegistry.Add (name, t);
        }

        public static Piece CreatePiece(string name, TeamColor team, int ID)
        {
            return (Piece)Activator.CreateInstance (_pieceRegistry [name], team, ID);
        }

        public void Draw()
        {
            SwinGame.DrawBitmap (MyBitmap (), BOTTOM_LEFT_CELL_X + Cell.X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y - Cell.Y * CELL_WIDTH);
        }

        public abstract PieceType Type
        {
            get;
        }

        public int X
        {
            get
            {
                return _x;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
        }

		public Piece(TeamColor team, int ID)
		{
			_team = team;
            _id = ID;
		}

        public int ID
        {
            get
            {
                return _id;
            }
        }

		public TeamColor Team
		{
			get
			{
				return _team;
			}
		}

		public bool isSelected
		{
			get
			{
				return _isSelected;
			}
		}

        public void Deployed(Cell c)
        {
            if (c != null)
            {
				_x = c.X;
				_y = c.Y;
				_cell = c; 
            } else
            {
                _x = -1;
                _y = -1;
                _cell = null;
            }

        }

        public Cell Cell
        {
            get
            {
                return _cell;
            }

        }

        public void RemoveCell()
        {
            Deployed (null);
        }

        public void Select()
        {
            _isSelected = true;
        }

        public void Deselect()
        {
            _isSelected = false;
        }

		public abstract List<Cell> GetPossibleMoves(Board board);

        public string Key(Piece p)
        {
            foreach (string s in _pieceRegistry.Keys)
                if (_pieceRegistry [s] == p.GetType ())
                    return s;
            return null;
        }

        public void Save (StreamWriter writer)
        {
            writer.WriteLine (Key (this));
            writer.WriteLine (Team);
            writer.WriteLine (ID);
            writer.WriteLine (X);
            writer.WriteLine (Y);
        }

        public static void Load(StreamReader reader, Player[] players)
        {
            string pieceType = reader.ReadLine ();
            TeamColor team = (reader.ReadLine () == "Black") ? TeamColor.Black : TeamColor.White;
            Console.WriteLine (pieceType);
            int iD = Convert.ToInt32 (reader.ReadLine ());
            int x = Convert.ToInt32 (reader.ReadLine ());
            int y = Convert.ToInt32 (reader.ReadLine ());
            Piece p = CreatePiece (pieceType, team, iD);
            if (players [0].Team == team)
                players [0].AddPiece (p, players [0].Board.Cells [x, y]);
            else
                players [1].AddPiece (p, players [1].Board.Cells [x, y]);
        }
	}
}
