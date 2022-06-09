using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class NotebookListView {
        IServiceProvider _serviceProvider;
        public NotebookListView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView(NotebookListViewDataType data) {
            var getNotebooks = _serviceProvider.GetService<GetNotebooks>();
            AnsiConsole.Clear();
            var notebooks = new List<Notebook>();
            if (getNotebooks != null)
            {
                await AnsiConsole.Status()
                    .Spinner(Spinner.Known.SquareCorners)
                    .SpinnerStyle(Style.Parse("green"))
                    .StartAsync("Cargando libretas...", async (ctx) =>
                    {
                        notebooks = await getNotebooks.Get();
                    });
            }

            Helpers.WriteRuleWidget("LIBRETAS");

            var options = notebooks.Select(n => new OptionMenu<ListOptions, Notebook>(n.Name, ListOptions.Item, n)).ToList();
            options.Add(new OptionMenu<ListOptions, Notebook>("\\Regresar", ListOptions.Back));
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<ListOptions, Notebook>>()
                    .UseConverter(t => t.Text)
                    .AddChoices(options)
            );

            if (option.Value != null)
            {
                var notebookNotesViewData = new NotebookNotesViewDataType(option.Value);
                var editNotebookViewData = new EditNotebookViewDataType(option.Value);
                return !data.Edit
                    ? new ViewData(ViewCodes.NotebookNotesViewCode, notebookNotesViewData) 
                    : new ViewData(ViewCodes.EditNotebookViewCode, editNotebookViewData);
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