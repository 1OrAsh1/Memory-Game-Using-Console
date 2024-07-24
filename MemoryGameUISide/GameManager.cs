using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MemoryGameLogic;
namespace MemoryGameUISide
{
    class GameManager
    {
        public enum playSoloOrWithOtherPlayer { playSolo = 0, playWithOtherPlayer = 1 }
        private Player m_player1, m_player2;
        private playSoloOrWithOtherPlayer m_soloOrWithOtherPlayer;
        public playSoloOrWithOtherPlayer sGsoloOrWithOtherPlayer
        {
            get { return m_soloOrWithOtherPlayer; }
            set { m_soloOrWithOtherPlayer = value; }
        }
        private Board gameBoard;
        private InputChecker inputChecker;
        private PrintsToScreen printer;
        public GameManager()
        {
            inputChecker = new InputChecker();
            printer = new PrintsToScreen();
        }
        public void HandlePreGame()
        {
            GetPlayerNames();
            GetGameMode();
            GetBoardSize();
        }
        private void GetPlayerNames()
        {
            printer.EnterNameRequest();
            this.m_player1 = new Player(Console.ReadLine());

            if (m_soloOrWithOtherPlayer == playSoloOrWithOtherPlayer.playWithOtherPlayer)
            {
                printer.EnterPlayerTwoRequest();
                this.m_player2 = new Player(Console.ReadLine());
            }
            else
            {
                this.m_player2 = new Player("Computer");
            }

        }
        private void GetGameMode()
        {
            bool validInput = false;
            while (!validInput)
            {
                printer.OneOrTwoPlayersMessage(m_player1);
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    validInput = inputChecker.CheckValidSoloOrDuo(choice);
                    if (validInput)
                    {
                        m_soloOrWithOtherPlayer = (playSoloOrWithOtherPlayer)choice;
                    }
                    else
                    {
                        printer.InvalidInput();
                    }

                }
                else
                {
                    printer.InvalidInput();
                }

            }

        }
        private void GetBoardSize()
        {
            bool validInput = false;
            while (!validInput)
            {
                printer.RequestBoardSize();
                if (int.TryParse(Console.ReadLine(), out int boardSizeX) &&
                    int.TryParse(Console.ReadLine(), out int boardSizeY))
                {
                    if (inputChecker.CheckIfBoardSizeIsLegal(boardSizeX, boardSizeY))
                    {
                        validInput = true;
                        this.gameBoard = new Board(boardSizeX, boardSizeY);
                    }
                    else
                    {
                        printer.InvalidInput();
                    }

                }
                else
                {
                    printer.InvalidInput();
                }

            }

        }
        public void HandleGameRun()
        {
            DetermineFirstTurn();
            while (true)
            {
                gameBoard.PrintBoardToScreen();
                printer.PrintPlayerPoints(m_player1, m_player2);
                if (m_player1.Turn)
                {
                    PlayerTurn(m_player1);
                }
                else
                {
                    if (m_soloOrWithOtherPlayer == playSoloOrWithOtherPlayer.playSolo)
                    {
                        ComputerTurn();
                    }
                    else
                    {
                        PlayerTurn(m_player2);
                    }

                }

                if (IsGameOver())
                {
                    break;
                }
                Ex02.ConsoleUtils.Screen.Clear();
            }

        }
        private void DetermineFirstTurn()
        {
            Random random = new Random();
            if (random.Next(2) == 0)
            {
                m_player1.Turn = true;
                m_player2.Turn = false;
                Console.WriteLine("Player 1 starts!");
            }
            else
            {
                m_player1.Turn = false;
                m_player2.Turn = true;
                Console.WriteLine("Player 2 starts!");
            }

        }
        private void PlayerTurn(Player player)
        {
            Console.WriteLine($"Player {player.Name}'s turn.");
            string input1 = GetPlayerInput("Enter the coordinates of the first square (e.g., A1): ");
            int x1, y1;
            int errorType = inputChecker.ParseCoordinates(input1, gameBoard, out x1, out y1);
            if (errorType != InputChecker.InputErrorType.None)
            {
                HandleInputError(errorType);
                PlayerTurn(player); // Recursively call PlayerTurn to prompt for input again
                return;
            }
            string input2 = GetPlayerInput("Enter the coordinates of the second square (e.g., B2): ");
            int x2, y2;
            errorType = inputChecker.ParseCoordinates(input2, gameBoard, out x2, out y2);
            if (errorType != InputChecker.InputErrorType.None)
            {
                HandleInputError(errorType);
                PlayerTurn(player); // Recursively call PlayerTurn to prompt for input again
                return;
            }
            ProcessPlayerMove(player, x1, y1, x2, y2);
        }
        private void HandleInputError(int errorType)
        {
            switch (errorType)
            {
                case InputChecker.InputErrorType.OutOfBounds:
                    printer.OutOfBoundsError();
                    break;
                case InputChecker.InputErrorType.AlreadyShown:
                    printer.SquareAlreadyShown();
                    break;
            }
        }
        private string GetPlayerInput(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (input == "Q")
            {
                Environment.Exit(1);
            }
            return input;
        }
        private void ProcessPlayerMove(Player player, int x1, int y1, int x2, int y2)
        {
            gameBoard.GetSquare(x1, y1).SetShowValue(true);
            gameBoard.GetSquare(x2, y2).SetShowValue(true);
            Ex02.ConsoleUtils.Screen.Clear();
            gameBoard.PrintBoardToScreen();
            System.Threading.Thread.Sleep(2000);
            if (gameBoard.GetSquare(x1, y1).GetValue() == gameBoard.GetSquare(x2, y2).GetValue())
            {
                player.Points++;
                Console.WriteLine($"Player {player.Name} found a match!");
            }
            else
            {
                gameBoard.GetSquare(x1, y1).SetShowValue(false);
                gameBoard.GetSquare(x2, y2).SetShowValue(false);
                Console.WriteLine($"Player {player.Name} didn't find a match.");
                player.SwitchTurns(GetOtherPlayer(player));
            }

        }
        private void ComputerTurn()
        {
            Random random = new Random();
            int x1, y1, x2, y2;
            do
            {
                x1 = random.Next(0, gameBoard.Height);
                y1 = random.Next(0, gameBoard.Width);
            } while (gameBoard.GetSquare(x1, y1).GetShowValue());

            do
            {
                x2 = random.Next(0, gameBoard.Height);
                y2 = random.Next(0, gameBoard.Width);
            } while ((x1 == x2 && y1 == y2) || gameBoard.GetSquare(x2, y2).GetShowValue());

            ProcessComputerMove(x1, y1, x2, y2);
        }
        private void ProcessComputerMove(int x1, int y1, int x2, int y2)
        {
            gameBoard.GetSquare(x1, y1).SetShowValue(true);
            gameBoard.GetSquare(x2, y2).SetShowValue(true);
            Ex02.ConsoleUtils.Screen.Clear();
            gameBoard.PrintBoardToScreen();
            Console.WriteLine($"Computer turn!");
            System.Threading.Thread.Sleep(2000);
            if (gameBoard.GetSquare(x1, y1).GetValue() == gameBoard.GetSquare(x2, y2).GetValue())
            {
                m_player2.Points++;
                Console.WriteLine("Computer found a match!");
            }
            else
            {
                gameBoard.GetSquare(x1, y1).SetShowValue(false);
                gameBoard.GetSquare(x2, y2).SetShowValue(false);
                Console.WriteLine("Computer didn't find a match.");
                m_player2.SwitchTurns(m_player1);
            }

        }
        private bool IsGameOver()
        {
            bool isGameOver = false;
            int totalSquares = gameBoard.Height * gameBoard.Width;
            int revealedSquares = gameBoard.GetTotalRevealedSquares();
            if (revealedSquares == totalSquares)
            {
                HandleGameOver();
                isGameOver = true;
            }
            return isGameOver;
        }
        private void HandleGameOver()
        {
            Console.WriteLine("All squares are revealed. Game over!");
            PrintWinner();
            Console.WriteLine("Do you want to play another round? (Y/N)");
            string playAgain = Console.ReadLine().ToUpper();
            if (playAgain == "Y")
            {
                ResetGame();
                HandleGameRun();
            }
            else
            {
                Console.WriteLine("Bye bye!");
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }

        }
        private void PrintWinner()
        {
            if (m_player1.Points > m_player2.Points)
            {
                Console.WriteLine($"{m_player1.Name} wins!");
            }
            else if (m_player1.Points < m_player2.Points)
            {
                Console.WriteLine($"{m_player2.Name} wins!");
            }
            else
            {
                Console.WriteLine("It's a DRAW");
            }

        }
        private void ResetGame()
        {
            m_player1.Points = 0;
            m_player2.Points = 0;
            GetBoardSize();
            Ex02.ConsoleUtils.Screen.Clear();
        }
        private Player GetOtherPlayer(Player currentPlayer)
        {
            return currentPlayer == m_player1 ? m_player2 : m_player1;
        }
    }
}