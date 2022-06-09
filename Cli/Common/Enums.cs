namespace Cli.Common {
    public enum MainMenuOptions {
        ShowNotebooks,
        CreateNotebook,
        EditNotebook,
        SearchNote,
        Exit
    }

    public enum ListOptions {
        Item,
        Create,
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
        CreateNotebookViewCode,
        EditNotebookViewCode,
        SearchNoteViewCode,
        NoteSearchResultViewCode,
        NoteSearchedDetailViewCode,
        NoteOptionsViewCode,
        NotebookNotesViewCode,
        CreateNoteViewCode,
        NoteDetailViewCode,
        InvalidView,
        ExitApp,
        ErrorView
    }
}