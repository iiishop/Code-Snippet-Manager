using System.Reflection;

namespace Code_Snippet_Manager
{
    public class Functions
    {
        public static void Get_All_Classes_From_Parent_Class(Type? parent_class)
        {
            try
            {
                if (parent_class == null)
                {
                    throw new Exception("No parent class");
                }

                List<string> commands = new List<string>();
                foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (type.IsSubclassOf(parent_class))
                    {
                        commands.Add(type.Name);
                    }
                }
                commands.Sort();
                foreach (var command in commands)
                {
                    Console.WriteLine(command);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// By utilizing only the string representation of the class and method names, this approach is capable of acquiring and executing a method within a class.
        /// </summary>
        /// <param name="className">the name of the class which the method we need in</param>
        /// <param name="methodName">the name of the method</param>
        /// <param name="args">the args that will be added when executing the method(can be null)</param>
        /// <param name="ignoreClassCase">ignore class case if is true</param>
        /// <param name="ignoreMethodCase">ignore method case if is true</param>
        public static void Get_And_Execute_Method(string? className, string methodName, object[]? args, bool ignoreClassCase = false, bool ignoreMethodCase = false)
        {
            try
            {
                if (string.IsNullOrEmpty(className))
                    throw new Exception("No class name");

                var type = Type.GetType(className, false, ignoreClassCase) ?? throw new Exception($"Class: {className} not found");

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
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        public static string Get_Multi_Input()
        {
            //ConsoleKeyInfo cki;
            //Console.TreatControlCAsInput = true;//防止Ctrl+C复制
            Console.WriteLine("Press the CTRL+Enter key to quit: \n");
            string result = string.Empty;
            while (true)
            {
                var cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Enter)
                {
                    result += System.Environment.NewLine;//如果输入回车，则加入换行标志
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    result += cki.KeyChar;
                }
            }
            return result;



            /*string result = string.Empty;
            do
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Enter)
                {
                    result += System.Environment.NewLine;//如果输入回车，则加入换行标志
                    Console.SetCursorPosition(0, Console.CursorTop + 1);//光标下移一行
                }
                result += cki.KeyChar;
            }
            while (cki.Key != ConsoleKey.Enter || (cki.Modifiers & ConsoleModifiers.Control) == 0);//按Ctrl+Enter退出
            Console.WriteLine("result");
            return result;*/
        }
    }
}