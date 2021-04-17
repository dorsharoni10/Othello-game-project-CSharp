using System.Collections.Generic;

namespace B19_Ex05_Alex_321336182_Dor_300109006
{
    public class Game
    {
        public enum ePlayerType
        {
            Human,
            PC
        }

        private const string k_FirstPlayerName = "Black";
        private const string k_SecondPlayerName = "White";

        public Game()
        {
            FirstPlayer = new HumanPlayer(k_FirstPlayerName, Board.eCellContent.X);
        }

        public IPlayer FirstPlayer { get; private set; }

        public IPlayer SecondPlayer { get; private set; }

        public Board Board { get; private set; }

        public void StartNewGame(ePlayerType i_VersusPlayerType, int i_BoardSize)
        {
            var secondPlayerType = i_VersusPlayerType;

            if (secondPlayerType == ePlayerType.Human)
            {
                SecondPlayer = new HumanPlayer(k_SecondPlayerName, Board.eCellContent.O);
            }
            else
            {
                SecondPlayer = new PcRandomPlayer(k_SecondPlayerName, Board.eCellContent.O);
            }

            initializeBoard(i_BoardSize);
        }

        public Dictionary<IPlayer, int> GetPlayersDisksCount()
        {
            int counterPlayer1 = 0, counterPlayer2 = 0;
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (Board.GetCellContent(i, j) == FirstPlayer.DiskType)
                    {
                        counterPlayer1++;
                    }
                    else if (Board.GetCellContent(i, j) == SecondPlayer.DiskType)
                    {
                        counterPlayer2++;
                    }
                }
            }

            return new Dictionary<IPlayer, int>
            {
                { FirstPlayer, counterPlayer1 },
                { SecondPlayer, counterPlayer2 }
            };
        }

        public void DoTurn(Coordinate i_Turn, Board.eCellContent i_DiskOfTurn)
        {
            Board.SetCellContent(i_Turn.Row, i_Turn.Column, i_DiskOfTurn);
            var cellsToFlip = Board.GetCellsToFlip(i_DiskOfTurn, i_Turn);
            foreach (var cell in cellsToFlip)
            {
                Board.SetCellContent(cell.Row, cell.Column, i_DiskOfTurn);
            }
        }

        private void initializeBoard(int i_BoardSize)
        {
            Board = new Board(i_BoardSize);
            var middle = Board.Size / 2;
            Board.SetCellContent(middle - 1, middle - 1, Board.eCellContent.O);
            Board.SetCellContent(middle - 1, middle, Board.eCellContent.X);
            Board.SetCellContent(middle, middle, Board.eCellContent.O);
            Board.SetCellContent(middle, middle - 1, Board.eCellContent.X);
        }
    }
}
