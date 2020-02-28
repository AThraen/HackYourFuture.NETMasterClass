using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThirtyOne.Shared.Models;
using ThirtyOne.Web.Helpers;
using ThirtyOne.Web.Models;

namespace ThirtyOne.Web.Controllers
{
    public class GameController : Controller
    {

        private readonly IGameService _gameService;

        public GameController(IGameService gs)
        {
            _gameService = gs;
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IActionResult New(string Name)
        {
            Game g = new Game();

            Random r = new Random();
            g.GameId = r.Next(1000, 9999);
            while (_gameService.GameExist(g.GameId)) //Check if the ID is already in use
                g.GameId = r.Next(1000, 9999);

            WebPlayer human = new WebPlayer(Name);
            g.Players.Add(human);
            ComputerPlayer computer = new ComputerPlayer("Computer");
            g.Players.Add(computer);

            g.StartGame();

            _gameService.SaveGame(g);

            return RedirectToAction("Index", new { Id=g.GameId});
        }

        public IActionResult Index(int Id)
        {
            try
            {
                Game g = _gameService.LoadGame(Id);

                WebPlayer human = g.Players.First() as WebPlayer;

                GameViewModel viewModel = new GameViewModel() { CurrentGame = g, CurrentPlayer = human };

                return View(viewModel);
            } catch(FileNotFoundException exc)
            {
                //Game does not exist, send back to start.
                return Redirect("/");
            }
            catch 
            {
                throw;
            }
        }

        private IActionResult GameOver(Game g)
        {
            _gameService.DeleteGame(g.GameId);
            return View("GameOver",g);
        }

        public IActionResult MakeAMove(int Id,PlayerAction Move)
        {
            Game g = _gameService.LoadGame(Id);
            WebPlayer human = g.CurrentPlayer as WebPlayer;

            switch (Move)
            {
                case PlayerAction.Knock:
                    human.HasKnocked = true;
                    human.LastAction = "knocked";
                    g.NextTurn(); //Computers turn, then game over
                    return GameOver(g);
                case PlayerAction.DrawFromDeck:
                    human.DrawFromDeck(g);
                    human.LastAction = "drew from deck";
                    break;
                case PlayerAction.DrawFromTable:
                    human.DrawFromTable(g);
                    human.LastAction = "drew from table";
                    break;
            }
            _gameService.SaveGame(g);
            return RedirectToAction("Index", new { Id = g.GameId } );
        }

        public IActionResult DropCard(int Id, int Card)
        {
            Game g = _gameService.LoadGame(Id);
            WebPlayer human = g.CurrentPlayer as WebPlayer;
            human.LastAction += " then dropped " + human.Hand[Card].ToString();
            human.DropCard(g, Card);
            
            if (g.NextTurn()) return GameOver(g);

            //Save game state
            _gameService.SaveGame(g);

            return RedirectToAction("Index", new { Id = g.GameId });
        }
    }
}