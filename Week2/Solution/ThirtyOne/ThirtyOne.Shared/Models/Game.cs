using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyOne.Shared.Helpers;

namespace ThirtyOne.Shared.Models
{
    public class Game
    {
        /// <summary>
        /// Deck of cards
        /// </summary>
        public Deck Deck { get; set; }

        /// <summary>
        /// Cards on the table
        /// </summary>
        public List<Card> Table { get; set; }

        /// <summary>
        /// List of players
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// The index of the player that currently has the turn
        /// </summary>
        public int CurrentTurn { get; set; }

        /// <summary>
        /// Current game state
        /// </summary>
        public GameState State { get; set; }

        /// <summary>
        /// Random generator
        /// </summary>
        private Random _random;

        /// <summary>
        /// The Winner
        /// </summary>
        public Player Winner { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Game()
        {
            State = GameState.WaitingToStart;
            _random = new Random();
            Players = new List<Player>();
        }

        /// <summary>
        /// Constructor that starts game right away
        /// </summary>
        /// <param name="randomNumberGenerator"></param>
        /// <param name="players"></param>
        public Game(Random randomNumberGenerator, params Player[] players) : this()
        {
            Players = players.ToList();
            StartGame();
        }

        [JsonIgnore]
        public Player CurrentPlayer
        {
            get { return Players[CurrentTurn]; }
        }

        /// <summary>
        /// Evaluates if the game is over - and if so, sets the correct state and winner
        /// </summary>
        /// <param name="called">Has the game been called?</param>
        /// <returns>returns true if game over, otherwise false</returns>
        public bool EvaluateIfGameOver(bool called)
        {
            var winPlayer = (called)
                ? Players.Where(p => p.Hand.Count == 3).OrderByDescending(p => p.Hand.CalculateScore())
                    .First() //The game has been called, highest score is the winner
                : Players.FirstOrDefault(p => p.Hand.Count == 3 && p.Hand.CalculateScore() == 31); //Game has not been called, but a player has 31 and wins.

            if (winPlayer != null)
            {
                Winner = winPlayer;
                State = GameState.GameOver;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Completes a turn and moves on to the next. Also evaluates if game is over.
        /// </summary>
        /// <returns>true if gameover, otherwise false</returns>
        public bool NextTurn()
        {
            //Ask player to do their turn
            CurrentPlayer.Turn(this);

            if (EvaluateIfGameOver(false))
            {
                //Player won, report
                return true;
            }

            //Move to the next player
            CurrentTurn++;

            if (CurrentTurn >= Players.Count)
            {
                CurrentTurn = 0;
            }
            
            if (CurrentPlayer.HasKnocked)
            {
                //Next player had already knocked - let's evaluate the call
                EvaluateIfGameOver(true);
                //Game over!
                return true;
            }

            if (Deck.CardsLeft == 0)
            {
                //If there's no more cards in the deck, let's take those from the table
                Deck.Cards.AddRange(Table);
                Table.Clear();
            }

            if (CurrentPlayer is ComputerPlayer)
            {
                return NextTurn(); //If the next player is the computer, execute that turn right away.
            }
            
            return false;
        }

        /// <summary>
        /// Deals initial cards to players
        /// </summary>
        private void InitialDeal()
        {
            //Deal
            foreach (var p in Players)
            {
                for (int i = 0; i < 3; i++) p.Hand.Add(Deck.DrawCard());
            }

            Table.Add(Deck.DrawCard());
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            Deck = new Deck();
            Table = new List<Card>();
            
            Deck.Initialize();
            Deck.Shuffle(_random);
            Winner = null;
            CurrentTurn = 0;
            InitialDeal();
            State = GameState.InProgress;
        }


        #region Serialization

        /// <summary>
        /// Serializes game into Json
        /// </summary>
        /// <returns>a json string</returns>
        public string SerializeGame()
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            return JsonConvert.SerializeObject(this, jsonSerializerSettings);
        }

        /// <summary>
        /// Deserializes game from Json
        /// </summary>
        /// <param name="json">Serialized game</param>
        /// <returns>Game</returns>
        public static Game DeserializeGame(string json)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            return JsonConvert.DeserializeObject<Game>(json, jsonSerializerSettings);
        }


        #endregion

    }
}