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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vks;

        public VaultsController(VaultsService vs, VaultKeepsService vks)
        {
            _vs = vs;
            _vks = vks;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetAsync(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.Get(id, user?.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<VaultKeep>>> GetKeepsAsync(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<KeepVaultViewModel> keeps = _vks.GetByVaultId(id, user?.Id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vault>> CreateAsync([FromBody] Vault vault)
        {
            try
            {
                var user = await HttpContext.GetUserInfoAsync<Account>();
                vault.CreatorId = user?.Id;
                Vault newVault = _vs.Create(vault);
                newVault.Creator = user;
                return Created($"/api/vaults/{newVault.Id}", newVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditAsync([FromBody] Vault vault, int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                vault.Id = id;
                vault.CreatorId = user.Id;
                Vault updated = _vs.Edit(vault);
                return Ok(updated);
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
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                _vs.Delete(id, user.Id);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}