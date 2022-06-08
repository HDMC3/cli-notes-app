using System.Net;
using Services.Models;

namespace Services {
    public class CreateNotebook {
        private Supabase.Client _supabaseClient;
        public CreateNotebook(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task<HttpStatusCode> Create(Notebook notebook) {
            var response = await _supabaseClient.From<Notebook>().Insert(notebook);
            return response.ResponseMessage.StatusCode;
        }
    }
}