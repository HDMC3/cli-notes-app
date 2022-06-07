using Spectre.Console;

namespace Cli.Common {
    public class Helpers {
        public static void WriteRuleWidget(string title) {
            AnsiConsole.WriteLine();
            var rule = new Rule(title);
            rule.Alignment = Justify.Left;
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();
        }
    }
}