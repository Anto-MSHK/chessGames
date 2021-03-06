using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static private void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}\n");
            Console.ResetColor();
        }
        static private void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Warning: {message}");
            Console.ResetColor();
        }
        static private void WriteImportant(string message, ConsoleColor color)
        {
            Console.Write("\n");
            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == '{')
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
        
        static private void WriteCell(string str, ConsoleColor colorBG, ConsoleColor colorFG)
        {
            Console.BackgroundColor = colorBG;
            Console.ForegroundColor = colorFG;
            Console.Write(str);
            Console.ResetColor();
        }

        static private int ReadInt()
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

                if(str == "e")
                {
                    return 0;
                }

            } while (!int.TryParse(str, out result));
            Console.SetCursorPosition(posl, post);
            Console.Write(str);
            return result;
        }
        static private char ReadChar()
        {
            int posl = Console.CursorLeft, post = Console.CursorTop;
            char result;
            string str = "";
            bool isNumber;
            do
            {
                isNumber = false;
                Console.SetCursorPosition(posl, post);
                str = Console.ReadLine().ToLower();
                Console.SetCursorPosition(posl, post);

                foreach (char c in str)
                    Console.Write(" ");

                for(int i = 0; i<=9; i++)
                {
                   if(str == $"{i}") {
                        isNumber = true;
                   }
                }

            } while (!char.TryParse(str, out result) || isNumber);
            Console.SetCursorPosition(posl, post);
            Console.Write(str);
            return result;
        }

        public static string[,] fakeTable = new string[8, 8] {  { "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓" },
                                                                { "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░" },
                                                                { "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓" },
                                                                { "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░" },
                                                                { "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓" },
                                                                { "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░" },
                                                                { "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓" },
                                                                { "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░", "▓▓▓", "░░░" }};

        public static string[] piecesNames =  new string[6] { "king", "queen", "rook", "bishop", "horse", "pawn" };

        static void convertCellToString(int x, int y, Cell[,] chessTable)
        {
            Cell actualCell = chessTable[x, y];
            string type = chessTable[x, y].type;
            int side = chessTable[x, y].side;
            bool isProtected = false, isAttacked = false;

            if (type == "cell") {
               // if (actualCell.isHere == true)
                // WriteCell("   ", ConsoleColor.White, ConsoleColor.Black);  //output of moves
               // else
                Console.Write(fakeTable[x, y]);
            }
            else
            {
                if (actualCell.isProtected != null)
                    foreach (int[] protd in actualCell.isProtected)
                    {
                        if (protd != null) { 
                            isProtected = true;
                            break;
                        }
                    }

                if (isProtected == true)
                    WriteCell(" ", ConsoleColor.Green, ConsoleColor.Green);
                else
                    Console.Write(" ");

                if (side == 0)
                {
                    WriteCell(type[0].ToString(), ConsoleColor.White, ConsoleColor.Black);
                }
                if (side == 1)
                {
                    WriteCell(type[0].ToString(), ConsoleColor.DarkBlue, ConsoleColor.White);
                }

                if (actualCell.isAttacked != null)
                    foreach (int[] atackd in actualCell.isAttacked)
                    {
                        if (atackd != null) {
                            isAttacked = true;
                            break;
                        }
                    }

                if (isAttacked == true)
                    WriteCell(" ", ConsoleColor.Red, ConsoleColor.Red);
                else
                    Console.Write(" ");
            }
        }
        static void printChessTable(Cell[,] chessTable)
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
                        Console.Write($" {i + 1} ");
                        Console.ResetColor();
                    }
                    convertCellToString(i, j, chessTable);
                }
            }

            Console.Write($"\n");
        }

        static string decryption(int errCode)
        {
            switch (errCode)
            {
                case 1:
                    return "The chess piece in this position is already standing!";
                case 2:
                    return "The coordinates of the chess piece must be less than 8!";
                case 3:
                    return "The coordinates of the chess piece must be greater than 1!";
                case 4:
                    return "A pawn in this position turns into any of the pieces!";
                default:
                    return "";
            }
        }

        static void basicOutput(Player player)
        {
            ChessTable cheseTable = ChessTable.GetInstance();

            printChessTable(cheseTable.table);
            WriteImportant("Enter the positions of the chess pieces of the {" + Convert.ToInt32(player.side + 1) + "} player: ", ConsoleColor.Red);
        }

        static void addChessPiece(Player player)
        {
            char operation;
            int isAdded = 0,
                index = 0;
            string curentPiece = "";

            do {
                basicOutput(player);

                Console.Write($"Select a chess piece:");
                operation = ReadChar();
                Console.WriteLine("");

                if (operation == 'e') {  Console.Clear(); break; }
                
                do
                {
                    if (operation == 'd') { Console.Clear(); deleteChessPiece(player); break; }

                    if (isAdded != 0) {
                    basicOutput(player);
                    WriteError(decryption(isAdded));
                    }

                    foreach (string p in piecesNames)
                    {
                        if (p[0] == operation)
                            curentPiece = p;
                    }
                    
                    Console.WriteLine($"Enter position {curentPiece}:");
                    WriteWarning("Only numbers! 1 => ? <= 8.");
                    WriteWarning("To deselect a chess piece, press e.");
                    Console.Write("\n");
                        
                    Console.Write($"x: ");

                    int x = ReadInt();

                    if (x == 0)
                    {
                        Console.Clear();
                        break;
                    }

                    Console.Write($"\ny: ");

                    int y = ReadInt();

                    if (y == 0) { 
                        Console.Clear();
                        break;
                    }

                    isAdded = player.enterCoordinates(x, y, index, operation);

                    Console.Clear();


                } while (isAdded != 0);
                index++;
            } while (operation != 'e');
        }

        static void deleteChessPiece(Player player)
        {
            int isDeleted = 0;
            ChessTable cheseTable = ChessTable.GetInstance();

            basicOutput(player);

            Console.WriteLine($"Remove a piece from the chessboard:");
            Console.WriteLine($"Enter position:");
            WriteWarning("Only numbers! 1 => ? <= 8.");
            Console.Write("\n");

            Console.Write($"x: ");

            int x = ReadInt();

            if (x == 0)
            {
                Console.Clear();
                return;
            }

            Console.Write($"\ny: ");

            int y = ReadInt();

            if (y == 0)
            {
                Console.Clear();
                return;
            }

            isDeleted = player.deletePiece(x, y);

            Console.Clear();

        }
        static void placementChessPieces(Player player)
        {
            addChessPiece(player);
        }


            static void Main(string[] args)
        {

            Player firstPlayer = new Player(0);
            Player secondPlayer = new Player(1);

            placementChessPieces(firstPlayer);
            placementChessPieces(secondPlayer);
            
            Console.ReadKey();
        }
    }
}
