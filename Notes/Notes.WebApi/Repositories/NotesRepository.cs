using Notes.WebApi.Controllers;
using Notes.WebApi.Models;
using Notes.WebApi.Services;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.WebApi.Repositories
{
  public class NotesRepository
  {
    public static NoteModel GetById(IEnumerable<NoteModel> notes, int id)
    {
      if (notes == null) return null;
      return notes.SingleOrDefault(i => i.Id == id);
    }

    public static NoteModel Save(List<NoteModel> notes, NoteModel note)
    {
      if (note.Id == 0)
      {
        var maxId = notes.Any()
                    ? notes.Select(i => i.Id).Max()
                    : 0;
        var nextId = maxId + 1;
        note.Id = nextId;
      }
      else
      {
        notes.RemoveAll(i => i.Id == note.Id);
      }

      notes.Add(note);

      return note;
    }

    public static bool DeleteById(List<NoteModel> notes, int id)
    {
      notes.RemoveAll(i => i.Id == id);
      return true;
    }
  }
}
