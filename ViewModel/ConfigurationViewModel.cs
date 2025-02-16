using System.Timers;
using System.Windows;
using TinasLabb03.Command;
using TinasLabb03.Dialogs;
using TinasLabb03.Model;

namespace TinasLabb03.ViewModel
{
    // ViewModel för konfigurationen av frågepaket, hanterar redigering av frågor och packinställningar.

    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel mainWindowViewModel;
        private QuestionViewModel? _selectedQuestion;
        private bool _isRightPanelVisible;
        private System.Timers.Timer? _autoSaveTimer;


        public static IEnumerable<Difficulty> Difficulties => Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();

        public DelegateCommand ManageCategoriesCommand { get; }

        public ConfigurationViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel ?? throw new ArgumentNullException(nameof(mainWindowViewModel));
            IsRightPanelVisible = false; // Högerpanelen är gömd som standard

            // Initiera kommandon för att lägga till och ta bort frågor
            AddQuestionCommand = new DelegateCommand(AddQuestion, CanModifyPack);
            RemoveQuestionCommand = new DelegateCommand(RemoveQuestion, CanModifyPack);
            PackOptionsCommand = new DelegateCommand(OpenPackOptions, CanModifyPack);
            ManageCategoriesCommand = new DelegateCommand(obj => mainWindowViewModel.OpenCategoryManagement());

            // Initiera autosave-timer (ingen start än)
            _autoSaveTimer = new System.Timers.Timer(2000);  // 2000 ms = 2 sekunder
            _autoSaveTimer.AutoReset = false;
            _autoSaveTimer.Elapsed += AutoSaveTimer_Elapsed;
        }

        public QuestionPackViewModel? ActivePack => mainWindowViewModel.ActivePack;

        // När SelectedQuestion ändras, prenumerera på dess PropertyChanged
        public QuestionViewModel? SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                if (_selectedQuestion != null)
                {
                    _selectedQuestion.PropertyChanged -= SelectedQuestion_PropertyChanged;
                }
                _selectedQuestion = value;
                if (_selectedQuestion != null)
                {
                    _selectedQuestion.PropertyChanged += SelectedQuestion_PropertyChanged;
                }
                IsRightPanelVisible = value != null; // Visa högerpanelen om en fråga är vald
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsRightPanelVisible));
            }
        }

        private void SelectedQuestion_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Vid varje ändring, starta om timern
            _autoSaveTimer?.Stop();
            _autoSaveTimer?.Start();
        }

        private async void AutoSaveTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync(() => mainWindowViewModel.UpdateActivePackAsync());
        }


        public bool IsRightPanelVisible
        {
            get => _isRightPanelVisible;
            set
            {
                _isRightPanelVisible = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand AddQuestionCommand { get; }
        public DelegateCommand RemoveQuestionCommand { get; }
        public DelegateCommand PackOptionsCommand { get; }

        private async void AddQuestion(object? obj)
        {
            if (ActivePack != null)
            {
                var newQuestion = new Question("New Question", "", new[] { "", "", "" }, Difficulty.Medium);
                var newQuestionViewModel = new QuestionViewModel(newQuestion);
                ActivePack.Questions.Add(newQuestionViewModel);
                SelectedQuestion = newQuestionViewModel;

                await mainWindowViewModel.UpdateActivePackAsync();

            }
        }

        private async void RemoveQuestion(object? obj)
        {
            if (ActivePack != null && SelectedQuestion != null)
            {
                ActivePack.Questions.Remove(SelectedQuestion);
                SelectedQuestion = null;

                await mainWindowViewModel.UpdateActivePackAsync();
            }
        }

        private async void OpenPackOptions(object? obj)
        {
            if (ActivePack != null)
            {
                var categoriesFromDb = await mainWindowViewModel.CategoryRepo.GetAllAsync();
                var categoryNames = categoriesFromDb.Select(c => c.Name);
                var dialog = new PackOptionsDialog(ActivePack, Difficulties, categoryNames, ActivePack.TimeLimitInSeconds);
                if (dialog.ShowDialog() == true)
                {
                    RaisePropertyChanged(nameof(ActivePack));
                }
            }
        }

        private bool CanModifyPack(object? obj) => ActivePack != null;
    }
}