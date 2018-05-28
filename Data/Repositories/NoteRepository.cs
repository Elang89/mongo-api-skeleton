using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoApi.Data.Interfaces;
using MongoApi.Models;
using MongoApi.Properties;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoApi.Data.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly MongoDbContext _context = null;

        public NoteRepository (IOptions<Settings> settings)
        {
            _context = new MongoDbContext (settings);
        }

        public async Task AddNote (Note item)
        {
            await _context.Notes.InsertOneAsync (item);
        }

        public async Task<IEnumerable<Note>> GetAllNotes ()
        {
            var result = await _context.Notes.Find (_ => true).ToListAsync ();
            return result;
        }

        public async Task<Note> GetNote (string id)
        {
            var note = await _context.Notes.Find (item => item.Id == id).FirstOrDefaultAsync ();
            return note;
        }

        public async Task<bool> RemoveNote (string id)
        {
            DeleteResult actionResult = await _context.Notes.DeleteOneAsync (Builders<Note>.Filter.Eq ("Id", id));
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateNote (string id, Note note)
        {
            var filter = Builders<Note>.Filter.Eq (item => item.Id, id);
            var update = Builders<Note>.Update
                .Set (item => item.Body, note.Body)
                .CurrentDate (item => item.UpdatedOn);

            UpdateResult actionResult = await _context.Notes.UpdateOneAsync (filter, update);

            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;

        }
    }
}