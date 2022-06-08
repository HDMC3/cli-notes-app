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
            var notebookNotesView = new NotebookNotesView(_serviceProvider);
            var noteDetailView = new NoteDetailView();
            var noteOptionsView = new NoteOptionsView();

            ViewData viewSelected = homeView.ShowView();
            
            while(viewSelected.Code != ViewCodes.ExitApp && viewSelected.Code != ViewCodes.InvalidView) {
                if (viewSelected.Data != null) {
                    if (viewSelected.Code == ViewCodes.NotebookNotesViewCode) {
                        var data = viewSelected.Data as NotebookNotesViewData;
                        viewSelected = await notebookNotesView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.NoteDetailViewCode) {
                        var data = viewSelected.Data as NoteDetailViewData;
                        viewSelected = noteDetailView.ShowView(data!);
                    }

                    if (viewSelected.Code == ViewCodes.NoteOptionsViewCode) {
                        var data = viewSelected.Data as NoteOptionsViewData;
                        viewSelected = noteOptionsView.ShowView(data!);
                    }
                } else {
                    if (viewSelected.Code == ViewCodes.HomeViewCode) {
                        viewSelected = homeView.ShowView();
                    }

                    if (viewSelected.Code == ViewCodes.NotebookListViewCode) {
                        viewSelected = await notebookListView.ShowView();
                    }
                }
            }

        }

    }
}