using Zad12.DTOs;
using Zad12.Models;

namespace Zad12.Repositories;

public interface ITripsRepository
{
    Task<(List<Trip> trips, int totalPages)> GetTripsAsync(int page, int pageSize);
}