///<summary>
/// The ViewingScreen represents the screen  
/// the user is at depending on how they react to 
/// the game
/// </summary>

namespace KingChess
{
    public enum ViewingScreen
    {
        /// <summary>
        /// The user seeing the mainmeu
        /// </summary>
        MENUSCREEN,

        /// <summary>
        /// The user seeing the game screen
        /// </summary>
        NEWGAMESCREEN,

        /// <summary>
        /// The user seeing the game screen but 
        /// with the Computer
        /// </summary>
        GAMEVSAISCREEN,

        /// <summary>
        /// The user is seeing a game screen which
        /// is loaded from file
        /// </summary>
        LOADGAMESCREEN,

        /// <summary>
        /// The user is at quitting the game
        /// </summary>
        QUITTING
    }
}
