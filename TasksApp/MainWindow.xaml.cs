using System;
using System.Collections.Specialized;
using System.Windows;
using TasksApp.Modules;
using System.Windows.Controls;

namespace TasksApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollectionModifed<TasksClass> tasksClasses;

        private JsonIOservice IOservice;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IOservice = new JsonIOservice();
            try
            {
                tasksClasses = IOservice.LoadTasks();
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            AddTasksDate();
            tasksClasses.CollectionChanged += Collection_Changed;
            MainDataGrid.ItemsSource = tasksClasses;
        }

        private void AddTasksDate()
        {
            foreach (var task in tasksClasses)
            {
                TaskCalendar.BlackoutDates.Add(new CalendarDateRange(task.TaskEditTime, task.TaskEditTime));
            }
        }

        private void Collection_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            IOservice.WriteToJsonFile(tasksClasses);
            AddTasksDate();
        }
    }
}
