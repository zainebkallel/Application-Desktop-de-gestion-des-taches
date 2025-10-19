using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.ViewModels;
using WpfApp1.Models;


namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public EditTaskWindow(TacheDao task)
        {
            InitializeComponent();
            var viewModel = new EditTaskViewModel(task, this);
            DataContext = viewModel;
        }
    }
}
