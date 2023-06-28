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
        public override void Execute(object[]? args)
        {
            Console.WriteLine("Help");
        }

        public override void Undo(object[]? args)
        {
            throw new System.NotImplementedException();
        }

        public override void Describe(object[]? args)
        {
            throw new System.NotImplementedException();
        }
    }
}