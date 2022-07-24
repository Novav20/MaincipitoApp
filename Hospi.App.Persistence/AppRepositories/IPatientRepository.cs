using System;
using System.Collections.Generic;
using System.Linq;
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
        Patient Get(int? patientId);
        Task<IList<Patient>> GetAllPatientsOrByName(string searchString=null);
        Doctor AssignDoctor(int patientId, int doctorId);
        Relative AssignRelative(int patientId, int relativeId);
    }
}