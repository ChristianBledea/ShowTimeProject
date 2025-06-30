using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.Models;

namespace ShowTime.Businesslogic.Abstractions
{
    public interface IArtistService
    {
        Task<Artist?> getArtistbyIdAsync(int id);
        Task<IEnumerable<Artist>> getAllArtistsAsync();
        Task AddArtistAsync(Artist artist);
        Task UpdateArtistAsync(Artist artist);
        Task DeleteArtistAsync(int id);
    }
}
