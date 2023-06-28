namespace Code_Snippet_Manager
{
    public abstract class Command
    {
        public abstract void Execute(object[]? args);
        public abstract void Undo(object[]? args);
        public abstract void Describe(object[]? args);
    }

    public class Help : Command
    {
        //自动获取命名空间
        private readonly string _namespace = typeof(Help).Namespace ?? "";
        public override void Execute(object[]? args)
        {
            if (args == null || args.Length == 0)
            {
                Functions.Get_All_Classes_From_Parent_Class(typeof(Command));
            }
            else
            {
                foreach (var arg in args)
                {
                    if (_namespace == "")
                    {
                        Functions.Get_And_Execute_Method(arg.ToString(), "Describe", null, true, true);
                    }
                    else
                    {
                        Functions.Get_And_Execute_Method($"{_namespace}.{arg.ToString()}", "Describe", null, true, true);
                    }
                }
            }
        }

        public override void Undo(object[]? args)
        {
            throw new System.NotImplementedException();
        }

        public override void Describe(object[]? args)
        {
            Console.WriteLine("Help");
        }
    }
}