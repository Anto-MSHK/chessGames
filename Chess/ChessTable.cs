using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class ChessTable
    {

        private Cell emptyСell = new Cell();
        public Cell[,] table = new Cell[8, 8];

        private ChessTable()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    table[i, j] = emptyСell;
                }
            }
        }

        public static ChessTable _instance = new ChessTable();

        public static ChessTable GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ChessTable();
            }
            return _instance;
        }

        public int addPiece(int x, int y, Cell piece)
        {
            if (x > 7 || y > 7)
                return 2;
            else if (x < 0 || y < 0)
                return 3;
            if (table[x, y].type == "cell")
            {
                table[x, y] = piece;
                return 0;
            }
            return 1;
        }

        public Cell[,] printPawn(int[,] moves)
        {
            ChessTable chessTable = ChessTable.GetInstance();
            Cell[,] fakeChessTable = chessTable.table;

            
            //fakeChessTable[moves[0, 0], moves[0, 1]].isHere = true;
            //fakeChessTable[moves[1, 0], moves[1, 1]].isHere = true;

            return fakeChessTable;
        }

    }
}
