using System.Net;
using Services.Models;

namespace Services {
    public class DeleteNote {
        Supabase.Client _supabaseClient;
        public DeleteNote(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task Delete(Note note) {
            var matchQuery = new Dictionary<string, string>();
            matchQuery.Add("id", note.Id.ToString());
            await _supabaseClient.From<Note>().Match(matchQuery).Delete();
        }
    }
}