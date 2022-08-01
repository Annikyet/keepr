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
  public class KeepsRepository
  {
        // Connect to Dapper
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Keep> GetProfileKeeps(string id)
    {
      string sql = @"
      SELECT *
      FROM keeps
      WHERE creatorId = @id;
      ";
      return _db.Query<Keep>(sql, new {id}).ToList();
    }

    public Keep Create(Keep keep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img)
      VALUES
      (@CreatorId, @Name, @Description, @Img);
      SELECT LAST_INSERT_ID();
      ";
      keep.Id = _db.ExecuteScalar<int>(sql, keep);
      return keep;
    }

    public List<Keep> GetAll()
    {
      string sql = @"
        SELECT
          a.*,
          k.*
        FROM keeps k
        JOIN accounts a
          ON a.id = k.creatorId;
      ";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }).ToList();
    }
  }
}