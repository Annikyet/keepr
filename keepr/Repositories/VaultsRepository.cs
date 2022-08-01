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
  }
}