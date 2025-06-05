using Microsoft.EntityFrameworkCore;
using Zad12.DTOs;
using Zad12.Models;

namespace Zad12.Repositories;

public class TripsRepository : ITripsRepository
{
    private readonly MyDbContext _context;

    public TripsRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<(List<Trip> trips, int totalPages)> GetTripsAsync(int page, int pageSize)
    {
        int totalTrips = await _context.Trips.CountAsync();
        int totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

        var trips = await _context.Trips
            .Include(t => t.IdCountries) 
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (trips, totalPages);
    }
}