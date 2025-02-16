using System.Collections.ObjectModel;
using TinasLabb03.Model;

namespace TinasLabb03.ViewModel
{
    /// <summary>
    /// ViewModel för en enskild fråga.
    /// Hanterar bindning för fråga, korrekt svar, felaktiga svar och alternativ.
    /// </summary>
    public class QuestionViewModel : ViewModelBase
    {
        private readonly Question model;
        private string? _selectedAnswer;

        // Lista med felaktiga svar (enkel stränglista)
        public ObservableCollection<string> IncorrectAnswers { get; }

        // Lista med svarsalternativ som ViewModels
        public ObservableCollection<AnswerOptionViewModel> Options { get; }

        /// <summary>
        /// Det valda svarsalternativet. Notifierar UI:t vid ändring.
        /// </summary>
        public string SelectedAnswer
        {
            get => _selectedAnswer!;
            set
            {
                if (_selectedAnswer != value)
                {
                    _selectedAnswer = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(IsCorrect));
                    RaisePropertyChanged(nameof(IsIncorrect));
                }
            }
        }

        // Booleska egenskaper för att indikera om det valda svaret är korrekt/inkorrekt
        public bool IsCorrect => SelectedAnswer == CorrectAnswer;
        public bool IsIncorrect => SelectedAnswer != null && SelectedAnswer != CorrectAnswer;

        public string Query
        {
            get => model.Query;
            set
            {
                if (model.Query != value)
                {
                    model.Query = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string CorrectAnswer
        {
            get => model.CorrectAnswer;
            set
            {
                if (model.CorrectAnswer != value)
                {
                    model.CorrectAnswer = value;
                    RaisePropertyChanged();
                    UpdateOptions();
                }
            }
        }

        public Difficulty Difficulty
        {
            get => model.Difficulty;
            set
            {
                if (model.Difficulty != value)
                {
                    model.Difficulty = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Konstruerar en QuestionViewModel baserad på en Question-modell.
        /// </summary>
        public QuestionViewModel(Question model)
        {
            this.model = model;
            IncorrectAnswers = new ObservableCollection<string>(model.IncorrectAnswers);
            Options = new ObservableCollection<AnswerOptionViewModel>();
            UpdateOptions();

            // När IncorrectAnswers ändras, uppdatera modellen och UI:t
            IncorrectAnswers.CollectionChanged += (sender, args) =>
            {
                model.IncorrectAnswers = IncorrectAnswers.ToArray();
                RaisePropertyChanged(nameof(IncorrectAnswers));
                UpdateOptions();
            };
        }

        /// <summary>
        /// Skapar en ny Question-modell baserad på nuvarande värden.
        /// Detta används för att synka ViewModel med Model.
        /// </summary>
        public Question ToModel()
        {
            return new Question
            {
                Query = this.Query,
                CorrectAnswer = this.CorrectAnswer,
                IncorrectAnswers = this.IncorrectAnswers.ToArray(),
                Difficulty = this.Difficulty
            };
        }

        /// <summary>
        /// Uppdaterar svarsalternativen: kombinerar felaktiga svar och korrekt svar,
        /// blandar dem slumpmässigt och uppdaterar Options.
        /// </summary>
        private void UpdateOptions()
        {
            Options.Clear();

            // Lägg till felaktiga svar
            foreach (var incorrectAnswer in model.IncorrectAnswers)
            {
                Options.Add(new AnswerOptionViewModel(incorrectAnswer, false));
            }

            // Lägg till det korrekta svaret
            Options.Add(new AnswerOptionViewModel(model.CorrectAnswer, true));

            // // Blanda alternativen slumpmässigt
            var shuffledOptions = Options.OrderBy(_ => Guid.NewGuid()).ToList();
            Options.Clear();
            foreach (var option in shuffledOptions)
            {
                Options.Add(option);
            }

            RaisePropertyChanged(nameof(Options));
        }
    }
}