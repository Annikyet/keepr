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
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Vault> GetProfileVaults(string id)
    {
      string sql = @"
        SELECT *
        FROM vaults
        WHERE creatorId = @id
        AND isPrivate = 0;";
      return _db.Query<Vault>(sql, new {id}).ToList();
    }

    internal List<Vault> GetMyVaults(string id)
    {
      string sql = @"
        SELECT *
        FROM vaults
        WHERE creatorId = @id;";
      return _db.Query<Vault>(sql, new {id}).ToList();
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = @"
        INSERT INTO vaults
        (creatorId, name, description, isPrivate)
        VALUES
        (@CreatorId, @Name, @Description, @IsPrivate);
        SELECT LAST_INSERT_ID();
        ";
      int vaultId = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData = GetById(vaultId);
      return vaultData;

    }
    
    internal Vault GetById(int id)
    {
      string sql = @"
        SELECT
          a.*,
          v.*
        FROM vaults v
        JOIN accounts a
          ON v.creatorId = a.id
        WHERE v.id = @id;
      ";
      Vault vault = _db.Query<Profile, Vault, Vault>(sql, (prof, vaul) =>
      {
        vaul.Creator = prof;
        return vaul;
      }, new {id}).FirstOrDefault();

      return vault;
    }

    internal Vault Update(Vault update)
    {
      string sql = @"
        UPDATE vaults
        SET
          name = @Name,
          description = @Description,
          isPrivate = @IsPrivate
        WHERE id = @Id;
      ";
      _db.Execute(sql, update);
      return update;
    }

    internal int Remove(int vaultId)
    {
      string sql = @"
        DELETE FROM vaults
        WHERE id = @vaultId
        LIMIT 1;
      ";
      _db.Execute(sql, new {vaultId});
      return vaultId;
    }
  }
}