using System.Reflection;
using Cli.Application;
using Cli.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Cli {
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.Load("Cli"))
                .Build();

            IServiceCollection services = new ServiceCollection();
            var client = await Supabase.Client.InitializeAsync(config["DB:url"], config["DB:key"]);
            services.AddSingleton(client);
            services.AddScoped<GetNotebooks>();
            services.AddScoped<GetNotes>();
            services.AddScoped<CreateNotebook>();
            services.AddScoped<EditNotebook>();
            services.AddScoped<DeleteNotebook>();
            services.AddScoped<SearchNote>();
            services.AddScoped<GetNotebook>();
            services.AddScoped<CreateNote>();
            services.AddScoped<EditNote>();
            services.AddScoped<DeleteNote>();
            services.AddScoped<NotebookListView>();
            services.AddScoped<CreateNotebookView>();
            services.AddScoped<NotebookNotesView>();
            services.AddScoped<EditNotebookView>();
            services.AddScoped<NoteSearchResultView>();
            services.AddScoped<NoteSearchedDetailView>();
            services.AddScoped<CreateNoteView>();
            services.AddScoped<EditNoteView>();
            services.AddScoped<DeleteNoteView>();
            services.AddScoped<DeleteNotebookView>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var app = new App(serviceProvider);
            await app.Run();
        }
    }
}
