using keepr.Models;
using keepr.Repositories;
using System;
using System.Collections.Generic;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal List<Vault> GetProfileVaults(string id)
    {
      return _repo.GetProfileVaults(id);
    }

    internal List<Vault> GetMyVaults(string id)
    {
      return _repo.GetMyVaults(id);
    }

    internal Vault Create(Vault original)
    {
      return _repo.Create(original);
    }

    internal Vault GetById(int vaultId, string userId)
    {
      Vault vault = _repo.GetById(vaultId);
      if (vault == null)
      {
        throw new Exception("No vault for you!");
      }
      if (vault.IsPrivate == true && vault.CreatorId != userId)
      {
          throw new Exception("No vault for you!");
      }
      return vault;
    }

    internal Vault GetByIdAnon(int vaultId)
    {
      Vault vault = _repo.GetById(vaultId);
      if (vault == null)
      {
        throw new Exception("No vault for you!");
      }
      if (vault.IsPrivate == true)
      {
        throw new Exception("No vault for you!");
      }
      return vault;
    }

    internal Vault Update(Vault update)
    {
      Vault original = GetById(update.Id, update.CreatorId);
      if (original == null)
      {
        throw new Exception("No vault for you!");
      }
      if (original.CreatorId != update.CreatorId)
      {
        throw new Exception("No vault for you!");
      }
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.Img = update.Img ?? original.Img;
      original.IsPrivate = update.IsPrivate != null ? update.IsPrivate : original.IsPrivate;
      update = _repo.Update(original);
      return update;
    }

    internal int Remove(int vaultId, string userId)
    {
      Vault original = GetById(vaultId, userId);
      if (original == null)
      {
        throw new Exception("No vault for you!");
      }
      if (original.CreatorId != userId)
      {
        throw new Exception("No vault for you!");
      }
      return _repo.Remove(vaultId);
    }
  }
}