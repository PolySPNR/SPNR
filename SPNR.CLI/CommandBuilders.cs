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
                new Option<string>("--org", description: "Organization"),
                new Option<string>("--fac", description: "Faculty"),
                new Option<string>("--dep", description: "Department")
            };
            
            listAuthorsCmd.Handler = CommandHandler.Create<string, string, string>(ListAuthorsHandler);
            
            var cmd = new Command("list", "List entities in database")
            {
                listAuthorsCmd
            };

            return cmd;
        }
    }
}