using System;
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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;

        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> CreateAsync([FromBody] VaultKeep vaultKeep)
        {
            try
            {
                var user = await HttpContext.GetUserInfoAsync<Account>();
                vaultKeep.CreatorId = user.Id;
                VaultKeep newVaultKeep = _vks.Create(vaultKeep);
                return Ok(_vks.Create(vaultKeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteAsync(int id)
        {
            try
            {
                var user = await HttpContext.GetUserInfoAsync<Account>();
                _vks.Delete(id, user?.Id);
                return Ok("delete successful");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}