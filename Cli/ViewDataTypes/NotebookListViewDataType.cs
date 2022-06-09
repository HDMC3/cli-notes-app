namespace Cli.ViewDataTypes {
    public class NotebookListViewDataType : IViewDataType {
        public bool Edit;
        public NotebookListViewDataType(bool edit = false) {
            Edit = edit;
        }
    }
}