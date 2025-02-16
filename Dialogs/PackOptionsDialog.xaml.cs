using System.Windows;
using System.Windows.Input;
using TinasLabb03.Model;
using TinasLabb03.ViewModel;

namespace TinasLabb03.Dialogs
{
    // Detta fönster används för att redigera inställningarna för ett befintligt frågepaket.

    public partial class PackOptionsDialog : Window
    {
        public QuestionPackViewModel ActivePack { get; set; } = null!;
        public IEnumerable<Difficulty> Difficulties { get; set; } = null!;
        public IEnumerable<Category> Categories { get; set; } = null!;

        public PackOptionsDialog(QuestionPackViewModel packViewModel, IEnumerable<Difficulty> difficulties, IEnumerable<string> categories, int timeLimitInSeconds)
        {
            InitializeComponent();
            DataContext = new
            {
                Pack = packViewModel,
                Difficulties = difficulties,
                Categories = categories,
                TimeLimit = timeLimitInSeconds
            };
        }

        // Hanterar "Save"-knappen
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Indikerar att användaren vill spara ändringar
            Close(); // Stänger dialogen
        }

        // Hanterar "Cancel"-knappen
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Indikerar att användaren vill avbryta ändringar
            Close(); // Stänger dialogen
        }

        // Hanterar "Close"-kommandot (t.ex. via Esc-tangent)
        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false; // Standard för avbrytande
            Close();
        }
    }
}