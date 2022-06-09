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
            var searchNoteView = new SearchNoteView(_serviceProvider);

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

                    if (viewSelected.Code == ViewCodes.EditNotebook) {
                        var data = viewSelected.Data as EditNotebookViewDataType;
                        viewSelected = await editNotebookView.ShowView(data!);
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

                    if (viewSelected.Code == ViewCodes.SearchNote) {
                        viewSelected = await searchNoteView.ShowView();
                    }
                }
            }

        }

    }
}