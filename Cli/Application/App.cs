using Cli.Common;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Models;
using Spectre.Console;

namespace Cli.Application
{
    public class App
    {
        private IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Run()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Cli Notes App")
                    .Alignment(Justify.Center)
                    .Color(new Color(211, 221, 87)));
            Thread.Sleep(3000);
            AnsiConsole.Clear();

            await ShowMainMenu();
        }

        public async Task ShowMainMenu()
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

            await SelectMainMenuOption(option);
        }

        public async Task SelectMainMenuOption(OptionMenu<MainMenuOptions, Object> option)
        {
            switch (option.Code)
            {
                case MainMenuOptions.ShowNotebooks:
                    await ShowNotebookList();
                    break;
                case MainMenuOptions.CreateNotebook:
                    AnsiConsole.Write("Creacion de libreta");
                    break;
                case MainMenuOptions.EditNotebook:
                    AnsiConsole.Write("Edicion de libreta");
                    break;
                case MainMenuOptions.CreateNote:
                    AnsiConsole.Write("Creacion de nota");
                    break;
                case MainMenuOptions.SearchNote:
                    AnsiConsole.Write("Busqueda de nota");
                    break;
                case MainMenuOptions.Exit:
                    AnsiConsole.Clear();
                    break;
                default:
                    AnsiConsole.Write("Opcion invalida");
                    break;
            }
        }

        public async Task ShowNotebookList()
        {
            var getNotebooks = _serviceProvider.GetService<GetNotebooks>();
            AnsiConsole.Clear();
            var notebooks = new List<Notebook>();
            if (getNotebooks != null)
            {
                await AnsiConsole.Status()
                    .Spinner(Spinner.Known.SquareCorners)
                    .SpinnerStyle(Style.Parse("green"))
                    .StartAsync("Cargando libretas...", async (ctx) =>
                    {
                        notebooks = await getNotebooks.Get();
                    });
            }

            Helpers.WriteRuleWidget("LIBRETAS");

            var options = notebooks.Select(n => new OptionMenu<ListOptions, Notebook>(n.Name, ListOptions.Item, n)).ToList();
            options.Add(new OptionMenu<ListOptions, Notebook>("Regresar", ListOptions.Back));
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<ListOptions, Notebook>>()
                    .UseConverter(t => t.Text)
                    .AddChoices(options)
            );

            if (option.Value != null)
            {
                await ShowNotebookOptions(option.Value.Name);
            }
            else
            {
                if (option.Code == ListOptions.Back)
                {
                    await ShowMainMenu();
                }
            }
        }

        public async Task ShowNotebookOptions(string notebookName)
        {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("LIBRETAS > " + notebookName);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<NotebookOptions, Object>>()
                    .UseConverter(t => t.Text)
                    .AddChoices(
                        new OptionMenu<NotebookOptions, Object>("Ver notas", NotebookOptions.ShowNotes),
                        new OptionMenu<NotebookOptions, Object>("Editar nota", NotebookOptions.EditNote),
                        new OptionMenu<NotebookOptions, Object>("Eliminar nota", NotebookOptions.DeleteNote),
                        new OptionMenu<NotebookOptions, Object>("Regresar", NotebookOptions.BackNotebookList)
                    )
            );

            await SelectNotebookOption(option);
        }

        public async Task SelectNotebookOption(OptionMenu<NotebookOptions, Object> option)
        {
            switch (option.Code)
            {
                case NotebookOptions.ShowNotes:
                    AnsiConsole.Write("Mostrar notas de libreta");
                    break;
                case NotebookOptions.EditNote:
                    AnsiConsole.Write("Edicion de nota");
                    break;
                case NotebookOptions.DeleteNote:
                    AnsiConsole.Write("Eliminacion de nota");
                    break;
                case NotebookOptions.BackNotebookList:
                    await ShowNotebookList();
                    break;
                default:
                    AnsiConsole.Write("Opcion invalida");
                    break;
            }
        }

    }
}