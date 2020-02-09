# Exercises for Week #1

### Creating the project
First we need to create the project and solution in Visual Studio. A solution is a group of multiple projects that (can) work together.
Select "Create New Project" in Visual Studio and then "Console App (.NET Core)". Make sure to select the C# version of it.

![](NewProject.png)

Name your project (and solution) ThirtyOne and select a location to store it on disk (it will create solution folders and project folders itself).

Click 'next' and you are ready to get started.
Congratulations, you now have your very own Console Project  and you can even run it and see a nice "Hello World" text.

### Creating the first model: The Card
Now, that we have the project created, we should start to build our world by addings models - that is class definitions of the objects we will use.
It's a good idea to *create these in a new folder called 'Models'*. You can create this in the solution explorer, under your project root.

The first model we will build is the basic Card, that represents a playing card.
So, we'll construct a new public class called "Card". Add a new class to the solution explorer.
The Class is what defines an object. And objects are instantiated from their class.

It will belong to one of 4 suits: Spades, Diamonds, Clubs or Hearts. It will have a face value between 1 (Ace) to 13 (King).
For these, we'll define public properties.

```csharp
public class Card
{
    public Suits Suit { get; set; }

    public int Rank { get; set; } //1-13

    public int Value
    {
        get
        {
            return (Rank == 1) ? 11 : 
                (Rank >= 10 && Rank < 14) ? 10 : Rank;
        }
    }	
}
```
Notice how the Suit and the Rank are regular get/set properties. They simply expose a *hidden* member field that probably looks like `private int Rank;` 
- it's generated on the fly by the compiler. However, the Value is calculated automatically based on the Rank whenever it is retrieved.

Instead of using an integer to keep track of which suit it is (and have a secret knowledge that 0 is Spades, 1 is Hearts and so on, it's makes for more readable code to create an Enum. 
An enum is a definition of multiple options for a field and can then later be used as a type.
We will declare that in another file, so go ahead and create a new file for the enum. Let's call it Suits.cs. In that file you simply define the options in your enum.


```csharp
public enum Suits
{
    Spades,
    Hearts,
    Clubs,
    Diamonds
}
```

We can then use this enum as a type on each card: 

```csharp
public Suits Suit { get; set; }
```

### The Deck
We will need a deck, to represent the source of cards. The Deck is the container we'll create the cards in initially. It's also what we'll shuffle and draw from.

As an essential property of the Deck is the List of cards.
A list is essentially an array, that we can easily add or remove items to/from.
To get a list particularly suited for Cards, we will use what's called Generics - a way to induce an object prepared for it, with other types.

**Properties:**
*)List<Card> Cards, the list of cards. 
*)int CardsLeft, a get property with a number of how many cards are left

**Methods:**
*) Deck(), Constructor, Purpose is to ensure the members are initialized and ready for use.
*) void Initialize(), Prepares the deck and creates the cards
*) void Shuffle(), Shuffles the deck by taking each card position and moving it to a random new position.
*) Card DrawCard(), Draws a card from the top of the deck and returns it.

You can find sample code [here](Solution/ThirtyOne/ThirtyOne/Models/Deck.cs).

### Trying out the code we have so far ###
This might be a good time to try to compile and run your code.
Since this is a console application, it will by default look for the ```static void Main(string[] args)``` method in the "Program" class and try to run that.
So, let's put some code in there to create a deck, initialize it, shuffle it and draw a card from it:

```csharp
    //Initial test
    Deck d = new Deck();
    d.Initialize();
    Random r = new Random();
    d.Shuffle(r);
    Card c = d.DrawCard();
```
Try to put a breakpoint at the first code line (where the Deck is created). You put a breakpoint by clicking in the margin next to it.
Now, press Run in visual studio and you should see the program stop at that line. You can mouse over the objects to see what they contain.
Try to step through it, line by line and see how the Deck changes.

![](BreakPoint.png)

### Card List Extensions
The players are going to hold lists of cards (for the hand). It would, however, be very useful if we could add a few additional methods to a List<Card>.
One approach we could take is to create a new class - a CardList and let that inherit List<Card> - but I suggest we try out a different approach: Extension methods.
Extention methods are really useful, as they let you extend already existing classes - even classes you can't edit or inherit - with your own helper methods.

The trick is simply to create a static class (meaning a class that cannot be instantiated, but is a static object in itself) and in that put static methods.
The first parameter in these methods should then be the type that you want to extend, preceeded with the keyword "this".

I suggest creating a 'Helpers' folder in your solution and placing the extension methods there - I call them CardListExtensions.

The first extension method is very important - it's a method that calculates the Thirty-One score for a list of cards. The second will simply let us output a list of cards in an easy way.

```csharp
    public static class CardListExtensions
    {

        /// <summary>
        /// Calculate score for a list of cards
        /// </summary>
        /// <param name="Cards"></param>
        /// <returns></returns>
        public static int CalculateScore(this IEnumerable<Card> Cards)
        {
            return Cards
                .GroupBy(c => c.Suit)
                .OrderByDescending(grp => grp.Sum(c => c.Value))
                .First()
                .Sum(c => c.Value);
        }

        /// <summary>
        /// Create a string list of all cards
        /// </summary>
        /// <param name="Cards"></param>
        /// <returns></returns>
        public static string ToListString(this IEnumerable<Card> Cards)
        {
            return string.Join(",", Cards);
        }
    }

```
The score is calculated by using System.LINQ - a useful toolset, that in itself is a collection of extension methods.
Essentially this code will take a list (or enumeration) of cards, group them by their Suit, order the groups descending by their summarized Thirty-One point score, take the first (with the highest score) and return that.

LINQ is a fairly advanced topic, and we will get back to it later. 
If you want to learn more about LINQ now, you can get an in-depth course [here](https://www.codingame.com/playgrounds/213/using-c-linq---a-practical-overview/welcome).

### The Game

```csharp
    public enum GameState
    {
        WaitingToStart,
        InProgress,
        GameOver
    }
```

See sample code [here](Solution/ThirtyOne/ThirtyOne/Models/Game.cs).

### The Base Player

```csharp
    public enum PlayerAction
    {
        TakeFromDeck,
        TakeFromTable,
        Knock
    }
```

See sample code [here](Solution/ThirtyOne/ThirtyOne/Models/Player.cs).

### The Computer Player

See sample code [here](Solution/ThirtyOne/ThirtyOne/Models/ComputerPlayer.cs).

### The Console Player

See sample code [here](Solution/ThirtyOne/ThirtyOne/Models/ConsolePlayer.cs).

### Putting it all together and trying the game

See sample code [here](Solution/ThirtyOne/ThirtyOne/Program.cs).






