using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class CreateNoteView {
        IServiceProvider _serviceProvider;
        public CreateNoteView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView(CreateNoteViewDataType data) {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("NUEVA NOTA");

            AnsiConsole.Write(new Markup("--- Libreta: ", new Style(foreground: Color.Grey)));
            AnsiConsole.Write(new Markup(data.Notebook.Name + " ---\n\n", new Style(foreground: Color.Grey)));

            var title = AnsiConsole.Prompt(
                new TextPrompt<string>("Titulo:")
                    .AllowEmpty()
            );

            var description = AnsiConsole.Prompt(
                new TextPrompt<string>("Descripcion:")
                    .AllowEmpty()
            );

            if (String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(description)) {
                var notebookNotesViewData = new NotebookNotesViewDataType(data.Notebook);
                return new ViewData(ViewCodes.NotebookNotesViewCode, notebookNotesViewData);
            }

            AnsiConsole.WriteLine();

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<(string, int)>()
                    .UseConverter(opt => opt.Item1)
                    .AddChoices(
                        ("Guardar", 1),
                        ("Cancelar", 2)
                    )
            );

            if (option.Item2 == 2) {
                var notebookNotesViewData = new NotebookNotesViewDataType(data.Notebook);
                return new ViewData(ViewCodes.NotebookNotesViewCode, notebookNotesViewData);
            }

            var createNote = _serviceProvider.GetService<CreateNote>();

            if (createNote == null)
                return new ViewData(ViewCodes.ErrorView);

            try
            {
                await AnsiConsole.Status()
                    .Spinner(Spinner.Known.SquareCorners)
                    .SpinnerStyle(new Style(foreground: Colors.primary))
                    .StartAsync("Guardando...", async (ctx) =>
                    {
                        await createNote.Create(
                            new Note { 
                                Title = title, 
                                Description = description,
                                NotebookId = data.Notebook.Id
                            }
                        );
                    });
                var notebookNotesViewData = new NotebookNotesViewDataType(data.Notebook);
                return new ViewData(ViewCodes.NotebookNotesViewCode, notebookNotesViewData);
            }
            catch (System.Exception)
            {
                return new ViewData(ViewCodes.ErrorView);
            }
        }
    }
}