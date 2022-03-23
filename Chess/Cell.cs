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
        public int[][] moves;
        public bool isAttacked;
        public bool isProtected;
        public bool isHere = false;

        public int side;
        public int NumberOf;

        public int[][] coordinates= new int [4][];

        private ChessTable chessTable;
        public Cell(string type, int side, int posX, int posY)
        {   
            this.side = side;
            this.type = type;
            this.posX = posX;
            this.posY = posY;

            coordinates[0] = new int[] { -1, -1 };
            coordinates[1] = new int[] { -1, -1 };
            coordinates[2] = new int[] { -1, -1 };
            coordinates[3] = new int[] { -1, -1 };


            chessTable = ChessTable.GetInstance();
        }

        public Cell(int posX, int posY)
        {
            type = "cell";
            side = 3;
            isAttacked = false;
            isProtected = false;
            this.posX = posX;
            this.posY = posY;

            coordinates[0] = new int[] { -1, -1 };
            coordinates[1] = new int[] { -1, -1 };
            coordinates[2] = new int[] { -1, -1 };
            coordinates[3] = new int[] { -1, -1 };
        }

        private void marking()
        {
            for (int i = 0; i < moves.Length; i++)
                if (moves[i] != null && (moves[i][0] != 8 && moves[i][1] != 8 && moves[i][0] != -1 && moves[i][1] != -1))
                {
                    chessTable.mark(moves[i][0], moves[i][1], this, 0);
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

        private void bishop()
        {
            int actual = 0;

            moves = new int[64][];

            for (int i = posX+1; i < 8; i++)
            {
                for (int j = posY+1; j < 8; j++)
                {
                    if((i == j)) {
                        moves[actual] = new int[] { i, j };
                        chessTable.mark(moves[actual][0], moves[actual][1], this, 0);
                        actual++;
                    }
                }
            }
            
            for (int i = posX+1; i < 8; i++)
            {
                for (int j = posY-1; j >= 0; j--)
                {
                    if(i + j == 6) { 
                        moves[actual] = new int[] { i, j };
                        chessTable.mark(moves[actual][0], moves[actual][1], this, 1);
                        actual++;
                    }
                }
            }
           
            for (int i = posX-1; i >= 0; i--)
            {
                for (int j = posY-1; j >= 0; j--)
                {
                    if ((i == j))
                    {
                        moves[actual] = new int[] { i, j };
                        chessTable.mark(moves[actual][0], moves[actual][1], this, 2);
                        actual++;
                    }
                   
                }
            }
            
            for (int i = posX-1; i >= 0; i--)
            {
                for (int j = posY+1; j < 8; j++)
                {
                    if (i + j == 6)
                    {
                        moves[actual] = new int[] { i, j };
                        chessTable.mark(moves[actual][0], moves[actual][1], this, 3);
                        actual++;
                    }
                }
            }
            
        }

        private void rook()
        {

        }
        private void queen()
        {

        }

        private void king()
        {

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
