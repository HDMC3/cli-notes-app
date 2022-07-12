using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Services;
using Spectre.Console;

namespace Cli.Views {
    public class DeleteNoteView {
        DeleteNote _deleteNote;
        public DeleteNoteView(DeleteNote deleteNote) {
            _deleteNote = deleteNote;
        }

        public async Task<ViewData> ShowView(DeleteNoteViewDataType data) {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("ELIMINAR NOTA");

            Helpers.WriteNotebookIndicator(data.Notebook.Name);

            AnsiConsole.Write(new Markup("Titulo: ", new Style(foreground: Colors.primary)));
            AnsiConsole.Write(data.Note.Title + "\n");
            AnsiConsole.Write(new Markup("Descripcion: ", new Style(foreground: Colors.primary)));
            AnsiConsole.Write(data.Note.Description + "\n");

            AnsiConsole.WriteLine();

            var option = Helpers.ConfirmationMenuPrompt(
                ("Confirmar", 1),
                ("Cancelar", 2)
            );

            if (option.Item2 == 2) {
                var noteOptionsViewDataType = new NoteOptionsViewDataType(data.Notebook, data.Note);
                return new ViewData(ViewCodes.NoteOptionsViewCode, noteOptionsViewDataType);
            }

            try
            {
                await Helpers.StartSpinnerAsync("Elimando...", async (ctx) =>
                    {
                        await _deleteNote.Delete(data.Note);
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