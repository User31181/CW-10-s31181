using Zad12.DTOs;

namespace Zad12.Services;

public interface ITripsService
{
    public Task<(List<TripDto> trips, int totalPages)> GetTripsAsync(int page, int pageSize);
}