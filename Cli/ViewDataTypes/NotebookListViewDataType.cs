namespace Cli.ViewDataTypes {
    public class NotebookListViewDataType : IViewDataType {
        public bool Edit;
        public bool Delete;
        public NotebookListViewDataType(bool edit = false, bool delete = false) {
            Edit = edit;
            Delete = delete;
        }
    }
}