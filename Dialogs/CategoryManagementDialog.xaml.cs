using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TinasLabb03.Data;
using TinasLabb03.Model;

namespace TinasLabb03.Dialogs
{
    /// <summary>
    /// Dialog för att hantera kategorier – lägga till och ta bort.
    /// </summary>

    public partial class CategoryManagementDialog : Window
    {
        private readonly CategoryRepository _categoryRepo;
        public ObservableCollection<Category> Categories { get; set; }

        public CategoryManagementDialog(CategoryRepository categoryRepo)
        {
            InitializeComponent();
            _categoryRepo = categoryRepo;
            Categories = new ObservableCollection<Category>();
            DataContext = this;
            LoadCategories();
        }

        private async void LoadCategories()
        {
            var cats = await _categoryRepo.GetAllAsync();
            Categories.Clear();
            foreach (var cat in cats)
            {
                Categories.Add(cat);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = CategoryNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(name))
            {
                var newCat = new Category(name);
                await _categoryRepo.AddAsync(newCat);
                Categories.Add(newCat);
                CategoryNameTextBox.Clear();
            }
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListBox.SelectedItem is Category selected)
            {
                await _categoryRepo.DeleteAsync(selected.Id!);
                Categories.Remove(selected);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Hanterar "Close"-kommandot (t.ex. via Esc-tangent)
        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false; // Standard för avbrytande
            Close();
        }
    }
}