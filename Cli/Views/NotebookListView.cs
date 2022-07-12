using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class NotebookListView {
        // IServiceProvider _serviceProvider;
        // public NotebookListView(IServiceProvider serviceProvider) {
        //     _serviceProvider = serviceProvider;
        // }
        GetNotebooks _getNotebooks;
        public NotebookListView(GetNotebooks getNotebooks) {
            _getNotebooks = getNotebooks;
        }

        public async Task<ViewData> ShowView(NotebookListViewDataType data) {

            AnsiConsole.Clear();

            var notebooks = new List<Notebook>();
            try
            {
                await Helpers.StartSpinnerAsync("Cargando libretas...", async (ctx) => {
                        notebooks = await _getNotebooks.Get();
                    });
            }
            catch (System.Exception)
            {
                return new ViewData(ViewCodes.ErrorView);
            }

            Helpers.WriteRuleWidget("LIBRETAS");

            if (notebooks.Count == 0)
                AnsiConsole.Markup("[grey]--- NO SE ENCONTRARON LIBRETAS ---\n\n[/]");

            var options = notebooks.Select(n => new OptionMenu<ListOptions, Notebook>(n.Name, ListOptions.Item, n)).ToList();
            options.Add(new OptionMenu<ListOptions, Notebook>("\\Regresar", ListOptions.Back));
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<ListOptions, Notebook>>()
                    .HighlightStyle(new Style(foreground: Colors.primary, decoration: Decoration.Underline))
                    .UseConverter(t => t.Text)
                    .AddChoices(options)
            );

            if (option.Value != null)
            {
                var notebookNotesViewData = new NotebookNotesViewDataType(option.Value);
                var editNotebookViewData = new EditNotebookViewDataType(option.Value);
                var deleteNotebookViewData = new DeleteNotebookViewDataType(option.Value);

                if (data.Edit)
                    return new ViewData(ViewCodes.EditNotebookViewCode, editNotebookViewData);
                else if (data.Delete)
                    return new ViewData(ViewCodes.DeleteNotebookViewCode, deleteNotebookViewData);
                else    
                    return new ViewData(ViewCodes.NotebookNotesViewCode, notebookNotesViewData);
            }
            
            if (option.Code == ListOptions.Back)
            {
                return new ViewData(ViewCodes.HomeViewCode);
            } else {
                return new ViewData(ViewCodes.InvalidView);
            }
        }
    }
}