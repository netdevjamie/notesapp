using Notes.WebApi.Models;
using System.Collections.Generic;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.WebApi.Services
{
  public class DatabaseFileService
  {
    private static string _notesFilePath(string webRootPath) => Path.Combine(webRootPath, "jamie.json");

    public static bool Save(string webRootPath, List<NoteModel> notes)
    {
      var myserializedList = System.Text.Json.JsonSerializer.Serialize(notes);
      File.WriteAllText(_notesFilePath(webRootPath), myserializedList);
      return true;
    }

    public static List<NoteModel> GetNotes(string webRootPath)
    {
      if (System.IO.File.Exists(_notesFilePath(webRootPath)))
      {
        var content = File.ReadAllText(_notesFilePath(webRootPath));
        var deserialized = System.Text.Json.JsonSerializer.Deserialize<List<NoteModel>>(content);
        return deserialized;
      }

      return new List<NoteModel>();
    }
  }
}
