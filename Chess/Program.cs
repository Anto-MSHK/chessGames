using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        public static Program program = new Program();

        public void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}\n");
            Console.ResetColor();
        }
        public void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Warning: {message}\n");
            Console.ResetColor();
        }
        public void WriteImportant(string message, ConsoleColor color)
        {
            Console.Write("\n");
            for (int i=0; i < message.Length; i++)
            {
                if(message[i] == '{')
                {
                    Console.ForegroundColor = color;
                }
                else if (message[i] == '}')
                {
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(message[i]);
                }
            }
            Console.WriteLine("\n");
        }

        public int ReadInt()
        {
            int posl = Console.CursorLeft, post = Console.CursorTop,
                result;
            string str = "";
            do
            {
                Console.SetCursorPosition(posl, post);
                str = Console.ReadLine();
                Console.SetCursorPosition(posl, post);

                foreach (char c in str)
                    Console.Write(" ");

            } while (!int.TryParse(str, out result));
            Console.SetCursorPosition(posl, post);
            Console.Write(str);
            return result;
        }
        public char ReadChar()
        {
            int posl = Console.CursorLeft, post = Console.CursorTop;
            char result;
            string str = "";
            do
            {
                Console.SetCursorPosition(posl, post);
                str = Console.ReadLine();
                Console.SetCursorPosition(posl, post);

                foreach (char c in str)
                    Console.Write(" ");

            } while (!char.TryParse(str, out result));
            Console.SetCursorPosition(posl, post);
            Console.Write(str);
            return result;
        }

        public sealed class ChessTable
        {
            public static ChessTable chessTable = new ChessTable();
            
            public string[,] table = new string[8, 8] {{ "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓" },
                                                       { "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░" },
                                                       { "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓" },
                                                       { "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░" },
                                                       { "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓" },
                                                       { "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░" },
                                                       { "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓" },
                                                       { "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░" }};

            public string[] chessPieces = { " k ", " q ", " r ", " r ", " b ", " b ", " h ", " h ", " p ", " p ", " p ", " p ", " p ", " p ", " p ", " p "  };

            int[][,] pieces = new int[2][,];

            private ChessTable() {}

            public string addPiece(int x, int y, int pieceNumber)
            {
                if(x > 7 || y > 7)
                    return "The position cannot be more than 8!";
                if (x < 0 || y < 0)
                    return "The position cannot be less than 0!";
                if (table[x, y] == "░░░" || table[x, y] == "▓▓▓")
                {
                    table[x, y] = chessPieces[pieceNumber];
                    return "";
                } else
                    return "A chess piece is already standing!";
                
            }

            public void print()
            {
                Console.Write($"   ");
                for (int i = 0; i < 8; i++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write($" {i + 1} ");
                    Console.ResetColor();
                }

                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine($"");
                    for (int j = 0; j < 8; j++)
                    {
                        if (j == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write($" {i+1} ");
                            Console.ResetColor();
                        }
                        Console.Write($"{table[i, j]}");
                    }
                }
                Console.WriteLine($"\n");
            }
            
        }

        class Player
        {
            private int[,] piecesPos = new int[16, 2];
            private string[] chessPieces = { "king", "queen", "1 rook", "2 rook", "1 bishop", "2 bishop", "1 horse", "2 horse",
                                             "1 pawn", "2 pawn", " 3 pawn", "4 pawn", "5 pawn", "6 pawn", "7 pawn", "8 pawn" };
            int playerNum;
            public Player(int playerNum)
            {
                this.playerNum = playerNum;
            }
            public int[,] enterCoordinates()
            {
                string isAdded = "";
                for (int i = 0; i < chessPieces.Length; i++)
                {
                    do
                    {
                        if (playerNum == 1)
                            program.WriteImportant("Enter the positions of the chess pieces of the {first player}: ", ConsoleColor.Red);
                        else if (playerNum == 2)
                            program.WriteImportant("Enter the positions of the chess pieces of the {second player}: ", ConsoleColor.Blue);

                        ChessTable.chessTable.print();
                        
                        if (isAdded != "")
                            program.WriteError(isAdded);

                        Console.WriteLine($"Enter position {chessPieces[i]}:");
                        program.WriteWarning("Only numbers! 1 => ? <= 8.");

                        Console.Write($"x: ");

                        piecesPos[i, 0] = program.ReadInt() - 1;

                        Console.Write($"\ny: ");

                        piecesPos[i, 1] = program.ReadInt() - 1;

                        isAdded = ChessTable.chessTable.addPiece(piecesPos[i, 0], piecesPos[i, 1], i);
                        Console.Clear();

                    } while(isAdded != "");
                }
                return piecesPos;
            }
        }

        static void Main(string[] args)
        {
            Player firstPlayer = new Player(1);
            Player secondPlayer = new Player(2);
            
            firstPlayer.enterCoordinates();
            secondPlayer.enterCoordinates();
            Console.Clear();
            ChessTable.chessTable.print();
            Console.ReadKey();
        }
    }
}
