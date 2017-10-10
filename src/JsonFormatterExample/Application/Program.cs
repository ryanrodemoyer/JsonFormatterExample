namespace Application
{
    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var pi = new ProgramInitializer(new DefaultProgramActions());
            pi.Go(args);
        }
    }
}
