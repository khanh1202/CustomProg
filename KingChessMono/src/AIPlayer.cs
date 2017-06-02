///<summary>
/// AIPlayer represents the AI player
/// in the chess game
/// </summary>
using SwinGameSDK;
namespace KingChess
{
    public class AIPlayer : Player
    {
        public AIPlayer(TeamColor color) : base(color)
        {
        }

        /// <summary>
        /// Takes the turn.
        /// </summary>
        /// <param name="point">MousePosition</param>
        /// <param name="game">the game</param>
        public override void TakeTurn(Point2D point, ChessGame game)
        {
            Move bestMove = BestMove (3, game, this);
            game.Board.Move (bestMove);
            game.ChangeTurn ();
        }

    }
}
