using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MusicDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeOnly Duration { get; set; }
        public string NameAlbum { get; set; }
        public static MusicDto Create(Music? music)
        {
            var dto = new MusicDto();
            dto.Id = music.Id;
            dto.Title = music.Title;
            dto.Duration = music.Duration;
            dto.NameAlbum = music.Album.Title;
            return dto;
        }
    }
}
