using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.EntityFrameWork;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
       
        private string _title;
        private string _description;
        private TaskCategory _taskCategory;
        private TaskStatues _taskStatues;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _id;
       

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
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
        


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
