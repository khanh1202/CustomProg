///<summary>
/// ChessGame represents the game rules and
/// game flow
/// </summary>

using System;
using System.IO;
using SwinGameSDK;
namespace KingChess
{
	public class ChessGame
	{
		public const int MARGIN_LEFT = 39;
		public const int MARGIN_TOP = 39;
		public const int MARGIN_RIGHT = 39;
		public const int MARGIN_BOTTOM = 42;
		public const int CELL_WIDTH = 58;
		private Player[] _players = new Player[2];
        private Board gameBoard;
		private int _playerInTurnIndex;
		private int _playerwaitingIndex;
        private Piece _chosenPiece;
        private GameState _state;
        private Messages _message = new Messages();
        private bool _isAI;

        public ChessGame(bool AI)
		{
            if (AI)
                _players [0] = new AIPlayer (TeamColor.Black);
            else
                _players [0] = new Player (TeamColor.Black);
            _isAI = AI;
			_players[1] = new Player(TeamColor.White);
			_players[0].Opponent = _players[1];
			_players [1].Opponent = _players [0];
            gameBoard = new Board ();
            _players [0].Board = gameBoard;
            _players [1].Board = gameBoard;
            _state = GameState.Selecting;
            Piece.ClearPieceRegistry ();
			Piece.RegisterPiece ("Pawn", typeof (Pawn));
			Piece.RegisterPiece ("Rook", typeof (Rook));
			Piece.RegisterPiece ("Knight", typeof (Knight));
			Piece.RegisterPiece ("Bishop", typeof (Bishop));
			Piece.RegisterPiece ("Queen", typeof (Queen));
			Piece.RegisterPiece ("King", typeof (King));
		}

        /// <summary>
        /// Draw the game
        /// </summary>
        public void Draw()
        {
            DrawBackGround ();
            gameBoard.Draw ();
            _players [0].DrawPieces ();
            _players [1].DrawPieces ();
            _message.Draw (this);
        }

        /// <summary>
        /// Draws the back ground of the game
        /// </summary>
        public void DrawBackGround()
        {
            SwinGame.DrawBitmap (SwinGame.BitmapNamed ("Background1"), 0, 0);
        }

        /// <summary>
        /// Gets the player inturn.
        /// </summary>
        /// <value>The player inturn.</value>
        public Player PlayerInturn
        {
            get
            {
                return _players [_playerInTurnIndex];
            }
        }

        /// <summary>
        /// Gets the array of players in the game
        /// </summary>
        /// <value>The players.</value>
		public Player[] Players
		{
			get
			{
				return _players;
			}
		}

        /// <summary>
        /// Gets the game board
        /// </summary>
        /// <value>The board.</value>
        public Board Board
        {
            get
            {
                return gameBoard;
            }
        }

        /// <summary>
        /// Gets the state of the game
        /// </summary>
        /// <value>The state.</value>
        public GameState State
        {
            get
            {
                return _state;
            }
        }

        /// <summary>
        /// Gets the chosen piece.
        /// </summary>
        /// <value>The chosen piece.</value>
        public Piece ChosenPiece
        {
            get
            {
                return _chosenPiece;
            }
        }

        /// <summary>
        /// Gets the value of whether the opponent is human or AI
        /// </summary>
        /// <value><c>true</c> if the opponent is AI; otherwise, <c>false</c>.</value>
        public bool isAI
        {
            get
            {
                return _isAI;
            }
        }

        /// <summary>
        /// Choose a piece to move
        /// </summary>
        /// <param name="p">the piece player want to move</param>
        public void SetChosenPiece(Piece p)
        {
            _chosenPiece = p;
        }

        /// <summary>
        /// Sets up game.
        /// </summary>
		public void SetUpGame()
		{
            _players [0].SetupPlayer ();
            _players [1].SetupPlayer ();
			_playerInTurnIndex = 1;
            _playerwaitingIndex = 0;
		}

        /// <summary>
        /// Releases the game.
        /// </summary>
        public void ReleaseGame()
        {
            _players [0].ReleasePiece ();
            _players [1].ReleasePiece ();
            gameBoard.ReleaseMove ();
        }

