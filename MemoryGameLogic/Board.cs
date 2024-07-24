using System;
using System.Collections;
namespace MemoryGameLogic
{
    public class Board
    {
        private int m_height;
        private int m_width;
        private Square[][] m_boardArray;
        private Random random;
        public int Height
        {
            get { return m_height; }
            set { m_height = value; }
        }
        public int Width
        {
            get { return m_width; }
            set { m_width = value; }
        }
        public Board(int height, int width)
        {
            // Assuming the inputs have already been checked and are according to the standard 
            this.m_height = height;
            this.m_width = width;
            BuildBoardArray();
            SetBoardValues();
        }
        public Square GetSquare(int x, int y)
        {
            return m_boardArray[x][y];
        }
        public void BuildBoardArray()
        {
            this.m_boardArray = new Square[m_height][];

            for (int i = 0; i < m_height; i++)
            {
                this.m_boardArray[i] = new Square[m_width];
            }

            for (int i = 0; i < m_height; i++)
            {
                for (int j = 0; j < m_width; j++)
                {
                    this.m_boardArray[i][j] = new Square();
                    this.m_boardArray[i][j].SetXY(i, j);
                }
            }
        }
        public void SetBoardValues()
        {
            int totalSquares = this.m_width * this.m_height;
            int numberOfPairs = totalSquares / 2;
            ArrayList values = new ArrayList();
            Hashtable usedValues = new Hashtable();  // Each pair of squares should have a unique value
            random = new Random();
            // Generate unique characters for each pair
            ValuesOfPairs(numberOfPairs, usedValues, values);
            // Shuffle the list of values
            ShuffleList(values);
            // Assign the shuffled values to the board
            GetValuesFromListToBoard(values);
        }
        public void ValuesOfPairs(int numberOfPairs, Hashtable usedValues, ArrayList values)
        {
            // Generate unique characters for each pair
            for (int i = 0; i < numberOfPairs; i++)
            {
                char newValue;
                do
                {
                    newValue = (char)random.Next('A', 'Z' + 1);
                } while (usedValues.ContainsKey(newValue));
                usedValues.Add(newValue, null);
                values.Add(newValue);
                values.Add(newValue);
            }
        }
        public void ShuffleList(ArrayList values)
        {
            // Shuffle the list of values
            for (int i = values.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                object temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }
        public void GetValuesFromListToBoard(ArrayList values)
        {
            // Assign the shuffled values to the board
            int index = 0;
            for (int i = 0; i < this.m_height; i++)
            {
                for (int j = 0; j < this.m_width; j++)
                {
                    this.m_boardArray[i][j].SetValue((char)values[index]);
                    index++;
                }
            }
        }
        public void PrintBoardToScreen()
        {
            Console.Write(" ");
            for (char c = 'A'; c < 'A' + this.m_width; c++)
            {
                Console.Write($"   {c}");
            }
            Console.WriteLine();
            Console.Write("  ");
            Console.WriteLine(new string('=', 4 * m_width + 1));
            for (int i = 0; i < this.m_height; i++)
            {
                Console.Write($"{i + 1} |");
                for (int j = 0; j < this.m_width; j++)
                {
                    if (this.m_boardArray[i][j].GetShowValue() == true)
                    {
                        Console.Write($" {m_boardArray[i][j].GetValue()} ");
                    }
                    else
                    {
                        Console.Write($"   ");
                    }

                    Console.Write("|");
                }
                Console.WriteLine();
                Console.Write("  ");
                Console.WriteLine(new string('=', 4 * m_width + 1));
            }
        }
        public int GetTotalRevealedSquares()
        {
            int revealedSquares = 0;
            for (int i = 0; i < this.m_height; i++)
            {
                for (int j = 0; j < this.m_width; j++)
                {
                    if (this.m_boardArray[i][j].GetShowValue())
                    {
                        revealedSquares++;
                    }
                }
            }
            return revealedSquares;
        }
    }
}
