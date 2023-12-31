﻿using AVG_TASK_APP.Models;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Repositories.Interface
{
    interface IWorkspaceRepository
    {
        void Add(Workspace workspace);
        void Update(Workspace workspace);
        void Remove(Workspace workspace);
        Workspace GetByNameForUser(string name, UserModel user);
        Workspace GetByCode(string code);
        Workspace GetById(int id);
        bool AddUserToWorkspace(Workspace workspace, UserModel user);
        IEnumerable<Workspace> GetAll(string sort = "desc");
        IEnumerable<UserModel> GetUsersForWorkspace(int idWorkspace);
        IEnumerable<Workspace> GetAllForUser(string sort = "desc");
    }
}
