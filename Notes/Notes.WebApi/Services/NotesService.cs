// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Notes.WebApi.Models;
using Notes.WebApi.Repositories;
using System.Collections.Generic;

namespace Notes.WebApi.Services
{
  public class NotesService
  {
    public static List<NoteModel> GetAllNotes(string webRootPath)
    {
      return DatabaseFileService.GetNotes(webRootPath);
    }

    public static NoteModel GetById(string webRootPath, int id)
    {
      var notes = GetAllNotes(webRootPath);
      return NotesRepository.GetById(notes, id);
    }

    public static NoteModel Save(string webRootPath, NoteModel note)
    {
      var notes = GetAllNotes(webRootPath);
      var savedNote = NotesRepository.Save(notes, note);
      DatabaseFileService.Save(webRootPath, notes);
      return savedNote;
    }

    public static bool SaveAll(string webRootPath, List<NoteModel> notes)
    {
      DatabaseFileService.Save(webRootPath, notes);
      return true;
    }

    public static bool DeleteById(string webRootPath, int id)
    {
      var notes = GetAllNotes(webRootPath);
      NotesRepository.DeleteById(notes, id);
      DatabaseFileService.Save(webRootPath, notes);
      return true;
    }
  }
}
