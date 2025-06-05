using Zad10.Models;

namespace Zad10.Services;

public interface IClientsRepository
{
    Task<Client?> GetClientByIdAsync(int idClient);
    Task<bool> HasClientTripsAsync(int idClient);
    Task<bool> DeleteClientAsync(int idClient);
    Task<bool> ClientExistsByPeselAsync(string pesel);
    Task<bool> IsClientSignedUpForTripAsync(string pesel, int tripId);
    Task<Trip?> GetTripWithDateAsync(int tripId);
    Task AddClientWithTripAsync(Client client, int tripId, DateTime? paymentDate);

}