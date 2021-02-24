using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using taskmasterbackend.Models;
using taskmasterbackend.Services;

namespace taskmasterbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListsController : ControllerBase
    {
        private readonly ListsService _service;
        private readonly ProfilesService _ps;

        public ListsController(ListsService service, ProfilesService ps)
        {
            _service = service;
            _ps = ps;
        }

        [HttpGet("{id}")]
        public ActionResult<List> Get(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/members")]
        public ActionResult<List> GetMembers(int id)
        {
            try
            {
                return Ok(_ps.GetMembersByListId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public ActionResult<List> Create([FromBody] List newProd)
        {
            try
            {
                List newList = _service.Create(newProd);
                return Ok(newList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<List> Edit([FromBody] List updated, int id)
        {
            try
            {
                List edited = _service.Edit(updated);
                return Ok(edited);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<List> Delete(int id)
        {
            try
            {
                List deleted = _service.Delete(id);
                return Ok(deleted);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}