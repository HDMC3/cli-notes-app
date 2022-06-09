namespace Cli.ViewDataTypes {
    public class NoteSearchResultViewDataType : IViewDataType {
        public string Query;
        public NoteSearchResultViewDataType(string query) {
            Query = query;
        }
    }
}