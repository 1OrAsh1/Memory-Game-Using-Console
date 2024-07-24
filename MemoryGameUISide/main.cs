using MemoryGameUISide;
using System;
using System.Collections.Generic;
using System.Text;
namespace MemoryGameUISide
{
    class Program
    {
        static void Main()
        {
            GameManager gameManager = new GameManager();
            gameManager.HandlePreGame();
            gameManager.HandleGameRun();
            Console.ReadLine();
        }
    }
}