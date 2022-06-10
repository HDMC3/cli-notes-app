using Cli.Common;
using Cli.ViewDataTypes;
using Cli.Views;
using Spectre.Console;

namespace Cli.Application
{
    public class App
    {
        private IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Run()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Cli Notes App")
                    .Alignment(Justify.Center)
                    .Color(Colors.primary)
            );
            Thread.Sleep(3000);
            AnsiConsole.Clear();

            var homeView = new HomeView();
            var notebookListView = new NotebookListView(_serviceProvider);
            var createNotebookView = new CreateNotebookView(_serviceProvider);
            var notebookNotesView = new NotebookNotesView(_serviceProvider);
            var noteDetailView = new NoteDetailView();
            var noteOptionsView = new NoteOptionsView();
            var errorView = new ErrorView();
            var editNotebookView = new EditNotebookView(_serviceProvider);
            var searchNoteView = new SearchNoteView();
            var noteSearchResultView = new NoteSearchResultView(_serviceProvider);
            var noteSearchedDetailView = new NoteSearchedDetailView(_serviceProvider);
            var createNoteView = new CreateNoteView(_serviceProvider);
            var editNoteView = new EditNoteView(_serviceProvider);
            var deleteNoteView = new DeleteNoteView(_serviceProvider);
            var deleteNotebookView = new DeleteNotebookView(_serviceProvider);

            ViewData viewSelected = homeView.ShowView();
            
            while(viewSelected.Code != ViewCodes.ExitApp && viewSelected.Code != ViewCodes.InvalidView) {
                if (viewSelected.Data != null) {
                    if (viewSelected.Code == ViewCodes.NotebookNotesViewCode) {
                        var data = viewSelected.Data as NotebookNotesViewDataType;
                        viewSelected = await notebookNotesView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.NotebookListViewCode) {
                        var data = viewSelected.Data as NotebookListViewDataType;
                        viewSelected = await notebookListView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.NoteDetailViewCode) {
                        var data = viewSelected.Data as NoteDetailViewDataType;
                        viewSelected = noteDetailView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.NoteOptionsViewCode) {
                        var data = viewSelected.Data as NoteOptionsViewDataType;
                        viewSelected = noteOptionsView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.EditNotebookViewCode) {
                        var data = viewSelected.Data as EditNotebookViewDataType;
                        viewSelected = await editNotebookView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.NoteSearchResultViewCode) {
                        var data = viewSelected.Data as NoteSearchResultViewDataType;
                        viewSelected = await noteSearchResultView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.NoteSearchedDetailViewCode) {
                        var data = viewSelected.Data as NoteSearchedDetailViewDataType;
                        viewSelected = await noteSearchedDetailView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.CreateNoteViewCode) {
                        var data = viewSelected.Data as CreateNoteViewDataType;
                        viewSelected = await createNoteView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.EditNoteViewCode) {
                        var data = viewSelected.Data as EditNoteViewDataType;
                        viewSelected = await editNoteView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.DeleteNoteViewCode) {
                        var data = viewSelected.Data as DeleteNoteViewDataType;
                        viewSelected = await deleteNoteView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.DeleteNotebookViewCode) {
                        var data = viewSelected.Data as DeleteNotebookViewDataType;
                        viewSelected = await deleteNotebookView.ShowView(data!);
                    }
                } else {
                    if (viewSelected.Code == ViewCodes.HomeViewCode) {
                        viewSelected = homeView.ShowView();
                    }

                    if (viewSelected.Code == ViewCodes.CreateNotebookViewCode) {
                        viewSelected = await createNotebookView.ShowView();
                    }

                    if (viewSelected.Code == ViewCodes.ErrorView) {
                        viewSelected = errorView.ShowView();
                    }

                    if (viewSelected.Code == ViewCodes.SearchNoteViewCode) {
                        viewSelected = searchNoteView.ShowView();
                    }
                }
            }

        }

    }
}