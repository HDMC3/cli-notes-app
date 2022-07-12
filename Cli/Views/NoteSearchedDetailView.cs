using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class NoteSearchedDetailView {
        GetNotebook _getNotebook;
        public NoteSearchedDetailView(GetNotebook getNotebook) {
            _getNotebook = getNotebook;
        }

        public async Task<ViewData> ShowView(NoteSearchedDetailViewDataType data) {
            AnsiConsole.Clear();
            
            Notebook? notebook = null;
            try
            {
                await Helpers.StartSpinnerAsync("Cargando...", async (ctx) =>
                    {
                        notebook = await _getNotebook.Get(data.Note.NotebookId);
                    });
            }
            catch (System.Exception)
            {
                return new ViewData(ViewCodes.ErrorView);
            }

            AnsiConsole.Clear();
            Helpers.WriteRuleWidget("DETALLE DE NOTA");
            
            if (notebook != null)
                Helpers.WriteNotebookIndicator(notebook.Name);

            AnsiConsole.Write(new Markup("Titulo:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.Title + "\n");
            AnsiConsole.Write(new Markup("Descripcion:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.Description + "\n");
            AnsiConsole.Write(new Markup("Fecha de creacion:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.CreatedAt.ToString());
            
            AnsiConsole.WriteLine();

            var option = Helpers.ConfirmationMenuPrompt(
                ("Regresar a resultados", 1),
                ("Menu principal", 2)
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