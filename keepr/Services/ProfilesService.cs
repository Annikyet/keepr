using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Services;
using keepr.Repositories;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Services
{
  public class ProfilesService
  {
    // 'Instantiate' ProfilesRepository
    private readonly ProfilesRepository _repo;
    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    public Profile Get(string id)
    {
      Profile foundProfile = _repo.Get(id);
      if (foundProfile == null)
      {
        throw new Exception("Profile not found");
      }
      return foundProfile;
    }
  }
}