using System;
using System.Collections.Generic;
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

        private readonly GameService _gameService;

        public GameController()
        {
            _gameService = new GameService();
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

            return RedirectToAction("Index", new { Id=g.GameId.ToString()});
        }

        public IActionResult Index(int Id)
        {
            Game g = _gameService.LoadGame(Id);

            WebPlayer human = g.Players.First() as WebPlayer;

            GameViewModel viewModel = new GameViewModel() { CurrentGame = g, CurrentPlayer = human };

            return View(viewModel);
        }
    }
}