using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Services;
using keepr.Repositories;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;

namespace keepr.Repositories
{
  public class ProfilesRepository
  {
    // Connect to Dapper
    private readonly IDbConnection _db;
    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    public Profile Get(string id)
    {
      string sql = @"
      SELECT *
      FROM accounts
      WHERE id = @id;";
      return _db.QueryFirstOrDefault<Profile>(sql, new {id});
    }
  }
}