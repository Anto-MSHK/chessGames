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
        public int[,] moves = new int[2,2];
        private bool isAttacked;
        private bool isProtected;
        public bool isHere = false;

        public int side;
        public int NumberOf;
        public Cell(string type, int side)
        {
            this.side = side;
            this.type = type;
        }

        public Cell()
        {
            type = "cell";
            isAttacked = false;
            isProtected = false;
        }

        private void pawn()
        {
            if (side == 1)
            {
                moves[0, 0] = posX - 1;
                moves[0, 1] = posY - 1;
                moves[1, 0] = posX - 1;
                moves[1, 1] = posY + 1;
            } else if (side == 2)
            {
                moves[0, 0] = posX + 1;
                moves[0, 1] = posY - 1;
                moves[1, 0] = posX + 1;
                moves[1, 1] = posY + 1;
            }

        }

        private void horse()
        {

        }

        private void bishop()
        {

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

        public bool[] condition()
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
            return new bool[3] { isAttacked, isProtected, isHere };
        }
    }
}
