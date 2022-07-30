using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;

namespace Hospi.App.Persistence.AppRepositories
{
    public interface IPatientRepository
    {
        bool PatientExists(int id);
        Patient Add(Patient patient);
        void Update(Patient patient);
        void Delete(Patient patient);
        Task<Patient> Get(int? patientId);
        Task<Patient> Get(Expression<Func<Patient, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IList<Patient>> GetAllOrFilterPatients(string searchString = null);
        Task<IList<Patient>> GetAllOrFilterPatients(int Id);
        Doctor AssignDoctor(int patientId, int doctorId);
        Task<Relative> AssignRelative(int patientId, Relative relative);
        History AssignHistory(int patientId, int historyId);
        Task<VitalSign> AssignVitalSign (int patientId, VitalSign vitalSign);

    }
}