using System.Collections.Generic;
using System.Data;
using System.Linq;
using Keeper.Models;
using Dapper;

namespace Keeper.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            string sql = @"
        INSERT INTO vaultkeeps 
          (vaultId, keepId, creatorId) 
        VALUES 
          (@VaultId, @KeepId, @CreatorId); 
        SELECT LAST_INSERT_ID();";
            newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            return newVaultKeep;
        }

        internal VaultKeep CheckForExists(VaultKeep newVaultKeep)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE vaultId = @VaultId AND keepId = @KeepId LIMIT 1";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, newVaultKeep);
        }

        internal VaultKeep Get(int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @id";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal List<VaultKeepViewModel> GetByKeepId(int id)
        {
            string sql = @"
      SELECT
        v.*,
        vk.id AS VaultKeepId
      FROM vaultkeeps vk
      JOIN vaults v ON v.id = vk.vaultId
      WHERE vk.keepId = @id
      ";
            return _db.Query<VaultKeepViewModel>(sql, new { id }).ToList();
        }

        internal List<KeepVaultViewModel> GetByVaultId(int id)
        {
            string sql = @"
      SELECT
        a.*,
        k.*,
        vk.id AS vaultKeepId
      FROM vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = vk.creatorId
      WHERE vk.vaultId = @id
      ";
            return _db.Query<Account, KeepVaultViewModel, KeepVaultViewModel>(sql, (prof, keep) =>
            {
                keep.Creator = prof;
                return keep;
            }, new { id }).ToList();
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }


    }
}