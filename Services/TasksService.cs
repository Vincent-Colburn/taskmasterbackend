using System;
using taskmasterbackend.Models;
using taskmasterbackend.Repositories;
using taskmasterbackend.Exceptions;

namespace taskmasterbackend.Services
{
    public class TasksService
    {
        private readonly TasksRepository _repo;
        private readonly ListsRepository _ls;
        public TasksService(TasksRepository repo, ListsRepository ls)
        {
            _repo = repo;
            _ls = ls;
        }

        internal void Create(Task ts, string id)
        {
            List list = _ls.GetById(ts.ListId);
            if (list == null)
            {
                throw new Exception("Invalid Id");
            }
            if (list.CreatorId != id)
            {
                throw new NotAuthorized("Not The Owner of this Party");
            }
            _repo.Create(ts);
        }

        internal void Delete(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            _repo.Delete(id);
        }
    }
}