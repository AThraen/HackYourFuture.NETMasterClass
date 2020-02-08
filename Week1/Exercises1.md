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

