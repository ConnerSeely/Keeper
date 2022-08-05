using System;
using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault Get(int id, string userId)
        {
            Vault foundVault = _repo.Get(id);
            if (foundVault == null || foundVault.isPrivate == true && foundVault.CreatorId != userId)
            {
                throw new Exception("Invalid Id");
            }
            return foundVault;
        }

        internal Vault Create(Vault vault)
        {
            Vault newVault = _repo.Create(vault);
            return newVault;
        }

        internal Vault Edit(Vault vault)
        {
            Vault original = Get(vault.Id, vault.CreatorId);
            if (vault.CreatorId != original.CreatorId)
            {
                throw new Exception("Invalid Id");
            }
            original.Name = vault.Name ?? original.Name;
            original.Description = vault.Description ?? original.Description;
            original.isPrivate = vault.isPrivate;
            _repo.Edit(original);
            return original;
        }

        internal void Delete(int id, string userId)
        {
            Vault foundVault = Get(id, userId);
            _repo.Delete(id);
        }

        internal List<Vault> GetVaultsByCreatorId(string id)
        {
            return _repo.GetVaultsByCreatorId(id);
        }

        internal List<Vault> GetMyVaults(string id)
        {
            return _repo.GetMyVaults(id);
        }
    }
}