using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesAPI.Models;

namespace NotesAPI.Services
{
    public class InMemoryNoteRepository : INoteRepository
    {
        private readonly NoteAPIContext _context;

        public InMemoryNoteRepository(NoteAPIContext context)
        {
            _context = context;
        }
           
        public Note Add(Note note)
        {
            note.CreatedDate = DateTime.Now;
            var addedNote =_context.Add(note);
            _context.SaveChanges();
            note.Id = addedNote.Entity.Id;

            return note;
        }

        public void Delete(Note note)
        {
            _context.Remove(note);
            _context.SaveChanges();
        }

        public IEnumerable<Note> GetAll()
        {
            return _context.Notes.ToList();
        }

        public Note GetById(int id)
        {
            return _context.Notes.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Note note)
        {
            var noteToUpdate = GetById(note.Id);
            noteToUpdate.Text = note.Text;
            _context.Update(noteToUpdate);
            _context.SaveChanges();
        }
    }
}
