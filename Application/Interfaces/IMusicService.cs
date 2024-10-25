using Application.DTOs;
using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMusicService
    {

       void AddSong(int idAlbum,AddMusicDto song);
        void DeleteMusic(int id);
        List<MusicDto> GetAllMusic();
        MusicDto GetMusic(int id);
        void UpdateMusic(int id, UpdateMusicDto updateMusicDto);
    }
}
