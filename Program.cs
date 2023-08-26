using System;

namespace FourInARow
{
    internal class Program
    {

        const int P1 = 1;
        const int P2 = 2;
        const int TIE = 3;

        // הפעולה מקבלת את מספר השורות והעמודות מהשחקן ומתחילה את המשחק
        static void Main(string[] args)
        {
            int rows, cols;
            string input;
            int[] winsChart = new int[3];
            bool playAgain = true;

            Console.WriteLine("Enter number of rows: ");
            input = Console.ReadLine();
            while (int.TryParse(input, out rows) == false || rows < 4)
            {
                Console.WriteLine($"Invalid input, Must be a natural number that is greater or equal 4.\nEnter number of rows:");
                input = Console.ReadLine();
            }
            Console.WriteLine("Enter number of columns: ");
            input = Console.ReadLine();
            while (int.TryParse(input, out cols) == false || cols < 4)
            {
                Console.WriteLine($"Invalid input, Must be a natural number that is greater or equal 4.\nEnter number of columns:");
                input = Console.ReadLine();
            }

            while (playAgain)
            {
                int winnerPlayer = StartGame(rows, cols);
                winsChart[winnerPlayer - 1]++;

                Console.WriteLine("Current wins chart is: ");
                Console.WriteLine($"Player 1: {winsChart[0]} wins");
                Console.WriteLine($"Player 2: {winsChart[1]} wins");
                Console.WriteLine($"Ties: {winsChart[2]}");
                Console.WriteLine("Do you want to play again? (y/n)");
                string answer = Console.ReadLine();
                while (answer != "y" && answer != "n")
                {
                    Console.WriteLine("Invalid answer. Please enter y/n");
                    answer = Console.ReadLine();
                }
                if (answer == "y")
                {
                    winnerPlayer = StartGame(rows, cols);
                    winsChart[winnerPlayer - 1]++;

                    Console.WriteLine("Current wins chart is: ");
                    Console.WriteLine($"Player 1: {winsChart[0]} wins");
                    Console.WriteLine($"Player 2: {winsChart[1]} wins");
                    Console.WriteLine($"Ties: {winsChart[2]}");
                    Console.WriteLine("Do you want to play again? (y/n)");
                    answer = Console.ReadLine();
                    while (answer != "y" && answer != "n")
                    {
                        Console.WriteLine("Invalid answer. Please enter y/n");
                        answer = Console.ReadLine();
                    }
                }
                else
                {
                    playAgain = false;
                }
            }


        }

        // הפעולה יוצרת עצם לוח שהוא מנהל את המשחק, הפעולה תיקח מהשחקנים את הקלט ותפעיל את הפעולות על המשחק
        static int StartGame(int rows, int cols)
        {
            int currentPlayer, chosenCol;
            string input;

            Board board = new Board(rows, cols);

            currentPlayer = P1;

            while (board.GetWinnerAsimons() == null)
            {
                Console.Clear();
                board.Print();

                if (board.IsFull())
                {
                    Console.WriteLine("The board is full! It's a tie!");
                    return 3;
                }

                Console.WriteLine($"Player {currentPlayer} turn");
                Console.WriteLine("Enter column: ");
                input = Console.ReadLine();
                while (int.TryParse(input, out chosenCol) == false)
                {
                    Console.WriteLine($"Invalid input, Must be a natural number between 1 - {cols}\nEnter a column:");
                    input = Console.ReadLine();
                }


                while (chosenCol > cols || chosenCol < 1)
                {
                    Console.WriteLine($"Invalid column. Must be between 1 - {cols}\nEnter a column:");
                    input = Console.ReadLine();
                    while (int.TryParse(input, out chosenCol) == false)
                    {
                        Console.WriteLine($"Invalid input, Must be a natural number between 1 - {cols}\nEnter a column:");
                        input = Console.ReadLine();
                    }
                }

                while (board.IsFull(chosenCol - 1))
                {
                    Console.WriteLine($"The column is full! Please enter a natural number between 1 - {cols}");
                    input = Console.ReadLine();
                    while (int.TryParse(input, out chosenCol) == false && (chosenCol > cols || chosenCol < 1))
                    {
                        Console.WriteLine($"Invalid input, Must be a natural number between 1 - {cols}\nEnter a column:");
                        input = Console.ReadLine();
                    }
                }

                board.DropAsimon(chosenCol - 1, currentPlayer);
                currentPlayer = currentPlayer == P1 ? P2 : P1;
            }

            Console.Clear();
            Asimon[] winner = board.GetWinnerAsimons();
            board.Print(winner);
            Console.WriteLine($"The winner is player {board.GetWinner(winner)}");

            return board.GetWinner(winner);
        }
    }
}
