using RandomNameGeneratorLibrary;
using RockPaperScissorsLizardSpock.Model;
using RockPaperScissorsLizardSpock.Model.Infrastructure;
using RockPaperScissorsLizardSpock.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RockPaperScissorsLizardSpock.ViewModel
{
    public class RPScLSViewModel:ObservableBase
    {
        #region Attributes
        Game game;
        string message = "";
        PersonNameGenerator personGenerator = new PersonNameGenerator();
        Dictionary<GameState, string> messages;
        Visibility configurationVisibility;
        Visibility playingVisibility;
        Visibility finalVisibility;
        Visibility showHandsVisibility;
        Visibility streakModeVisibility;
        #endregion

        #region Constructors
        public RPScLSViewModel()
        {
            game = new Game();
            game.PLeft = new Player
            {
                Name = "",
                Surname = "",
                ColorChosen = new()
            };
            game.NRounds = 3;
            SelectMultiPlayerMode();
            InitializeMessages();
            CreateCommands();
        }
        #endregion

        #region Initialization and Logic Methods
        private void InitializeMessages()
        {
            messages = new Dictionary<GameState, string>();
            messages.Add(GameState.Configuration, "Fill the names and colors of the players");
            messages.Add(GameState.Playing, "Pick an option and play");
            messages.Add(GameState.ShowingHands, "The winner this round is: ");
            messages.Add(GameState.Final, "The game result is: ");
            ChangeState(GameState.Configuration);
        }
        private void ChangeState(GameState gameState)
        {
            game.State = gameState;
            Message = messages[gameState];
            if (game.IsStreakMode)
                streakModeVisibility = Visibility.Visible;
            else
                streakModeVisibility = Visibility.Collapsed;

            if (gameState == GameState.Configuration)
            {
                ConfigurationVisibility = Visibility.Visible;
                PlayingVisibility = Visibility.Collapsed;
                ShowHandsVisibiity = Visibility.Collapsed;
                FinalVisibility = Visibility.Collapsed;
            }
            else if (gameState == GameState.Playing)
            {
                ConfigurationVisibility = Visibility.Collapsed;
                PlayingVisibility = Visibility.Visible;
                ShowHandsVisibiity = Visibility.Collapsed;
                FinalVisibility = Visibility.Collapsed;
            }
            else if (gameState == GameState.ShowingHands)
            {
                ConfigurationVisibility = Visibility.Collapsed;
                PlayingVisibility = Visibility.Visible;
                ShowHandsVisibiity = Visibility.Visible;
                FinalVisibility = Visibility.Collapsed;
            }
            else if (gameState == GameState.Final)
            {
                ConfigurationVisibility = Visibility.Collapsed;
                PlayingVisibility = Visibility.Collapsed;
                ShowHandsVisibiity = Visibility.Collapsed;
                FinalVisibility = Visibility.Visible;
            }
        }
        private void CheckVictor()
        {
            if (game.PLeft.OptionChosen == game.PRight.OptionChosen)
                game.Winner = Winner.Draw;
            else
            {
                switch (game.PLeft.OptionChosen)
                {
                    case Option.Rock:
                        switch (game.PRight.OptionChosen)
                        {
                            case Option.Paper:
                                game.Winner = Winner.Right;
                                break;
                            case Option.Scissors:
                                game.Winner = Winner.Left;
                                break;
                            case Option.Lizard:
                                game.Winner = Winner.Left;
                                break;
                            case Option.Spock:
                                game.Winner = Winner.Right;
                                break;
                        }
                        break;
                    case Option.Paper:
                        switch (game.PRight.OptionChosen)
                        {
                            case Option.Rock:
                                game.Winner = Winner.Left;
                                break;
                            case Option.Scissors:
                                game.Winner = Winner.Right;
                                break;
                            case Option.Lizard:
                                game.Winner = Winner.Right;
                                break;
                            case Option.Spock:
                                game.Winner = Winner.Left;
                                break;
                        }
                        break;
                    case Option.Scissors:
                        switch (game.PRight.OptionChosen)
                        {
                            case Option.Rock:
                                game.Winner = Winner.Right;
                                break;
                            case Option.Paper:
                                game.Winner = Winner.Left;
                                break;
                            case Option.Lizard:
                                game.Winner = Winner.Left;
                                break;
                            case Option.Spock:
                                game.Winner = Winner.Right;
                                break;
                        }
                        break;
                    case Option.Lizard:
                        switch (game.PRight.OptionChosen)
                        {
                            case Option.Rock:
                                game.Winner = Winner.Right;
                                break;
                            case Option.Paper:
                                game.Winner = Winner.Left;
                                break;
                            case Option.Scissors:
                                game.Winner = Winner.Right;
                                break;
                            case Option.Spock:
                                game.Winner = Winner.Left;
                                break;
                        }
                        break;
                    case Option.Spock:
                        switch (game.PRight.OptionChosen)
                        {
                            case Option.Rock:
                                game.Winner = Winner.Left;
                                break;
                            case Option.Paper:
                                game.Winner = Winner.Right;
                                break;
                            case Option.Scissors:
                                game.Winner = Winner.Left;
                                break;
                            case Option.Lizard:
                                game.Winner = Winner.Right;
                                break;
                        }
                        break;
                }
            }
            if (game.Winner == Winner.Left)
            {
                game.PWinner = game.PLeft;
                game.PLeft.NVictories++;
            }
            else if (game.Winner == Winner.Right)
            {
                game.PWinner = game.PRight;
                game.PRight.NVictories++;
            }
            else
            {
                game.PWinner = new();
                game.PWinner.Name = "Draw";
                game.PWinner.Surname = "";
            }
        }
        private void CheckExistsWinner()
        {
            if (game.PWinner.NVictories >= (game.NRounds / 2) + 1)
            {
                if (game.IsStreakMode && game.Winner == Winner.Left)
                {
                    game.Streak++;
                    game.RoundNow = 1;
                    game.PLeft.NVictories = 0;
                    SelectSinglePlayerMode();
                    Start();
                }
                else
                {
                    game.Streak = 0;
                    ChangeState(GameState.Final);
                    game.RoundNow--; //we update the round to show the one where the result was decided instead of the next one
                }
            }
            else
            {
                ChangeState(GameState.Playing);
            }
        }
        #endregion

        #region Properties
        public Visibility ConfigurationVisibility { get => configurationVisibility; set => SetProperty(ref configurationVisibility, value); }
        public Visibility PlayingVisibility { get => playingVisibility; set => SetProperty(ref playingVisibility, value); }
        public Visibility FinalVisibility { get => finalVisibility; set => SetProperty(ref finalVisibility, value); }
        public Visibility ShowHandsVisibiity { get => showHandsVisibility; set => SetProperty(ref showHandsVisibility, value); }
        public Game Game { get => game; set => SetProperty(ref game, value); }
        public string Message { get => message; set => SetProperty(ref message, value); }
        #endregion

        #region Commands Declaration
        public ICommand StartCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }
        public ICommand SinglePlayerCommand { get; private set; }
        public ICommand MultiPlayerCommand { get; private set; }
        public ICommand RematchCommand { get; private set; }
        public ICommand GoToMainMenuCommand { get; private set; }
        public ICommand MultiplayerSelectOptionCommand { get; private set; }

        private void CreateCommands()
        {
            StartCommand = new RelayCommand(o => Start(), o => CanStart());
            PlayCommand = new RelayCommand(o => Play(),o => CanPlay());
            SinglePlayerCommand = new RelayCommand(o => SelectSinglePlayerMode());
            MultiPlayerCommand = new RelayCommand(o => SelectMultiPlayerMode());
            RematchCommand = new RelayCommand(o => Rematch());
            GoToMainMenuCommand = new RelayCommand(o => GoToMainMenu());
            MultiplayerSelectOptionCommand = new RelayCommand(o => MultiPlayerSelectOption(o), o => CanUseKeyboard());
        }
        #endregion

        #region Commands Implementation
        private void SelectSinglePlayerMode()
        {
            game.Mode = GameMode.SinglePlayer;

            game.PRight = new Player
            {
                Name = personGenerator.GenerateRandomFirstName(),
                Surname = personGenerator.GenerateRandomLastName(),
                ColorChosen = Color.FromArgb(255, 100, 100, 100)
            };
        }

        private void SelectMultiPlayerMode()
        {
            game.Mode = GameMode.Multiplayer;

            game.PRight = new Player
            {
                Name = "",
                Surname = "",
                ColorChosen = new()
            };
        }

        private async void Play()
        {
            ChangeState(GameState.ShowingHands);
            CheckVictor();

            await Task.Delay(3000);

            if (game.Winner != Winner.Draw)
                game.RoundNow++; //we only update the round if the result wasn't a draw

            game.PLeft.OptionChosen = Option.Unchosen;
            if (game.Mode == GameMode.SinglePlayer)
            {
                game.PRight.OptionChosen = game.CpuSelectOption();
            }
            else if (game.Mode == GameMode.Multiplayer)
            {
                game.PLeft.IsReady = false;
                game.PRight.IsReady = false;
                game.PRight.OptionChosen = Option.Unchosen;
            }
            CheckExistsWinner();
        }

        private bool CanStart()
        {
            return game.PLeft.IsValid && game.PRight.IsValid;
        }
        private void Start()
        {
            ChangeState(GameState.Playing);
            if (game.Mode == GameMode.SinglePlayer)
            {
                game.PRight.OptionChosen = game.CpuSelectOption();
            }    
        }
        private bool CanPlay()
        {
            if (game.Mode == GameMode.Multiplayer)
            {
                if (!game.PLeft.IsReady || !game.PRight.IsReady)
                    return false;
            }
            return (game.NRounds >= game.RoundNow) //game's not finished
                    && (game.PLeft.OptionChosen != Option.Unchosen) && (game.PRight.OptionChosen != Option.Unchosen) // and both players have selected their option
                    && (game.State == GameState.Playing); //and gamestate is on playing (case showing hands gave trouble)
        }
        private void Rematch()
        {
            game.RoundNow = 1;
            game.PLeft.NVictories = 0;
            game.PRight.NVictories = 0;
            ChangeState(GameState.Playing);
        }
        private void GoToMainMenu()
        {
            MainWindow window = new MainWindow() { DataContext = new RPScLSViewModel() };
            window.Show();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
        }
        private void MultiPlayerSelectOption(object parameter)
        {
            switch ((string)parameter) {
                //left player cases
                case "LeftRock":
                    game.PLeft.OptionChosen = Option.Rock;
                    game.PLeft.IsReady = true;
                    break;
                case "LeftPaper":
                    game.PLeft.OptionChosen = Option.Paper;
                    game.PLeft.IsReady = true;
                    break;
                case "LeftScissors":
                    game.PLeft.OptionChosen = Option.Scissors;
                    game.PLeft.IsReady = true;
                    break;
                case "LeftLizard":
                    game.PLeft.OptionChosen = Option.Lizard;
                    game.PLeft.IsReady = true;
                    break;
                case "LeftSpock":
                    game.PLeft.OptionChosen = Option.Spock;
                    game.PLeft.IsReady = true;
                    break;

                //right player cases 
                case "RightRock":
                    game.PRight.OptionChosen = Option.Rock;
                    game.PRight.IsReady = true;
                    break;
                case "RightPaper":
                    game.PRight.OptionChosen = Option.Paper;
                    game.PRight.IsReady = true;
                    break;
                case "RightScissors":
                    game.PRight.OptionChosen = Option.Scissors;
                    game.PRight.IsReady = true;
                    break;
                case "RightLizard":
                    game.PRight.OptionChosen = Option.Lizard;
                    game.PRight.IsReady = true;
                    break;
                case "RightSpock":
                    game.PRight.OptionChosen = Option.Spock;
                    game.PRight.IsReady = true;
                    break;
            }
        }
        private bool CanUseKeyboard()
        {
            return game.Mode == GameMode.Multiplayer;
        }
        #endregion
    }
}
