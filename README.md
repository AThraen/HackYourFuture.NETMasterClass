# HackYourFuture.NETMasterClass
This is the base repository for the HackYourFuture masterclass in .NET & C#.
The plan is that this will contain everything that is needed to both hold and attend the course.
This includes:
* Curriculum
* Slides
* Exercises
* Solutions
* Knowledge reviews


## Curriculum .NET Masterclass - "Zero to Hero".

Each module is approx 4 hours including dinner. About 50-50 timesplit between classroom vs exercises.
As far as possible we'll work on a project that make sense to the students - and hopefully combine it all in a final product after 5 lessons.
Each class will have moderate homework.

We will be doing it in .NET Core and using Visual Studio Community - which works both on Mac and PC.

### The Project
The main project that the students will be coding throughout the class is a simple and classic card game: [Thirty-One](https://en.wikipedia.org/wiki/Thirty-one_(card_game)).
It has clear object models, and will accommodate most of the topics in the curriculum - while still being easy to understand.
Towards the end of the course, there will be plenty of extension points for extra-credit effort - where former HYF skills such as html, css, javascript and react can be used.

### Prerequisites
Students starting on the .NET master class should have some general experience with programming and GitHub, as well as good knowledge of web technologies such as HTML, CSS and Javascript - corresponding to the level achieved after completing the HYF standard curriculum. 
Each student should bring a laptop (mac or pc) with [Visual Studio Community edition](https://visualstudio.microsoft.com/vs/community/) installed and working. Note that Visual Studio is not the same as Visual Studio Code.

### First Lesson
Introduction to .NET, C#, Visual Studio and OOP.
First Lesson Goals:
* Understand the basics from object oriented programming - and how it differs from functional programming and scripting
* Understand what the .NET framework is - and how you use it.
* Strongly typed
* Namespaces and Usings
* Inheritance
* Interfaces
* Objects, methods, properties and events.
* Working with visual studio and compiling and debugging code
* Use the System.IO namespace (or similar)
* Exercises: Building a simple Console application, with the basics of the card game mentioned as "The Project".

### Second lesson
More in-depth with c# and .NET basics.
Second Lesson Goals:
* Static vs Instance
* Public vs Private vs Protected vs Internal
* Generics and lists
* LINQ
* Exceptions
* Typical OOP patterns
* Class libraries and references
* Nuget packages
* Use Restsharp or SharpZipLib or similar package.
* Exercises & homework : Extract game logic from the earlier developed Console App into a separate library and rewrite the console to use that. Extend game logic with persistence (Json files).

### Third lesson
MVC and Web introduction.
Third Lesson Goals:
* Extension methods, anonymous objects and Lambda methods
* Understand the Model-View-Controller approach to web development
* Learn how Razor and Models work together.
* Understand the basics of routing and how controllers get called.
* Understand how you can map a form to a model and handle submissions.
* Examine and adjust Web.Config
* Debug and explore the Request and Response objects
* Exercises & homework: Build a basic web-version (ASP.NET Core MVC) of the project made in Lesson 1 & 2 - ideally using the same class library.

### Fourth lesson
Using Azure services in your application and hosing it on Azure.
Fourth Lesson Goals:
* Follow-up on previous 3 lessons
* Examine the Azure platform and what it provides.
* Experiment with Cognitive Services as well as Cosmos DB (or other data storage)
* Learn how to publish a web application to Azure
* Introduction to Azure Functions.
* Exercises & homework: Polish the functionality and look and feel of the web version of the game. Persist game state in Azure. Publish game to Azure App Services.

### Fifth Lesson
Putting it all together and finishing the main project application
Fifth Lesson Goals:
* Finalize the project we have been using throughout the previous lessons. 
* Presenting your work to the others
* Improve the UI with React / JS and better HTML+CSS
* Discussing build pipelines and unit testing
* Looking at a real-life web project
* Exercises: Extra credit work
