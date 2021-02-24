using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Party_Planner.Models;

namespace Party_Planner.Repositories
{
    public class PartiesRepository
    {
        private readonly IDbConnection _db;

        public PartiesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<List> GetAll()
        {
            string sql = @"
      SELECT * FROM listss";
            return _db.Query<List>(sql);
        }


        // REVIEW[epic=Populate] add the creator to the object
        internal List GetById(int id)
        {
            string sql = @" 
      SELECT 
      list.*,
      pr.*
      FROM lists list
      JOIN profiles pr ON list.creatorId = pr.id
      WHERE id = @id";
            return _db.Query<List, Profile, List>(sql, (list, profile) =>
            {
                list.Creator = profile;
                return list;
            }, new { id }, splitOn: "id").FirstOrDefault();

        }

        internal List Create(List newList)
        {
            string sql = @"
      INSERT INTO lists 
      ( name, description, creatorId) 
      VALUES 
      (@Name, @Description, @CreatorId);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newList);
            newList.Id = id;
            return newList;
        }

        internal List Edit(List updated)
        {
            string sql = @"
        UPDATE lists
        SET
         name = @Name
        WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }

        // REVIEW[epic=many-to-many] This sql will add the relationship id to a Party, as the PartyPartyMemberViewModel
        // REVIEW[epic=Populate] and get the creator
        internal IEnumerable<ProfileTaskUserViewModel> GetListsByProfileId(string id)
        {
            string sql = @"
      SELECT
      list.*,
      list.id as ListId,
      pr.*
      FROM tasks task
      JOIN lists list ON task.taskId == list.id
      JOIN profiles pr ON part.creatorId = pr.id
      WHERE memberId = @id
      ";
            return _db.Query<ProfileTaskUserViewModel, Profile, ProfileTaskUserViewModel>(sql, (list, profile) =>
            {
                list.Creator = profile;
                return list;
            }
              , new { id }, splitOn: "id");
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM parties WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}