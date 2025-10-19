using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp1.Command;
using WpfApp1.EntityFrameWork;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class EditTaskViewModel : INotifyPropertyChanged
    {
        private readonly TacheDao _task;
        private readonly DatabaseContext _dbContext;
        private readonly Window _editTaskWindow;

        private int _startHour;
        private int _endHour;
        private string _startAmPm;
        private string _endAmPm;


        public EditTaskViewModel(TacheDao task, Window editTaskWindow)
        {
            _task = task;
            _dbContext = new DatabaseContext();
            _editTaskWindow = editTaskWindow;

            // Initialize properties
            Title = task.Title;
            Description = task.Description;
            StartDate = task.StartDate;
            EndDate = task.EndDate;
            SelectedCategory = task.Category;

            StartHour = StartDate.Hour % 12 == 0 ? 12 : StartDate.Hour % 12;
            StartAmPm = StartDate.Hour >= 12 ? "PM" : "AM";

            EndHour = EndDate.Hour % 12 == 0 ? 12 : EndDate.Hour % 12;
            EndAmPm = EndDate.Hour >= 12 ? "PM" : "AM";

            Hours = Enumerable.Range(1, 12).ToList();
            AmPmOptions = new List<string> { "AM", "PM" };

            SaveCommand = new RelayCommand(SaveTask);
            CancelCommand = new RelayCommand(CloseWindow);
            Categories = new ObservableCollection<TaskCategory>(Enum.GetValues(typeof(TaskCategory)).Cast<TaskCategory>());
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaskCategory SelectedCategory { get; set; }
        public ObservableCollection<TaskCategory> Categories { get; }

        public List<int> Hours { get; }
        public List<string> AmPmOptions { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
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
        private void SaveTask()
        {
            // Update the task
            _task.Title = Title;
            _task.Description = Description;
            _task.StartDate = StartDate;
            _task.EndDate = EndDate;
            _task.Category = SelectedCategory;

            // Save changes to the database
            _dbContext.Tasks.Update(_task);
            _dbContext.SaveChanges();

            // Close the window
            CloseWindow();
        }

        private void CloseWindow()
        {
            // Close the EditTaskWindow
            _editTaskWindow?.Close();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
