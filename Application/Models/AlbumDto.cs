using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Cover { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string State { get; set; }
        public decimal Price { get; set; }
        public List<MusicDto> Songs { get; set; } = new List<MusicDto>();
        public static AlbumDto Create(Album album)
        {
            AlbumDto albumDto = new AlbumDto(); 

            albumDto.Id = album.Id;
            albumDto.Title = album.Title;
            albumDto.Artist = album.Artist;
            albumDto.Genre = album.Genre;
            albumDto.Cover = album.Cover;
            albumDto.ReleaseDate = album.ReleaseDate;
            albumDto.Songs = album.Songs.Select(MusicDto.Create).ToList();
            switch (album.State)
            {   
                case AlbumState.Pending:
                    albumDto.State = "Pending";
                    break;
                case AlbumState.Accepted:
                    albumDto.State = "Accepted";
                    break;
                case AlbumState.Rejected:
                    albumDto.State = "Rejected";
                    break;
            }
            albumDto.Price = album.Price;

            return albumDto;
        }
        public static List<AlbumDto> CreateList(IEnumerable<Album> albums)
        {
            
            var listDto = new List<AlbumDto>();
            foreach (var album in albums)
            {
                if (album.IsActive == true)
                {
                    listDto.Add(Create(album));
                }
                
            }
            return listDto;
        }
    }
}
