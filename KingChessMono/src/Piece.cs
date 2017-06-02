///<summary>
/// The abstract class which contains 
/// roles and collaborations of a piece to the
/// game
/// </summary>
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

        /// <summary>
        /// Gets the bitmap of a piece
        /// </summary>
        /// <returns>The Bitmap</returns>
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

        /// <summary>
        /// Registers the piece to the dictionary
        /// </summary>
        /// <param name="name">the string representation of the piece type</param>
        /// <param name="t">the piece type</param>
        public static void RegisterPiece(string name, Type t)
        {
            _pieceRegistry.Add (name, t);
        }

        /// <summary>
        /// Creates an instance to Piece class
        /// </summary>
        /// <returns>A Piece</returns>
        /// <param name="name">string representation of that piece</param>
        /// <param name="team">team color of the piece</param>
        /// <param name="ID">ID of the piece</param>
        public static Piece CreatePiece(string name, TeamColor team, int ID)
        {
            return (Piece)Activator.CreateInstance (_pieceRegistry [name], team, ID);
        }

        /// <summary>
        /// Clears the piece registry.
        /// </summary>
        public static void ClearPieceRegistry()
        {
            _pieceRegistry.Clear ();
        }

        /// <summary>
        /// Draw the piece
        /// </summary>
        public void Draw()
        {
            SwinGame.DrawBitmap (MyBitmap (), BOTTOM_LEFT_CELL_X + Cell.X * CELL_WIDTH, BOTTOM_LEFT_CELL_Y - Cell.Y * CELL_WIDTH);
        }

        /// <summary>
        /// Gets the type of the piece
        /// </summary>
        /// <value>Piece Type</value>
        public abstract PieceType Type
        {
            get;
        }

        /// <summary>
        /// X-coordinate of the piece
        /// </summary>
        /// <value>X-coordinate</value>
        public int X
        {
            get
            {
                return _x;
            }
        }

		/// <summary>
		/// Y-coordinate of the piece
		/// </summary>
		/// <value>Y-coordinate</value>
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

        /// <summary>
        /// Gets the identifier of a Piece, especially pieces that 
        /// exist in pair like Bishop, Rook or Knight
        /// </summary>
        /// <value>The identifier.</value>
        public int ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the team color
        /// </summary>
        /// <value>The color of the team</value>
		public TeamColor Team
		{
			get
			{
				return _team;
			}
		}

        /// <summary>
        /// Gets a value indicating whether the piece is selected.
        /// </summary>
        /// <value>boolean to determine if a piece is selected</value>
		public bool isSelected
		{
			get
			{
				return _isSelected;
			}
		}

        /// <summary>
        /// Gets the value of a piece to determine Board Value
        /// </summary>
        /// <value>The value.</value>
        public abstract int Value
        {
            get;
        }

        /// <summary>
        /// Assign a Cell to a piece
        /// </summary>
        /// <returns></returns>
        /// <param name="c">the cell</param>
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

        /// <summary>
        /// Gets the cell the piece is standing on
        /// </summary>
        /// <value>A Cell</value>
        public Cell Cell
        {
            get
            {
                return _cell;
            }

        }

        /// <summary>
        /// Removes the cell from the piece
        /// </summary>
        public void RemoveCell()
        {
            Deployed (null);
        }

        /// <summary>
        /// Select the piece
        /// </summary>
        public void Select()
        {
            _isSelected = true;
        }

        /// <summary>
        /// Deselect the piece.
        /// </summary>
        public void Deselect()
        {
            _isSelected = false;
        }

		public abstract List<Cell> GetPossibleMoves(Board board);

        /// <summary>
        /// Key of the dictionary
        /// </summary>
        /// <returns>The string representation of a piece</returns>
        /// <param name="p">the Piece</param>
        public string Key(Piece p)
        {
            foreach (string s in _pieceRegistry.Keys)
                if (_pieceRegistry [s] == p.GetType ())
                    return s;
            return null;
        }

        /// <summary>
        /// Save its information to the file
        /// </summary>
        /// <returns></returns>
        /// <param name="writer">A StreamWriter</param>
        public void Save (StreamWriter writer)
        {
            writer.WriteLine (Key (this));
            writer.WriteLine (Team);
            writer.WriteLine (ID);
            writer.WriteLine (X);
            writer.WriteLine (Y);
        }

        /// <summary>
        /// Load itself from the file
        /// </summary>
        /// <returns></returns>
        /// <param name="reader">A StreamReader</param>
        /// <param name="players">the players in the game</param>
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
