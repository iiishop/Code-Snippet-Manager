using System.Reflection;
using Newtonsoft.Json;
namespace Code_Snippet_Manager
{
    public class Program
    {
        public static int MaxId;
        public class Save_Data
        {
            public int MaxId;
            public List<Code_Snippet>? Code_Snippets;
        }
        public static void Main(string[] args)
        {
            var _namespace = "Code_Snippet_Manager";
            for (; ; )
            {
                Console.Write("请输入命令> ");
                var command = Console.ReadLine() ?? "";
                //将command分割为命令名和参数
                var commandName = command.Split(' ')[0];
                var commandArgs = command.Split(' ').Length > 1 ? command.Split(' ')[1..] : null;
                Functions.Get_And_Execute_Method(_namespace + "." + commandName, "Execute", commandArgs, true, true);
            }
        }


        public T? Load_Data_From_Json<T>(T DataObject, string FileName, string FilePath, bool IsThisFolder)
        {
            try
            {
                if (IsThisFolder)
                {
                    FilePath = System.IO.Directory.GetCurrentDirectory() + "\\" + FilePath;
                }
                var json = System.IO.File.ReadAllText(FilePath + "\\" + FileName + ".json");
                var data = JsonConvert.DeserializeObject<T>(json);
                return data ?? throw new System.Exception("Data is null");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return default;
            }
        }


        public void Save_Data_To_Json<T>(T DataObject, string FileName, string FilePath, bool IsThisFolder)
        {
            try
            {
                var json = JsonConvert.SerializeObject(DataObject);
                if (IsThisFolder)
                {
                    FilePath = System.IO.Directory.GetCurrentDirectory() + "\\" + FilePath;
                }
                System.IO.File.WriteAllText(FilePath + "\\" + FileName + ".json", json);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
