using NUnit.Framework;
using System;
namespace KingChess
{
	[TestFixture()]
	public class TestBoard
	{
		Board board;
		Player[] players;
		Rook rook;
		Pawn pawn;
		[SetUp()]
		public void init()
		{
			rook = new Rook(TeamColor.Black);
			pawn = new Pawn(TeamColor.White);
			board = new Board();
			players = new Player[2];
			players[1] = new Player(TeamColor.Black);
			players[0] = new Player(TeamColor.White);
			players[1].Opponent = players[0];
			players[0].Opponent = players[1];
			players[0].Board = board;
			players[1].Board = board;
		}

		[Test()]
		public void TestMove()
		{
			board.Cells[1, 0].Piece = rook;
			board.Cells[1, 4].Piece = pawn;
			players[1].AddPiece(rook);
			players[0].AddPiece(pawn);
			board.Move(players[1], rook, 1, 4);
			Assert.AreEqual(rook.Cell, board.Cells[1, 4]);
			Assert.AreEqual(board.Cells[1, 4].Piece, rook);
			Assert.AreEqual(board.Cells[1, 0].Piece, null);
			Assert.AreEqual(pawn.Cell, null);
			Assert.IsFalse(players[0].Pieces.Contains(pawn));
		}

	}
}
