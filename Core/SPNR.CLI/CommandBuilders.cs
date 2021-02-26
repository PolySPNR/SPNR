using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

namespace SPNR.CLI
{
    public partial class EntryPoint
    {
        private void BuildCommands()
        {
            var rootCmd = new RootCommand("SPNR")
            {
                BuildImportCommand(),
                BuildListCommand()
            };

            rootCmd.InvokeAsync(Program.Arguments).Wait();
        }
        
        private Command BuildImportCommand()
        {
            var impAuthorsCmd = new Command("authors", "Import authors to database")
            {
                new Argument<FileInfo>("file", "Path to docx file").ExistingOnly()
            };

            impAuthorsCmd.Handler = CommandHandler.Create<FileInfo>(ImportAuthorsHandler);

            var cmd = new Command("import", "Data import base command")
            {
                impAuthorsCmd
            };

            return cmd;
        }
        
        private Command BuildListCommand()
        {
            var listAuthorsCmd = new Command("authors", "List authors")
            {
                new Option<int>("--id", () => 1, "ID of author to start listing"),
                new Option<int>("--max", () => 10, "Maximum authors to show"),
                new Option<string>("--org", "Organization"),
                new Option<string>("--fac", "Faculty"),
                new Option<string>("--dep", "Department"),
                new Option<string>("--pos", "Position")
            };
            
            listAuthorsCmd.Handler = CommandHandler.Create<int, int, string, string, string, string>(ListAuthorsHandler);
            
            var cmd = new Command("list", "List entities in database")
            {
                listAuthorsCmd
            };

            return cmd;
        }
    }
}