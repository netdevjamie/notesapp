using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Notes.WebApi.Models;
using Notes.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class NoteController : ControllerBase
  {
    private IWebHostEnvironment _webHostEnvironment;
    private List<NoteModel> Notes
    {
      get
      {
        return NotesService.GetAllNotes(_webHostEnvironment.WebRootPath);
      }

      set
      {
        NotesService.SaveAll(_webHostEnvironment.WebRootPath, value);
      }
    }

    // GET: api/<NoteController>
    [HttpGet]
    public IEnumerable<NoteModel> Get()
    {
      return Notes;
    }

    // GET api/<NoteController>/5
    [HttpGet("{id}")]
    public NoteModel Get(int id)
    {
      var note = NotesService.GetById(_webHostEnvironment.WebRootPath, id);
      if (note == null)
      {
        throw new HttpRequestException("note not found");
      }

      return note;
    }

    // POST api/<NoteController>
    [HttpPost]
    public NoteModel Post([FromBody] NoteModel note)
    {
      if (note != null)
      {
        return NotesService.Save(_webHostEnvironment.WebRootPath, note);
      }
      else
      {
        throw new HttpRequestException("bad request");
      }
    }

    // PUT api/<NoteController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<NoteController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      NotesService.DeleteById(_webHostEnvironment.WebRootPath, id);
    }

    public NoteController(IWebHostEnvironment webHostEnvironment)
    {
      _webHostEnvironment = webHostEnvironment;
    }
  }
}
