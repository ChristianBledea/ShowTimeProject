using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.Businesslogic.Dtos;
using ShowTime.DataAccess.Models;

namespace ShowTime.Businesslogic.Abstractions
{
    public interface IArtistService
    {
        Task<ArtistGetDto?> getArtistbyIdAsync(int id);
        Task<IList<ArtistGetDto>> getAllArtistsAsync();
        Task AddArtistAsync(ArtistCreateDto artist);
        Task UpdateArtistAsync(int id, UpdateArtistDto artist);
        Task DeleteArtistAsync(int id);
    }
}
