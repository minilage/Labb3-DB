using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TinasLabb03.ViewModel
{
    /// <summary>
    /// Bas-klass för alla ViewModels som implementerar INotifyPropertyChanged.
    /// Används för att notifiera UI:t vid förändringar.
    /// </summary>

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Höjer PropertyChanged-händelsen.
        /// CallerMemberName gör att man inte behöver ange property-namnet manuellt.
        /// </summary>
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}