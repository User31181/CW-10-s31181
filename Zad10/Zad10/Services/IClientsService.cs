using Zad10.DTOs;

namespace Zad10.Services;

public interface IClientsService
{

    public  Task<(bool success, string? error)> DeleteClientAsync(int idClient);
    Task<(bool success, string? error)> AssignClientToTripAsync(int idTrip, AssignClientToTripDto dto);

}