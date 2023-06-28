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
            try
            {

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// This method can get and execute a method in a class by only using the string of the class name and the method name
        /// </summary>
        /// <param name="className">the name of the class which the method we need in</param>
        /// <param name="methodName">the name of the method</param>
        /// <param name="args">the args that will be added when executing the method(can be null)</param>
        /// <param name="ignoreClassCase">ignore class case if is true</param>
        /// <param name="ignoreMethodCase">ignore method case if is true</param>
        public static void Get_And_Execute_Method(string? className, string methodName, object[]? args, bool ignoreClassCase = false, bool ignoreMethodCase = false)
        {
            if (className == null)
                throw new Exception("No class name");

            var type = Type.GetType(className, false, ignoreClassCase);
            if (type == null)
                throw new Exception($"Class: {className} not found");

            var method = type.GetMethod(methodName);
            if (method == null)
            {
                if (ignoreMethodCase)
                {
                    foreach (var m in type.GetMethods())
                    {
                        if (m.Name.ToLower() == methodName.ToLower())
                        {
                            Console.WriteLine($"Method: {methodName} cannot be found, is Method: {m.Name} you wish for ?");
                            method = m;
                            goto MethodCheckComplete;
                        }
                    }
                }
                throw new Exception($"Method: {methodName} is not exist in Class: {className}");
            }
        MethodCheckComplete:
            method.Invoke(Activator.CreateInstance(type), new object[] { args ?? new object[] { } });
        }


        public T Load_Data_From_Json<T>(T DataObject, string FileName, string FilePath, bool IsThisFolder)
        {
            if (IsThisFolder)
            {
                FilePath = System.IO.Directory.GetCurrentDirectory() + "\\" + FilePath;
            }
            var json = System.IO.File.ReadAllText(FilePath + "\\" + FileName + ".json");
            var data = JsonConvert.DeserializeObject<T>(json);
            return data ?? throw new System.Exception("Data is null");
        }


        public void Save_Data_To_Json<T>(T DataObject, string FileName, string FilePath, bool IsThisFolder)
        {
            var json = JsonConvert.SerializeObject(DataObject);
            if (IsThisFolder)
            {
                FilePath = System.IO.Directory.GetCurrentDirectory() + "\\" + FilePath;
            }
            System.IO.File.WriteAllText(FilePath + "\\" + FileName + ".json", json);
        }
    }
}
