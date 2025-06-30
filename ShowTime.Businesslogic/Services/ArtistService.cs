using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.Businesslogic.Abstractions;
using ShowTime.Businesslogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;

namespace ShowTime.Businesslogic.Services
{
    
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artistRepository;
        

        public ArtistService(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository ?? throw new ArgumentNullException(nameof(artistRepository));
        }

        public async Task<ArtistGetDto?> getArtistbyIdAsync(int id)
        {
            try
            {
                var artist = await _artistRepository.GetById(id);
                if (artist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }
                return new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    ImageUrl = artist.Image,
                    Genre = artist.Genre,
                };

            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while retrieving the artist.", ex);
            }
            

        }
        public async Task<IEnumerable<ArtistGetDto>> getAllArtistsAsync()
        {
            try
            {
                var artists = await _artistRepository.GetAll();
                return artists.Select(artist => new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    ImageUrl = artist.Image,
                    Genre = artist.Genre,
                    
                });
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving artists.", ex);
            }
        }
        public Task AddArtistAsync(Artist artist)
        {
            throw new NotImplementedException();
        }
        public Task UpdateArtistAsync(Artist artist)
        {
            throw new NotImplementedException();
        }
        public Task DeleteArtistAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Artist?> IArtistService.getArtistbyIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Artist>> IArtistService.getAllArtistsAsync()
        {
            throw new NotImplementedException();
        }
    }
    
    
}
