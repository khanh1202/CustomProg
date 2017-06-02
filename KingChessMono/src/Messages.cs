///<summary>
///Messages class represents the messages and buttons displayed
/// at the right hand side of the screen
/// </summary>
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

        /// <summary>
        /// Determines the checking condition of the game and return a string
        /// </summary>
        /// <returns>A string message</returns>
        /// <param name="game">The game</param>
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

        /// <summary>
        /// Draws the checking state on the screen
        /// </summary>
        /// <param name="game">the Game</param>
        public void DrawCheckingState(ChessGame game)
        {
            SwinGame.DrawText (DetermineCheck (game), Color.Chocolate, SwinGame.FontNamed ("Chelsea"), CHECKING_STATE_X, CHECKING_STATE_Y);
        }

        /// <summary>
        /// Draws the last 10 moves of the game
        /// </summary>
        /// <param name="moves">The list of moves made</param>
        public void DrawMoves(List<Move> moves)
        {
            for (int i = 0; i < 10; i++)
                SwinGame.DrawText (i + 1 + ". ", Color.Purple, SwinGame.FontNamed ("Chelsea"), MOVES_X, FIRST_MOVE_Y + i * 30);
            if (moves.Count < 10)
                for (int i = 0; i < moves.Count; i++)
	            {
	                string toDraw = moves [i].PieceMove.Team + "   " + moves [i].ConvertPieceToString (moves[i].PieceMove) + "   " + moves [i].ConvertCellToString (moves [i].CellFrom) + "   " + moves [i].ConvertCellToString (moves [i].CellTo);
                    SwinGame.DrawText (toDraw, Color.Purple, SwinGame.FontNamed ("Chelsea"), MOVES_X + DISTANCE, FIRST_MOVE_Y + i * 30);
	            } 
            else
                for (int i = moves.Count - 10; i < moves.Count; i++) 
                {
					string toDraw = moves [i].PieceMove.Team + "   " + moves [i].ConvertPieceToString (moves [i].PieceMove) + "   " + moves [i].ConvertCellToString (moves [i].CellFrom) + "   " + moves [i].ConvertCellToString (moves [i].CellTo);
                    SwinGame.DrawText (toDraw, Color.Purple, SwinGame.FontNamed ("Chelsea"), MOVES_X + DISTANCE, FIRST_MOVE_Y + (10 + i - moves.Count) * 30);
				}


		}

        /// <summary>
        /// Draws the buttons.
        /// </summary>
        public void DrawButtons()
        {
			SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Undo_active"), 600, 350);
			SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Replay_active"), 730, 350);
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Save1"), 600, 450);
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Mainmenu"), 600, 500);
        }

        /// <summary>
        /// Draws the winner on the screen 
        /// </summary>
        /// <param name="game">Game.</param>
        public void DrawWinner(ChessGame game)
        {
            string toCheck = DetermineCheck (game);
            if (toCheck == "Black checkmated")
                SwinGame.DrawText ("White wins", Color.Crimson, SwinGame.FontNamed ("Chelsea"), 620, 400);
            else if (toCheck == "White checkmated")
                SwinGame.DrawText ("Black wins", Color.Crimson, SwinGame.FontNamed ("Chelsea"), 620, 400);
            else
                SwinGame.DrawText (game.PlayerInturn.Team + " 's turn", Color.Crimson, SwinGame.FontNamed ("Chelsea"), 620, 400);
        }

        /// <summary>
        /// Draw the messages
        /// </summary>
        /// <returns></returns>
        /// <param name="game">Chess Game</param>
        public void Draw(ChessGame game)
        {
            DrawCheckingState(game);
            DrawMoves(game.Board.Moves);
            DrawButtons ();
            DrawWinner (game);
        }
    }
}
