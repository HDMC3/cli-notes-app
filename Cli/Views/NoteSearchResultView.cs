using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class NoteSearchResultView {
        SearchNote _searchNote;
        public NoteSearchResultView(SearchNote searchNote) {
            _searchNote = searchNote;
        }

        public async Task<ViewData> ShowView(NoteSearchResultViewDataType data) {
            AnsiConsole.Clear();
            
            var notes = new List<Note>();
            try
            {
                await Helpers.StartSpinnerAsync("Buscando...", async (ctx) =>
                    {
                        notes = await _searchNote.Search(data.Query);
                    });
            }
            catch (System.Exception)
            {
                return new ViewData(ViewCodes.ErrorView);
            }

            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("RESULTADO DE BUSQUEDA");
            AnsiConsole.Write("Termino buscado: ", new Style(foreground: Colors.primary));
            AnsiConsole.Write(data.Query + "\n");
            AnsiConsole.WriteLine();

            if (notes.Count == 0)
                AnsiConsole.Markup("[grey]--- NO SE ENCONTRARON RESULTADOS ---\n\n[/]");

            var options = notes.Select(n => new OptionMenu<ListOptions, Note>(n.Title, ListOptions.Item, n)).ToList();
            options.Add(new OptionMenu<ListOptions, Note>("\\Regresar a buscar nota", ListOptions.Back));
            options.Add(new OptionMenu<ListOptions, Note>("\\Menu principal", ListOptions.BackMainMenu));
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<ListOptions, Note>>()
                    .HighlightStyle(new Style(foreground: Colors.primary, decoration: Decoration.Underline))
                    .PageSize(10)
                    .UseConverter(t => t.Text)
                    .AddChoices(options)
            );

            if (option.Value != null)
            {
                var noteSearchedDetailViewData = new NoteSearchedDetailViewDataType(option.Value, data.Query);
                return new ViewData(ViewCodes.NoteSearchedDetailViewCode, noteSearchedDetailViewData);
            }
            
            if (option.Code == ListOptions.Back)
                return new ViewData(ViewCodes.SearchNoteViewCode);
            else if (option.Code == ListOptions.BackMainMenu)
                return new ViewData(ViewCodes.HomeViewCode);
            else 
                return new ViewData(ViewCodes.InvalidView);
        }
    }
}