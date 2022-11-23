using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsLizardSpock.Model
{
    public enum Option
    {
        Unchosen, Rock, Paper, Scissors, Lizard, Spock
    }
    public enum GameState
    {
        Configuration, 
        Playing, 
        ShowingHands, 
        Final 
    }
    public enum Winner
    {
        Left, Right, Draw
    }
    public enum GameMode
    {
        SinglePlayer, Multiplayer
    }
}
