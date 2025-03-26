using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _notificationMessage;
        public string NotificationMessage
        {
            get => _notificationMessage;
            set
            {
                _notificationMessage = value;
                OnPropertyChanged(nameof(NotificationMessage));
            }
        }

        public ObservableCollection<string> Results { get; set; } = new();

        public MainViewModel()
        {
            NotificationMessage = "Welcome to the Roulette App!";
        }
    }
}
