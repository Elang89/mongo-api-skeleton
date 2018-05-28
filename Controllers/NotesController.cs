using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoApi.Data.Interfaces;
using MongoApi.Models;

namespace MongoApi.Controllers
{
    [Route ("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesController (INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get ()
        {
            var notes = await _noteRepository.GetAllNotes ();
            return Ok (notes);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (string id)
        {
            var note = await _noteRepository.GetNote (id);
            return Ok (note);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] Note newNote)
        {
            await _noteRepository.AddNote (new Note
            {
                Id = newNote.Id,
                    Body = newNote.Body,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    UserId = newNote.UserId
            });

            return Ok ();
        }

        [HttpPut]
        public async Task<IActionResult> Put (string id, [FromBody] Note note)
        {
            var result = await _noteRepository.UpdateNote (id, note);
            return Ok (result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (string id)
        {
            var result = await _noteRepository.RemoveNote (id);
            return Ok (result);
        }
    }
}