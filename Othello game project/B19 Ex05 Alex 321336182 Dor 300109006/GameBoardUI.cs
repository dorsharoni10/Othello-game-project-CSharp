using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B19_Ex05_Alex_321336182_Dor_300109006
{
    public partial class GameBoardUI : Form
    {
        private const string k_MessegeBlackTurn = "Othello – Black’s Turn";
        private const string k_MessegeWhiteTurn = "Othello – White’s Turn";
        private static int s_NumberOfTimeBlackWin = 0;
        private static int s_NumberOfTimeWhiteWin = 0;
        private readonly Dictionary<Board.eCellContent, Color> r_DiskColorMap = new Dictionary<Board.eCellContent, Color>
        {
            { Board.eCellContent.X, Color.Black },
            { Board.eCellContent.O, Color.White },
            { Board.eCellContent._, Color.Green },
            { Board.eCellContent.Empty, Color.Transparent }
        };

        private int m_SizeOfMatrixButtons;
        private Button[,] m_ButtonsGameMatrix;
        private Game m_Game;
        private bool m_gameFinished = false;
        private IPlayer m_CurrentPlayer;
        private bool m_IsAgainstFriend;
     
        public GameBoardUI(int i_SizeOfMatrixButtons, bool i_IsAgainstFriend)
        {
            this.m_SizeOfMatrixButtons = i_SizeOfMatrixButtons;
            m_ButtonsGameMatrix = new Button[m_SizeOfMatrixButtons, m_SizeOfMatrixButtons];
            this.m_IsAgainstFriend = i_IsAgainstFriend;
            for (int row = 0; row < m_SizeOfMatrixButtons; row++)
            {
                for (int column = 0; column < m_SizeOfMatrixButtons; column++)
                {
                    Button button = new Button();
                    button.Size = new Size(new Point(50, 50));
                    button.Location = new Point((50 * column) + 10, (50 * row) + 10);
                    button.AutoSize = true;
                    button.Click += matrixButtonClick;
                    m_ButtonsGameMatrix[row, column] = button;
                    this.Controls.Add(button);
                }
            }

            InitializeComponent();
            initSizeBoard(m_SizeOfMatrixButtons);
            m_gameFinished = false;
            m_Game = new Game();
            m_Game.StartNewGame(i_IsAgainstFriend ? Game.ePlayerType.Human : Game.ePlayerType.PC, i_SizeOfMatrixButtons);
            m_CurrentPlayer = m_Game.FirstPlayer;
            drawBoard(m_Game.Board, m_Game.Board.GetAvailableTurns(m_CurrentPlayer.DiskType));
            this.Text = k_MessegeBlackTurn;
            this.ShowDialog();
        }

        private void setAsAvailableForTurn(List<Coordinate> i_coordinatesToUpdate)
        {
            foreach (var coordinate in i_coordinatesToUpdate)
            {
                setAsAvailableForTurn(coordinate.Row, coordinate.Column);
            }
        }

        private void setAsAvailableForTurn(int i_Row, int i_Column)
        {
            m_ButtonsGameMatrix[i_Row, i_Column].BackColor = Color.Green;
            m_ButtonsGameMatrix[i_Row, i_Column].ForeColor = Color.Green;
            m_ButtonsGameMatrix[i_Row, i_Column].Text = string.Empty;
            m_ButtonsGameMatrix[i_Row, i_Column].Enabled = true;
        }

        private void drawBoard(Board i_Board, List<Coordinate> i_AvailableForTurn)
        {
            clearAvailableForTurn();

            for (var row = 0; row < m_SizeOfMatrixButtons; row++)
            {
                for (var column = 0; column < m_SizeOfMatrixButtons; column++)
                {
                    var cellDiskOnBoard = i_Board.GetCellContent(row, column);
                    var cellColorOnBoard = r_DiskColorMap[cellDiskOnBoard];

                    if (cellDiskOnBoard == Board.eCellContent.Empty)
                    {
                        m_ButtonsGameMatrix[row, column].BackColor = Color.Transparent;
                        m_ButtonsGameMatrix[row, column].Enabled = false;
                    }
                    else
                    {
                        m_ButtonsGameMatrix[row, column].BackColor = cellColorOnBoard;
                        m_ButtonsGameMatrix[row, column].ForeColor = cellColorOnBoard == Color.Black ? Color.White : Color.Black;
                        m_ButtonsGameMatrix[row, column].Enabled = true;
                        m_ButtonsGameMatrix[row, column].Text = "O";
                    }
                }
            }

            setAsAvailableForTurn(i_AvailableForTurn);
        }

        private void clearAvailableForTurn()
        {
            for (var row = 0; row < m_SizeOfMatrixButtons; row++)
            {
                for (var column = 0; column < m_SizeOfMatrixButtons; column++)
                {
                    if (m_ButtonsGameMatrix[row, column].BackColor == Color.Green)
                    {
                        m_ButtonsGameMatrix[row, column].BackColor = Color.Transparent;
                    }
                }
            }
        }

        private void initSizeBoard(int i_SizeOfMatrixButtons)
        {
            switch (i_SizeOfMatrixButtons)
            {
                case 6:
                    this.Size = new Size(260, 300);
                    break;
                case 8:
                    this.Size = new Size(335, 380);
                    break;
                case 10:
                    this.Size = new Size(405, 460);
                    break;
                case 12:
                    this.Size = new Size(485, 540);
                    break;
            }
        }

        private void matrixButtonClick(object sender, EventArgs e)
        {
            var buttonCoordinate = getButtonCoordinate(sender as Button);
            int row = buttonCoordinate.Item1;
            int column = buttonCoordinate.Item2;

            if (m_Game.Board.GetAvailableTurns(m_CurrentPlayer.DiskType)
                    .Any(coordinate => coordinate.Row == row && coordinate.Column == column) == false)
            {
                return;
            }

            m_Game.DoTurn(new Coordinate(row, column), m_CurrentPlayer.DiskType);

            m_CurrentPlayer = m_CurrentPlayer.Name == m_Game.FirstPlayer.Name
                ? m_Game.SecondPlayer
                : m_Game.FirstPlayer;

            if (m_CurrentPlayer is PcRandomPlayer)
            {
                var pcAvailableTurns = m_Game.Board.GetAvailableTurns(m_CurrentPlayer.DiskType);

                if (pcAvailableTurns.Count != 0)
                {
                    var pcPlayerTurn = (m_CurrentPlayer as PcRandomPlayer).GetPlayerTurn(pcAvailableTurns);
                    m_Game.DoTurn(new Coordinate(pcPlayerTurn.Row, pcPlayerTurn.Column), m_CurrentPlayer.DiskType);
                }

                m_CurrentPlayer = m_Game.FirstPlayer;
            }

            var currentPlayerAvailableTurns = m_Game.Board.GetAvailableTurns(m_CurrentPlayer.DiskType);
            if (currentPlayerAvailableTurns.Count == 0)
            {
                m_CurrentPlayer = m_CurrentPlayer.Name == m_Game.FirstPlayer.Name
                    ? m_Game.SecondPlayer
                    : m_Game.FirstPlayer;

                currentPlayerAvailableTurns = m_Game.Board.GetAvailableTurns(m_CurrentPlayer.DiskType);
                if (currentPlayerAvailableTurns.Count == 0)
                {
                    m_gameFinished = true;
                }
            }

            if(m_CurrentPlayer.Name.ToString().ToLower() == "black")
            {
                this.Text = k_MessegeBlackTurn;
            }
            else
            {
                this.Text = k_MessegeWhiteTurn;
            }

            drawBoard(m_Game.Board, currentPlayerAvailableTurns);
            gameFinish();
        }

        private void gameFinish()
        {
            var playersDisksCount = m_Game.GetPlayersDisksCount();
            int max = 0, min = 0, valueWhite = 0, valueBlack = 0;
            string playerNameWin = "Nobody";

            if (m_gameFinished)
            {
                foreach (var player in playersDisksCount)
                {    
                    if (max < player.Value)
                    {
                        max = player.Value;
                        playerNameWin = player.Key.Name;
                    }

                    if (player.Key.Name.ToLower() == "white")
                    {
                        valueWhite = player.Value;
                    }
                    else
                    {
                        valueBlack = player.Value;
                    }
                }

                if (playerNameWin.ToLower() == "white")
                {
                    s_NumberOfTimeWhiteWin++;
                }
                else if (playerNameWin.ToLower() == "black")
                {
                    s_NumberOfTimeBlackWin++;
                }

                min = Math.Min(valueWhite, valueBlack);
                max = Math.Max(valueWhite, valueBlack);

                var playerChoice = MessageBox.Show($"{playerNameWin} Won!! ({max}/{min}) ({s_NumberOfTimeBlackWin}/{s_NumberOfTimeWhiteWin})\nWould you like another round?", "Game Finished", MessageBoxButtons.YesNo);
                playAgain(playerChoice.ToString());
            }
        }

        private void playAgain(string i_PlayerChoice)
        {
            if (i_PlayerChoice.ToLower() == "yes")
            {
                this.Hide();
                GameBoardUI game = new GameBoardUI(m_SizeOfMatrixButtons, m_IsAgainstFriend);
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private Tuple<int, int> getButtonCoordinate(Button i_Button)
        {
            Tuple<int, int> tuple = null;
            for (int row = 0; row < m_SizeOfMatrixButtons; row++)
            {
                for (int column = 0; column < m_SizeOfMatrixButtons; column++)
                {
                    if (m_ButtonsGameMatrix[row, column].Equals(i_Button))
                    {
                        tuple = new Tuple<int, int>(row, column);
                    }
                }
            }

            return tuple;
        }
    }
}
