using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp72
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskDbContext _dbContext;

        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new TaskDbContext();
            LoadTasks();
        }

        private void LoadTasks()
        {
            TaskListView.ItemsSource = _dbContext.Tasks.ToList();
        }

        private void TaskTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddTask();
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(TaskTextBox.Text))
            {
                var task = new Task
                {
                    Description = TaskTextBox.Text,
                    IsCompleted = false
                };

                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();
                TaskTextBox.Clear();
                LoadTasks();
            }
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var taskToDelete = (sender as Button)?.DataContext as Task;

            if (taskToDelete != null)
            {
                _dbContext.Tasks.Remove(taskToDelete);
                _dbContext.SaveChanges();
                LoadTasks();
            }
        }
    }
}


