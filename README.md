# Yatzy-Kata
# Testing Approach & Design Approach:

Going by Tdd, and more of a real object tester, I tried to avoid mocks as much as possible.
this is due to me not wanting to create and delete a lot of interfaces and I refused to mock classes with implementations, as 
the issues that would derive from a poorly written mock is not worth the hassle to me. That being said to start top down, I did create a fair bit of interfaces that i knew i would delete later just to test the behaviour of my Highest classes.
Therefore when I could Justify the upper level of my program, I needed to get rid of these redundant interfaces and replace them with real objects. This caused a refactor hell, although I did refactor as I went.
The main benefit of testing this way seemed like a great way to design as you go. Also generally I like to Encapsulate my Program as much as possible where anything That was not going to be used outside of the class it belonged to would be Private.
Therefore attempting to test these private methods and properties within the only few public methods was a new concept to me. 

For Design Approach I decided to Utilize a Factory design pattern for the Logic of the Yatzy-Game itself, where rounds and turns are created by Factories to abstract the object creation process away from the client code. Also I wanted that high-level modules should not depend on low-level modules, but rather both should depend on abstractions.
For Domain Logic, I decided to encapsulate most data manipulating logic within the player,
this way The Game logic classes do not need to know what the Player is actually doing but just fork out rounds and turns.
I think I designed this poorly and maybe should of did the reverse. 




# Self served Learning Outcomes

### YAGNI - Level 3 
Can identify in code when YAGNI has been violated.

### Command Query Separation - Level 4
Can identify and provide examples of command methods and query methods in code. Can identify in code methods that violate command query separation.

### Removing Duplication - Level 4
Able to articulate in depth the difference between domain duplication and code duplication. Consistently identifies and removes domain duplication. Consistently leaves code duplication in code.

### Revealing Intent - Level 4

Classes, methods and variable names reference mainly domain concepts with occasional technical concepts. Comments are very rarely used.





