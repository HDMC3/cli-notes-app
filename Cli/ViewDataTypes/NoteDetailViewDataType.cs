using Services.Models;

namespace Cli.ViewDataTypes {
    public class NoteDetailViewDataType : IViewDataType {
        public Notebook Notebook;
        public Note Note;
        public NoteDetailViewDataType(Notebook notebook, Note note) {
            Notebook = notebook;
            Note = note;
        }
    }
}