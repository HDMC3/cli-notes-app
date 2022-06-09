using Cli.Application;
using Cli.Common;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class SearchNoteView {
        IServiceProvider _serviceProvider;
        public SearchNoteView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView() {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("BUSCAR NOTA");

            var query = AnsiConsole.Ask<string>("Termino de busqueda:");

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<(string, int)>()
                    .UseConverter(opt => opt.Item1)
                    .AddChoices(
                        ("Buscar", 1),
                        ("Cancelar", 2)
                    )
            );

            if (option.Item2 == 1) {
                var searchNote = _serviceProvider.GetService<SearchNote>();
                var notes = new List<Note>();
                if (searchNote != null) {
                    try
                    {
                        await AnsiConsole.Status()
                            .Spinner(Spinner.Known.SquareCorners)
                            .SpinnerStyle(new Style(foreground: Colors.primary))
                            .StartAsync("Buscando...", async (ctx) =>
                            {
                                notes = await searchNote.Search(query);
                            });
                        foreach(var note in notes) {
                            AnsiConsole.WriteLine(note.Title);
                        }
                        Console.Read();
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