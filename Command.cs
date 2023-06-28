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
            Console.WriteLine("Help [command]\nYou can use this command to get help of other commands by using the command name as the argument such as: Help Help");
        }
    }


    public class SetBaseID : Command
    {
        public override void Execute(object[]? args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please enter a number");
                return;
            }

            if (int.TryParse(args[0].ToString(), out var id))
            {
                Program.MaxId = id;
                Console.WriteLine($"Base id set to {id}");
            }
            else
            {
                Console.WriteLine("Please enter a number");
            }
        }

        public override void Undo(object[]? args)
        {
            throw new System.NotImplementedException();
        }

        public override void Describe(object[]? args)
        {
            Console.WriteLine("SetBaseID [number]\nYou can use this command to set the base id of the next code snippet that will be created");
        }
    }


    public class ShowAllSnippets : Command
    {
        public override void Execute(object[]? args)
        {
            int i = 0;
            if (args == null || args.Length == 0)
            {
                foreach (var snippet in Program.CodeSnippets ?? throw new Exception("No code snippets"))
                {
                    Console.WriteLine($"{i++}.{snippet.ToString()}");
                }
            }
            else
            {
                foreach (var arg in args)
                {
                    var language = arg.ToString();
                    foreach (var snippet in Program.CodeSnippets ?? throw new Exception("No code snippets"))
                    {
                        if (snippet.Language.ToString() == language)
                        {
                            Console.WriteLine($"{i++}.{snippet.ToString()}");
                        }
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
            Console.WriteLine("ShowAllSnippets [language]\nYou can use this command to show all code snippets in the database");
        }
    }


    public class ShowAllSupportLanguage : Command
    {
        public override void Execute(object[]? args)
        {
            int i = 0;
            foreach (var language in Enum.GetValues(typeof(Language)))
            {
                Console.WriteLine($"{i++}.{language}");
            }
        }

        public override void Undo(object[]? args)
        {
            throw new System.NotImplementedException();
        }

        public override void Describe(object[]? args)
        {
            Console.WriteLine("ShowAllSupportLanguage\nYou can use this command to show all support languages");
        }
    }


    public class ShowAllModes : Command
    {
        public override void Execute(object[]? args)
        {
            int i = 0;
            foreach (var mode in Enum.GetValues(typeof(Mode)))
            {
                Console.WriteLine($"{i++}.{mode}");
            }
        }

        public override void Undo(object[]? args)
        {
            throw new System.NotImplementedException();
        }

        public override void Describe(object[]? args)
        {
            Console.WriteLine("ShowAllModes\nYou can use this command to show all modes");
        }
    }


    public class AddSnippet : Command
    {
        public override void Execute(object[]? args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please enter a title");
                return;
            }

            var title = args[0].ToString() ?? throw new Exception("No title");
            var language = Language.CSharp;
            var mode = Mode.Other;
            var code = "";
            var notes = "";

            new ShowAllSupportLanguage().Execute(null);
        ChooseLanguage:
            Console.WriteLine("Please enter a language index");
            int languageInput = int.TryParse(Console.ReadLine(), out languageInput) ? languageInput : 0;
            if (languageInput < 0 || languageInput >= Enum.GetValues(typeof(Language)).Length)
            {
                Console.WriteLine("Invalid language index");
                goto ChooseLanguage;
            }
            language = (Language)languageInput;

            new ShowAllModes().Execute(null);
        ChooseMode:
            Console.WriteLine("Please enter a mode index");
            int modeInput = int.TryParse(Console.ReadLine(), out modeInput) ? modeInput : 0;
            if (modeInput < 0 || modeInput >= Enum.GetValues(typeof(Mode)).Length)
            {
                Console.WriteLine("Invalid mode index");
                goto ChooseMode;
            }
            mode = (Mode)modeInput;

            Console.WriteLine("Please enter the code");
            code = Functions.Get_Multi_Input() ?? throw new Exception("No code");

            Console.WriteLine("Please enter the notes");
            notes = Functions.Get_Multi_Input() ?? throw new Exception("No notes");

            Program.CodeSnippets.Add(new Code_Snippet(title, language, mode, code, notes));
            Console.WriteLine($"Code snippet {title} added");
        }

        public override void Undo(object[]? args)
        {
            throw new System.NotImplementedException();
        }

        public override void Describe(object[]? args)
        {
            Console.WriteLine("AddSnippet Title\nYou can use this command to add a code snippet to the database");
        }
    }
}