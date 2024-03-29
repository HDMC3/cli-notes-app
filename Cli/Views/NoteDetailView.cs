using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Spectre.Console;

namespace Cli.Views {
    public class NoteDetailView {
        public ViewData ShowView(NoteDetailViewDataType data) {
            AnsiConsole.Clear();
            
            Helpers.WriteRuleWidget("DETALLE DE NOTA");

            Helpers.WriteNotebookIndicator(data.Notebook.Name);
            
            AnsiConsole.Write(new Markup("Titulo:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.Title + "\n");
            AnsiConsole.Write(new Markup("Descripcion:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.Description + "\n");
            AnsiConsole.Write(new Markup("Fecha de creacion:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(data.Note.CreatedAt.ToString());
            
            AnsiConsole.WriteLine();

            var option = Helpers.ConfirmationMenuPrompt(
                ("Regresar a opciones de nota", 1),
                ("Menu principal", 2)
            );

            if (option.Item2 == 1) {
                var noteOptionsViewData = new NoteOptionsViewDataType(data.Notebook, data.Note);
                return new ViewData(ViewCodes.NoteOptionsViewCode, noteOptionsViewData);
            }
            else if (option.Item2 == 2)
                return new ViewData(ViewCodes.HomeViewCode);
            else
                return new ViewData(ViewCodes.InvalidView);
        }
    }
}