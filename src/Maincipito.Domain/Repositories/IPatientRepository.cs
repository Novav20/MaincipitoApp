using Maincipito.Domain.Entities;

namespace Maincipito.Domain.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Patient>> GetAllAsync(string? searchString, CancellationToken cancellationToken = default);
    Task<Doctor?> AssignDoctorAsync(int patientId, int doctorId, CancellationToken cancellationToken = default);
    Task<Relative?> AssignRelativeAsync(int patientId, Relative relative, CancellationToken cancellationToken = default);
    Task<Relative?> AssignRelativeAsync(int patientId, int relativeId, CancellationToken cancellationToken = default);
    Task<History?> AssignHistoryAsync(int patientId, History history, CancellationToken cancellationToken = default);
    Task<VitalSign?> AssignVitalSignAsync(int patientId, VitalSign vitalSign, CancellationToken cancellationToken = default);
}