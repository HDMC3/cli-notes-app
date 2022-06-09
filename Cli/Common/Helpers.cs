using Spectre.Console;

namespace Cli.Common {
    public class Helpers {
        public static void WriteRuleWidget(string title) {
            AnsiConsole.WriteLine();
            title = title.Replace("|", "[#d3dd57]|[/]");
            var rule = new Rule($"[white]{title}[/]");
            rule.Style = Style.Parse("#d3dd57");
            rule.Border(BoxBorder.Double);
            rule.Alignment = Justify.Left;
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();
        }

        public static Task StartSpinnerAsync(string loadingText, Func<StatusContext, Task> callback) {
            return AnsiConsole.Status()
                .Spinner(Spinner.Known.SquareCorners)
                .SpinnerStyle(new Style(foreground: Colors.primary))
                .StartAsync(loadingText, callback);
        } 
    }
}