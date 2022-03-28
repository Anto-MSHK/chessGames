﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class ChessTable
    {

        public Cell[,] table = new Cell[8, 8];

        private ChessTable()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    table[i, j] = new Cell(i, j);
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

        private void updating()
        {
            for(int i = 0; i < 8; i++){
                for (int j = 0; j < 8; j++)
                {
                    table[i, j].condition();
                }
            }
        }

        public int addPiece(int x, int y, Cell piece)
        {
            if (x > 7 || y > 7)
                return 2;
            else if (x < 0 || y < 0)
                return 3;
            if (table[x, y].type == "cell")
            { 
                if (piece.type == "pawn" && piece.side == 0 && x == 0)
                {
                    return 4;
                } else if (piece.type == "pawn" && piece.side == 1 && x == 7)
                {
                    return 4;
                }
                else {
                    table[x, y] = piece;
                    piece.posX = x;
                    piece.posY = y;
                    updating();
                    return 0;
                }
            }
            return 1;
            
        }
        public void mark (int x, int y, Cell pieceInitiator, int dir)
        {
            Cell pieceMarked = table[x, y];

            Cell pieceObsolete = table[pieceInitiator.coordinates[dir][0], pieceInitiator.coordinates[dir][1]];

            if(pieceInitiator.type == "bishop" || pieceInitiator.type == "rook" || pieceInitiator.type == "queen") { 
                pieceObsolete.isProtected = false;
                pieceObsolete.isAttacked = false;
            }

            if (pieceInitiator.side == pieceMarked.side) {
                pieceMarked.isProtected = true;
                pieceInitiator.coordinates[dir] = new int[] { x, y };
            }
            if (pieceInitiator.side != pieceMarked.side && pieceMarked.side != 3) {
                pieceMarked.isAttacked = true;
                pieceInitiator.coordinates[dir] = new int[] { x, y };
            }
            else
                pieceMarked.isHere = true;
        }

    }
}
