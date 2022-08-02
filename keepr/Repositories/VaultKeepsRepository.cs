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
using Dapper;
using System.Data;
using System.Linq;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Check(VaultKeep newVaultKeep)
    {
      string sql = @"
        SELECT *
        FROM vaultKeeps
        WHERE vaultId = @VaultId
          AND keepId = @KeepId;
      ";
      return _db.Query<VaultKeep>(sql, newVaultKeep).FirstOrDefault();
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
        INSERT INTO vaultKeeps
        (creatorId, vaultId, keepId)
        VALUES
        (@CreatorId, @VaultId, @KeepId);
        SELECT LAST_INSERT_ID();
      ";
      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"
        SELECT *
        FROM vaultKeeps
        WHERE id = @Id;
      ";
      return _db.Query<VaultKeep>(sql, new {id}).FirstOrDefault();
    }

    internal int Remove(int id)
    {
      string sql = @"
        DELETE FROM vaultKeeps
        WHERE id = @id
        LIMIT 1;
      ";
      _db.Execute(sql, new {id});
      return id;

    }

    internal List<KeepVaultViewModel> GetVaultKeeps(int vaultId)
    {
      string sql = @"
        SELECT
          a.*,
          k.*,
          vk.id
        FROM keeps k
        JOIN accounts a
          ON a.id = k.creatorId
        JOIN vaultKeeps vk
          ON vk.keepId = k.id
        WHERE vk.vaultId = @vaultId;
      ";
      return _db.Query<Profile, KeepVaultViewModel, int, KeepVaultViewModel>(sql, (prof, keep, vkid) =>
      {
        keep.Creator = prof;
        keep.VaultKeepId = vkid;
        return keep;
      }, new {vaultId}).ToList();
    }
  }
}