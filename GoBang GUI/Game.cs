using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBang_GUI
{
    public class Game
    {
        private  GoBang_Lib.Type _player;
        private GoBang_Lib.Chess[] _chesses;
        private string _name = "";


        public GoBang_Lib.Type Player
        {
            get { return _player; }
            set { _player = value; }

        }
        public GoBang_Lib.Chess[] Chesses
        {
            get { return _chesses; }
            set { _chesses = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Game(string name,GoBang_Lib.Chess[] chessboard, GoBang_Lib.Type player)
        {
            _name = name;
            _chesses= chessboard;
            _player = player;
        }
        public Game() { }
    }
}
