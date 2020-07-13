using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TasksApp.Modules
{
    public class TasksClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void TaskChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private DateTime taskEditTime = DateTime.Now;

        private string taskInfo = null;

        private bool isDone = false;

        public DateTime TaskEditTime
        {
            get 
            {
                return taskEditTime;
            }
            set 
            { 
                taskEditTime = value;
                TaskChanged("CreateTime");
            }
        }

        public string TaskInfo
        {
            get 
            { 
                return taskInfo ?? "NoInfo"; 
            }
            set 
            { 
                taskInfo = value;
                TaskChanged("TaskInfo");
            }
        }

        public bool IsDone
        {
            get { return isDone; }
            set
            {
                if (isDone != value)
                {
                    isDone = value;
                    TaskChanged("IsDone");
                }
            }
        }
    }
}
