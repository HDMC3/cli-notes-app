using Services.Models;

namespace Services {
    public class GetNotes {
        private Supabase.Client _supabaseClient;
        public GetNotes(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task<List<Note>> Get(Guid notebookId) {
            var response = await _supabaseClient.From<Note>()
                .Filter("notebook_id", Postgrest.Constants.Operator.Equals, notebookId.ToString())
                .Get();
            return response.Models;
        }
    }
}