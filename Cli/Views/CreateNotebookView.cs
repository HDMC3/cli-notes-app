using Cli.Application;
using Cli.Common;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class CreateNotebookView {
        IServiceProvider _serviceProvider;
        public CreateNotebookView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView() {
            AnsiConsole.Clear();
            Helpers.WriteRuleWidget("NUEVA LIBRETA");
            var name = AnsiConsole.Prompt(
                new TextPrompt<string>("Nombre de la libreta:")
                    .AllowEmpty()
            );
            
            if (String.IsNullOrWhiteSpace(name)) 
                return new ViewData(ViewCodes.HomeViewCode);

            AnsiConsole.WriteLine();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<(string, int)>()
                    .UseConverter(opt => opt.Item1)
                    .AddChoices(
                        ("Guardar", 1),
                        ("Cancelar", 2)
                    )
            );

            if (option.Item2 == 1) {
                var createNotebook = _serviceProvider.GetService<CreateNotebook>();
                if (createNotebook != null) {
                    try
                    {
                        await AnsiConsole.Status()
                            .Spinner(Spinner.Known.SquareCorners)
                            .SpinnerStyle(new Style(foreground: Colors.primary))
                            .StartAsync("Guardando...", async (ctx) =>
                            {
                                await createNotebook.Create(new Notebook { Name = name });
                            });
                        return new ViewData(ViewCodes.HomeViewCode);
                    }
                    catch (System.Exception)
                    {
                        return new ViewData(ViewCodes.ErrorView);
                    }
                }

                return new ViewData(ViewCodes.ExitApp);
            } else {
                return new ViewData(ViewCodes.HomeViewCode);
            }
            
        }
    }
}