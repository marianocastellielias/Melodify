using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IAlbumRepository _albumRepository;
        public MusicService(IMusicRepository musicRepository, IAlbumRepository albumRepository)
        {

            _musicRepository = musicRepository;
            _albumRepository = albumRepository;
        }

        //Agrega una cancion al Album
        public void AddSong(int idAlbum, AddMusicDto addMusicDto)
        {
            var album = _albumRepository.GetByIdAsync(idAlbum).Result
                ?? throw new NullReferenceException("El album ingresado no existe");

            var music = new Music
            {
                Title = addMusicDto.Title,
                Duration = addMusicDto.Duration,
                Album = album
            };
            _musicRepository.AddAsync(music).Wait();

        }
    }
}
