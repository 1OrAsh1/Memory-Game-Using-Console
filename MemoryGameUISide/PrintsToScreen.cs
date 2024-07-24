using System;
using MemoryGameLogic;
namespace MemoryGameUISide
{
    public class PrintsToScreen
    {
        public void EnterNameRequest()
        {
            Console.WriteLine("Hello Player!\nPlease enter your name");
        }
        public void EnterPlayerTwoRequest()
        {
            Console.WriteLine("Hello Player 2!\nPlease enter your name");
        }
        public void RequestBoardSize()
        {
            Console.WriteLine("Enter desired X dimension, enter and Y dimension. (X multiplied by Y must be an Even number)");
        }
        public void InvalidInput()
        {
            Console.WriteLine("Invalid Input");
        }
        public void OneOrTwoPlayersMessage(Player playerToPrint)
        {
            Console.WriteLine($"Welcome {playerToPrint.Name}! Write 0 to play vs the computer or 1 to play vs another player");
        }
        public void PrintPlayerPoints(Player player1, Player player2)
        {
            Console.WriteLine($"{player1.Name} points: {player1.Points}");
            Console.WriteLine($"{player2.Name} points: {player2.Points}");
        }
        public void OutOfBoundsError()
        {
            Console.WriteLine("The selected square is out of the board size. Please choose a valid square.");
        }

        public void SquareAlreadyShown()
        {
            Console.WriteLine("The selected square has already been shown. Please choose another square.");
        }
    }
}