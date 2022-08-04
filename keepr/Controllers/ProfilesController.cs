using System.Collections.Generic;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace keepr.Controllers
{
  // Establish endpoint path
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    // 'Instantiate' ProfilesService
    private readonly ProfilesService _ps;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;
    public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
    }

    // Get Profile by Id endpoint
    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _ps.Get(id);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _ks.GetProfileKeeps(id);
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetProfileVaults(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults;
        if (userInfo == null || userInfo?.Id != id)
        {
          // System.Console.WriteLine("didn't auth");
         vaults = _vs.GetProfileVaults(id);
        }
        else
        {
          // System.Console.WriteLine("authed");
         vaults = _vs.GetMyVaults(id);
        }
        
        return Ok(vaults);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}