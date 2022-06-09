using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class EditNoteView {
        IServiceProvider _serviceProvider;
        public EditNoteView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView(EditNoteViewDataType data) {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("EDITAR NOTA");

            Helpers.WriteNotebookIndicator(data.Notebook.Name);
            
            AnsiConsole.Write(new Markup("Titulo: ", new Style(foreground: Colors.primary)));
            AnsiConsole.Write(data.Note.Title + "\n");
            AnsiConsole.Write(new Markup("Descripcion: ", new Style(foreground: Colors.primary)));
            AnsiConsole.Write(data.Note.Description + "\n");
            
            AnsiConsole.WriteLine();
            var title = AnsiConsole.Prompt(
                new TextPrompt<string>("Nuevo titulo: ")
                    .AllowEmpty()
            );   
            var description = AnsiConsole.Prompt(
                new TextPrompt<string>("Nueva descripcion:")
                    .AllowEmpty()
            );

            if (String.IsNullOrWhiteSpace(title) && String.IsNullOrWhiteSpace(description)) {
                var noteOptionsViewData = new NoteOptionsViewDataType(data.Notebook, data.Note);
                return new ViewData(ViewCodes.NoteOptionsViewCode, noteOptionsViewData);
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
                var noteOptionsViewData = new NoteOptionsViewDataType(data.Notebook, data.Note);
                return new ViewData(ViewCodes.NoteOptionsViewCode, noteOptionsViewData);
            }

            var editNote = _serviceProvider.GetService<EditNote>();
            if (editNote == null)
                return new ViewData(ViewCodes.ErrorView);

            try
            {
                await Helpers.StartSpinnerAsync("Guardando...", async (ctx) =>
                    {
                        data.Note.Title = String.IsNullOrWhiteSpace(title) ? data.Note.Title : title;
                        data.Note.Description = String.IsNullOrWhiteSpace(description) ? data.Note.Description : description;
                        await editNote.Edit(data.Note);
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