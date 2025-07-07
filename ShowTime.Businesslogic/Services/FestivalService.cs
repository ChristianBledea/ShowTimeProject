using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.Businesslogic.Abstractions;
using ShowTime.Businesslogic.Dtos;
using ShowTime.DataAccess.Repositories.Abstractions;
using ShowTime.DataAccess.Models;

namespace ShowTime.Businesslogic.Services
{
    public class FestivalService : IFestivalService
    {
        private readonly IRepository<Festival> _festivalRepository;

        public FestivalService(IRepository<Festival> festivalRepository)
        {
            _festivalRepository = festivalRepository ?? throw new ArgumentNullException(nameof(festivalRepository));
        }

        public async Task<FestivalGetDto?> GetFestivalAsync(int id)
        {
            try
            {
                var festival = await _festivalRepository.GetById(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                return new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    Capacity = festival.Capacity,
                    SplashArt = festival.SplashArt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the festival.", ex);
            }
        }

        public async Task<IList<FestivalGetDto>> GetAllFestivalsAsync()
        {
            try
            {
                var festivals = await _festivalRepository.GetAll();
                return festivals.Select(festival => new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    Capacity = festival.Capacity,
                    SplashArt = festival.SplashArt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving festivals.", ex);
            }
        }

        public async Task AddFestivalAsync(FestivalCreateDto festivalCreateDto)
        {
            try
            {
                var festival = new Festival
                {
                    Name = festivalCreateDto.Name,
                    Location = festivalCreateDto.Location,
                    StartDate = festivalCreateDto.StartDate,
                    EndDate = festivalCreateDto.EndDate,
                    Capacity = festivalCreateDto.Capacity,
                    SplashArt = festivalCreateDto.SplashArt
                };
                await _festivalRepository.Add(festival);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the festival.", ex);
            }
        }

        public async Task UpdateFestivalAsync(int id, UpdateFestivalDto festivalUpdateDto)
        {
            try
            {
                var festival = await _festivalRepository.GetById(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }

                festival.Name = festivalUpdateDto.Name;
                festival.Location = festivalUpdateDto.Location;
                festival.StartDate = festivalUpdateDto.StartDate;
                festival.EndDate = festivalUpdateDto.EndDate;
                festival.Capacity = festivalUpdateDto.Capacity;
                festival.SplashArt = festivalUpdateDto.SplashArt;

                await _festivalRepository.Update(festival);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the festival.", ex);
            }
        }

        public async Task DeleteFestivalAsync(int id)
        {
            try
            {
                var festival = await _festivalRepository.GetById(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                await _festivalRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the festival.", ex);
            }
        }
    }
}
