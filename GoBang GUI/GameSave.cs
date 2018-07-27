using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GoBang_GUI
{
   



     

    [Serializable]
    public class GameSave:INotifyPropertyChanged
    {
        private ObservableCollection<Game> _games = new ObservableCollection<Game>();
        private ObservableCollection<string> _games_name = new ObservableCollection<string>();

        public Game SelectedGames;



        public GameSave()
        {
            SelectedGames = new Game();
        }



        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set
            {
                _games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        public ObservableCollection<string> GamesName
        {
            get { return _games_name ; }
            set
            {
                _games_name = value;
                OnPropertyChanged(nameof(GamesName));
            }
        }




        public void AddGame(Game game)
        {
            if (_games.Contains(game))
                return;
            _games.Add(game);
            _games_name.Add(game.Name);
            OnPropertyChanged("Games");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
