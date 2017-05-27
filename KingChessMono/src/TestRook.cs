using NUnit.Framework;
using System;
namespace KingChess
{
	[TestFixture()]
	public class TestRookAndOtherPieces
	{
		Board board;
		Rook rook;
		Pawn pawn;
		Bishop bishop;
		Knight knight;
		Queen queen;
		King king;
		Player[] players = new Player[2];
		[SetUp()]
		public void init()
		{
			board = new Board();
			rook = new Rook(TeamColor.Black);
			pawn = new Pawn(TeamColor.White);
			bishop = new Bishop(TeamColor.Black);
			knight = new Knight(TeamColor.White);
			queen = new Queen(TeamColor.Black);
			king = new King(TeamColor.White);
			players[0] = new Player(TeamColor.White);
			players[1] = new Player(TeamColor.Black);
		}

		[Test()]
		public void TestRookPossibleMoves()
		{
			board.Cells[0, 0].Piece = rook;
			rook.GetPossibleMoves(board);
			Assert.IsTrue(board.Cells[0, 3].isPossibleMoveOf(rook, board));
			Assert.IsTrue(board.Cells[7, 0].isPossibleMoveOf(rook, board));
			Assert.IsFalse(board.Cells[3, 3].isPossibleMoveOf(rook, board));
		}

		[Test()]
		public void TestRookBlockedByPawn()
		{
			board.Cells[2, 2].Piece = rook;
			board.Cells[2, 3].Piece = pawn;

			Assert.IsTrue(board.Cells[2, 3].isPossibleMoveOf(rook, board));
			Assert.IsTrue(board.Cells[7, 2].isPossibleMoveOf(rook, board));
			Assert.IsFalse(board.Cells[2, 4].isPossibleMoveOf(rook, board));
		}

		[Test()]
		public void TestBishopPossibleMoves()
		{
			board.Cells[1, 4].Piece = bishop;
			board.Cells[4, 7].Piece = rook;
			Assert.IsTrue(board.Cells[3, 6].isPossibleMoveOf(bishop, board));
			Assert.IsTrue(board.Cells[0, 3].isPossibleMoveOf(bishop, board));
			Assert.IsTrue(board.Cells[3, 2].isPossibleMoveOf(bishop, board));
			Assert.IsFalse(board.Cells[4, 7].isPossibleMoveOf(bishop, board));
		}

		[Test()]
		public void TestKnightPossibleMoves()
		{
			board.Cells[1, 0].Piece = knight;
			board.Cells[3, 1].Piece = pawn;
			board.Cells[0, 2].Piece = bishop;
			Assert.IsTrue(board.Cells[0, 2].isPossibleMoveOf(knight, board));
			Assert.IsTrue(board.Cells[2, 2].isPossibleMoveOf(knight, board));
			Assert.IsFalse(board.Cells[3, 1].isPossibleMoveOf(knight, board));
			Assert.IsFalse(board.Cells[0, 0].isPossibleMoveOf(knight, board));
		}

		[Test()]
		public void TestQueenPossibleMoves()
		{
			board.Cells[3, 0].Piece = queen;
			board.Cells[2, 1].Piece = bishop;
			board.Cells[4, 1].Piece = pawn;
			board.Cells[3, 4].Piece = rook;
			Assert.IsTrue(board.Cells[4, 1].isPossibleMoveOf(queen, board));
			Assert.IsTrue(board.Cells[3, 3].isPossibleMoveOf(queen, board));
			Assert.IsFalse(board.Cells[2, 1].isPossibleMoveOf(queen, board));
			Assert.IsFalse(board.Cells[3, 4].isPossibleMoveOf(queen, board));
		}

		[Test()]
		public void TestPawnPossibleMovesNotBlocked()
		{
			board.Cells[3, 3].Piece = pawn;
			board.Cells[2, 4].Piece = knight;
			board.Cells[4, 4].Piece = bishop;
			Assert.IsFalse(board.Cells[2, 4].isPossibleMoveOf(pawn, board));
			Assert.IsTrue(board.Cells[4, 4].isPossibleMoveOf(pawn, board));
			Assert.IsFalse(board.Cells[3, 5].isPossibleMoveOf(pawn, board));
			Assert.IsTrue(board.Cells[3, 4].isPossibleMoveOf(pawn, board));
		}

		[Test()]
		public void TestPawnPossibleMovesBlocked()
		{
			board.Cells[3, 3].Piece = pawn;
			board.Cells[2, 4].Piece = knight;
			board.Cells[4, 4].Piece = bishop;
			board.Cells[3, 4].Piece = queen;
			Assert.IsFalse(board.Cells[2, 4].isPossibleMoveOf(pawn, board));
			Assert.IsTrue(board.Cells[4, 4].isPossibleMoveOf(pawn, board));
			Assert.IsFalse(board.Cells[3, 5].isPossibleMoveOf(pawn, board));
			Assert.IsFalse(board.Cells[3, 4].isPossibleMoveOf(pawn, board));
		}

		[Test()]
		public void TestPawnPossibleMovesFirstMove()
		{
			board.Cells[3, 1].Piece = pawn;
			Assert.IsTrue(board.Cells[3, 2].isPossibleMoveOf(pawn, board));
			Assert.IsTrue(board.Cells[3, 3].isPossibleMoveOf(pawn, board));
            Assert.IsFalse (board.Cells [3, 0].isPossibleMoveOf (pawn, board));
		}

