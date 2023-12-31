﻿using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AVG_TASK_APP.Repositories
{
    public class TableRepository : RepositoryBase, ITableRepository
    {
        private AppDbContext dbContext
        {
            get
            {
                var connection = GetConnection();
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseMySql(connection, serverVersion);

                return new AppDbContext(optionsBuilder.Options);
            }
        }
        public void Add(Table table)
        {
            IUserRepository userRepository = new UserRepository();
            AppDbContext dbContextTmp = dbContext;
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            int id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);
            dbContextTmp.Tables.Add(table);
            dbContextTmp.SaveChanges();
            UserTable userTable = new UserTable()
            {
                Id_User = id,
                Id_Table = table.Id,
                Role = 1,
            };
            dbContextTmp.UserTables.Add(userTable);
            dbContextTmp.SaveChanges();
        }

        public IEnumerable<Table> GetAll(string sort = "desc")
        {
            if (sort.Equals("desc"))
                return dbContext.Tables.Where(s => s.Deleted_At == null).OrderByDescending(s => s.Created_At).ToList();
            else
                return dbContext.Tables.Where(s => s.Deleted_At == null).OrderBy(s => s.Created_At).ToList();
        }
        public IEnumerable<Table> GetAllForUser(string sort = "desc")
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return null;
            }

            int id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);
            var tables = dbContext.UserTables
                                .Where(s => s.Id_User == id)
                                .Select(s => s.Table);

            if (sort.Equals("desc"))
            {
                return tables.Where(s => s.Deleted_At == null).OrderByDescending(s => s.Created_At).ToList();
            }

            return tables.Where(s => s.Deleted_At == null).OrderBy(s => s.Created_At).ToList();
        }

        public IEnumerable<Table> GetAllForWorkspace(int idWorkspace, string sort = "desc")
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return null;
            }

            int id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);
            var tables = dbContext.UserTables
                                .Where(s => s.Id_User == id)
                                .Select(s => s.Table).ToList();

            if (sort.Equals("desc"))
                return tables.Where(s => s.Id_Workspace == idWorkspace && s.Deleted_At == null).OrderByDescending(s => s.Created_At).ToList();
            else
                return tables.Where(s => s.Id_Workspace == idWorkspace && s.Deleted_At == null).OrderBy(s => s.Created_At).ToList();
        }

        public Table GetById(int idTable)
        {
            return dbContext.Tables.Where(s => s.Id == idTable && s.Deleted_At == null).FirstOrDefault();
        }

        public Workspace GetWorkspace(int idTable)
        {
            int idWorkspace = dbContext.Tables.FirstOrDefault(s => s.Id == idTable).Id_Workspace;
            return dbContext.Workspaces.FirstOrDefault(s => s.Id == idWorkspace && s.Deleted_At == null);
        }

        public int GetRole(int idTable)
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            int id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);

            return dbContext.UserTables.FirstOrDefault(s => s.Id_Table == idTable && s.Id_User == id).Role;
        }

        public void Remove(Table table)
        {
            dbContext.Tables.Remove(table);
            dbContext.SaveChanges();
        }

        public void Update(Table table)
        {
            AppDbContext dbContextTmp = dbContext;
            dbContextTmp.Tables.Update(table);
            dbContextTmp.SaveChanges();
        }

        public IEnumerable<Table> GetByContainName(string name, string sort = "desc")
        {
            List<Table> tables = null;
            tables = dbContext.Tables.Where(s => s.Name.Contains(name) && s.Deleted_At == null).ToList();

            return tables;
        }

        public IEnumerable<UserModel> GetUsersForTable (int idTable)
        {
            return dbContext.UserTables
                  .Where(s => s.Id_Table == idTable)
                  .Select(x => x.User).ToList();
        }

        public bool AddUserToTable(Table table, UserModel user)
        {
            AppDbContext dbContextTmp = dbContext;
            try
            {

                if (dbContext.UserTables.FirstOrDefault(x => x.Id_Table == table.Id && x.Id_User == user.Id) != null)
                    return false;
                UserTable userTable = new UserTable()
                {
                    Id_User = user.Id,
                    Id_Table = table.Id,
                    Role = 0,
                };

                dbContextTmp.UserTables.Add(userTable);
                dbContextTmp.SaveChanges();

                IWorkspaceRepository workspaceRepository = new WorkspaceRepository();

                workspaceRepository.AddUserToWorkspace(table.Workspace, user);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

