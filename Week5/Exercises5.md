# Exercises for Week #4

The overall goal for week 4 is to extend the Web version of the game from Week3 to be a fully functioning single-player (+ computer player) game. But, we will start by preparing our GameService for a future Azure deployment.

### Preparing GameService for Dependency Injection
You might recall the GameService? The class responsible for saving, loading and checking the game state to files.
But as we discussed last week, in the future, it might change later how/where we will store this state.
So, the first thing we will do in these exercises is to prepare it to easily be replaced, using a [design pattern](https://en.wikipedia.org/wiki/Software_design_pattern) called [Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection). Relax, it's not as complicated as it sounds.
Right now, the GameController in it's constructor creates a new GameService object that it uses. Instead we want it to take a parameter indicating which kind of GameService it should use. Maybe in the future we'll introduce a SQLGameService or an AzureGameService. As long as we make sure they all have the same methods, it shouldn't really matter to the GameController.
So - first of all, we'll create an interface defining those methods, called *IGameService*.
You can of course create and write the interface yourself, but you could also ask Visual Studio to create it for you.
You do this by right-clicking on the class name, *GameService* and selecting *Quick actions and refactorings*.
!()[Refactorings.PNG]
Then, you simply select *Extract interface* and pick which methods the interface should specify.
!()[ExtractInterface.PNG]

Luckily, ASP.NET Core supports Dependency Injection out of the box. That means, that it maintains a list of registered *services* it should use when specific objects are required in constructor parameters.
So, instead of creating a *new GameService()* in the GameController, we simply need to take an IGameService as an input parameter put that in our private field (which should of course also should be changed to IGameService).
!()[using-igameservice.PNG]

Last thing we need to do, for this to work is to register the service in the StartUp.cs class, *ConfigureServices* method, like this:
```csharp
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IGameService, GameService>();
        }
```

### Adding PlayerActions in the Game Index View
We left off last week at the point where we could start a new game and show the basic game state. The biggest thing we have to deal with now is to let the user perform the actions a player in the game can typically perform when it is their turn:
- Draw a card from the Deck
- Draw a card from the Table
- Knock (call)
To make a nice and easy way to transmit the players actions, we'll introduce an Enum with those 3 states.

Try to make it yourself - but if you get lost, you can check against the sample code [here](Solution/ThirtyOne/ThirtyOne.Web/Models/PlayerAction.cs).

### GameController - MakeAMove
When a player makes a move (one of the above mentioned actions), we will need an action method in the Controller to handle it.
The Action method should take the Id of the game as input, as well as the move (PlayerAction) made by the player.
This is what can should happen:
1. The player knocks. Mark the player as .HasKnocked=true. Call *NextTurn()* on the game object to complete the turn, and let the computer player take it's turn. Then move straight to the GameOver view.
2. The player draws a card (either from the deck or the hand). Let the player draw from the appropiate place, save the game and redirect to the Index view.

Try to write this code yourself, from the logic outlined above. If you get into problems, the sample code is [here](Solution/ThirtyOne/ThirtyOne.Web/Controllers/GameController.cs).

### GameController - Handle drop of card, complete move.
To complete the players move (in the cases where they drew a card), they also need to pick a card to drop. We will update the Index view to take of this shortly, but before we do that, we should also have an Action method on the Game controller that can drop a card from the player hand and end the turn.
It should take 2 parameters: Game ID and Card (Index of card on the players hand, typically 0-3).
This is what it should do:
1. Load the game from the game id
2. Drop the card onto the table
3. Complete the turn by executing the *NextTurn* method on the Game object.
4. If the game is over, return the GameOver view we will create next
5. Otherwise, save the new game state and redirect to the Index method.

Try to write this code yourself, from the logic outlined above. If you get into problems, the sample code is [here](Solution/ThirtyOne/ThirtyOne.Web/Controllers/GameController.cs).

### GameController - Game Over
Both the two new methods introduced above can in GameOver situations need to return the GameOver view. To make it easy, we can add a method that takes care of the GameOver situation and returns the GameOver view.
It needs to take the current Game as an input parameter, clean up (delete) the game file and return a GameOver view with the Game model as it's view model.

```csharp
        private IActionResult GameOver(Game g)
        {
            _gameService.DeleteGame(g.GameId);
            return View("GameOver",g);
        }
```


### GameController - Extra credit work
Before we leave the GameController to focus on the views, here are a few optional extra-credit tasks:
1. Update the *LastAction* property on the WebPlayer with the actions they perform, so we can output it in the game. For example "knocked", "drew card from table, dropped 2 of clubs" and so on.
2. Introduce some exception handling. For example if the Index method is called with a game ID that doesn't exist, redirect to the Home/Index view instead of throwing an ugly exception.

Try to write this code yourself, from the logic outlined above. If you get into problems, the sample code is [here](Solution/ThirtyOne/ThirtyOne.Web/Controllers/GameController.cs).

### Index View update
Now, let's update the views. We will start with the main Game Index view.

**First**, we should handle that the player can now be in 2 states. Either he is beginning his turn (he only has 3 cards on hand) or he is half-way through his turn and should decide on a card to drop (he has 4 cards on his hand). We should only show the Table and it's actions in the first case. And only allow him to choose a card to drop in the second.
We can use a basic if statement wrapped around the relevant areas to achieve this.
```@if (Model.CurrentPlayer.Hand.Count <= 3){...}```

**Secondly**, we should wrap the possible actions (cards or knock icon) in <A> tags with an HREF that points to the appropiate action method, and passes along the proper information. This goes both for the actions to draw card / knock, and for those when you need to drop a card. Like this:
  ```
  <a href="@Url.Action("MakeAMove","Game",new {Move=PlayerAction.DrawFromDeck, Id=Model.CurrentGame.GameId })">
    <img src="~/images/Cards/back.png" class="playingcard selectablecard" />
  </a>
  ```
The ```@Url.Action([action],[controller],[method parameters])``` extension method allows you do have the system build the proper url.

Once again, try to update the view yourself, but if you get into trouble, the sample solution is [here](Solution/ThirtyOne/ThirtyOne.Web/Views/Game/Index.cshtml).


### View - Game Over
We also need to add a new view in the Game folder, called *GameOver.cshtml*. This is the view that is called with the Game model when the game is over, and here you should show the winnner, and perhaps show both players in descending points order - as well as their cards.
Remember to use the correct namespaces in the top of the view (to use the FileName() and CalculateScore() extension methods):
```
@using ThirtyOne.Shared.Helpers
@using ThirtyOne.Web.Helpers
@model ThirtyOne.Shared.Models.Game
```

And the rest should be smooth sailing. You can use LINQ to order the players by descending point score: 
```
@foreach (var p in Model.Players.OrderByDescending(p => p.Hand.CalculateScore()))
    {
    ...
    }
```

If you struggle with it, the sample solution is [here](Solution/ThirtyOne/ThirtyOne.Web/Views/Game/GameOver.cshtml).



