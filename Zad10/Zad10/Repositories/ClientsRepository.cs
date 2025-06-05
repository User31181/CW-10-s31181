using Zad10.Services;

namespace Zad10.Repositories;

using Microsoft.EntityFrameworkCore;
using Zad10.Models;

public class ClientsRepository : IClientsRepository
{
    private readonly MyDbContext _context;

    public ClientsRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetClientByIdAsync(int idClient)
    {
        return await _context.Clients.FindAsync(idClient);
    }

    public async Task<bool> HasClientTripsAsync(int idClient)
    {
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);
        if (client == null)
            return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ClientExistsByPeselAsync(string pesel)
    {
        return await _context.Clients.AnyAsync(c => c.Pesel == pesel);
    }

    public async Task<bool> IsClientSignedUpForTripAsync(string pesel, int tripId)
    {
        return await _context.ClientTrips
            .Include(ct => ct.IdClientNavigation)
            .AnyAsync(ct => ct.IdTrip == tripId && ct.IdClientNavigation.Pesel == pesel);
    }

    public async Task<Trip?> GetTripWithDateAsync(int tripId)
    {
        return await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == tripId);
    }

    public async Task AddClientWithTripAsync(Client client, int tripId, DateTime? paymentDate)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = tripId,
            RegisteredAt = DateTime.UtcNow,
            PaymentDate = paymentDate
        };

        await _context.ClientTrips.AddAsync(clientTrip);
        await _context.SaveChangesAsync();
    }
}
