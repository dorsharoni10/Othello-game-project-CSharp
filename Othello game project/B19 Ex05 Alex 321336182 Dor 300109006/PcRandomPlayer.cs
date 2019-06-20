using System;
using System.Collections.Generic;
using System.Linq;

namespace B19_Ex05_Alex_321336182_Dor_300109006
{
    public class PcRandomPlayer : IPlayer
    {
        private Random m_Random;

        public PcRandomPlayer(string i_Name, Board.eCellContent i_DiskType)
        {
            Name = i_Name;
            DiskType = i_DiskType;
            m_Random = new Random(DateTime.Now.Millisecond);
        }

        public string Name { get; }

        public Board.eCellContent DiskType { get; }

        public Coordinate GetPlayerTurn(List<Coordinate> i_AvailableTurns)
        {
            return i_AvailableTurns.ElementAt(m_Random.Next(0, i_AvailableTurns.Count));
        }
    }
}
