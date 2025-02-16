namespace TinasLabb03.ViewModel
{
    /// <summary>
    /// ViewModel för ett svarsalternativ. Hanterar egenskaper som om alternativet är valt, 
    /// korrekt, fel eller om man kan gissa.
    /// </summary>

    public class AnswerOptionViewModel : ViewModelBase
    {
        private bool _isSelected;
        private bool _isCorrect;
        private bool _isWrong;
        private bool _canGuess;

        public string Text { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged(nameof(IsSelected));
                }
            }
        }

        public bool IsCorrect
        {
            get => _isCorrect;
            set
            {
                if (_isCorrect != value)
                {
                    _isCorrect = value;
                    RaisePropertyChanged(nameof(IsCorrect));
                }
            }
        }

        public bool IsWrong
        {
            get => _isWrong;
            set
            {
                if (_isWrong != value)
                {
                    _isWrong = value;
                    RaisePropertyChanged(nameof(IsWrong));
                }
            }
        }

        public bool CanGuess
        {
            get => _canGuess;
            set
            {
                if (_canGuess != value)
                {
                    _canGuess = value;
                    RaisePropertyChanged(nameof(CanGuess));
                }
            }
        }

        public AnswerOptionViewModel(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
            CanGuess = true;
        }
    }
}