using System;

namespace FourInARow
{
    internal class Board
    {
        private int[,] board;
        private int rows;
        private int cols;
        const int EMPTY = 0;
        const int WIN = 4;
        const int P1 = 1;
        const int P2 = 2;
        /*
         טענת כניסה: הפעולה מקבלת פרמטר לכמות השורות ופרמטר לכמות העמודות בלוח משחק.

טענת יציאה: הפעולה מאפסת את המערך הדו ממדי ומחזירה הפנייה מסוג טיפוס המחלקה.

         */
        public Board(int rows, int cols)
        {
            board = new int[rows, cols];
            this.rows = rows;
            this.cols = cols;
        }

        /*
         טענת כניסה: תקבל מספר שלם של עמודה להכניס את האסימון המתאים ובנוסף עוד מספר שלם של השחקן המיועד לאותו תור (1 או 2)

טענת יציאה: הפעולה תכניס את האסימון של השחקן המיועד לעמודה הרצויה ותחזיר
        True אם הוא נכנס או False אם הוא לא (העמודה מלאה).

         */

        public bool DropAsimon(int col, int player)
        {
            for (int i = rows - 1; i >= 0; i--)
            {
                if (board[i, col] == EMPTY)
                {
                    board[i, col] = player;
                    return true;
                }
            }
            return false;
        }

        /*
         טענת כניסה: הפעולה מקבלת פרמטר ממספר שלם לעמודה המתאימה.

טענת יציאה: הפעולה מחזירה אמת או שקר לאם העמודה המתאימה מלאה או לא.

         */
        public bool IsFull(int col)
        {
            for (int i = rows - 1; i >= 0; i--)
            {
                if (!(board[i, col] == P1 || board[i, col] == P2))
                    return false;
            }
            return true;
        }
        /*
         
         טענת כניסה: הפעולה לא מקבלת פרמטרים.

טענת יציאה: הפעולה מחזירה אמת או שקר אם הלוח ריק או לא.

         */
        public bool IsFull()
        {
            for (int i = 0; i < cols; i++)
            {
                if (!IsFull(i))
                    return false;
            }
            return true;
        }

        /*
         טענת כניסה: הפעולה לא מקבלת פרמטרים.

טענת יציאה: הפעולה תדפיס את לוח המשחק.

         */

