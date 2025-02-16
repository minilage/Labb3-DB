using System.Collections.ObjectModel;
using TinasLabb03.Model;

namespace TinasLabb03.ViewModel
{
    /// <summary>
    /// ViewModel för ett QuestionPack. Exponerar egenskaper för bindning i UI:t.
    /// </summary>
    public class QuestionPackViewModel : ViewModelBase
    {
        private readonly QuestionPack model;

        /// <summary>
        /// Konstruerar en QuestionPackViewModel baserad på en QuestionPack-modell.
        /// Konverterar modellens frågor till QuestionViewModels.
        /// </summary>
        public QuestionPackViewModel(QuestionPack model)
        {
            this.model = model;
            // Konverterar modellens frågor till QuestionViewModels
            Questions = new ObservableCollection<QuestionViewModel>(
                model.Questions.Select(q => new QuestionViewModel(q))
            );
        }

        // Exponerar modellens Id (MongoDB genererat).
        public string? Id => model.Id;

        // Packets namn med TwoWay-binding
        public string Name
        {
            get => model.Name;
            set
            {
                model.Name = value;
                RaisePropertyChanged();
            }
        }

        public Difficulty Difficulty
        {
            get => model.Difficulty;
            set
            {
                model.Difficulty = value;
                RaisePropertyChanged();
            }
        }

        public int TimeLimitInSeconds
        {
            get => model.TimeLimitInSeconds;
            set
            {
                model.TimeLimitInSeconds = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Kategorinamn (exempelvis "General") som används för att visa kategorin.
        /// </summary>
        public string Category
        {
            get => model.Category;
            set
            {
                model.Category = value;
                RaisePropertyChanged();
            }
        }


        // Samling av frågor i paketet (som QuestionViewModels).
        public ObservableCollection<QuestionViewModel> Questions { get; }

        /// <summary>
        /// Synkroniserar ViewModel med Model.
        /// Kopierar grundläggande egenskaper och uppdaterar frågelistan.
        /// </summary>
        public QuestionPack ToModel()
        {
            model.Name = this.Name;
            model.Difficulty = this.Difficulty;
            model.TimeLimitInSeconds = this.TimeLimitInSeconds;
            model.Category = this.Category;

            // Tömmer modellens frågor och lägger till de aktuella frågorna från ViewModel
            model.Questions.Clear();
            foreach (var qvm in this.Questions)
            {
                model.Questions.Add(qvm.ToModel());
            }

            return model;
        }
    }
}