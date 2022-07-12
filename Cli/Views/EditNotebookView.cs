using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Services;
using Spectre.Console;

namespace Cli.Views {
    public class EditNotebookView {
        EditNotebook _editNotebook;
        public EditNotebookView(EditNotebook editNotebook) {
            _editNotebook = editNotebook;
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

            var option = Helpers.ConfirmationMenuPrompt(
                ("Guardar", 1),
                ("Cancelar", 2)
            );

            if (option.Item2 == 2)
                return new ViewData(ViewCodes.HomeViewCode);

            try
            {
                await Helpers.StartSpinnerAsync("Guardando...", async (ctx) =>
                    {
                        data.Notebook.Name = name;
                        await _editNotebook.Edit(data.Notebook);
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