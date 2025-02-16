using System.Windows;
using System.Windows.Input;
using TinasLabb03.ViewModel;

namespace TinasLabb03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Hanterar global navigering, laddning av data, state-ändringar (t.ex. fullscreen) 
    /// samt tangentbordsgenvägar (t.ex. Esc för att lämna helskärmsläge).
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new MainWindowViewModel();

            Loaded += MainWindow_Loaded;
            StateChanged += MainWindow_StateChanged;
        }

        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// Hanterar global navigering, laddning av data, state-ändringar (t.ex. fullscreen) 
        /// samt tangentbordsgenvägar (t.ex. Esc för att lämna helskärmsläge).
        /// </summary>
        private async void MainWindow_Loaded(object? sender, RoutedEventArgs? e)
        {
            // Kontrollera att viewModel inte är null (det borde aldrig vara det)
            if (viewModel != null)
            {
                // Ladda data vid start
                await viewModel.LoadDataAsync();
            }
            else
            {
                Console.WriteLine("ViewModel är null vid laddning av data.");
            }
        }

        /// <summary>
        /// Övervakar fönstrets state-ändringar och säkerställer att fullscreen-utseendet 
        /// tvingas när vi är i playview.
        /// </summary>
        private void MainWindow_StateChanged(object? sender, EventArgs e)
        {
            // Om vi är i playview, tvinga fullscreen-utseende
            if (viewModel.CurrentView is PlayerViewModel playerViewModel)
            {
                if (WindowState == WindowState.Maximized)
                {
                    // När fönstret är maximalt, ta bort titelfältet
                    WindowStyle = WindowStyle.None;
                    playerViewModel.AdjustScaleForFullscreen(true);
                }
                else if (WindowState == WindowState.Normal)
                {
                    // När fönstret är normalt, återställ titelfältet
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    playerViewModel.AdjustScaleForFullscreen(false);
                }
            }
        }

        /// <summary>
        /// Hanterar Esc-tangenten så att vi lämnar fullscreen-läget (om aktivt).
        /// </summary>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Escape)
            {
                if (DataContext is MainWindowViewModel vm)
                {
                    // Om vi är i PlayerView
                    if (vm.CurrentView is PlayerViewModel playerViewModel)
                    {
                        // Om vi är i fullscreen (maximized) så lämnar vi fullscreen först.
                        if (WindowState == WindowState.Maximized)
                        {
                            ExitFullScreen();
                        }
                        else
                        {
                            // Om vi är i normalt läge, byt tillbaka till ConfigurationView.
                            vm.CurrentView = vm.ConfigurationViewModel;
                        }
                    }
                }
            }
        }

        //protected override void OnPreviewKeyDown(KeyEventArgs e)
        //{
        //    base.OnPreviewKeyDown(e);

        //    if (e.Key == Key.Escape)
        //    {
        //        if (WindowState == WindowState.Maximized) // Kontrollera om vi är i helskärmsläge
        //        {
        //            ExitFullScreen(); // Lämna helskärmsläge
        //        }
        //    }
        //}

        /// <summary>
        /// Återställer fönstrets state och stil från fullscreen till normalt läge.
        /// </summary>
        private void ExitFullScreen()
        {
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.SingleBorderWindow;

            // Använd DataContext, då det redan är inställt på viewModel
            if (DataContext is MainWindowViewModel viewModel)
            {
                // Återställ skalan för PlayerViewModel om den är aktiv
                if (viewModel.CurrentView is PlayerViewModel playerViewModel)
                {
                    playerViewModel.AdjustScaleForFullscreen(false);
                }

                System.Console.WriteLine("Lämnar helskärmsläge.");
            }
        }

        /// <summary>
        /// Hanterar Close-kommandot (t.ex. via Esc-knappen i vissa dialoger).
        /// </summary>
        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}