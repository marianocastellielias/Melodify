using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User? GetUserByEmail(string? email);

        Task<List<User>> GetAllUsersAsync();

        Task<List<Album>> GetMyAlbumsAsync(int userId);

        Task<User> GetByIdAsync(int id);

    }
}
