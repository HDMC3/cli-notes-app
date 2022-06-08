using Services.Models;

namespace Cli.ViewDataTypes {
    public class NotebookNotesViewDataType : IViewDataType {
        public Notebook Notebook;
        public NotebookNotesViewDataType(Notebook notebook) {
            Notebook = notebook;
        }
    }
}