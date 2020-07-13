using System;
using System.Collections.Specialized;
using System.Windows;
using TasksApp.Modules;
using System.Windows.Controls;
using System.Windows.Input;

namespace TasksApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollectionModifed<TasksClass> tasksClasses;

        private ObservableCollectionModifed<TasksClass> tasksOnSelectedDate;

        private JsonIOservice IOservice;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IOservice = new JsonIOservice();
            try
            {
                tasksClasses = IOservice.LoadTasks();
                tasksOnSelectedDate = new ObservableCollectionModifed<TasksClass>();
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            AddTasksDate();
            tasksClasses.CollectionChanged += Collection_Changed;
            StackPanelTasks.DataContext = tasksOnSelectedDate;
            MainDataGrid.ItemsSource = tasksClasses;
        }

        private void AddTasksDate()
        {
            foreach (var task in tasksClasses)
            {
                TaskCalendar.BlackoutDates.Add(new CalendarDateRange(task.TaskEditTime, task.TaskEditTime));
            }
        }

        private void AddTasksOnSelectedDate(DateTime? date)
        {

            if (date != null)
            {
                DateTime dateTime = (DateTime)date;
                foreach (var task in tasksClasses)
                {
                    if (task.TaskEditTime.Date.Equals(dateTime.Date))
                    {
                        tasksOnSelectedDate.Add(task);
                    }
                } 
            }
        }

        private void Collection_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            IOservice.WriteToJsonFile(tasksClasses);
            AddTasksDate();
        }

        private void MainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ;  
        }

        private void TaskCalendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ;
        }
    }
}
