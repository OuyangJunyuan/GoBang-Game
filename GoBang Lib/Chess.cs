using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace GoBang_Lib
{
    [Serializable]
    public class Chess
    {
        public int number=0;
        public Type type=GoBang_Lib.Type.Empety;
        public bool isnew = false;
        public int x, y;

        public Chess( Type typeInit,int numberInit,bool isnewInit,int col,int row)
        {
            number = numberInit;
            type = typeInit;
            isnew = isnewInit;
            x = col;
            y = row;      
        }
        public Chess()
        {
            x = 0;
        }
    }
    [Serializable]
    public enum ComputerSkillLevel
    {
        Dumb,
        Good,
        Cheats
    }

}
