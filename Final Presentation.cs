namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                char playerOneMarker;



                // Let Player One choose 'X' or 'O'.
                do
                {
                    Console.Write("Player 1, choose your marker (X or O): ");
                    playerOneMarker = char.ToUpper(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                } while (playerOneMarker != 'X' && playerOneMarker != 'O');



                char playerTwoMarker = (playerOneMarker == 'X') ? 'O' : 'X';



                PlayTicTacToe(playerOneMarker, playerTwoMarker);



                Console.Write("Do you want to play again? (Y/N): ");
                string playAgainInput = Console.ReadLine().Trim();
                if (playAgainInput.ToUpper() != "Y")
                {
                    break;
                }
            }
        }



        static void PlayTicTacToe(char playerOneMarker, char playerTwoMarker)
        {
            // Create the game board as a 2D char array.
            char[,] gameboard = new char[3, 3];
            InitializeBoard(gameboard);



            // Play the game.
            bool gameOver = false;
            int currentPlayer = 1;
            int movesCount = 0;



            while (!gameOver)
            {
                Console.Clear();
                DrawBoard(gameboard);



                // Get the player's move.
                int row, col;

                // Make the move.
                do
                {
                    Console.WriteLine("Player {0}'s turn.", currentPlayer);
                    Console.Write("Enter your move (row col), or 'Q' to quit: ");
                    string input = Console.ReadLine().Trim();

                    if (input.ToUpper() == "Q")
                    {
                        gameOver = true;
                        break;
                    }

                    string[] coords = input.Split(' ');
                    row = int.Parse(coords[0]);
                    col = int.Parse(coords[1]);

                } while (!PlaceMarker(gameboard, (currentPlayer == 1) ? playerOneMarker : playerTwoMarker, row, col));

                movesCount++;



                // Check for a winner or a draw.
                if (WinCheck(gameboard, playerOneMarker))
                {
                    gameOver = true;
                    Console.Clear();
                    DrawBoard(gameboard);
                    Console.WriteLine("Player 1 wins!");
                }
                else if (WinCheck(gameboard, playerTwoMarker))
                {
                    gameOver = true;
                    Console.Clear();
                    DrawBoard(gameboard);
                    Console.WriteLine("Player 2 wins!");
                }
                else if (movesCount == 9)
                {
                    gameOver = true;
                    Console.Clear();
                    DrawBoard(gameboard);
                    Console.WriteLine("The game is a draw.");
                }

                // Switch players.
                currentPlayer = (currentPlayer == 1) ? 2 : 1;
            }
        }



        static void InitializeBoard(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        static bool PlaceMarker(char[,] board, char marker, int row, int col)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3)
            {
                Console.WriteLine("Invalid move. Please try again.");
                return false;
            }

            if (board[row, col] != ' ')
            {
                Console.WriteLine("That cell is already occupied. Please try again.");
                return false;
            }

            // Make the move.
            board[row, col] = marker;
            return true;
        }


        static bool WinCheck(char[,] gameboard, char marker)
        {
            // Check for a win in each row.
            for (int i = 0; i < 3; i++)
            {
                if (gameboard[i, 0] == marker && gameboard[i, 1] == marker && gameboard[i, 2] == marker)
                {
                    return true;
                }
            }

            // Check for a win in each column.
            for (int i = 0; i < 3; i++)
            {
                if (gameboard[0, i] == marker && gameboard[1, i] == marker && gameboard[2, i] == marker)
                {
                    return true;
                }
            }

            // Check for a win diagonally.
            if ((gameboard[0, 0] == marker && gameboard[1, 1] == marker && gameboard[2, 2] == marker) ||
                (gameboard[0, 2] == marker && gameboard[1, 1] == marker && gameboard[2, 0] == marker))
            {
                return true;
            }

            return false;
        }



        static void DrawBoard(char[,] gameboard)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(gameboard[i, j]);
                    if (j < 2)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();



                if (i < 2)
                {
                    Console.WriteLine("---------");
                }
            }
        }
    }
}