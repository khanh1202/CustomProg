using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace KingChess
{
    public class Messages
    {
        private const int CHECKING_STATE_X = 600;
        private const int CHECKING_STATE_Y = 30;
        private const int DISTANCE = 17;
        private const int MOVES_X = 570;
        private const int FIRST_MOVE_Y = 50;

        public string DetermineCheck(ChessGame game)
        {
            for (int i = 0; i < 2; i++)
            {
                if (game.Players[i].King == null || game.Players [i].King.isCheckmated (game.Players [(i + 1) % 2], game.Board))
                    return game.Players [i].Team + " checkmated";
            }
			for (int i = 0; i < 2; i++) 
            {
                if (game.Players [i].King.isChecked (game.Players [(i + 1) % 2], game.Board))
					return game.Players [i].Team + " in check";
			}
            return "In safety";
        }

        public void DrawCheckingState(ChessGame game)
        {
            SwinGame.DrawText (DetermineCheck (game), Color.Chocolate, CHECKING_STATE_X, CHECKING_STATE_Y);
        }

        public void DrawMoves(List<Move> moves)
        {
            for (int i = 0; i < 10; i++)
                SwinGame.DrawText (i + 1 + ". ", Color.Purple, MOVES_X, FIRST_MOVE_Y + i * 30);
            if (moves.Count < 10)
                for (int i = 0; i < moves.Count; i++)
	            {
	                string toDraw = moves [i].PieceMove.Team + "   " + moves [i].ConvertPieceToString (moves[i].PieceMove) + "   " + moves [i].ConvertCellToString (moves [i].CellFrom) + "   " + moves [i].ConvertCellToString (moves [i].CellTo);
	                SwinGame.DrawText (toDraw, Color.Purple, MOVES_X + DISTANCE, FIRST_MOVE_Y + i * 30);
	            } 
            else
                for (int i = moves.Count - 10; i < moves.Count; i++) 
                {
					string toDraw = moves [i].PieceMove.Team + "   " + moves [i].ConvertPieceToString (moves [i].PieceMove) + "   " + moves [i].ConvertCellToString (moves [i].CellFrom) + "   " + moves [i].ConvertCellToString (moves [i].CellTo);
                SwinGame.DrawText (toDraw, Color.Purple, MOVES_X + DISTANCE, FIRST_MOVE_Y + (10 + i - moves.Count) * 30);
				}


		}

        public void DrawButtons()
        {
			SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Undo_active"), 570, 350);
			SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Replay_active"), 700, 350);
        }

        public void Draw(ChessGame game)
        {
            DrawCheckingState(game);
            DrawMoves(game.Board.Moves);
            DrawButtons ();
        }
    }
}
