using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesAPI.Models;

namespace NotesAPI.Services
{
    public interface INoteRepository
    {
        Note Add(Note note);
        IEnumerable<Note> GetAll();
        Note GetById(int id);
        void Delete(Note note);
        void Update(Note note);
    }
}
