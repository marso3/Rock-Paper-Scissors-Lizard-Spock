using RockPaperScissorsLizardSpock.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsLizardSpock.Model
{
    public class Game:ObservableBase
    {
        Player pLeft;
        Player pRight;
        Player pWinner;

        int nRounds;
        int roundNow = 1;

        Winner winner;
        Option resultNow;

        GameState state;
        GameMode mode;

        Random rnd = new();

        bool isStreakMode = false;
        int streak = 0;

        public Player PLeft { get => pLeft; set => SetProperty(ref pLeft, value); }
        public Player PRight { get => pRight; set => SetProperty(ref pRight, value); }
        public Player PWinner { get => pWinner; set => SetProperty(ref pWinner, value); }

        public int NRounds { get => nRounds; set => SetProperty(ref nRounds, value); }
        public int RoundNow { get => roundNow; set => SetProperty(ref roundNow, value); }

        public Winner Winner { get => winner; set => SetProperty(ref winner, value); }
        public Option ResultNow { get => resultNow; set => SetProperty(ref resultNow, value); }

        public GameState State
        {
            get => state; set
            { 
                SetProperty(ref state, value);
                NotifyPropertyChanged();
            }
        }
        public GameMode Mode { get => mode; set => SetProperty(ref mode, value); }

        public bool IsStreakMode { get => isStreakMode; set => SetProperty(ref isStreakMode, value); }
        public int Streak { get => streak; set => SetProperty(ref streak, value); }

        public Option CpuSelectOption()
        {
            return (Option)rnd.Next(1, 6);
        }
    }
}
