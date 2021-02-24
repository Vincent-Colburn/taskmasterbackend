using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskmasterbackend.Models;
using taskmasterbackend.Services;

namespace taskmasterbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _service;
        private readonly ListsService _ps;

        public ProfilesController(ProfilesService service, ListsService ps)
        {
            _service = service;
            _ps = ps;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _service.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(("{id}/lists"))]
        [Authorize]
        public ActionResult<IEnumerable<ProfileTaskUserViewModel>> GetLists(string id)
        {
            try
            {
                IEnumerable<ProfileTaskUserViewModel> lists = _ps.GetByProfileId(id);
                return Ok(lists);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}