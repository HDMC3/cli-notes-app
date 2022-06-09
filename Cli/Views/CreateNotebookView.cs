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

            var option = Helpers.ConfirmationMenuPrompt(
                ("Guardar", 1),
                ("Cancelar", 2)
            );

            if (option.Item2 == 2)
                return new ViewData(ViewCodes.HomeViewCode);

            var createNotebook = _serviceProvider.GetService<CreateNotebook>();
            if (createNotebook == null)
                return new ViewData(ViewCodes.ErrorView);

            try
            {
                await Helpers.StartSpinnerAsync("Guardando...", async ctx => 
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
    }
}