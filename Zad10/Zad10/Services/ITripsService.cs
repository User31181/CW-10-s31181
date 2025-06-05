using Zad10.DTOs;

namespace Zad10.Services;

public interface ITripsService
{
    public Task<(List<TripDto> trips, int totalPages)> GetTripsAsync(int page, int pageSize);
}