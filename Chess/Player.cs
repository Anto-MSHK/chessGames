using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Player
    {
        private int[,] piecesPos = new int[16, 2];
        public int side;
        ChessTable cheseTable = ChessTable.GetInstance();
        public Player(int playerNum)
        {
            side = playerNum;
        }


        public int enterCoordinates(int x, int y, int index, char operation)
        {
            Cell piece;
            switch (operation)
            {
                case 'k':
                    piece = new Cell("king", side, x, y);
                    break;
                case 'q':
                    piece = new Cell("queen", side, x, y);
                    break;
                case 'r':
                    piece = new Cell("rook", side, x, y);
                    break;
                case 'b':
                    piece = new Cell("bishop", side, x, y);
                    break;
                case 'h':
                    piece = new Cell("horse", side, x, y);
                    break;
                case 'p':
                    piece = new Cell("pawn", side, x, y);
                    break;
                default:
                    piece = new Cell(x, y);
                    break;
            }

            int pieceAddSuccess = cheseTable.AddPiece(x-1, y-1, piece);

            if (pieceAddSuccess == 0)
            {
                
            }

            return pieceAddSuccess;
        }

        public int deletePiece(int x, int y)
        {
            int pieceDeleteSuccess = cheseTable.deletePiece(x - 1, y - 1);

            return pieceDeleteSuccess;
        }
    }


}
