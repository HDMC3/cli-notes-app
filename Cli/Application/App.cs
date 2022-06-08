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
                    .Color(Colors.primary)
            );
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
                await ShowNotebookNotes(option.Value);
            }
            else
            {
                if (option.Code == ListOptions.Back)
                {
                    await ShowMainMenu();
                }
            }
        }

        public async Task ShowNoteOptions(Notebook notebook, Note note)
        {
            AnsiConsole.Clear();

            Helpers.WriteRuleWidget("OPCIONES | " + note.Title);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<NoteOptions, Object>>()
                    .UseConverter(t => t.Text)
                    .AddChoices(
                        new OptionMenu<NoteOptions, Object>("Ver nota", NoteOptions.ShowNote),
                        new OptionMenu<NoteOptions, Object>("Editar nota", NoteOptions.EditNote),
                        new OptionMenu<NoteOptions, Object>("Eliminar nota", NoteOptions.DeleteNote),
                        new OptionMenu<NoteOptions, Object>("Regresar a lista de notas", NoteOptions.BackNotebookList),
                        new OptionMenu<NoteOptions, Object>("Menu principal", NoteOptions.BackMainMenu)
                    )
            );

            await SelectNoteOption(option, notebook, note);
        }

        public async Task SelectNoteOption(OptionMenu<NoteOptions, Object> option, Notebook notebook, Note note)
        {
            switch (option.Code)
            {
                case NoteOptions.ShowNote:
                    await ShowNote(notebook, note);
                    break;
                case NoteOptions.EditNote:
                    AnsiConsole.Write("Edicion de nota");
                    break;
                case NoteOptions.DeleteNote:
                    AnsiConsole.Write("Eliminacion de nota");
                    break;
                case NoteOptions.BackNotebookList:
                    await ShowNotebookNotes(notebook);
                    break;
                case NoteOptions.BackMainMenu:
                    await ShowMainMenu();
                    break;
                default:
                    AnsiConsole.Write("Opcion invalida");
                    break;
            }
        }

        public async Task ShowNotebookNotes(Notebook notebook)
        {
            var getNotes = _serviceProvider.GetService<GetNotes>();
            AnsiConsole.Clear();
            var notes = new List<Note>();
            if (getNotes != null)
            {
                await AnsiConsole.Status()
                    .Spinner(Spinner.Known.SquareCorners)
                    .SpinnerStyle(Style.Parse("green"))
                    .StartAsync("Cargando notas...", async (ctx) =>
                    {
                        notes = await getNotes.Get(notebook.Id);
                    });
            }

            Helpers.WriteRuleWidget("NOTAS | " + notebook.Name);

            var options = notes.Select(n => new OptionMenu<ListOptions, Note>(n.Title, ListOptions.Item, n)).ToList();
            options.Add(new OptionMenu<ListOptions, Note>("Regresar a lista de libretas", ListOptions.Back));
            options.Add(new OptionMenu<ListOptions, Note>("Menu principal", ListOptions.BackMainMenu));
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OptionMenu<ListOptions, Note>>()
                    .PageSize(10)
                    .UseConverter(t => t.Text)
                    .AddChoices(options)
            );

            if (option.Value != null)
            {
                await ShowNoteOptions(notebook, option.Value);
            }
            else
            {
                if (option.Code == ListOptions.Back)
                    await ShowNotebookList();
                if (option.Code == ListOptions.BackMainMenu)
                    await ShowMainMenu();
            }
        }

        public async Task ShowNote(Notebook notebook, Note note) {
            AnsiConsole.Clear();
            Helpers.WriteRuleWidget(note.Title + " | " + notebook.Name);
            AnsiConsole.Write(new Markup("Titulo:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(note.Title + "\n");
            AnsiConsole.Write(new Markup("Descripcion:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(note.Description + "\n");
            AnsiConsole.Write(new Markup("Fecha de creacion:\n", new Style(foreground: Colors.primary, decoration: Decoration.Bold)));
            AnsiConsole.WriteLine(note.CreatedAt.ToString() + "\n");
            
            AnsiConsole.WriteLine();

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<(string, int)>()
                    .UseConverter(opt => opt.Item1)
                    .AddChoices(
                        ("Regresa a opciones de nota", 1),
                        ("Menu principal", 2)
                    )
            );

            if (option.Item2 == 1)
                await ShowNoteOptions(notebook, note);
            if (option.Item2 == 2)
                await ShowMainMenu();
        }

    }
}