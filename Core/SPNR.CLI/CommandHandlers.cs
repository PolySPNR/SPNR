using System;
using System.CommandLine.Rendering;
using System.CommandLine.Rendering.Views;
using System.IO;
using System.Threading.Tasks;
using SPNR.Core.Models.AuthorInfo;

namespace SPNR.CLI
{
    public partial class EntryPoint
    {
        private async Task ImportAuthorsHandler(FileInfo file)
        {
            await _dataService.ImportAuthorsFromFile(file.FullName);
        }

        private async Task ListAuthorsHandler(int id, int max, string org, string fac, string dep, string pos)
        {
            var list = await _dataService.ListAuthors(id, max, org, fac, dep, pos);

            foreach (var author in list)
            {
                Console.WriteLine($"Author: #{author.AuthorId} {author.Name}");
                Console.WriteLine($"Organization: #{author.OrganizationId} {author.Organization.Name}");
                Console.WriteLine($"Faculty: #{author.FacultyId} {author.Faculty.Name}");
                Console.WriteLine($"Department: #{author.DepartmentId} {author.Department.Name}");
                Console.WriteLine($"Organization: #{author.PositionId} {author.Position.Name}\n");
            }
        }
        
        private static void RenderView<T>(ItemsView<T> view)
        {
            var context = Program.InvocationContext;

            var consoleRenderer = new ConsoleRenderer(
                context.Console,
                context.BindingContext.OutputMode());

            var screen = new ScreenView(consoleRenderer, context.Console) {Child = view};

            screen.Render(new Region(0, 0, new Size(Console.WindowWidth, view.Items.Count + 1)));
            screen.Dispose();

            Console.WriteLine("\n");
        }

    }
}