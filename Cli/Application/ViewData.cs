using Cli.Common;
using Cli.ViewDataTypes;

namespace Cli.Application {
    public class ViewData{
        public ViewCodes Code;
        public IViewDataType? Data;

        public ViewData(ViewCodes code) {
            Code = code;
        }
        public ViewData(ViewCodes code, IViewDataType data) {
            Code = code;
            Data = data;
        }

    }
}