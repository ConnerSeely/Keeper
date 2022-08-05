using System.Collections.Generic;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {

        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        private readonly AccountService _acctServ;

        public ProfilesController(KeepsService keepsService, VaultsService vaultsService, AccountService acctServ)
        {
            _keepsService = keepsService;
            _vaultsService = vaultsService;
            _acctServ = acctServ;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> getById(string id)
        {
            try
            {
                Profile profile = _acctServ.GetProfileById(id);
                return Ok(profile);
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeeps(string id)
        {
            try
            {
                List<Keep> keeps = _keepsService.GetKeepsByCreatorId(id);
                return Ok(keeps);
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> GetVaults(string id)
        {
            try
            {
                List<Vault> vaults = _vaultsService.GetVaultsByCreatorId(id);
                return Ok(vaults);
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}