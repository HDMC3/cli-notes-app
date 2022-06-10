using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class NotebookNotesView {
        IServiceProvider _serviceProvider;
        public NotebookNotesView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView(NotebookNotesViewDataType data) {
            var getNotes = _serviceProvider.GetService<GetNotes>();
            AnsiConsole.Clear();

            if (getNotes == null) 
                return new ViewData(ViewCodes.ErrorView);

            var notes = new List<Note>();
            try
            {
                await Helpers.StartSpinnerAsync("Cargando notas...", async (ctx) =>
                    {
                        notes = await getNotes.Get(data.Notebook.Id);
                    });
            }
            catch (System.Exception)
            {
                return new ViewData(ViewCodes.ErrorView);
            }

            Helpers.WriteRuleWidget("NOTAS | " + data.Notebook.Name);

            if (notes.Count == 0)
                AnsiConsole.Markup("[grey]--- NO SE ENCONTRARON NOTAS ---\n\n[/]");

            var options = notes.Select(n => new OptionMenu<ListOptions, Note>(n.Title, ListOptions.Item, n)).ToList();
            options.Add(new OptionMenu<ListOptions, Note>("\\Nueva nota", ListOptions.Create));
            options.Add(new OptionMenu<ListOptions, Note>("\\Regresar a lista de libretas", ListOptions.Back));
            options.Add(new OptionMenu<ListOptions, Note>("\\Menu principal", ListOptions.BackMainMenu));
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<ListOptions, Note>>()
                    .PageSize(10)
                    .UseConverter(t => t.Text)
                    .AddChoices(options)
            );

            if (option.Value != null)
            {
                var noteOptionsViewData = new NoteOptionsViewDataType(data.Notebook, option.Value);
                return new ViewData(ViewCodes.NoteOptionsViewCode, noteOptionsViewData);
            }
            
            if (option.Code == ListOptions.Create){
                var createNoteViewData = new CreateNoteViewDataType(data.Notebook);
                return new ViewData(ViewCodes.CreateNoteViewCode, createNoteViewData);
            }
            else if (option.Code == ListOptions.Back) {
                var notebookListViewData = new NotebookListViewDataType(false);
                return new ViewData(ViewCodes.NotebookListViewCode, notebookListViewData);
            }
            else if (option.Code == ListOptions.BackMainMenu)
                return new ViewData(ViewCodes.HomeViewCode);
            else 
                return new ViewData(ViewCodes.InvalidView);
        }
    }
}