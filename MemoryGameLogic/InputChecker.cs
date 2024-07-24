using System;
namespace MemoryGameLogic
{
    public class InputChecker
    {
        public static class InputErrorType
        {
            public const int None = 0;
            public const int OutOfBounds = 1;
            public const int AlreadyShown = 2;
        }
        public bool CheckValidSoloOrDuo(int valueToCheck)
        {
            return valueToCheck == 0 || valueToCheck == 1;
        }
        private bool CheckIfBoardSizeIsEven(int boardSizeX, int boardSizeY)
        {
            return (boardSizeX * boardSizeY) % 2 == 0;
        }
        private bool CheckIfBoardSizeInLimitSize(int boardSizeX, int boardSizeY)
        {
            return (boardSizeX <= 6 && boardSizeX >= 4) && (boardSizeY <= 6 && boardSizeY >= 4);
        }
        public bool CheckIfBoardSizeIsLegal(int boardSizeX, int boardSizeY)
        {
            return CheckIfBoardSizeIsEven(boardSizeX, boardSizeY) && CheckIfBoardSizeInLimitSize(boardSizeX, boardSizeY);
        }
        public int ParseCoordinates(string input, Board gameBoard, out int x, out int y)
        {
            x = -1;
            y = -1;
            int errorType = InputErrorType.None;

            if (input.Length != 2)
            {
                errorType = InputErrorType.OutOfBounds;
            }
            else
            {
                input = input.ToUpper();
                char colChar = input[0];
                char rowChar = input[1];
                if (colChar < 'A' || colChar >= 'A' + gameBoard.Width || rowChar < '1' || rowChar >= '1' + gameBoard.Height)
                {
                    errorType = InputErrorType.OutOfBounds;
                }
                else
                {
                    x = rowChar - '1';
                    y = colChar - 'A';
                    if (gameBoard.GetSquare(x, y).GetShowValue())
                    {
                        errorType = InputErrorType.AlreadyShown;
                    }
                }

            }

            return errorType;
        }
    }
}