        public void Print()
        {
            for (int i = 0; i < cols; i++)
            {
                Console.Write($"  {i + 1} ");
            }
            Console.WriteLine();
            for (int i = 0; i < rows; i++)
            {
                Console.Write("|");
                for (int j = 0; j < cols; j++)
                {
                    ConsoleColor color = board[i, j] == 1 ? ConsoleColor.Red : board[i, j] == 2 ? ConsoleColor.DarkYellow : ConsoleColor.Black;
                    Console.BackgroundColor = color;
                    Console.Write($" {board[i, j]} ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        /*
         טענת כניסה: הפעולה מקבלת מערך של האסימונים המנצחים.

טענת יציאה: הפעולה תדפיס את הלוח משחק ותדגיש את האסימונים המנצחים.

         */
        public void Print(Asimon[] winner)
        {
            for (int i = 0; i < cols; i++)
            {
                Console.Write($"  {i + 1} ");
            }
            Console.WriteLine();
            for (int i = 0; i < rows; i++)
            {
                Console.Write("|");
                for (int j = 0; j < cols; j++)
                {
                    ConsoleColor color = board[i, j] == 1 ? ConsoleColor.Red : board[i, j] == 2 ? ConsoleColor.DarkYellow : ConsoleColor.Black;
                    Console.BackgroundColor = color;
                    bool found = false;
                    for (int k = 0; k < winner.Length; k++)
                    {
                        if (winner[k].GetRow() == i && winner[k].GetCol() == j)
                            found = true;
                    }
                    if (found)
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write($" {board[i, j]} ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        /*
         טענת כניסה: הפעולה תקבל מערך של האסימונים המנצחים.

טענת יציאה: הפעולה תחזיר את מס' השחקן המנצח באותם אסימונים המנצחים.

         */

        public int GetWinner(Asimon[] winner)
        {
            return winner[0].GetPlayer();
        }


        /*
         טענת כניסה: הפעולה לא תקבל פרמטרים.

טענת יציאה: הפעולה תחזיר מערך של עצמים מסוג Asimon שמכילים בערכם את השורה והעמודה של האסימון מהקומבינציה המנצחת

         */
        public Asimon[] GetWinnerAsimons()
        {

            int p1row = 0,
                p2row = 0,
                p1col = 0,
                p2col = 0,
                p1diag = 0,
                p2diag = 0,
                p1secDiag = 0,
                p2secDiag = 0;

            Asimon[] p1 = new Asimon[WIN];
            Asimon[] p2 = new Asimon[WIN];

            // Checks for combination in a row:
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (p1row == WIN)
                        return p1;
                    if (p2row == WIN)
                        return p2;

                    if (board[i, j] == P1)
                    {
                        p1[p1row] = new Asimon(i, j, P1);
                        p1row++;
                        p2row = 0;
                    }
                    else if (board[i, j] == P2)
                    {
                        p2[p2row] = new Asimon(i, j, P2);
                        p2row++;
                        p1row = 0;
                    }
                    else
                    {
                        p1row = 0;
                        p2row = 0;
                    }

                }
                if (p1row == WIN)
                    return p1;
                if (p2row == WIN)
                    return p2;
                p1row = 0;
                p2row = 0;
            }

            // Checks for combination in a column:
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (p1col == WIN)
                        return p1;
                    if (p2col == WIN)
                        return p2;

                    if (board[j, i] == P1)
                    {
                        p1[p1col] = new Asimon(j, i, P1);
                        p1col++;
                        p2col = 0;
                    }
                    else if (board[j, i] == P2)
                    {
                        p2[p2col] = new Asimon(j, i, P2);
                        p2col++;
                        p1col = 0;
                    }
                    else
                    {
                        p1col = 0;
                        p2col = 0;
                    }
                }
                if (p1col == WIN)
                    return p1;
                if (p2col == WIN)
                    return p2;
                p1col = 0;
                p2col = 0;
            }

            // Checks for combination in a diagonal line;
            for (int i = 0; i < rows - (WIN - 1); i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    for (int k = 0; k + i < rows && k + j < cols; k++)
                    {
                        if (p1diag == WIN || p1secDiag == WIN)
                            return p1;
                        if (p2diag == WIN || p2secDiag == WIN)
                            return p2;

                        int row = k + i;
                        int col = k + j;
                        int secCol = cols - 1 - col;

                        if (board[row, secCol] == P1)
                        {
                            p1[p1secDiag] = new Asimon(row, secCol, P1);
                            p1secDiag++;
                            p2secDiag = 0;
                        }
                        else if (board[row, secCol] == P2)
                        {
                            p2[p2secDiag] = new Asimon(row, secCol, P2);
                            p2secDiag++;
                            p1secDiag = 0;
                        }
                        else
                        {
                            p1secDiag = 0;
                            p2secDiag = 0;
                        }

                        if (board[row, col] == P1)
                        {
                            p1[p1diag] = new Asimon(row, col, P1);
                            p1diag++;
                            p2diag = 0;
                        }
                        else if (board[row, col] == P2)
                        {
                            p2[p2diag] = new Asimon(row, col, P2);
                            p2diag++;
                            p1diag = 0;
                        }
                        else
                        {
                            p1diag = 0;
                            p2diag = 0;
                        }
                    }
                    if (p1diag == WIN || p1secDiag == WIN)
                        return p1;
                    if (p2diag == WIN || p2secDiag == WIN)
                        return p2;
                    p1diag = 0;
                    p2diag = 0;
                    p1secDiag = 0;
                    p2secDiag = 0;
                }


            }

            return null;
        }
    }
}
