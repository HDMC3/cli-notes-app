using Cli.Application;
using Services.Models;

namespace Cli.ViewDataTypes {
    public class NoteOptionsViewData : IViewDataType {
        public Notebook Notebook;
        public Note Note;
        public NoteOptionsViewData(Notebook notebook, Note note) {
            Notebook = notebook;
            Note = note;
        }
    }
}