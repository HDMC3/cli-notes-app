using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Spectre.Console;

namespace Cli.Views {
    public class DeleteNoteView {
        IServiceProvider _serviceProvider;
        public DeleteNoteView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
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

            var deleteNote = _serviceProvider.GetService<DeleteNote>();

            if (deleteNote == null) 
                return new ViewData(ViewCodes.ErrorView);

            try
            {
                await Helpers.StartSpinnerAsync("Elimando...", async (ctx) =>
                    {
                        await deleteNote.Delete(data.Note);
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