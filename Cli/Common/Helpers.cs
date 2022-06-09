using Postgrest;
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

        public static void WriteNotebookIndicator(string notebookName) {
            AnsiConsole.Write(new Markup("--- Libreta: ", new Style(foreground: Color.Grey)));
            AnsiConsole.Write(new Markup(notebookName + " ---\n\n", new Style(foreground: Color.Grey, decoration: Decoration.Bold)));
        }

        public static (string, int) ConfirmationMenuPrompt(params (string, int)[] choices) {
            return AnsiConsole.Prompt(
                new SelectionPrompt<(string, int)>()
                    .UseConverter(opt => opt.Item1)
                    .AddChoices(choices)
            );
        }
    }
}