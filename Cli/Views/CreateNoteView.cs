using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class CreateNoteView {
        IServiceProvider _serviceProvider;
        public CreateNoteView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView(CreateNoteViewDataType data) {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("NUEVA NOTA");

            var title = AnsiConsole.Ask<string>("Titulo:");
            var description = AnsiConsole.Ask<string>("Descripcion:");

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
                var createNote = _serviceProvider.GetService<CreateNote>();
                if (createNote != null) {
                    try
                    {
                        await AnsiConsole.Status()
                            .Spinner(Spinner.Known.SquareCorners)
                            .SpinnerStyle(new Style(foreground: Colors.primary))
                            .StartAsync("Guardando...", async (ctx) =>
                            {
                                await createNote.Create(
                                    new Note { 
                                        Title = title, 
                                        Description = description,
                                        NotebookId = data.Notebook.Id
                                    }
                                );
                            });
                        var notebookNotesViewData = new NotebookNotesViewDataType(data.Notebook);
                        return new ViewData(ViewCodes.NotebookNotesViewCode, notebookNotesViewData);
                    }
                    catch (System.Exception)
                    {
                        return new ViewData(ViewCodes.ErrorView);
                    }
                }

                return new ViewData(ViewCodes.ErrorView);
            } else {
                return new ViewData(ViewCodes.HomeViewCode);
            }
        }
    }
}