using NUnit.Framework;
using System;
using SwinGameSDK;
namespace KingChess
{
	[TestFixture()]
	public class TestGame
	{
        ChessGame game;
		[SetUp()]
		public void init()
		{
            game = new ChessGame ();
            game.SetUpGame ();
		}

		[Test()]
        public void TestSelectedPiece()
		{
            game.Board.Cells [0, 1].Piece.isSelected = true;
            Assert.AreEqual (game.Board.Cells[0, 1].Piece.GetType (), typeof (Pawn));
		}

        [Test()]
        public void TestFetchCell()
        {
            Point2D point = new Point2D ();
            Point2D point2 = new Point2D ();
            point2.X = 230;
            point2.Y = 230;
            point.X = 170;
            point.Y = 475;
            Cell c = game.FetchCell (point);
            Cell c1 = game.FetchCell (point2);
            Assert.AreEqual (c, game.Board.Cells[2, 0]);
            Assert.AreEqual (c1, game.Board.Cells [3, 4]);
            Assert.IsTrue (game.Players[1].Pieces.Contains (c.Piece));
        }

        [Test()]
        public void TestCondition()
        {
            int res = 0;
            int test = 1;
            if (test == 1)
                res = 1;
            else if (res == 1)
                res = 2;
            Assert.AreEqual (res, 1);
        }

        [Test()]
        public void TestCellIsPossibleMove()
        {
            Point2D point = new Point2D ();
			point.X = 170;
			point.Y = 340;
            Cell c = game.FetchCell (point);
            Assert.IsTrue (c.isPossibleMoveOf (game.Board.Cells[2, 1].Piece, game.Board));
            Assert.AreEqual (c, game.Board.Cells[2, 2]);
        }
	}
}
