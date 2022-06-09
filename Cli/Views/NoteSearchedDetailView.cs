using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class NoteSearchedDetailView {
        IServiceProvider _serviceProvider;
        public NoteSearchedDetailView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView(NoteSearchedDetailViewDataType data) {
            AnsiConsole.Clear();

            var getNotebook = _serviceProvider.GetService<GetNotebook>();
            Notebook? notebook = null;
            if (getNotebook != null) {
                try
                {
                    await Helpers.StartSpinnerAsync("Cargando...", async (ctx) =>
                        {
                            notebook = await getNotebook.Get(data.Note.NotebookId);
                        });
                }
                catch (System.Exception)
                {
                    return new ViewData(ViewCodes.ErrorView);
                }
            }

            AnsiConsole.Clear();
            Helpers.WriteRuleWidget("DETALLE DE NOTA");
            
            if (notebook != null) {
                AnsiConsole.Write(new Markup("Libreta:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
                AnsiConsole.WriteLine(notebook.Name + "\n");
            }
            AnsiConsole.Write(new Markup("Titulo:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.Title + "\n");
            AnsiConsole.Write(new Markup("Descripcion:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.Description + "\n");
            AnsiConsole.Write(new Markup("Fecha de creacion:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.CreatedAt.ToString());
            
            AnsiConsole.WriteLine();

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<(string, int)>()
                    .UseConverter(opt => opt.Item1)
                    .AddChoices(
                        ("Regresa a resultados", 1),
                        ("Menu principal", 2)
                    )
            );

            if (option.Item2 == 1) {
                var noteSearchResultViewData = new NoteSearchResultViewDataType(data.Query);
                return new ViewData(ViewCodes.NoteSearchResultViewCode, noteSearchResultViewData);
            }
            else if (option.Item2 == 2)
                return new ViewData(ViewCodes.HomeViewCode);
            else
                return new ViewData(ViewCodes.InvalidView);
        }
    }
}