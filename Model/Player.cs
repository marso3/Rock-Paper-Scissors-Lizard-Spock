using RockPaperScissorsLizardSpock.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RockPaperScissorsLizardSpock.Model
{
    public class Player:ObservableBase
    {
        string name = "";
        string surname = "";
        bool isValid = false;
        bool isReady = false;
        int nVictories;
        Option optionChosen;
        Color colorChosen = Color.FromArgb(255, 100, 100, 100);

        public string Name { get => name; 
            set
            { 
                SetProperty(ref name, value);
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("CompleteName");
                ValidatePlayer();
            } 
        }
        public string Surname { get => surname; 
            set 
            { 
                SetProperty(ref surname, value);
                NotifyPropertyChanged("Surname");
                NotifyPropertyChanged("CompleteName");
                ValidatePlayer();
            } 
        }
        public string CompleteName
        {
            get => Name + " " + Surname;
        }
        public bool IsValid { get => isValid;
            set
            {
                SetProperty(ref isValid, value);
                NotifyPropertyChanged();
            }
        }
        public bool IsReady
        {
            get => isReady;
            set
            {
                SetProperty(ref isReady, value);
                NotifyPropertyChanged();
            }
        }
        public int NVictories { get => nVictories; set => SetProperty(ref nVictories, value); }
        public Option OptionChosen
        {
            get => optionChosen;
            set 
            { 
                SetProperty(ref optionChosen, value);
                if (optionChosen != Option.Unchosen)
                    IsReady = true;
                else
                    IsReady = false;
            }
        }
        public Color ColorChosen
        {
            get => colorChosen;
            set
            {
                SetProperty(ref colorChosen, value);
                NotifyPropertyChanged();
            }
        }
        private void ValidatePlayer()
        {
            IsValid = !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname);
        }
    }
}
