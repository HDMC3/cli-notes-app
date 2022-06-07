namespace Cli.Application
{
    public class OptionMenu<T, V>
    {
        public string Text { get; set; }
        public T Code { get; set; }
        public V? Value;

        public OptionMenu(string Text, T Code) {
            this.Text = Text;
            this.Code = Code;
        }

        public OptionMenu(string Text, T Code, V Value) {
            this.Text = Text;
            this.Code = Code;
            this.Value = Value;
        }
    }
}