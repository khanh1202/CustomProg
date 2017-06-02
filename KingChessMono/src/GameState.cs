///<summary>
/// Different states of a move
/// made by the player
/// </summary>

namespace KingChess
{
    public enum GameState
    {
        /// <summary>
        /// The state when player is choosing a 
        /// piece to move
        /// </summary>
        Selecting,

        /// <summary>
        /// The state when the player has chosen
        /// a piece to move and is chosing a cell 
        /// to move the piece to
        /// </summary>
        Moving,

        /// <summary>
        /// The state when the move has been made
        /// </summary>
		Moved,

        /// <summary>
        /// The state when one team has the King 
        /// checkmated and the game is over
        /// </summary>
		Ended
    }
}
