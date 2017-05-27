using System;
using SwinGameSDK;
using System.Collections.Generic;
namespace KingChess
{
	public class Board
	{
		private const int _WIDTH = 8;
		private const int _HEIGHT = 8;
		private List<Move> _moves = new List<Move>();
		private Cell[,] cells = new Cell[_WIDTH, _HEIGHT];
		public Board()
		{
			for (int i = 0; i < _WIDTH; i++)
				for (int j = 0; j < _HEIGHT; j++)
					cells[i, j] = new Cell(i, j, null);
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
				_moves.Add(new Move(p, Cells[x, y].Piece, p.Cell, Cells[x, y]));
                p.Cell.RemovePiece (player.Opponent);
                Cells [x, y].RemovePiece (player.Opponent);
                Cells [x, y].Piece = p;
			}
		}

	}
}
