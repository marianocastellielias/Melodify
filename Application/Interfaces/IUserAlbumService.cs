﻿using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserAlbumService
    {
        Task<List<AlbumDto>> GetMyAlbums(int userId);
    }
}
