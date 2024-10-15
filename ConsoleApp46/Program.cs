using System;

namespace TicTacToeSubmissionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.SetCursorPosition(10, 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Tic Tac Toe");

            var ticTacToe = new TicTacToe();
            ticTacToe.Run();

            Console.ForegroundColor = oldColor;
            Console.SetCursorPosition(20, 25);
            Console.WriteLine("Thank you for playing");
            Console.ReadLine();
        }
    }

    public class TicTacToe
    {
        private char[] gamingBoard;
        private char activePlayer;

        public TicTacToe()
        {
            gamingBoard = new char[9];
            activePlayer = 'X';
        }

        public void Run()
        {
            bool isPlaying = true;
            int numberOfMoves = 0;

            while (isPlaying)
            {
                Console.Clear();
                DrawBoard();
                Console.WriteLine($"Player {activePlayer}, enter your move (1-9): ");

                int playerMove;
                //ensures valid move is entered
                while (!int.TryParse(Console.ReadLine(), out playerMove) || playerMove < 1 || playerMove > 9 || gamingBoard[playerMove - 1] != '\0')
                {
                    Console.WriteLine("Invalid move. Please try again (1-9): ");
                }

                gamingBoard[playerMove - 1] = activePlayer;//update gaming board with the playe's move
                numberOfMoves++;
                
                //check for a win condition
                if (CheckWin())
                {
                    Console.Clear();  //clear the console to display the final board
                    DrawBoard();   //Draw the board before announcing the winner
 
                    Console.WriteLine($"Player {activePlayer} wins!"); //announce the winner
                    isPlaying = false;  //end the game
                }
                else if (numberOfMoves == 9)
                {
                    Console.Clear();  //clear the console to display the final board
                    DrawBoard();   //Draw the board before announcing the draw
                    Console.WriteLine("It's a draw!");  //announce the draw
                    isPlaying = false;  //end the game
                }
                else
                {
                    activePlayer = (activePlayer == 'X') ? 'O' : 'X';
                }
            }
        }

        // Method to draw the current state of the board
        private void DrawBoard()
        {
            Console.WriteLine("Current board:");
            Console.WriteLine($" {GetSymbol(0)} | {GetSymbol(1)} | {GetSymbol(2)} ");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {GetSymbol(3)} | {GetSymbol(4)} | {GetSymbol(5)} ");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {GetSymbol(6)} | {GetSymbol(7)} | {GetSymbol(8)} ");
        }

        //Method to get the symbol at a specific index on the board
        private char GetSymbol(int index)
        {
            return gamingBoard[index] == '\0' ? ' ' : gamingBoard[index];
        }

        // Method to check for a win condition
        private bool CheckWin()
        {
            int[][] winningSequences = new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };


            // Loop through each winning sequence to check for a win
            foreach (var sequence in winningSequences)
            {
                // Check if the current sequence has the same symbol in all positions
                if (gamingBoard[sequence[0]] != '\0' &&
                    gamingBoard[sequence[0]] == gamingBoard[sequence[1]] &&
                    gamingBoard[sequence[0]] == gamingBoard[sequence[2]])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
