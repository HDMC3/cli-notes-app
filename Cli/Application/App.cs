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
        private bool Exit;
        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Exit = false;
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

            // while(!Exit) {
            // }
            await ShowMainMenu();
        }

        public async Task ShowMainMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine();

            var rule = new Rule("MENU PRINCIPAL");
            rule.Alignment = Justify.Left;
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();

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
                    
                    var getNotebooks = _serviceProvider.GetService<GetNotebooks>();
                    AnsiConsole.Clear();
                    if (getNotebooks != null)
                    {
                        var notebooks = new List<Notebook>();
                        await AnsiConsole.Status()
                            .Spinner(Spinner.Known.SquareCorners)
                            .SpinnerStyle(Style.Parse("green"))
                            .StartAsync("Cargando libretas...", async (ctx) =>
                            {
                                notebooks = await getNotebooks.Get();
                            });
                        await ShowNotebookList(notebooks);
                    }
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

        public async Task ShowNotebookList(ICollection<Notebook> notebooks)
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine();
            var rule = new Rule("LIBRETAS");
            rule.Alignment = Justify.Left;
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();
            var options = notebooks.Select(n => new OptionMenu<ListOptions, Notebook>(n.Name, ListOptions.Item, n)).ToList();
            options.Add(new OptionMenu<ListOptions, Notebook>("Regresar", ListOptions.Back));
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<ListOptions, Notebook>>()
                    .UseConverter(t => t.Text)
                    .AddChoices(options)
            );

            if (option.Value != null)
            {
                AnsiConsole.MarkupInterpolated($"Lista de notas de la libreta");
            }
            else
            {
                if (option.Code == ListOptions.Back)
                {
                    await ShowMainMenu();
                }
            }
        }
    }
}