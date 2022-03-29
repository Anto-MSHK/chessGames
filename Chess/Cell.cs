using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Cell
    {
        public string type;
        public int posX;
        public int posY;
        public int[][] moves = new int[64][];
        public List<int[]> isAttacked = new List<int[]>();
        public List<int[]> isProtected = new List<int[]>();
        public bool isHere = false;
        public int side;
        public int NumberOf;

        public int[][] coordinates = new int[8][];

        private ChessTable chessTable;
        public Cell(string type, int side, int posX, int posY)
        {   
            this.side = side;
            this.type = type;
            this.posX = posX;
            this.posY = posY;

            resetCoordinates();

            chessTable = ChessTable.GetInstance();
        }

        public Cell(int posX, int posY)
        {
            type = "cell";
            side = 3;
            this.posX = posX;
            this.posY = posY;
        }

        private void resetCoordinates()
        {
            coordinates[0] = new int[] { 7, 7 };
            coordinates[1] = new int[] { 7, 0 };
            coordinates[2] = new int[] { 0, 0 };
            coordinates[3] = new int[] { 0, 7 };
            coordinates[4] = new int[] { 7, 7 };
            coordinates[5] = new int[] { 7, 0 };
            coordinates[6] = new int[] { 0, 0 };
            coordinates[7] = new int[] { 0, 7 };
        }

        private void marking()
        {
            for (int i = 0; i < moves.Length; i++)
                if (moves[i] != null && (moves[i][0] < 8 && moves[i][1] < 8 && moves[i][0] > -1 && moves[i][1] > -1))
                {
                    chessTable.mark(moves[i][0], moves[i][1], this, i);
                }
        }

        private void pawn()
        {
            moves = new int[2][];
            if (side == 0)
            {
                moves[0] = new int[] { posX - 1, posY - 1 };
                moves[1] = new int[] { posX - 1, posY + 1 };
            } else if (side == 1)
            {
                moves[0] = new int[] { posX + 1, posY - 1 };
                moves[1] = new int[] { posX + 1, posY + 1 };
            }
            marking();
        }

        private void horse()
        {
            moves = new int[8][];

            moves[0] = new int[] { posX - 2, posY + 1 };
            moves[1] = new int[] { posX - 1, posY + 2 };
            moves[2] = new int[] { posX + 2, posY + 1 };
            moves[3] = new int[] { posX + 1, posY + 2 };

            moves[4] = new int[] { posX - 2, posY - 1 };
            moves[5] = new int[] { posX - 1, posY - 2 };
            moves[6] = new int[] { posX + 2, posY - 1 };
            moves[7] = new int[] { posX + 1, posY - 2 };
            marking();
        }

        private int bishop()
        {
            int actual = 0;

            int curX = posX, curY = posY;
            for (int i = posX+1; i < 8; i++)
                for (int j = posY+1; j < 8; j++)
                {
                    if ((i - curX == 1) && (j - curY == 1) && (coordinates[0][0] >= i && coordinates[0][1] >= j)) {
                        moves[actual] = new int[] { i, j };
                        chessTable.mark(moves[actual][0], moves[actual][1], this, 0);
                        curX = i;
                        curY = j;
                        actual++;
                    }
                }
            coordinates[0] = new int[] { curX, curY };

            curX = posX; curY = posY;
            for (int i = posX+1; i < 8; i++)
                for (int j = posY-1; j >= 0; j--)
                {
                    if((i - curX == 1) && (j - curY == -1) && (coordinates[1][0] >= i && coordinates[1][1] <= j)) { 
                        moves[actual] = new int[] { i, j };
                        chessTable.mark(moves[actual][0], moves[actual][1], this, 1);
                        curX = i;
                        curY = j;
                        actual++;
                    }
                }
            coordinates[1] = new int[] { curX, curY };

            curX = posX; curY = posY;
            for (int i = posX-1; i >= 0; i--)
                for (int j = posY-1; j >= 0; j--)
                {
                    if ((i - curX == -1) && (j - curY == -1) && (coordinates[2][0] <= i && coordinates[2][1] <= j))
                    {
                        moves[actual] = new int[] { i, j };
                        chessTable.mark(moves[actual][0], moves[actual][1], this, 2);
                        curX = i;
                        curY = j;
                        actual++;
                    }
                   
                }
            coordinates[2] = new int[] { curX, curY  };

            curX = posX; curY = posY;
            for (int i = posX-1; i >= 0; i--)
                for (int j = posY+1; j < 8; j++)
                {
                    if ((i - curX == -1) && (j - curY == 1) && (coordinates[3][0] <= i && coordinates[3][1] >= j))
                    {
                        moves[actual] = new int[] { i, j };
                        chessTable.mark(moves[actual][0], moves[actual][1], this, 3);
                        curX = i;
                        curY = j;
                        actual++;
                    }
                }
            coordinates[3] = new int[] { curX, curY };
            return actual;
        }

        private void rook(int actual = 0)
        {

            int curX = posX;
            for (int i = posX+1; i < 8; i++)
                if ((coordinates[4][0] >= i))
                {
                    moves[actual] = new int[] { i, posY };
                    chessTable.mark(moves[actual][0], moves[actual][1], this, 4);
                    curX = i;
                    actual++;
                }
            coordinates[4] = new int[] { curX, posY };

            int curY = posY;
            for (int i = posY - 1; i > -1; i--)
                if ((coordinates[5][1] <= i))
                {
                    moves[actual] = new int[] { posX, i };
                    chessTable.mark(moves[actual][0], moves[actual][1], this, 5);
                    curY = i;
                    actual++;
                }
            coordinates[5] = new int[] { posX, curY };
            
            curX = posX;
            for (int i = posX - 1; i > -1; i--)
                if ((coordinates[6][0] <= i))
                {
                    moves[actual] = new int[] { i, posY };
                    chessTable.mark(moves[actual][0], moves[actual][1], this, 6);
                    curX = i;
                    actual++;
                }
            coordinates[6] = new int[] { curX, posY };

            curY = posY;
            for (int i = posY + 1; i < 8; i++)
                if ((coordinates[7][1] >= i))
                {
                    moves[actual] = new int[] { posX, i };
                    chessTable.mark(moves[actual][0], moves[actual][1], this, 7);
                    curY = i;
                    actual++;
                }
            coordinates[7] = new int[] { posX, curY };

        }
        private void queen()
        {
            int a = bishop();
            int[][] bishopDirections = new int [8][];
            coordinates.CopyTo(bishopDirections, 0);

            rook(a);
            int[][] rookDirections = new int[8][];
            coordinates.CopyTo(rookDirections, 0);

            for (int i = 0; i <= 3; i++)
                coordinates[i] = bishopDirections[i];
            for (int i = 4; i <= 7; i++)
                coordinates[i] = rookDirections[i];
        }

        private void king()
        {
            moves = new int[8][];
                moves[0] = new int[] { posX - 1, posY - 1 };
                moves[1] = new int[] { posX - 1, posY };
                moves[2] = new int[] { posX - 1, posY + 1 };
                moves[3] = new int[] { posX + 1, posY };
                moves[4] = new int[] { posX + 1, posY - 1 };
                moves[5] = new int[] { posX, posY - 1 };
                moves[6] = new int[] { posX + 1, posY + 1 };
                moves[7] = new int[] { posX, posY + 1 };

            marking();
        }

        public void condition()
        {
            switch (type)
            {
                case "king":
                    king();
                    break;
                case "queen":
                    queen();
                    break;
                case "rook":
                    rook();
                    break;
                case "bishop":
                    bishop();
                    break;
                case "horse":
                    horse();
                    break;
                case "pawn":
                    pawn();
                    break;
                default:
                    //cell();
                    break;
            }
        }
    }
}
