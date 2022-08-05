using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;
        private readonly VaultKeepsService _vks;

        public KeepsController(KeepsService ks, VaultKeepsService vks)
        {
            _ks = ks;
            _vks = vks;
        }

        [HttpGet]
        public ActionResult<List<Keep>> Get()
        {
            try
            {
                List<Keep> keeps = _ks.Get();
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Keep> Get(int id)
        {
            try
            {
                Keep keep = _ks.Get(id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public ActionResult<List<VaultKeepViewModel>> GetVaults(int id)
        {
            try
            {
                List<VaultKeepViewModel> vaults = _vks.GetByKeepId(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Keep>> CreateAsync([FromBody] Keep keep)
        {
            try
            {
                var user = await HttpContext.GetUserInfoAsync<Account>();
                keep.CreatorId = user?.Id;
                Keep newKeep = _ks.Create(keep);
                newKeep.Creator = user;
                return Created($"/api/keeps/{newKeep.Id}", newKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Edit([FromBody] Keep keep, int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                keep.Id = id;
                keep.CreatorId = user.Id;
                Keep editKeep = _ks.Edit(keep);
                return Ok(editKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> DeleteAsync(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                _ks.Delete(id, user.Id);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}