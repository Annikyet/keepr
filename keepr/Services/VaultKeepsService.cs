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
  public class VaultKeepsService
  {
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultsService vs, KeepsService ks, VaultKeepsRepository repo)
    {
      _vs = vs;
      _ks = ks;
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      Vault vault = _vs.GetById(newVaultKeep.VaultId, newVaultKeep.CreatorId);
      if (vault == null)
      {
        throw new Exception("No vault for you!");
      }
      if (vault.CreatorId != newVaultKeep.CreatorId)
      {
        throw new Exception("No vault for you!");
      }
      Keep keep = _ks.CheckById(newVaultKeep.KeepId);
      if (keep == null)
      {
        throw new Exception("Keep does not exist!");
      }
      VaultKeep existingVK = _repo.Check(newVaultKeep);
      if (existingVK != null)
      {
        throw new Exception("Keep is already in vault!");
      }
      _ks.IncKept(newVaultKeep.KeepId);
      return _repo.Create(newVaultKeep);
    }

    internal VaultKeep GetById(int id)
    {
      return _repo.GetById(id);
    }

    internal int Remove(VaultKeep vaultKeep)
    {
      Vault vault = _vs.GetById(vaultKeep.VaultId, vaultKeep.CreatorId);
      if (vault == null)
      {
        throw new Exception("No vault for you!");
      }
      if (vault.CreatorId != vaultKeep.CreatorId)
      {
        throw new Exception("No vault for you!");
      }
      _ks.DecKept(vaultKeep.KeepId); // why no decrement?
      return _repo.Remove(vaultKeep.Id);
    }

    internal List<Keep> GetVaultKeeps(int vaultId)
    {
      return _repo.GetVaultKeeps(vaultId);
    }
  }
}