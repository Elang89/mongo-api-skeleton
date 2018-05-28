using System.Collections.Generic;
using System.Threading.Tasks;
using MongoApi.Models;

namespace MongoApi.Data.Interfaces
{
    public interface INoteRepository
    {
        Task AddNote (Note item);
        Task<IEnumerable<Note>> GetAllNotes ();
        Task<Note> GetNote (string id);
        Task<bool> RemoveNote (string id);
        Task<bool> UpdateNote (string id, Note note);
    }
}