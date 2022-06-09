using Services.Models;

namespace Services {
    public class GetNotebook {
        Supabase.Client _supabaseClient;
        public GetNotebook(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task<Notebook> Get(Guid notebookId) {
            var response = await _supabaseClient.From<Notebook>().Filter("id", Postgrest.Constants.Operator.Equals, notebookId.ToString()).Single();
            return response;
        }
    }
}