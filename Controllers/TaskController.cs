using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskmasterbackend.Exceptions;
using taskmasterbackend.Models;
using taskmasterbackend.Services;

namespace taskmasterbackend.Controllers
{
    public class PartyMembersController : ControllerBase
    {
        private readonly TasksService _service;

        public TasksController(TasksService service)
        {
            _service = service;
        }

        //REVIEW[epic=many-to-many] Which methods are actually needed here?
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> Create([FromBody] Task pm)
        {
            try
            {
                //   This is the same as req.user.id string that is used to verify and attach to the next portion
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _service.Create(pm, userInfo.Id);
                return Ok("success");
            }
            catch (NotAuthorized e)
            {
                return Forbid(e.Message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Delete 
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("success");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}