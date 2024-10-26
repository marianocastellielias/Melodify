using Application.DTOs;
using Application.Interfaces;
using Application.Models;
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
        public List<MusicDto> GetAllMusic()
        {
            var songs = _musicRepository.GetAllMusicWithAlbum().Result;
            return songs.Select(MusicDto.Create).ToList();
        }
        public MusicDto GetMusic(int id)
        {
            var song = _musicRepository.GetMusic(id).Result
            ?? throw new NullReferenceException("Id inexistente.");

            return MusicDto.Create(song);
        }

        public void AddSong(int idAlbum, AddMusicDto addMusicDto)
        {
            var album = _albumRepository.GetByIdAsync(idAlbum).Result
                ?? throw new NullReferenceException("El album ingresado no existe");

            var music = new Music
            {
                Title = addMusicDto.Title,
                Duration = new TimeOnly(0, addMusicDto.Minute, addMusicDto.Second),
                Album = album
            };
            _musicRepository.AddAsync(music).Wait();

        }
        public void UpdateMusic(int id, UpdateMusicDto updateMusicDto)
        {
            var song = _musicRepository.GetByIdAsync(id).Result 
                ?? throw new NullReferenceException("No se encuentra la música solicitada");

            song.Title = updateMusicDto.Title;
            song.Duration = new TimeOnly(0, updateMusicDto.Minute, updateMusicDto.Second);

            _musicRepository.UpdateAsync(song).Wait();
        }

        public void DeleteMusic(int id)
        {
            var song = _musicRepository.GetByIdAsync(id).Result
                    ?? throw new NullReferenceException("No se encuentra la música solicitada");
            _musicRepository.DeleteAsync(song).Wait();
        }
    }
}
