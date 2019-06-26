using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ElevenNote.WebMVC.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        private bool SetStarState(int noteId, bool newState)
        {
            //create service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            //get the note
            var detail = service.GetNoteById(noteId);

            //create noteedit model instance with new star state
            var updatedNote = new NoteEdit
            {
                NoteID = detail.NoteID,
                Title = detail.Title,
                Content = detail.Content,
                IsStarred = newState
            };

            //return a value indicating whether update succeeded
            return service.UpdateNote(updatedNote);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);
        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);
    }
}