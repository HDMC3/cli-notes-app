using Services.Models;

namespace Cli.ViewDataTypes {
    public class NotebookNotesViewData : IViewDataType {
        public Notebook Notebook;
        public NotebookNotesViewData(Notebook notebook) {
            Notebook = notebook;
        }
    }
}