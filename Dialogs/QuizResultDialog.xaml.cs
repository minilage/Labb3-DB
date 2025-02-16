using System.Windows;
using System.Windows.Input;

namespace TinasLabb03.Dialogs
{
    public partial class QuizResultDialog : Window
    {
        public QuizResultDialog(int correct, int total)
        {
            InitializeComponent();
            // Sätt egenskaper eller DataContext för bindningar
            ResultText = $"You got {correct} out of {total} correct!";
            DataContext = this;
        }

        public string ResultText { get; set; }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true; // eller false, beroende på hur du vill att Esc ska tolkas
            Close();
        }
    }
}