		[Test()]
		public void TestKingPossibleMoves()
		{
			board.Cells[1, 3].Piece = king;
			Assert.IsTrue(board.Cells[2, 4].isPossibleMoveOf(king, board));
			Assert.IsTrue(board.Cells[0, 4].isPossibleMoveOf(king, board));
			Assert.IsTrue(board.Cells[0, 4].isPossibleMoveOf(king, board));	
			Assert.IsTrue(board.Cells[1, 4].isPossibleMoveOf(king, board));
			Assert.IsTrue(board.Cells[0, 2].isPossibleMoveOf(king, board));
		}

		[Test()]
		public void TestKingInCheck()
		{
			players[0].AddPiece(king);
			//players[1].AddPiece(bishop);
			players[1].AddPiece(rook);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			board.Cells[1, 3].Piece = king;
			//board.Cells[5, 7].Piece = bishop;
			board.Cells[1, 7].Piece = rook;
			Assert.IsTrue(king.isChecked(players[1], board));
		}

		[Test()]
		public void TestKingInCheckmateHasEscapeRoute()
		{
			players[0].AddPiece(king);
			players[1].AddPiece(rook);
			players[1].AddPiece(queen);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			players[0].Board = board;
			players[1].Board = board;
			board.Cells[0, 0].Piece = king;
			board.Cells[2, 2].Piece = queen;
			board.Cells[7, 0].Piece = rook;

			Assert.IsFalse(king.isCheckmated(players[1], board));
		}

		[Test()]
		public void TestKingInCheckmateHasProtectingPiece()
		{
			players[0].AddPiece(king);
			players[1].AddPiece(rook);
			players[1].AddPiece(queen);
			players[0].AddPiece(knight);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			players[0].Board = board;
			players[1].Board = board;
			board.Cells[0, 0].Piece = king;
			board.Cells[6, 1].Piece = queen;
			board.Cells[7, 0].Piece = rook;
			board.Cells[1, 2].Piece = knight;

			Assert.IsFalse(king.isCheckmated(players[1], board));
		}

		[Test()]
		public void TestKingInCheckmate()
		{
			players[0].AddPiece(king);
			players[1].AddPiece(rook);
			players[1].AddPiece(queen);
			players[1].AddPiece(bishop);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			players[0].Board = board;
			players[1].Board = board;
			board.Cells[0, 0].Piece = king;
			board.Cells[2, 3].Piece = queen;
			board.Cells[7, 0].Piece = rook;
			board.Cells[0, 2].Piece = bishop;

			Assert.IsTrue(king.isCheckmated(players[1], board));
		}

		[Test()]
		public void TestKingInCheckmateWithBishopChecking()
		{
			players[0].AddPiece(king);
			players[1].AddPiece(rook);
			players[1].AddPiece(queen);
			players[1].AddPiece(bishop);
			players[0].AddPiece(knight);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			players[0].Board = board;
			players[1].Board = board;
			board.Cells[0, 0].Piece = king;
			board.Cells[2, 3].Piece = queen;
			board.Cells[1, 7].Piece = rook;
			board.Cells[2, 2].Piece = bishop;
			board.Cells[3, 2].Piece = knight;

			Assert.IsFalse(king.isCheckmated(players[1], board));
		}

		[Test()]
		public void TestKingInCheckmateWithBishopCheckingKnightOutOfRange()
		{
			players[0].AddPiece(king);
			players[1].AddPiece(rook);
			players[1].AddPiece(queen);
			players[1].AddPiece(bishop);
			players[0].AddPiece(knight);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			players[0].Board = board;
			players[1].Board = board;
			board.Cells[0, 0].Piece = king;
			board.Cells[2, 3].Piece = queen;
			board.Cells[1, 7].Piece = rook;
			board.Cells[2, 2].Piece = bishop;
			board.Cells[5, 2].Piece = knight;

			Assert.IsTrue(king.isCheckmated(players[1], board));
		}

		[Test()]
		public void TestKingInCheckmateCanBlockPathButRevealOtherChecking()
		{
			players[0].AddPiece(king);
			players[1].AddPiece(rook);
			players[1].AddPiece(queen);
			players[1].AddPiece(bishop);
			players[0].AddPiece(knight);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			players[0].Board = board;
			players[1].Board = board;
			board.Cells[0, 0].Piece = king;
			board.Cells[0, 4].Piece = queen;
			board.Cells[1, 7].Piece = rook;
			board.Cells[3, 3].Piece = bishop;
			board.Cells[2, 2].Piece = knight;

			Assert.IsTrue(king.isCheckmated(players[1], board));
		}

		[Test()]
		public void TestKingInCheckmateOtherPiecesCanCapturePieceChecking()
		{
			players[0].AddPiece(king);
			players[1].AddPiece(rook);
			players[1].AddPiece(queen);
			players[0].AddPiece(knight);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			players[0].Board = board;
			players[1].Board = board;
			board.Cells[7, 7].Piece = king;
			board.Cells[1, 6].Piece = queen;
			board.Cells[4, 7].Piece = rook;
			board.Cells[5, 5].Piece = knight;

			Assert.IsFalse(king.isCheckmated(players[1], board));
		}

		[Test()]
		public void TestKingInCheckmateCanCapturePieceChecking()
		{
			players[0].AddPiece(king);
			players[1].AddPiece(rook);
			players[1].AddPiece(queen);
			players[0].Opponent = players[1];
			players[1].Opponent = players[0];
			players[0].Board = board;
			players[1].Board = board;
			board.Cells[7, 7].Piece = king;
			board.Cells[1, 6].Piece = queen;
			board.Cells[6, 7].Piece = rook;
			Assert.IsFalse(king.isCheckmated(players[1], board));
		}


	}
}
