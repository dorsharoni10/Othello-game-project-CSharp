using System;
using System.Collections.Generic;
using System.Linq;

namespace B19_Ex05_Alex_321336182_Dor_300109006
{
    public class Board
    {
        public enum eCellContent
        {
            Empty,
            X,
            O,
            _
        }

        private eCellContent[,] m_Map;

        public Board(int i_Size)
        {
            Size = i_Size;
            m_Map = new eCellContent[i_Size, i_Size];
        }

        public int Size { get; }

        public void SetCellContent(int i_Row, int i_Column, eCellContent i_CellContent)
        {
            m_Map[i_Row, i_Column] = i_CellContent;
        }

        public eCellContent GetCellContent(int i_Row, int i_Column)
        {
            return m_Map[i_Row, i_Column];
        }

        public void ClearBoard()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    m_Map[row, column] = eCellContent.Empty;
                }
            }
        }

        public List<Coordinate> GetAvailableTurns(eCellContent i_DiskType)
        {
            return getAvailableTurnsAndCellsToFlip(i_DiskType, null).Item1;
        }

        public List<Coordinate> GetCellsToFlip(eCellContent i_DiskType, Coordinate i_LastTurn)
        {
            return getAvailableTurnsAndCellsToFlip(i_DiskType, i_LastTurn).Item2;
        }

        // Returns Tuple of Item1 = availableTurns, Item2 = oppositeDisksToFlip
        private Tuple<List<Coordinate>, List<Coordinate>> getAvailableTurnsAndCellsToFlip(eCellContent i_DiskType, Coordinate i_LastTurn)
        {
            var validMoves = new List<Coordinate>();
            var oppositeDisk = (i_DiskType == eCellContent.O) ? eCellContent.X : eCellContent.O;
            eCellContent tempCellContent;
            var cellCandidatesToFlip = new List<Coordinate>();
            var cellsToFlip = new List<Coordinate>();
            var lastTurnRow = i_LastTurn != null ? i_LastTurn.Row : -1;
            var lastTurnColumn = i_LastTurn != null ? i_LastTurn.Column : -1;

            bool didContinue = true, didFindOppositePiece = false;
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (GetCellContent(r, c) == i_DiskType)
                    {
                        // right horizontally
                        cellCandidatesToFlip.Clear();
                        for (int i = c + 1; i < Size; i++)
                        {
                            tempCellContent = GetCellContent(r, i);
                            if (tempCellContent == eCellContent.Empty && didFindOppositePiece == false)
                            {
                                didContinue = false;
                                break;
                            }

                            if (tempCellContent == oppositeDisk && didContinue == true)
                            {
                                cellCandidatesToFlip.Add(new Coordinate(r, i));
                                didFindOppositePiece = true;
                                continue;
                            }
                            else if (tempCellContent != oppositeDisk && tempCellContent != eCellContent.Empty && didContinue == true)
                            {
                                if (lastTurnRow == r && lastTurnColumn == i)
                                {
                                    cellsToFlip.AddRange(cellCandidatesToFlip);
                                }

                                break;
                            }
                            else if (tempCellContent == eCellContent.Empty && didFindOppositePiece == true && didContinue == true)
                            {
                                validMoves.Add(new Coordinate(r, i));
                                break;
                            }
                        }

                        // left horizontally
                        didContinue = true;
                        didFindOppositePiece = false;
                        cellCandidatesToFlip.Clear();
                        for (int i = c - 1; i >= 0; i--)
                        {
                            tempCellContent = GetCellContent(r, i);
                            if (tempCellContent == eCellContent.Empty && didFindOppositePiece == false)
                            {
                                didContinue = false;
                                break;
                            }

                            if (tempCellContent == oppositeDisk && didContinue == true)
                            {
                                cellCandidatesToFlip.Add(new Coordinate(r, i));
                                didFindOppositePiece = true;
                                continue;
                            }
                            else if (tempCellContent != oppositeDisk && tempCellContent != eCellContent.Empty && didContinue == true)
                            {
                                if (lastTurnRow == r && lastTurnColumn == i)
                                {
                                    cellsToFlip.AddRange(cellCandidatesToFlip);
                                }

                                break;
                            }
                            else if (GetCellContent(r, i) == eCellContent.Empty && didFindOppositePiece == true && didContinue == true)
                            {
                                validMoves.Add(new Coordinate(r, i));
                                break;
                            }
                        }

                        // down vertically
                        didContinue = true;
                        didFindOppositePiece = false;
                        cellCandidatesToFlip.Clear();
                        for (int i = r + 1; i < Size; i++)
                        {
                            tempCellContent = GetCellContent(i, c);
                            if (tempCellContent == eCellContent.Empty && didFindOppositePiece == false)
                            {
                                didContinue = false;
                                break;
                            }

                            if (tempCellContent == oppositeDisk && didContinue == true)
                            {
                                cellCandidatesToFlip.Add(new Coordinate(i, c));
                                didFindOppositePiece = true;
                                continue;
                            }
                            else if (tempCellContent != oppositeDisk && tempCellContent != eCellContent.Empty && didContinue == true)
                            {
                                if (lastTurnRow == i && lastTurnColumn == c)
                                {
                                    cellsToFlip.AddRange(cellCandidatesToFlip);
                                }

                                break;
                            }
                            else if (tempCellContent == eCellContent.Empty && didFindOppositePiece == true && didContinue == true)
                            {
                                validMoves.Add(new Coordinate(i, c));
                                break;
                            }
                        }

                        // up vertically
                        didContinue = true;
                        didFindOppositePiece = false;
                        cellCandidatesToFlip.Clear();
                        for (int i = r - 1; i >= 0; i--)
                        {
                            tempCellContent = GetCellContent(i, c);
                            if (tempCellContent == eCellContent.Empty && didFindOppositePiece == false)
                            {
                                didContinue = false;
                                break;
                            }

                            if (tempCellContent == oppositeDisk && didContinue == true)
                            {
                                cellCandidatesToFlip.Add(new Coordinate(i, c));
                                didFindOppositePiece = true;
                                continue;
                            }
                            else if (tempCellContent != oppositeDisk && tempCellContent != eCellContent.Empty && didContinue == true)
                            {
                                if (lastTurnRow == i && lastTurnColumn == c)
                                {
                                    cellsToFlip.AddRange(cellCandidatesToFlip);
                                }

                                break;
                            }
                            else if (tempCellContent == eCellContent.Empty && didFindOppositePiece == true && didContinue == true)
                            {
                                validMoves.Add(new Coordinate(i, c));
                                break;
                            }
                        }

                        // upper left diagonally
                        didContinue = true;
                        didFindOppositePiece = false;
                        cellCandidatesToFlip.Clear();
                        for (int i = r - 1, j = c - 1; i >= 0 && j >= 0; i--, j--)
                        {
                            tempCellContent = GetCellContent(i, j);
                            if (tempCellContent == eCellContent.Empty && didFindOppositePiece == false)
                            {
                                didContinue = false;
                                break;
                            }
                            else if (tempCellContent == oppositeDisk && didContinue == true)
                            {
                                cellCandidatesToFlip.Add(new Coordinate(i, j));
                                didFindOppositePiece = true;
                                continue;
                            }
                            else if (tempCellContent != oppositeDisk && tempCellContent != eCellContent.Empty)
                            {
                                if (lastTurnRow == i && lastTurnColumn == j)
                                {
                                    cellsToFlip.AddRange(cellCandidatesToFlip);
                                }

                                break;
                            }
                            else if (tempCellContent == eCellContent.Empty && didFindOppositePiece == true)
                            {
                                validMoves.Add(new Coordinate(i, j));
                                break;
                            }
                        }

                        // upper right diagonally
                        didContinue = true;
                        didFindOppositePiece = false;
                        cellCandidatesToFlip.Clear();
                        for (int i = r - 1, j = c + 1; i >= 0 && j < Size; i--, j++)
                        {
                            tempCellContent = GetCellContent(i, j);
                            if (tempCellContent == eCellContent.Empty && didFindOppositePiece == false)
                            {
                                didContinue = false;
                                break;
                            }

                            if (tempCellContent == oppositeDisk && didContinue == true)
                            {
                                cellCandidatesToFlip.Add(new Coordinate(i, j));
                                didFindOppositePiece = true;
                                continue;
                            }
                            else if (tempCellContent != oppositeDisk && tempCellContent != eCellContent.Empty)
                            {
                                if (lastTurnRow == i && lastTurnColumn == j)
                                {
                                    cellsToFlip.AddRange(cellCandidatesToFlip);
                                }

                                break;
                            }
                            else if (tempCellContent == eCellContent.Empty && didFindOppositePiece == true)
                            {
                                validMoves.Add(new Coordinate(i, j));
                                break;
                            }
                        }

                        // lower right diagonally
                        didContinue = true;
                        didFindOppositePiece = false;
                        cellCandidatesToFlip.Clear();
                        for (int i = r + 1, j = c + 1; i < Size && j < Size; i++, j++)
                        {
                            tempCellContent = GetCellContent(i, j);
                            if (tempCellContent == eCellContent.Empty && didFindOppositePiece == false)
                            {
                                didContinue = false;
                                break;
                            }

                            if (tempCellContent == oppositeDisk && didContinue == true)
                            {
                                cellCandidatesToFlip.Add(new Coordinate(i, j));
                                didFindOppositePiece = true;
                                continue;
                            }
                            else if (tempCellContent != oppositeDisk && tempCellContent != eCellContent.Empty)
                            {
                                if (lastTurnRow == i && lastTurnColumn == j)
                                {
                                    cellsToFlip.AddRange(cellCandidatesToFlip);
                                }

                                break;
                            }
                            else if (tempCellContent == eCellContent.Empty && didFindOppositePiece == true)
                            {
                                validMoves.Add(new Coordinate(i, j));
                                break;
                            }
                        }

                        // lower left diagonally
                        didContinue = true;
                        didFindOppositePiece = false;
                        cellCandidatesToFlip.Clear();
                        for (int i = r + 1, j = c - 1; i < Size && j >= 0; i++, j--)
                        {
                            tempCellContent = GetCellContent(i, j);
                            if (tempCellContent == eCellContent.Empty && didFindOppositePiece == false)
                            {
                                didContinue = false;
                                break;
                            }

                            if (tempCellContent == oppositeDisk && didContinue == true)
                            {
                                cellCandidatesToFlip.Add(new Coordinate(i, j));
                                didFindOppositePiece = true;
                                continue;
                            }
                            else if (tempCellContent != oppositeDisk && tempCellContent != eCellContent.Empty)
                            {
                                if (lastTurnRow == i && lastTurnColumn == j)
                                {
                                    cellsToFlip.AddRange(cellCandidatesToFlip);
                                }

                                break;
                            }
                            else if (tempCellContent == eCellContent.Empty && didFindOppositePiece == true)
                            {
                                validMoves.Add(new Coordinate(i, j));
                                break;
                            }
                        }
                    }
                }
            }
            //// cellsToFlip distinct
            cellsToFlip = cellsToFlip.GroupBy(cell => (cell.Row * 10) + cell.Column).Select(duplicates => duplicates.First()).ToList();

            return new Tuple<List<Coordinate>, List<Coordinate>>(validMoves, cellsToFlip);
        }
    }
}