using System.Net;
using Services.Models;

namespace Services {
    public class EditNotebook {
        private Supabase.Client _supabaseClient;
        public EditNotebook(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task<HttpStatusCode> Edit(Notebook notebook) {
            var matchQuery = new Dictionary<string, string>();
            matchQuery.Add("id", notebook.Id.ToString());
            var response = await _supabaseClient.From<Notebook>().Match(matchQuery).Update(notebook);
            return response.ResponseMessage.StatusCode;
        }
    }
}