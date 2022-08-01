using keepr.Controllers;
using keepr.Models;
using keepr.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      return _repo.GetProfileKeeps(id);
    }

    internal Keep Create(Keep keep)
    {
      Keep newKeep = _repo.Create(keep);
      return newKeep;
    }

    internal List<Keep> GetAll()
    {
      List<Keep> keeps = _repo.GetAll();
      return keeps;
    }
  }
}