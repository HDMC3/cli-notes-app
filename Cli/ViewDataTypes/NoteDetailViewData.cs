using Cli.Application;
using Services.Models;

namespace Cli.ViewDataTypes {
    public class NoteDetailViewData : IViewDataType {
        public Notebook Notebook;
        public Note Note;
        public NoteDetailViewData(Notebook notebook, Note note) {
            Notebook = notebook;
            Note = note;
        }
    }
}