using Services.Models;

namespace Cli.ViewDataTypes {
    public class DeleteNotebookViewDataType : IViewDataType {
        public Notebook Notebook;
        public DeleteNotebookViewDataType(Notebook notebook) {
            Notebook = notebook;
        }
    }
}