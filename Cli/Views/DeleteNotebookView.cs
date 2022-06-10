using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Spectre.Console;

namespace Cli.Views {
    public class DeleteNotebookView {
        private IServiceProvider _serviceProvider;
        public DeleteNotebookView(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<ViewData> ShowView(DeleteNotebookViewDataType data) {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("ELIMINAR LIBRETA");

            AnsiConsole.Write(new Markup("Nombre:\n", new Style(foreground: Colors.primary)));
            AnsiConsole.Write(data.Notebook.Name + "\n\n");

            AnsiConsole.Write("Se elminara la libreta y todas sus notas\n\n");

            var option = Helpers.ConfirmationMenuPrompt(
                ("Eliminar", 1),
                ("Cancelar", 2)
            );

            var deleteNotebook = _serviceProvider.GetService<DeleteNotebook>();

            if (deleteNotebook == null) 
                return new ViewData(ViewCodes.ErrorView);

            if (option.Item2 == 2) 
                return new ViewData(ViewCodes.HomeViewCode);

            try
            {
                await Helpers.StartSpinnerAsync("Eliminando...", async ctx => {
                    await deleteNotebook.Delete(data.Notebook.Id);
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