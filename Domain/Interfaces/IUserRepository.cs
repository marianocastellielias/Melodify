﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string? email);

        public interface IUserRepository : IRepositoryBase<User>
        {
        }
    }
}
