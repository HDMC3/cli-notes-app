using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Spectre.Console;

namespace Cli.Views {
    public class SearchNoteView {

        public ViewData ShowView() {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("BUSCAR NOTA");

            var query = AnsiConsole.Prompt(
                new TextPrompt<string>("Termino de busqueda:")
                    .AllowEmpty()
            );

            if (String.IsNullOrWhiteSpace(query)) 
                return new ViewData(ViewCodes.HomeViewCode);

            AnsiConsole.WriteLine();

            var option = Helpers.ConfirmationMenuPrompt(
                ("Buscar", 1),
                ("Cancelar", 2)
            );

            if (option.Item2 == 1) {
                var noteSearchResultViewData = new NoteSearchResultViewDataType(query);
                return new ViewData(ViewCodes.NoteSearchResultViewCode, noteSearchResultViewData);
            } else {
                return new ViewData(ViewCodes.HomeViewCode);
            }
        }
    }
}