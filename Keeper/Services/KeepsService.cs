using System;
using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal List<Keep> Get()
        {
            return _repo.Get();
        }

        internal Keep Get(int id)
        {
            Keep foundKeep = _repo.Get(id);
            foundKeep.Views += 1;
            _repo.Edit(foundKeep);
            if (foundKeep == null)
            {
                throw new Exception("Invalid Id");
            }
            return foundKeep;
        }

        internal Keep Create(Keep keep)
        {
            Keep newKeep = _repo.Create(keep);
            return newKeep;
        }

        internal Keep Edit(Keep keep)
        {
            Keep original = Get(keep.Id);
            if (keep.CreatorId != original.CreatorId)
            {
                throw new Exception("Invalid Id");
            }
            if (keep.CreatorId == original.CreatorId)
            {
                keep.Views = original.Views;
                keep.Kept = original.Kept;
            }
            original.Name = keep.Name ?? original.Name;
            original.Description = keep.Description ?? original.Description;
            original.Img = keep.Img ?? original.Img;
            original.Views = keep.Views ?? original.Views;
            original.Kept = keep.Kept ?? original.Kept;
            _repo.Edit(original);
            return original;
        }

        internal void Delete(int id, string userId)
        {
            Keep foundKeep = Get(id);
            if (foundKeep.CreatorId != userId)
            {
                throw new Exception("Invalid Id");
            }
            _repo.Delete(id);
        }

        internal List<Keep> GetKeepsByCreatorId(string id)
        {
            return _repo.GetKeepsByCreatorId(id);
        }
    }
}