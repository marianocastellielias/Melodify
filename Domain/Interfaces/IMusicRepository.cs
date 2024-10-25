using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMusicRepository : IRepositoryBase<Music>
    {
        Task<List<Music>> GetAllMusicWithAlbum();
        Task<Music?> GetMusic(int id);
    }
}
