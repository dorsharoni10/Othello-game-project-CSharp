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
    public partial class GameSettings : Form
    {
        private const string k_SixSize = "Board Size: 6x6 (click to increase)";
        private const string k_EightSize = "Board Size: 8x8 (click to increase)";
        private const string k_TenSize = "Board Size: 10x10 (click to increase)";
        private const string k_TwelveSize = "Board Size: 12x12 (click to reset)";
        private const int k_Small = 6;
        private const int k_Medium = 8;
        private const int k_Large = 10;
        private const int k_XL = 12;

        private int m_SizeBoardUserSelect = 6;
        private bool m_isPlayAgainstFriend;

        public GameSettings()
        {
            InitializeComponent();
        }

        private void buttonChangeBoardSize_Click(object sender, EventArgs e)
        {
            switch (m_SizeBoardUserSelect)
            {
                case k_Small:
                    {
                        buttonChangeBoardSize.Text = k_EightSize;
                        m_SizeBoardUserSelect = k_Medium;
                        break;
                    }

                case k_Medium:
                    {
                        buttonChangeBoardSize.Text = k_TenSize;
                        m_SizeBoardUserSelect = k_Large;
                        break;
                    }

                case k_Large:
                    {
                        buttonChangeBoardSize.Text = k_TwelveSize;
                        m_SizeBoardUserSelect = k_XL;
                        break;
                    }

                case k_XL:
                    {
                        m_SizeBoardUserSelect = k_Small;
                        buttonChangeBoardSize.Text = k_SixSize;
                        break;
                    }
            }
        }

        private void buttonPlayAgainstFriend_Click(object sender, EventArgs e)
        {
            m_isPlayAgainstFriend = true;
            this.Hide();
            GameBoardUI gameBoardUI = new GameBoardUI(m_SizeBoardUserSelect, m_isPlayAgainstFriend);
            this.Close();
        }

        private void buttonPlayAgainstComputer_Click(object sender, EventArgs e)
        {
            m_isPlayAgainstFriend = false;
            this.Hide();
            GameBoardUI gameBoardUI = new GameBoardUI(m_SizeBoardUserSelect, m_isPlayAgainstFriend);
            this.Close();
        }
    }
}
