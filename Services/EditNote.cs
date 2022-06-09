using System.Net;
using Services.Models;

namespace Services {
    public class EditNote {
        Supabase.Client _supabaseClient;
        public EditNote(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task<HttpStatusCode> Edit(Note note) {
            var matchQuery = new Dictionary<string, string>();
            matchQuery.Add("id", note.Id.ToString());
            var response = await _supabaseClient.From<Note>().Match(matchQuery).Update(note);
            return response.ResponseMessage.StatusCode;
        }
    }
}