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
        public int[][] movingPoints = new int[64][];
        public List<int[]> isAttacked = new List<int[]>();
        public List<int[]> isProtected = new List<int[]>();
        public bool isHere = false;
        public int side;
        public int NumberOf;

        public int[][] directions = new int[8][];

        private ChessTable chessTable;

        private void ResetDirections()
        {
            directions[0] = new int[] { 7, 7 };
            directions[1] = new int[] { 7, 0 };
            directions[2] = new int[] { 0, 0 };
            directions[3] = new int[] { 0, 7 };
            directions[4] = new int[] { 7, 7 };
            directions[5] = new int[] { 7, 0 };
            directions[6] = new int[] { 0, 0 };
            directions[7] = new int[] { 0, 7 };
        }

        public Cell(string type, int side, int posX, int posY) // constructor for creating shapes
        {   
            this.side = side;
            this.type = type;
            this.posX = posX;
            this.posY = posY;

            ResetDirections();

            chessTable = ChessTable.GetInstance();
        }

        public Cell(int posX, int posY) // constructor for empty cells
        {
            this.type = "cell";
            this.side = 3;
            this.posX = posX;
            this.posY = posY;
        }

<<<<<<< HEAD
=======
        public void resetCoordinates()
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
>>>>>>> 081c05da8a5c6132cec3a041e8f6602247f1daed

        private void MarkAllMovePointsOfIndexedPieces()
        {
            for (int i = 0; i < movingPoints.Length; i++)
                if (movingPoints[i] != null && (movingPoints[i][0] < 8 && movingPoints[i][1] < 8 && movingPoints[i][0] > -1 && movingPoints[i][1] > -1))
                {
                    chessTable.Mark(movingPoints[i][0], movingPoints[i][1], this, i);
                }
        }

        private void Pawn()
        {
            movingPoints = new int[2][];
            if (side == 0)
            {
                movingPoints[0] = new int[] { posX - 1, posY - 1 };
                movingPoints[1] = new int[] { posX - 1, posY + 1 };

            } else if (side == 1)
            {
                movingPoints[0] = new int[] { posX + 1, posY - 1 };
                movingPoints[1] = new int[] { posX + 1, posY + 1 };
            }
            MarkAllMovePointsOfIndexedPieces();
        }

        private void Horse()
        {
            movingPoints = new int[8][];

            movingPoints[0] = new int[] { posX - 2, posY + 1 };
            movingPoints[1] = new int[] { posX - 1, posY + 2 };
            movingPoints[2] = new int[] { posX + 2, posY + 1 };
            movingPoints[3] = new int[] { posX + 1, posY + 2 };

            movingPoints[4] = new int[] { posX - 2, posY - 1 };
            movingPoints[5] = new int[] { posX - 1, posY - 2 };
            movingPoints[6] = new int[] { posX + 2, posY - 1 };
            movingPoints[7] = new int[] { posX + 1, posY - 2 };

            MarkAllMovePointsOfIndexedPieces();
        }

        private void King()
        {
            movingPoints = new int[8][];

            movingPoints[0] = new int[] { posX - 1, posY - 1 };
            movingPoints[1] = new int[] { posX - 1, posY };
            movingPoints[2] = new int[] { posX - 1, posY + 1 };
            movingPoints[3] = new int[] { posX + 1, posY };

            movingPoints[4] = new int[] { posX + 1, posY - 1 };
            movingPoints[5] = new int[] { posX, posY - 1 };
            movingPoints[6] = new int[] { posX + 1, posY + 1 };
            movingPoints[7] = new int[] { posX, posY + 1 };

            MarkAllMovePointsOfIndexedPieces();
        }

      
        private int Bishop()
        {
            int totalCountsOfMovingPoints = 0;

            int nextX = posX, nextY = posY;

            void NextPosInDirectionAndMark(int x, int y, int direction) { 
                movingPoints[totalCountsOfMovingPoints] = new int[] { x, y };
                chessTable.Mark(movingPoints[totalCountsOfMovingPoints][0], movingPoints[totalCountsOfMovingPoints][1], this, direction);
                nextX = x;
                nextY = y;
                totalCountsOfMovingPoints++;
            }

           
            for (int i = posX+1; i < 8; i++)
                for (int j = posY+1; j < 8; j++)
                {
                    if ((i - nextX == 1) && (j - nextY == 1) && (directions[0][0] >= i && directions[0][1] >= j)) {
                        NextPosInDirectionAndMark(i, j, 0);
                    }
                }
            directions[0] = new int[] { nextX, nextY };

            nextX = posX; nextY = posY;
            for (int i = posX+1; i < 8; i++)
                for (int j = posY-1; j >= 0; j--)
                {
                    if((i - nextX == 1) && (j - nextY == -1) && (directions[1][0] >= i && directions[1][1] <= j)) {
                        NextPosInDirectionAndMark(i, j, 1);
                    }
                }
            directions[1] = new int[] { nextX, nextY };

            nextX = posX; nextY = posY;
            for (int i = posX-1; i >= 0; i--)
                for (int j = posY-1; j >= 0; j--)
                {
                    if ((i - nextX == -1) && (j - nextY == -1) && (directions[2][0] <= i && directions[2][1] <= j))
                    {
                        NextPosInDirectionAndMark(i, j, 2);
                    }
                   
                }
            directions[2] = new int[] { nextX, nextY  };

            nextX = posX; nextY = posY;
            for (int i = posX-1; i >= 0; i--)
                for (int j = posY+1; j < 8; j++)
                {
                    if ((i - nextX == -1) && (j - nextY == 1) && (directions[3][0] <= i && directions[3][1] >= j))
                    {
                        NextPosInDirectionAndMark(i, j, 3);
                    }
                }
            directions[3] = new int[] { nextX, nextY };
            return totalCountsOfMovingPoints;
        }

        private void Rook(int totalCountsWithBishop = 0) // the transmitted parameter will not be zero only if the queen's moves are calculated
        {
            int totalCountsOfMovingPoints = totalCountsWithBishop;
            int nextX = posX, nextY = posY;

            void NextPosInDirectionAndMark(int valueI, bool isX, int direction)
            {
                if (isX) {
                    movingPoints[totalCountsOfMovingPoints] = new int[] { valueI, posY };
                    nextX = valueI;
                }
                else {
                    movingPoints[totalCountsOfMovingPoints] = new int[] { posX, valueI };
                    nextY = valueI;
                }
                chessTable.Mark(movingPoints[totalCountsOfMovingPoints][0], movingPoints[totalCountsOfMovingPoints][1], this, direction);
                totalCountsOfMovingPoints++;
            }

            for (int i = posX+1; i < 8; i++)
                if ((directions[4][0] >= i)) { 
                   NextPosInDirectionAndMark(i, true, 4);
                }

            directions[4] = new int[] { nextX, posY };

            nextY = posY;

            for (int i = posY - 1; i > -1; i--)
                if ((directions[5][1] <= i))
                {
                    NextPosInDirectionAndMark(i, false, 5);
                }

            directions[5] = new int[] { posX, nextY };
            
            nextX = posX;
            for (int i = posX - 1; i > -1; i--)
                if ((directions[6][0] <= i))
                {
                    NextPosInDirectionAndMark(i, true, 6);
                }

            directions[6] = new int[] { nextX, posY };

            nextY = posY;
            for (int i = posY + 1; i < 8; i++)
                if ((directions[7][1] >= i))
                {
                    NextPosInDirectionAndMark(i, false, 7);
                }

            directions[7] = new int[] { posX, nextY };

        }
        private void Queen()
        {
            int a = Bishop();
            int[][] bishopDirections = new int [8][];
            directions.CopyTo(bishopDirections, 0);

            Rook(a);
            int[][] rookDirections = new int[8][];
            directions.CopyTo(rookDirections, 0);

            for (int i = 0; i <= 3; i++)
                directions[i] = bishopDirections[i];
            for (int i = 4; i <= 7; i++)
                directions[i] = rookDirections[i];
        }


        public void condition()
        {
            switch (type)
            {
                case "king":
                    King();
                    break;
                case "queen":
                    Queen();
                    break;
                case "rook":
                    Rook();
                    break;
                case "bishop":
                    Bishop();
                    break;
                case "horse":
                    Horse();
                    break;
                case "pawn":
                    Pawn();
                    break;
                default:
                    //cell();
                    break;
            }
        }
    }
}
