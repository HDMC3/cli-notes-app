using Cli.Application;
using Cli.Common;
using Cli.ViewDataTypes;
using Spectre.Console;

namespace Cli.Views
{
    public class HomeView
    {
        public ViewData ShowView()
        {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("MENU PRINCIPAL");

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<MainMenuOptions, Object>>()
                    .HighlightStyle(new Style(foreground: Colors.primary, decoration: Decoration.Underline))
                    .UseConverter(t => t.Text)
                    .AddChoices(new OptionMenu<MainMenuOptions, Object>[]{
                        new OptionMenu<MainMenuOptions, Object>("Ver todas las libretas", MainMenuOptions.ShowNotebooks),
                        new OptionMenu<MainMenuOptions, Object>("Crear nueva libreta", MainMenuOptions.CreateNotebook),
                        new OptionMenu<MainMenuOptions, Object>("Editar libreta", MainMenuOptions.EditNotebook),
                        new OptionMenu<MainMenuOptions, Object>("Eliminar libreta", MainMenuOptions.DeleteNotebook),
                        new OptionMenu<MainMenuOptions, Object>("Buscar nota", MainMenuOptions.SearchNote),
                        new OptionMenu<MainMenuOptions, Object>("Salir", MainMenuOptions.Exit),
                    })
            );

            return SelectOption(option);
        }

        public ViewData SelectOption(OptionMenu<MainMenuOptions, Object> option)
        {
            switch (option.Code)
            {
                case MainMenuOptions.ShowNotebooks:
                    return new ViewData(ViewCodes.NotebookListViewCode, new NotebookListViewDataType());
                case MainMenuOptions.CreateNotebook:
                    return new ViewData(ViewCodes.CreateNotebookViewCode);
                case MainMenuOptions.EditNotebook:
                    return new ViewData(ViewCodes.NotebookListViewCode, new NotebookListViewDataType(edit: true));
                case MainMenuOptions.DeleteNotebook:
                    return new ViewData(ViewCodes.NotebookListViewCode, new NotebookListViewDataType(delete: true));
                case MainMenuOptions.SearchNote:
                    return new ViewData(ViewCodes.SearchNoteViewCode);
                case MainMenuOptions.Exit:
                    AnsiConsole.Clear();
                    return new ViewData(ViewCodes.ExitApp);
                default:
                    return new ViewData(ViewCodes.InvalidView);
            }
        }
    }

}