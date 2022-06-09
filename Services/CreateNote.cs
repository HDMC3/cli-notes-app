using System.Net;
using Services.Models;

namespace Services {
    public class CreateNote {
        Supabase.Client _supabaseClient;
        public CreateNote(Supabase.Client supabaseClient) {
            _supabaseClient = supabaseClient;
        }

        public async Task<HttpStatusCode> Create(Note note) {
            var response = await _supabaseClient.From<Note>().Insert(note);
            return response.ResponseMessage.StatusCode;
        }
    }
}