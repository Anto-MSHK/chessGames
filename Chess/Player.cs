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
                    piece = new Cell("king", side);
                    break;
                case 'q':
                    piece = new Cell("queen", side);
                    break;
                case 'r':
                    piece = new Cell("rook", side);
                    break;
                case 'b':
                    piece = new Cell("bishop", side);
                    break;
                case 'h':
                    piece = new Cell("horse", side);
                    break;
                case 'p':
                    piece = new Cell("pawn", side);
                    break;
                default:
                    piece = new Cell();
                    break;
            }

            int pieceAddSuccess = cheseTable.addPiece(x-1, y-1, piece);

            if (pieceAddSuccess == 0)
            {
                piece.posX = x - 1;
                piece.posY = y - 1;
            }

            return pieceAddSuccess;
        }
    }
}
