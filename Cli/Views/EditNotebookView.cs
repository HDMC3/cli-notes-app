using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class EditNotebookView {
        IServiceProvider _serviceProvider;
        public EditNotebookView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView(EditNotebookViewDataType data) {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("EDITAR LIBRETA");

            AnsiConsole.Write(new Markup("Nombre: ", new Style(foreground: Colors.primary)));
            AnsiConsole.Write(data.Notebook.Name + "\n");
            AnsiConsole.WriteLine();
            var name = AnsiConsole.Prompt(
                new TextPrompt<string>("Nuevo nombre:")
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

            if (option.Item2 == 2)
                return new ViewData(ViewCodes.HomeViewCode);

            var editNotebook = _serviceProvider.GetService<EditNotebook>();
            
            if (editNotebook == null)
                return new ViewData(ViewCodes.ErrorView);

            try
            {
                await Helpers.StartSpinnerAsync("Guardando...", async (ctx) =>
                    {
                        data.Notebook.Name = name;
                        await editNotebook.Edit(data.Notebook);
                    });
                return new ViewData(ViewCodes.HomeViewCode);
            }
            catch (System.Exception)
            {
                return new ViewData(ViewCodes.ErrorView);
            }
        }
    }
}