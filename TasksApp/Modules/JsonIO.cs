using Newtonsoft.Json;
using System.IO;

namespace TasksApp.Modules
{
    public class JsonIOservice
    {
        private JsonSerializer serializer = new JsonSerializer();
        public string JsonPath { get; set; } = Directory.GetCurrentDirectory() + "/TaskInfo.json";
        public void WriteToJsonFile(ObservableCollectionModifed<TasksClass> tasks)
        {
            using (StreamWriter writer = File.CreateText(JsonPath))
            {
                serializer.Serialize(writer, tasks);
            }
        }

        public ObservableCollectionModifed<TasksClass> LoadTasks()
        {
            var IsExist = File.Exists(JsonPath);
            ObservableCollectionModifed<TasksClass> readTasks = new ObservableCollectionModifed<TasksClass>();
            if (IsExist)
            {
                using (StreamReader reader = File.OpenText(JsonPath))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    readTasks = (ObservableCollectionModifed<TasksClass>)serializer.Deserialize(reader, readTasks.GetType());
                }
            }
            else
            {
                File.CreateText(JsonPath);
            }
            return readTasks;
        }
    }
}
