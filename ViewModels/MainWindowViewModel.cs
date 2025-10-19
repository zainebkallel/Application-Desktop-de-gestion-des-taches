using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Views;
using WpfApp1.Command;
using System.Collections.ObjectModel;
using WpfApp1.EntityFrameWork;
using System.Runtime.CompilerServices;
using WpfApp1.Models;
using System.Windows;


namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DateTime _currentDate;
        private DatabaseContext _dbContext;
        public DateTime CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
        public MainWindowViewModel()
        {
            _dbContext = new DatabaseContext();

            GetAllTasks();
            CurrentDate = DateTime.Now;
           
        }
       
      



        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand IOpenNewWindow => new RelayCommand(OpenNewWindow);
        public void OpenNewWindow()
        {
            NewTask newTask = new NewTask();
            newTask.Show();
        }




        public void GetAllTasks()
        {
            var dbTasks = _dbContext.Tasks.ToList();
            var sortedTasks = dbTasks.OrderBy(task => task.StartDate).ToList();

            ToDoTasks.Clear();
            ProgTasks.Clear();
            CompTasks.Clear();
            foreach (var dbTask in sortedTasks)
            {
                switch (dbTask.Statues)
                {
                    case TaskStatues.ToDo:
                        ToDoTasks.Add(dbTask);
                        break;
                    case TaskStatues.InProgress:
                        ProgTasks.Add(dbTask);
                        break;
                    case TaskStatues.Complete:
                        CompTasks.Add(dbTask);
                        break;
                }
            }


        }



        public ObservableCollection<TacheDao> ToDoTasks { get; set; } = new ObservableCollection<TacheDao>();
        public ObservableCollection<TacheDao> ProgTasks { get; set; } = new ObservableCollection<TacheDao>();
        public ObservableCollection<TacheDao> CompTasks { get; set; } = new ObservableCollection<TacheDao>();





        public void AddTask(TacheDao task)
        {
            switch (task.Statues)
            {
                case TaskStatues.ToDo:
                    ToDoTasks.Add(task);
                    break;
                case TaskStatues.InProgress:
                    ProgTasks.Add(task);
                    break;
                case TaskStatues.Complete:
                    CompTasks.Add(task);
                    break;
            }
        }



        private Task _selectedTask;
        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }




        public ICommand EditCommand => new RelayCommand<TacheDao>(EditTask);
        public ICommand DeleteCommand => new RelayCommand<TacheDao>(DeleteTask);

        private void EditTask(TacheDao task)
        {
            // Open the edit window with the task data
            var editWindow = new EditTaskWindow(task);
            editWindow.ShowDialog();

            // Refresh the tasks after editing
            GetAllTasks();
        }

        private void DeleteTask(TacheDao task)
        {
            var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _dbContext.Tasks.Remove(task);
                _dbContext.SaveChanges();
                GetAllTasks();


            }
        }
        public void RemoveTask(TacheDao task)
        {
            if (ToDoTasks.Contains(task))
            {
                ToDoTasks.Remove(task);
            }
            else if (ProgTasks.Contains(task))
            {
                ProgTasks.Remove(task);
            }
            else if (CompTasks.Contains(task))
            {
                CompTasks.Remove(task);
            }
        }
        public ICommand ChangeStatusCommand => new RelayCommand<TacheDao>(ChangeTaskStatus);
        public ICommand ChangeStatusCommand2 => new RelayCommand<TacheDao>(ChangeTaskStatus2);
        private void ChangeTaskStatus(TacheDao task)
        {
            if (task == null) return;

            // Remove the task from its current list
            RemoveTask(task);

            // Open the custom dialog to select new status
            var dialog = new ChangeStatusDialog();
            if (dialog.ShowDialog() == true)
            {
                // Get the selected status from the dialog
                var newStatus = dialog.SelectedStatus;

                if (newStatus == "InProgress")
                {
                    task.Statues = TaskStatues.InProgress;
                }
                else if (newStatus == "Complete")
                {
                    task.Statues = TaskStatues.Complete;
                }

                // Add the task to the target list
                AddTask(task);

                // Update the task in the database
                UpdateTask(task);
            }
        }
        private void ChangeTaskStatus2(TacheDao task)
        {
            if (task == null) return;

            // Remove the task from its current list
            RemoveTask(task);

            // Open the custom dialog to select new status
            var dialog = new ChangeStatusDialog2();
            if (dialog.ShowDialog() == true)
            {
                // Get the selected status from the dialog
                var newStatus = dialog.SelectedStatus;


                if (newStatus == "Complete")
                {
                    task.Statues = TaskStatues.Complete;
                }

                // Add the task to the target list
                AddTask(task);

                // Update the task in the database
                UpdateTask(task);
            }
        }

        public void UpdateTask(TacheDao task)
        {
            var existingTask = _dbContext.Tasks.Find(task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Category = task.Category;
                existingTask.Statues = task.Statues;
                existingTask.StartDate = task.StartDate;
                existingTask.EndDate = task.EndDate;

                _dbContext.SaveChanges();
            }
        }


    }

}  
       
      


