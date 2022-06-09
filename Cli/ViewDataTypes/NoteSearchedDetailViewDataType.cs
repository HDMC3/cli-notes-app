using Services.Models;

namespace Cli.ViewDataTypes {
    public class NoteSearchedDetailViewDataType : IViewDataType {
        public Note Note;
        public string Query;
        public NoteSearchedDetailViewDataType(Note note, string query) {
            Note = note;
            Query = query;
        }
    }
}