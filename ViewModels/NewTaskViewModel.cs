using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.EntityFrameWork;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class NewTaskViewModel : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private TaskCategory _taskCategory;
        private TaskStatues _taskStatues;

        private DateTime _startDate;
        private DateTime _endDate;
        private int _startHour;
        private int _endHour;
        private string _startAmPm;
        private string _endAmPm;   

        public int StartHour
        {
            get => _startHour;
            set
            {
                _startHour = value;
                UpdateStartDate();
                OnPropertyChanged(nameof(StartHour));
            }
        }

        public int EndHour
        {
            get => _endHour;
            set
            {
                _endHour = value;
                UpdateEndDate();
                OnPropertyChanged(nameof(EndHour));
            }
        }

        public string StartAmPm
        {
            get => _startAmPm;
            set
            {
                _startAmPm = value;
                UpdateStartDate();
                OnPropertyChanged(nameof(StartAmPm));
            }
        }

        public string EndAmPm
        {
            get => _endAmPm;
            set
            {
                _endAmPm = value;
                UpdateEndDate();
                OnPropertyChanged(nameof(EndAmPm));
            }
        }

        public List<int> Hours => Enumerable.Range(1, 12).ToList();
        public List<string> AmPmOptions => new List<string> { "AM", "PM" };

        private void UpdateStartDate()
        {
            int hour = StartHour;
            if (StartAmPm == "PM" && hour != 12) hour += 12;
            if (StartAmPm == "AM" && hour == 12) hour = 0;

            StartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, hour, StartDate.Minute, 0);
            OnPropertyChanged(nameof(StartDate));
        }

        private void UpdateEndDate()
        {
            int hour = EndHour;
            if (EndAmPm == "PM" && hour != 12) hour += 12;
            if (EndAmPm == "AM" && hour == 12) hour = 0;

            EndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, hour, EndDate.Minute, 0);
            OnPropertyChanged(nameof(EndDate));
        }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public TaskCategory TaskCategory
        {
            get => _taskCategory;
            set
            {
                _taskCategory = value;
                OnPropertyChanged();
            }
        }

        public TaskStatues TaskStatues
        {
            get => _taskStatues;
            set
            {
                _taskStatues = value;
                OnPropertyChanged();
            }
        }
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        
        public IEnumerable<TaskCategory> TaskCategories => Enum.GetValues(typeof(TaskCategory)).Cast<TaskCategory>();

        public IEnumerable<TaskStatues> TaskStatuses => Enum.GetValues(typeof(TaskStatues)).Cast<TaskStatues>();

        public ICommand SubmitCommand { get; }

        public NewTaskViewModel()
        {
            SubmitCommand = new RelayCommand(Submit);
            StartDate = DateTime.Today;
            

        }

        private void Submit()
        {
            var task = new TacheDao
            {
                Title = Title,
                Description = Description,
                Category = TaskCategory,
                StartDate = StartDate,
                EndDate = EndDate,
                Statues = TaskStatues
            };

            // Ensure we access the existing MainWindowViewModel instance

            var mainWindowViewModel = (MainWindowViewModel)App.Current.MainWindow.DataContext;
            var context = new DatabaseContext();


            context.Tasks.Add(task);
            context.SaveChanges();

            mainWindowViewModel.GetAllTasks();



            // Close the NewTask window after submitting
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is Views.NewTask)
                {
                    window.Close();
                    break;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
