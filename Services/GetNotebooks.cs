using Services.Models;

namespace Services {
    public class GetNotebooks {
        private Supabase.Client _supabaseClient;
        public GetNotebooks(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task<List<Notebook>> Get() {
            var response = await _supabaseClient.From<Notebook>().Get();
            return response.Models;
        }
    }
}