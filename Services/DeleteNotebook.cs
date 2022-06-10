using Services.Models;

namespace Services {
    public class DeleteNotebook {
        private Supabase.Client _supabaseClient;
        public DeleteNotebook(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task Delete(Guid notebookId) {
            var matchQuery = new Dictionary<string, string>();
            matchQuery.Add("id", notebookId.ToString());
            await _supabaseClient.From<Notebook>().Match(matchQuery).Delete();
        }
    }
}