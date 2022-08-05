using System.Collections.Generic;
using System.Data;
using System.Linq;
using Keeper.Models;
using Dapper;

namespace Keeper.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> Get()
        {
            string sql = @"
            SELECT k.*, a.* FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
            {
                keep.Creator = prof;
                return keep;
            }).ToList();
        }

        internal Keep Get(int id)
        {
            string sql = @"
            SELECT k.*, a.* FROM keeps k
            JOIN accounts a ON a.id = k.creatorId

            WHERE k.id = @id
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
            {
                keep.Creator = prof;
                return keep;
            }, new { id }).FirstOrDefault();
        }

        internal List<Keep> GetKeepsByCreatorId(string creatorId)
        {
            var sql = @"
        SELECT k.*, a.* FROM keeps k
        JOIN accounts a ON a.id = k.creatorId 
        
        WHERE creatorId = @creatorId";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { creatorId }).ToList();
        }

        internal Keep Create(Keep keep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, views, kept, creatorId)
            VALUES
            (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
            SELECT LAST_INSERT_ID();";
            keep.Id = _db.ExecuteScalar<int>(sql, keep);
            return keep;
        }

        internal void Edit(Keep original)
        {
            string sql = @"
                UPDATE keeps
                SET
                    name = @Name,
                    description = @Description,
                    img = @Img,
                    views = @Views,
                    kept = @Kept
                WHERE id = @Id;";
            _db.Execute(sql, original);
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}