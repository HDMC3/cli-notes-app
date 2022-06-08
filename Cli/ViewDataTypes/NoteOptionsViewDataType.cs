using Services.Models;

namespace Cli.ViewDataTypes {
    public class NoteOptionsViewDataType : IViewDataType {
        public Notebook Notebook;
        public Note Note;
        public NoteOptionsViewDataType(Notebook notebook, Note note) {
            Notebook = notebook;
            Note = note;
        }
    }
}