using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MusicRepository : EfRepository<Music>, IMusicRepository
    {
        public MusicRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
