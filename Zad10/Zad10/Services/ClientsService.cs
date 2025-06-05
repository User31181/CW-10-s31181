using Zad10.DTOs;
using Zad10.Models;

namespace Zad10.Services;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _repository;

    public ClientsService(IClientsRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool success, string? error)> DeleteClientAsync(int idClient)
    {
        var client = await _repository.GetClientByIdAsync(idClient);
        if (client == null)
            return (false, "Client not found");

        bool hasTrips = await _repository.HasClientTripsAsync(idClient);
        if (hasTrips)
            return (false, "Cannot delete client with assigned trips");

        bool deleted = await _repository.DeleteClientAsync(idClient);
        return (deleted, null);
    }
    
    public async Task<(bool success, string? error)> AssignClientToTripAsync(int idTrip, AssignClientToTripDto dto)
    {
        if (await _repository.ClientExistsByPeselAsync(dto.Pesel))
            return (false, "Client with this PESEL already exists");

        if (await _repository.IsClientSignedUpForTripAsync(dto.Pesel, idTrip))
            return (false, "Client is already signed up for this trip");

        var trip = await _repository.GetTripWithDateAsync(idTrip);
        if (trip == null)
            return (false, "Trip not found");

        if (trip.DateFrom <= DateTime.UtcNow)
            return (false, "Cannot sign up for a trip that has already started");

        var newClient = new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };

        await _repository.AddClientWithTripAsync(newClient, idTrip, dto.PaymentDate);
        return (true, null);
    }
}
