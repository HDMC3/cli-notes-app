namespace Cli.Common {
    public enum MainMenuOptions {
        ShowNotebooks,
        CreateNotebook,
        EditNotebook,
        CreateNote,
        SearchNote,
        Exit
    }

    public enum ListOptions {
        Item,
        BackMainMenu,
        Back
    }

    public enum  NoteOptions {
        ShowNote,
        EditNote,
        DeleteNote,
        BackNotebookList,
        BackMainMenu
    }

    public enum ViewCodes {
        HomeViewCode,
        NotebookListViewCode,
        NoteOptionsViewCode,
        NotebookNotesViewCode,
        NoteDetailViewCode,
        InvalidView,
        ExitApp
    }
}