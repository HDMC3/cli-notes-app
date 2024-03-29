using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class CreateNoteView {
        CreateNote _createNote;
        public CreateNoteView(CreateNote createNote) {
            _createNote = createNote;
        }

        public async Task<ViewData> ShowView(CreateNoteViewDataType data) {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("NUEVA NOTA");

            Helpers.WriteNotebookIndicator(data.Notebook.Name);

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

            var option = Helpers.ConfirmationMenuPrompt(
                ("Guardar", 1),
                ("Cancelar", 2)
            );

            if (option.Item2 == 2) {
                var notebookNotesViewData = new NotebookNotesViewDataType(data.Notebook);
                return new ViewData(ViewCodes.NotebookNotesViewCode, notebookNotesViewData);
            }

            try
            {
                await  Helpers.StartSpinnerAsync("Guardando...", async (ctx) =>
                    {
                        await _createNote.Create(
                            new Note { 
                                Title = title, 
                                Description = description,
                                CreatedAt = DateTimeOffset.Now,
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