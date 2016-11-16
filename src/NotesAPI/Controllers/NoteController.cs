using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Services;
using NotesAPI.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotesAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: note
        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return _noteRepository.GetAll();
        }

        // GET note/5
        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Get(int id)
        {
            var note = _noteRepository.GetById(id);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // POST note
        [HttpPost]
        public IActionResult Post([FromBody]Note value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdNote = _noteRepository.Add(value);

            return CreatedAtRoute("GetNote", new { id = createdNote.Id }, createdNote);

        }

        // PUT note/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Note value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var note = _noteRepository.GetById(id);

            if (note == null)
            {
                return NotFound();
            }
            value.Id = id;
            _noteRepository.Update(value);

            return NoContent();
        }

        // DELETE note/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = _noteRepository.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            _noteRepository.Delete(note);

            return NoContent();
        }
    }
}
