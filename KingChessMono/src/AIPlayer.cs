using System;
using System.Collections.Generic;
using SwinGameSDK;
namespace KingChess
{
    public class AIPlayer : Player
    {
        public AIPlayer(TeamColor color) : base(color)
        {
        }

        public override void TakeTurn(Point2D point, ChessGame game)
        {
            Move bestMove = BestMove (3, game, this);
            game.Board.Move (bestMove);
            game.ChangeTurn ();
        }

    }
}
