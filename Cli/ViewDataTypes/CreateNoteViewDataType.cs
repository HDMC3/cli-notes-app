using Services.Models;

namespace Cli.ViewDataTypes {
    public class CreateNoteViewDataType : IViewDataType {
        public Notebook Notebook;
        public CreateNoteViewDataType(Notebook notebook) {
            Notebook = notebook;
        }
    }
}