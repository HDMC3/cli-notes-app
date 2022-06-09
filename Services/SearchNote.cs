using Postgrest;
using Services.Models;

namespace Services {
    public class SearchNote {
        Supabase.Client _supabaseClient;
        public SearchNote(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task<List<Note>> Search(string query) {
            var filterQueries = new List<QueryFilter>() {
                new QueryFilter("title", Constants.Operator.Like, $"%{query}%"),
                new QueryFilter("description", Constants.Operator.Like, $"%{query}%")
            };
            var response = await _supabaseClient.From<Note>().Or(filterQueries).Get();

            return response.Models;
        }
    }
}