using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Spectre.Console;

namespace Cli.Views {
    public class SearchNoteView {

        public ViewData ShowView() {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("BUSCAR NOTA");

            var query = AnsiConsole.Ask<string>("Termino de busqueda:");

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<(string, int)>()
                    .UseConverter(opt => opt.Item1)
                    .AddChoices(
                        ("Buscar", 1),
                        ("Cancelar", 2)
                    )
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