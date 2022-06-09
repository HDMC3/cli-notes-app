using Cli.Application;
using Cli.Common;
using Spectre.Console;

namespace Cli.Views {
    public class ErrorView {
        public ViewData ShowView() {
            var panel = new Panel(new Markup("[red]Ha ocurrido un problema. Intenta nuevamente.[/]"));
            panel.Border(BoxBorder.Double);
            panel.BorderStyle(new Style(foreground: Color.Red));
            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<(string, int)>()
                    .UseConverter(opt => opt.Item1)
                    .AddChoices(
                        ("Menu principal", 1),
                        ("Salir", 2)
                    )
            );

            if (option.Item2 == 1)
                return new ViewData(ViewCodes.HomeViewCode);
            else 
                return new ViewData(ViewCodes.ExitApp);
        }
    }
}