using Zad10.DTOs;
using Zad10.Models;

namespace Zad10.Repositories;

public interface ITripsRepository
{
    Task<(List<Trip> trips, int totalPages)> GetTripsAsync(int page, int pageSize);
}