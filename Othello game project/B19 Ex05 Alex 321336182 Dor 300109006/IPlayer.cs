using System.Collections.Generic;

namespace B19_Ex05_Alex_321336182_Dor_300109006
{
    public interface IPlayer
    {
        string Name { get; }

        Board.eCellContent DiskType { get; }
    }
}
