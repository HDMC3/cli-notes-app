using Services.Models;

namespace Cli.ViewDataTypes {
    public class DeleteNoteViewDataType : IViewDataType {
        public Notebook Notebook;
        public Note Note;
        public DeleteNoteViewDataType(Notebook notebook, Note note) {
            Notebook = notebook;
            Note = note;
        }
    }
}