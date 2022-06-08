using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class NoteOptionsView {
        public ViewData ShowView(NoteOptionsViewData data) {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("OPCIONES | " + data.Note.Title);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<NoteOptions, Object>>()
                    .UseConverter(t => t.Text)
                    .AddChoices(
                        new OptionMenu<NoteOptions, Object>("Ver nota", NoteOptions.ShowNote),
                        new OptionMenu<NoteOptions, Object>("Editar nota", NoteOptions.EditNote),
                        new OptionMenu<NoteOptions, Object>("Eliminar nota", NoteOptions.DeleteNote),
                        new OptionMenu<NoteOptions, Object>("Regresar a lista de notas", NoteOptions.BackNotebookList),
                        new OptionMenu<NoteOptions, Object>("Menu principal", NoteOptions.BackMainMenu)
                    )
            );

            return SelectOption(option, data.Notebook, data.Note);
        }

        public ViewData SelectOption(OptionMenu<NoteOptions, Object> option, Notebook notebook, Note note)
        {
            switch (option.Code)
            {
                case NoteOptions.ShowNote:
                    var noteDetailViewData = new NoteDetailViewData(notebook, note);
                    return new ViewData(ViewCodes.NoteDetailViewCode, noteDetailViewData);
                case NoteOptions.EditNote:
                    AnsiConsole.Write("Edicion de nota");
                    return new ViewData(ViewCodes.ExitApp);
                case NoteOptions.DeleteNote:
                    AnsiConsole.Write("Eliminacion de nota");
                    return new ViewData(ViewCodes.ExitApp);
                case NoteOptions.BackNotebookList:
                    var notebookNotesViewData = new NotebookNotesViewData(notebook);
                    return new ViewData(ViewCodes.NotebookNotesViewCode, notebookNotesViewData);
                case NoteOptions.BackMainMenu:
                    return new ViewData(ViewCodes.HomeViewCode);
                default:
                    return new ViewData(ViewCodes.InvalidView);
            }
        }
    }
}