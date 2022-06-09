using Services.Models;

namespace Cli.ViewDataTypes {
    public class EditNoteViewDataType : IViewDataType {
        public Notebook Notebook;
        public Note Note;
        public EditNoteViewDataType(Notebook notebook, Note note) {
            Notebook = notebook;
            Note = note;
        }
    }
}