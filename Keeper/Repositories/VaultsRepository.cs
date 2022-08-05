using System.Collections.Generic;
using System.Data;
using System.Linq;
using Keeper.Models;
using Dapper;

namespace Keeper.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Vault> Get()
        {
            string sql = @"
            SELECT v.*, a.* FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
            {
                vault.Creator = prof;
                return vault;
            }).ToList();

        }

        internal Vault Get(int id)
        {
            string sql = @"
            SELECT v.*, a.* FROM vaults v
            JOIN accounts a ON a.id = v.creatorId

            WHERE v.id = @id
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
            {
                vault.Creator = prof;
                return vault;
            }, new { id }).FirstOrDefault();
        }

        internal Vault Create(Vault vault)
        {
            string sql = @"
                INSERT INTO vaults
                (name, description, isPrivate, creatorId)
                VALUES
                (@Name, @Description, @IsPrivate, @CreatorId);
                SELECT LAST_INSERT_ID();";
            vault.Id = _db.ExecuteScalar<int>(sql, vault);
            return vault;
        }

        internal void Edit(Vault original)
        {
            string sql = @"
                    UPDATE vaults
                    SET
                        name = @Name,
                        description = @Description,
                        isPrivate = @IsPrivate
                    WHERE id = @Id;";
            _db.Execute(sql, original);
        }

        internal List<Vault> GetMyVaults(string id)
        {
            var sql = @"
            SELECT v.*, a.* FROM vaults v
            JOIN accounts a ON a.id = v.creatorId

            WHERE v.creatorId = @id
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
           {
               vault.Creator = profile;
               return vault;
           }, new { id }).ToList();
        }

        internal List<Vault> GetVaultsByCreatorId(string creatorId)
        {
            var sql = @"
        SELECT v.*, a.* FROM vaults v
        JOIN accounts a ON a.id = v.creatorId 
        
        WHERE creatorId = @creatorId AND v.isPrivate = false";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { creatorId }).ToList();
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}