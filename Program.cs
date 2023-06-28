using System.Reflection;
using Newtonsoft.Json;
namespace Code_Snippet_Manager
{
    public class Program
    {
        public static int MaxId;
        public static List<Code_Snippet> CodeSnippets = new List<Code_Snippet>();
        public static int BaseId = -1;
        public class Save_Data
        {
            public int MaxId;
            public List<Code_Snippet> CodeSnippets = new List<Code_Snippet>();
        }
        public static void Main(string[] args)
        {
            var _namespace = "Code_Snippet_Manager";
            var saveData = Load_Data_From_Json<Save_Data>(new Save_Data(), "SaveData", "", true) ?? new Save_Data();
            MaxId = saveData.MaxId;
            CodeSnippets = saveData.CodeSnippets;
            string InputTips = "请输入命令> ";
            if (BaseId != -1 && CodeSnippets != null)
            {
                if (CodeSnippets[BaseId] == null)
                {
                    throw new Exception("BaseId is not valid");
                }
                InputTips = $"{CodeSnippets[BaseId].Title}> ";
            }
            for (; ; )
            {
                Console.Write(InputTips);
                var command = Console.ReadLine() ?? "";
                var commandName = command.Split(' ')[0];
                var commandArgs = command.Split(' ').Length > 1 ? command.Split(' ')[1..] : null;
                Functions.Get_And_Execute_Method($"{_namespace}.{commandName}", "Execute", commandArgs, true, true);
                Save_Data_To_Json<Save_Data>(new Save_Data() { MaxId = MaxId, CodeSnippets = CodeSnippets ?? new List<Code_Snippet>() }, "SaveData", "", true);
            }
        }


        public static T? Load_Data_From_Json<T>(T DataObject, string FileName, string FilePath, bool IsThisFolder)
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


        public static void Save_Data_To_Json<T>(T DataObject, string FileName, string FilePath, bool IsThisFolder)
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
