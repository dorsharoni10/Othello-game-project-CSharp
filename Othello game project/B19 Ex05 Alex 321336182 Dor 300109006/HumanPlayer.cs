using System;
using System.Collections.Generic;

namespace B19_Ex05_Alex_321336182_Dor_300109006
{
    public class HumanPlayer : IPlayer
    {
        public HumanPlayer(string i_Name, Board.eCellContent i_DiskType)
        {
            Name = i_Name;
            DiskType = i_DiskType
        }

        public string Name { get; }

        public Board.eCellContent DiskType { get; }
    }
}
