using Zad12.DTOs;
using Zad12.Repositories;

namespace Zad12.Services;

public class TripsService : ITripsService
{
    private readonly ITripsRepository _repository;

    public TripsService(ITripsRepository repository)
    {
        _repository = repository;
    }

    public async Task<(List<TripDto> trips, int totalPages)> GetTripsAsync(int page, int pageSize)
    {
        var (tripsFromDb, totalPages) = await _repository.GetTripsAsync(page, pageSize);

        var trips = tripsFromDb.Select(t => new TripDto
        {
            Name = t.Name,
            Description = t.Description,
            DateFrom = t.DateFrom,
            DateTo = t.DateTo,
            MaxPeople = t.MaxPeople,
            Countries = t.IdCountries.Select(c => c.Name).ToList(),
            Clients = t.ClientTrips.Select(ct => new ClientSimpleDto
            {
                FirstName = ct.IdClientNavigation.FirstName,
                LastName = ct.IdClientNavigation.LastName
            }).ToList()
        }).ToList();

        return (trips, totalPages);
    }
}