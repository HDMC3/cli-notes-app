using Cli.Application;
using Cli.Common;
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
                    .UseConverter(t => t.Text)
                    .AddChoices(new OptionMenu<MainMenuOptions, Object>[]{
                        new OptionMenu<MainMenuOptions, Object>("Ver todas las libretas", MainMenuOptions.ShowNotebooks),
                        new OptionMenu<MainMenuOptions, Object>("Crear nueva libreta", MainMenuOptions.CreateNotebook),
                        new OptionMenu<MainMenuOptions, Object>("Editar libreta", MainMenuOptions.EditNotebook),
                        new OptionMenu<MainMenuOptions, Object>("Crear nota", MainMenuOptions.CreateNote),
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
                    return new ViewData(ViewCodes.NotebookListViewCode);
                case MainMenuOptions.CreateNotebook:
                    AnsiConsole.Write("Creacion de libreta");
                    return new ViewData(ViewCodes.ExitApp);
                case MainMenuOptions.EditNotebook:
                    AnsiConsole.Write("Edicion de libreta");
                    return new ViewData(ViewCodes.ExitApp);
                case MainMenuOptions.CreateNote:
                    AnsiConsole.Write("Creacion de nota");
                    return new ViewData(ViewCodes.ExitApp);
                case MainMenuOptions.SearchNote:
                    AnsiConsole.Write("Busqueda de nota");
                    return new ViewData(ViewCodes.ExitApp);
                case MainMenuOptions.Exit:
                    AnsiConsole.Clear();
                    return new ViewData(ViewCodes.ExitApp);
                default:
                    AnsiConsole.Write("Opcion invalida");
                    return new ViewData(ViewCodes.InvalidView);
            }
        }
    }

}