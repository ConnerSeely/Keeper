using System;
using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly KeepsRepository _keepsRepo;
        private readonly VaultsService _vs;
        private readonly KeepsService _ks;

        public VaultKeepsService(VaultKeepsRepository repo, KeepsRepository keepsRepo, VaultsService vs, KeepsService ks)
        {
            _repo = repo;
            _keepsRepo = keepsRepo;
            _vs = vs;
            _ks = ks;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            Vault foundVault = _vs.Get(newVaultKeep.VaultId, newVaultKeep.CreatorId);
            if (foundVault.CreatorId != newVaultKeep.CreatorId)
            {
                throw new Exception("Invalid Id");
            }
            VaultKeep exists = _repo.CheckForExists(newVaultKeep);

            if (exists != null)
            {
                return exists;
            }

            Keep foundKeep = _ks.Get(newVaultKeep.KeepId);
            foundKeep.Kept += 1;
            _keepsRepo.Edit(foundKeep);
            return _repo.Create(newVaultKeep);
        }

        internal VaultKeep Get(int id)
        {
            VaultKeep found = _repo.Get(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal void Delete(int id, string userId)
        {
            VaultKeep foundVaultKeep = Get(id);
            Vault foundVault = _vs.Get(foundVaultKeep.VaultId, userId);
            if (foundVault.CreatorId != userId && foundVaultKeep.CreatorId != userId)
            {
                throw new Exception("Invalid Id");
            }

            Keep foundKeep = _ks.Get(foundVaultKeep.KeepId);
            foundKeep.Kept -= 1;
            _keepsRepo.Edit(foundKeep);
            _repo.Delete(id);
        }

        internal List<KeepVaultViewModel> GetByVaultId(int id, string userId)
        {
            Vault foundVault = _vs.Get(id, userId);
            if (foundVault == null)
            {
                throw new Exception("Invalid Id");
            }
            return _repo.GetByVaultId(id);
        }

        internal List<VaultKeepViewModel> GetByKeepId(int id)
        {
            return _repo.GetByKeepId(id);
        }
    }
}