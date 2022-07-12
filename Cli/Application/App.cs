using Cli.Common;
using Cli.ViewDataTypes;
using Cli.Views;
using Microsoft.Extensions.DependencyInjection;
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
            var notebookListView = _serviceProvider.GetService<NotebookListView>();
            var createNotebookView = _serviceProvider.GetService<CreateNotebookView>();
            var notebookNotesView = _serviceProvider.GetService<NotebookNotesView>();
            var noteDetailView = new NoteDetailView();
            var noteOptionsView = new NoteOptionsView();
            var errorView = new ErrorView();
            var editNotebookView = _serviceProvider.GetService<EditNotebookView>();
            var searchNoteView = new SearchNoteView();
            var noteSearchResultView = _serviceProvider.GetService<NoteSearchResultView>();
            var noteSearchedDetailView = _serviceProvider.GetService<NoteSearchedDetailView>();
            var createNoteView = _serviceProvider.GetService<CreateNoteView>();
            var editNoteView = _serviceProvider.GetService<EditNoteView>();
            var deleteNoteView = _serviceProvider.GetService<DeleteNoteView>();
            var deleteNotebookView = _serviceProvider.GetService<DeleteNotebookView>();

            if (notebookListView == null || createNotebookView == null || notebookNotesView == null ||
                editNotebookView == null || noteSearchResultView == null || noteSearchedDetailView == null ||
                createNoteView == null || editNoteView == null || deleteNoteView == null || deleteNotebookView == null) 
            {
                AnsiConsole.Write(new Markup("[red]Problema al iniciar aplicacion[/]"));
                return;
            }

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