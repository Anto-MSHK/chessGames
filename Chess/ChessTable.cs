using System;
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

        public int AddPiece(int x, int y, Cell piece)
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
<<<<<<< HEAD
        public void Mark (int x, int y, Cell pieceInitiator, int dir)
=======

        public int deletePiece(int x, int y)
        {
            if (x > 7 || y > 7)
                return 2;
            else if (x < 0 || y < 0)
                return 3;
            if (table[x, y].type != "cell")
            {
                deleteAllMarkersPiece(table[x, y]);
                Cell piece = new Cell(x, y);
                table[x, y] = piece;
                piece.posX = x;
                piece.posY = y;
                updating();
                return 0;
            }
            return 1;
        }

        public void mark (int x, int y, Cell pieceInitiator, int dir)
>>>>>>> 081c05da8a5c6132cec3a041e8f6602247f1daed
        {
            Cell pieceMarked = table[x, y];

            Cell pieceObsolete = table[pieceInitiator.directions[dir][0], pieceInitiator.directions[dir][1]];

            int[] coordInitiator = new int[] { pieceInitiator.posX, pieceInitiator.posY };

            int[] currentCoord = pieceMarked.isProtected.Find(coord => coord != null && coord.SequenceEqual(coordInitiator));
            if (pieceInitiator.side == pieceMarked.side && currentCoord == null)
            {
                pieceMarked.isProtected.Add(new int[] { pieceInitiator.posX, pieceInitiator.posY });
<<<<<<< HEAD
                pieceInitiator.directions[dir] = new int[] { x, y };
                int obsolete = pieceObsolete.isProtected.FindIndex(coord => coord != null && coord.SequenceEqual(coordInitiator));
                if(obsolete != -1 && pieceObsolete.directions[dir] != pieceMarked.directions[dir])
                    pieceObsolete.isProtected[obsolete] = null;

=======
                pieceInitiator.coordinates[dir] = new int[] { x, y };
>>>>>>> 081c05da8a5c6132cec3a041e8f6602247f1daed
            }
            currentCoord = pieceMarked.isAttacked.Find(coord => coord != null && coord.SequenceEqual(coordInitiator));
            if (pieceInitiator.side != pieceMarked.side && pieceMarked.side != 3)
            {
                pieceMarked.isAttacked.Add(new int[] { pieceInitiator.posX, pieceInitiator.posY });
<<<<<<< HEAD
                pieceInitiator.directions[dir] = new int[] { x, y };
                int obsolete = pieceObsolete.isAttacked.FindIndex(coord => coord != null && coord.SequenceEqual(coordInitiator));
                if (obsolete != -1 && pieceObsolete.directions[dir] != pieceMarked.directions[dir])
                    pieceObsolete.isAttacked[obsolete] = null;
=======
                pieceInitiator.coordinates[dir] = new int[] { x, y };
>>>>>>> 081c05da8a5c6132cec3a041e8f6602247f1daed
            }
            else
                pieceMarked.isHere = true;

            int obsolete = pieceObsolete.isProtected.FindIndex(coord => coord != null && coord.SequenceEqual(coordInitiator));
            if (obsolete != -1 && pieceObsolete.coordinates[dir] != pieceMarked.coordinates[dir])
                pieceObsolete.isProtected.RemoveAt(obsolete);

            obsolete = pieceObsolete.isAttacked.FindIndex(coord => coord != null && coord.SequenceEqual(coordInitiator));
            if (obsolete != -1 && pieceObsolete.coordinates[dir] != pieceMarked.coordinates[dir])
                pieceObsolete.isAttacked.RemoveAt(obsolete);

           
        }

        public void deleteAllMarkersPiece(Cell cell)
        {
            int[] posCell = new int[] { cell.posX, cell.posY };
            foreach (int[] coord in cell.coordinates)
            {
                int i = table[coord[0], coord[1]].isProtected.FindIndex(del => del != null && del.SequenceEqual(posCell));
                if (i != -1) { 
                    table[coord[0], coord[1]].isProtected.RemoveAt(i);
                }
                int j = table[coord[0], coord[1]].isAttacked.FindIndex(del => del != null && del.SequenceEqual(posCell));
                if (j != -1)
                {
                    table[coord[0], coord[1]].isAttacked.RemoveAt(j);
                }

            }

            List<int[]> actualDel = new List<int[]>();
            foreach (int[] coordDel in cell.isProtected)
                if (coordDel != null)
                    actualDel.Add(new int[] { coordDel[0], coordDel[1] });

            foreach (int[] coordDel in actualDel)
            {
                table[coordDel[0], coordDel[1]].resetCoordinates();
                table[coordDel[0], coordDel[1]].condition();
            }

            actualDel = new List<int[]>();

            foreach (int[] coordDel in cell.isAttacked)
                if (coordDel != null)
                    actualDel.Add(new int[] { coordDel[0], coordDel[1] });

            foreach (int[] coordDel in actualDel)
            {
                table[coordDel[0], coordDel[1]].resetCoordinates();
                table[coordDel[0], coordDel[1]].condition();
            }
        }

    }
}
