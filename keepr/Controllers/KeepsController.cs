using System.Collections.Generic;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keep.CreatorId = userInfo.Id;
        Keep newKeep = _ks.Create(keep);
        return Ok(newKeep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        List<Keep> keeps = _ks.GetAll();
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        Keep keep = _ks.GetById(id);
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Update([FromBody] Keep keep, int keepId)
    {
      try
      {

      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      keep.CreatorId = userInfo.Id;
      keep.Id = keepId;
      keep = _ks.Update(keep);
      return Ok(keep);
        
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }

    }

    [HttpDelete("{keepId}")]
    [Authorize]
    public async Task<ActionResult<int>> Remove(int keepId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        int removedId = _ks.Remove(keepId, userInfo.Id);
        return Ok(removedId);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}