using Cli.Application;
using Cli.Common;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Views {
    public class CreateNotebookView {
        CreateNotebook _createNotebook;
        public CreateNotebookView(CreateNotebook createNotebook) {
            _createNotebook = createNotebook;
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

            try
            {
                await Helpers.StartSpinnerAsync("Guardando...", async ctx => 
                {
                    await _createNotebook.Create(new Notebook { Name = name, CreatedAt = DateTimeOffset.Now });
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