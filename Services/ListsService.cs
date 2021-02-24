using System;
using System.Collections.Generic;
using taskmasterbackend.Models;
using taskmasterbackend.Repositories;

namespace taskmasterbackend.Services
{
    public class ListsService
    {
        private readonly ListsRepository _repo;
        public ListsService(ListsRepository repo)
        {
            _repo = repo;
        }
        internal IEnumerable<List> GetAll()
        {
            // FIXME Should not be able to get any and all lists
            return _repo.GetAll();
        }

        internal List GetById(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal List Create(List newProd)
        {
            return _repo.Create(newProd);
        }

        internal List Edit(List updated)
        {
            var data = GetById(updated.Id);
            updated.Name = updated.Name != null ? updated.Name : data.Name;
            return _repo.Edit(updated);
        }

        internal List Delete(int id)
        {
            var data = GetById(id);
            _repo.Delete(id);
            return data;
        }

        internal IEnumerable<ProfileTaskUserViewModel> GetByProfileId(string id)
        {
            IEnumerable<ProfileTaskUserViewModel> data = _repo.GetListsByProfileId(id);
            return data;
        }
    }
}