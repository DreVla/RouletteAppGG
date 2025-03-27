using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Newtonsoft.Json;
using RouletteApp.Model;

namespace RouletteApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Random _random = new Random();
        private TcpListener _tcpListener;
        private bool _isRunning;

        public ObservableCollection<RouletteResult> _results { get; set; } = new ObservableCollection<RouletteResult>();
        public ObservableCollection<RouletteResult> Results
        {
            get => _results;
            set
            {
                _results = value;
                OnPropertyChanged(nameof(Results));
            }
        }
        private string _notificationText;
        public string NotificationText
        {
            get => _notificationText;
            set
            {
                _notificationText = value;
                OnPropertyChanged(nameof(NotificationText));
            }
        }

        private bool _isNotificationVisible;
        public bool IsNotificationVisible
        {
            get => _isNotificationVisible;
            set
            {
                _isNotificationVisible = value;
                OnPropertyChanged(nameof(IsNotificationVisible));
            }
        }

        private int _activePlayerCount;
        public int ActivePlayerCount
        {
            get => _activePlayerCount;
            set
            {
                _activePlayerCount = value;
                OnPropertyChanged(nameof(ActivePlayerCount));
                Debug.WriteLine($"ActivePlayerCount set to: {value}");
            }
        }

        private int _biggestMultiplier;
        public int BiggestMultiplier
        {
            get => _biggestMultiplier;
            set
            {
                _biggestMultiplier = value;
                OnPropertyChanged(nameof(BiggestMultiplier));
                Debug.WriteLine($"BiggestMultiplier set to: {value}");
            }
        }

        public ICommand AddResultCommand { get; }
        public ICommand ShowNotificationCommand { get; }

        public MainViewModel()
        {
            AddResultCommand = new RelayCommand(AddResult);
            ShowNotificationCommand = new RelayCommand(ShowNotification);
            Task.Run(StartTcpListener);
        }

        private void AddResult()
        { 
            int position = _random.Next(0, 37);
            string color = GetColor(position);

            var newResult = new RouletteResult { position = position, color = color, multiplier = 0 };
            Results.Add(newResult);
            if (Results.Count > 10) Results.RemoveAt(0);

            int streak = CalculateStreak(color);
            newResult.multiplier = position * streak; 
        }


        private int CalculateStreak(string currentColor)
        {
            int streak = 0;
            for (int i = Results.Count - 1; i >= 0; i--)
            {
                if (Results[i].color == currentColor) streak++;
                else break;
            }
            return streak;
        }

        private void ShowNotification()
        {
            NotificationText = "Player VIP PlayerName has joined the table.";
            IsNotificationVisible = true;

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += (s, e) =>
            {
                IsNotificationVisible = false;
                timer.Stop();
            };
            timer.Start();
        }

        private async Task StartTcpListener()
        {
            _tcpListener = new TcpListener(IPAddress.Any, 4948);
            _tcpListener.Start();
            _isRunning = true;
            Debug.WriteLine("TCP Listener started on port 4948...");
            while (_isRunning)
            {
                try
                {
                    TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClientAsync(client));
                    Debug.WriteLine("Client connected...");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"TCP Listener error: {ex.Message}");
                    break;
                }
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var stats = JsonConvert.DeserializeObject<StatsMessage>(message);

                    if (stats != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Debug.WriteLine("Updating UI on main thread...");
                            if (stats.ActivePlayers.HasValue)
                                ActivePlayerCount = stats.ActivePlayers.Value;

                            if (stats.BiggestMultiplier.HasValue)
                                BiggestMultiplier = stats.BiggestMultiplier.Value;

                            Debug.WriteLine($"UI Updated: ActivePlayers={ActivePlayerCount}, BiggestMultiplier={BiggestMultiplier}");
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Client handling error: {ex.Message}");
            }
        }

        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            public RelayCommand(Action execute) => _execute = execute;
            public event EventHandler CanExecuteChanged;
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) => _execute();
        }

        /// <summary>
        /// Determines the color of the roulette position.
        /// In number ranges from 1 to 10 and 19 to 28, odd numbers are red and even are black.
        /// In ranges from 11 to 18 and 29 to 36, odd numbers are black and even are red.
        /// </summary>
        /// <param name="position">The position on the roulette wheel (0-36).</param>
        /// <returns>The color of the position as a string ("Red", "Black", or "Green").</returns>
        private string GetColor(int position)
        {
            if (position == 0) return "Green";

            bool isOdd = position % 2 != 0;

            if ((position >= 1 && position <= 10) || (position >= 19 && position <= 28))
            {
                return isOdd ? "Red" : "Black";
            }
            else if ((position >= 11 && position <= 18) || (position >= 29 && position <= 36))
            {
                return isOdd ? "Black" : "Red";
            }

            return "Black";
        }
    }
}
