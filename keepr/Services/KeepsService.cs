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

    internal Keep GetById(int id)
    {

     _repo.IncViews(id);
     Keep keep = _repo.GetById(id);
     if (keep == null)
     {
      throw new Exception("No keep for you!");
     }
     return keep;
    }

    internal Keep CheckById(int id)
    {
      return _repo.GetById(id);
    }

    internal Keep Update(Keep update)
    {
      Keep original = GetById(update.Id);
      if (original.CreatorId != update.CreatorId)
      {
        throw new Exception("Nacho Keep!");
      }
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.Img = update.Img ?? original.Img;
      _repo.Update(original);
      return original;
    }

    internal int Remove(int keepId, string userId)
    {
      Keep original = GetById(keepId);
      if (original.CreatorId != userId)
      {
        throw new Exception("Nacho Keep!");
      }
      _repo.Remove(keepId);
      return keepId;
    }

    internal void IncViews(int id)
    {
      _repo.IncViews(id);
    }

    internal void IncKept(int id)
    {
      _repo.IncKept(id);
    }

    internal void DecKept(int id)
    {
      _repo.DecKept(id);
    }
  }
}