        /// <summary>
        /// Loads the game from file
        /// </summary>
        /// <param name="filename">Filename.</param>
        public void LoadGame(string filename)
        {
            ReleaseGame ();
            StreamReader reader = new StreamReader (filename);
            reader.ReadLine ();
            string gamestate = reader.ReadLine ();
                _state = (GameState)Enum.Parse (typeof(GameState), gamestate);
            string playertomove = reader.ReadLine ();
            if (playertomove == "White")
                _playerInTurnIndex = 1;
            else
                _playerInTurnIndex = 0;
            _playerwaitingIndex = (_playerInTurnIndex + 1) % 2;
            int numOfPieces = Convert.ToInt32 (reader.ReadLine ());
            for (int i = 0; i < numOfPieces; i++)
                Piece.Load (reader, _players);
            reader.Close ();
        }

        /// <summary>
        /// Returns the Cell that player chooses by clicking the mouse
        /// </summary>
        /// <returns>The chosen cell</returns>
        /// <param name="point">Mouse Position</param>
        public Cell FetchCell(Point2D point)
        {
            float pointRelativeY = point.Y - MARGIN_TOP;
            float pointRelativeX = point.X - MARGIN_LEFT;
            int row = 7 - (int)Math.Floor (pointRelativeY / CELL_WIDTH);
            int col = (int)Math.Floor (pointRelativeX / CELL_WIDTH);
            if (row >= 0 && row < 8 && col >= 0 && col < 8)
                return gameBoard.Cells [col, row];
            return null;
        }

		/// <summary>
        /// Handles the reverse move.
        /// </summary>
        /// <param name="point">Mouse Position</param>
		public void HandleReverseMove(Point2D point)
        {
            if (SwinGame.PointInRect (point, 600, 350, 30, 30))
            {
                if (_isAI)
                {
                    gameBoard.ReverseMove (this, false);
                    gameBoard.ReverseMove (this, false);
                }
                else 
                    gameBoard.ReverseMove (this, true);
                if (_state == GameState.Ended)
                    _state = GameState.Selecting;
			}
        }

        /// <summary>
        /// Handles the replay.
        /// </summary>
        /// <param name="point">Mouse Position</param>
        public void HandleReplay(Point2D point)
        {
            int count = gameBoard.Moves.Count;
            if (SwinGame.PointInRect (point, 730, 350, 30, 30))
            {
				for (int i = 0; i < count; i++)
					gameBoard.ReverseMove (this, true);
                _state = GameState.Selecting;
            }
        }

        /// <summary>
        /// Handles saving game to file
        /// </summary>
        /// <param name="point">Mouse Position</param>
        public void HandleSaving(Point2D point)
        {
            if (SwinGame.PointInRect (point, 600, 450, 163, 50))
                gameBoard.Save ("Users/mac/Desktop/chess.txt", this);      
        }

        /// <summary>
        /// Handles the loading.
        /// </summary>
		public void HandleLoading ()
		{
             LoadGame ("/Users/mac/Desktop/chess.txt");			
		}

        /// <summary>
        /// Control the players to take turn
        /// </summary>
        /// <param name="point">Point.</param>
        public void TakeTheTurn(Point2D point)
        {
            _players [_playerInTurnIndex].TakeTurn (point, this);

        }

        /// <summary>
        /// Handles if the player clicks on Main menu
        /// </summary>
        /// <param name="point">MousePosition</param>
        /// <param name="screen">the screen</param>
        public void HandleBackScreen(Point2D point, Screen screen)
        {
            if (SwinGame.PointInRect (point, 600, 500, 163, 50))
            {
                screen.ChangeScreenViewing (ViewingScreen.MENUSCREEN);
            }
        }

        /// <summary>
        /// Changes the turn of players
        /// </summary>
        public void ChangeTurn()
        {
            _playerInTurnIndex = _playerwaitingIndex;
            _playerwaitingIndex = (_playerInTurnIndex + 1) % 2;
        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="state">The State to change to</param>
        public void ChangeState(GameState state)
        {
            _state = state;
        }

        /// <summary>
        /// Update the game
        /// </summary>
		public void Update ()
		{
            //if the king is taken
            if (_players [_playerInTurnIndex].King == null)
                _state = GameState.Ended;
            else
            {
				if (_state == GameState.Moved) 
                {
                    //if the King of one player is being checked
					if (_players [_playerInTurnIndex].King.isChecked (_players [_playerwaitingIndex], gameBoard)) 
                    {
                        ///if his King is checkmated
						if (_players [_playerInTurnIndex].King.isCheckmated (_players [_playerwaitingIndex], gameBoard))
							_state = GameState.Ended;
						else
							_state = GameState.Selecting;
					} 
                    else
						_state = GameState.Selecting;
				}
            }
		}
	}
}
