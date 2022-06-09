using Services.Models;

namespace Cli.ViewDataTypes {
    public class EditNotebookViewDataType : IViewDataType {
        public Notebook Notebook;
        public EditNotebookViewDataType(Notebook notebook) {
            Notebook = notebook;
        }

    }
}