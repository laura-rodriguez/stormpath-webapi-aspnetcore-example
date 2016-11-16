using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NotesAPI.Models
{
    public class NoteAPIContext : DbContext
    {
        public NoteAPIContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}
