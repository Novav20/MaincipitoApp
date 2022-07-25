using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospi.App.Persistence.AppRepositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MyAppContext _appContext;

        public PatientRepository(MyAppContext appContext)
        {
            _appContext = appContext;
        }
        public bool PatientExists(int id)
        {
            return _appContext.Patients.Any(e => e.Id == id);
        }
        public Patient Add(Patient patient)
        {
            var addedPatient = _appContext.Patients.Add(patient);
            _appContext.SaveChanges();
            return addedPatient.Entity;

        }

        public void Delete(Patient patient)
        {
            _appContext.Patients.Remove(patient);
            _appContext.SaveChanges();

        }

        public Patient Get(int? patientId)
        {
            return _appContext.Patients.FirstOrDefault(p => p.Id == patientId);
        }

        public void Update(Patient patient)
        {
            _appContext.Attach(patient).State = EntityState.Modified;
            _appContext.SaveChanges();
        }
        public async Task<IList<Patient>> GetAllPatientsOrByName(string searchString = null)
        {
            var patients = GetIQPatients();
            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(s => s.Name.Contains(searchString));
            }
            return await patients.ToListAsync();

        }
        private IQueryable<Patient> GetIQPatients()
        {
            return from m in _appContext.Patients select m;
        }

        public Doctor AssignDoctor(int patientId, int doctorId)
        {
            var foundPatient = _appContext.Patients.FirstOrDefault(p => p.Id == patientId);
            if (foundPatient != null)
            {
                var foundDoctor = _appContext.Doctors.FirstOrDefault(d => d.Id == doctorId);
                if (foundDoctor != null)
                {
                    foundPatient.Doctor = foundDoctor;
                    _appContext.Update(foundPatient);
                    _appContext.Update(foundDoctor);
                    _appContext.SaveChanges();
                    return foundDoctor;
                }
                return null;
            }
            return null;
        }
        public Relative AssignRelative(int patientId, int relativeId)
        {
            var foundPatient = _appContext.Patients.FirstOrDefault(p => p.Id == patientId);
            if (foundPatient != null)
            {
                var foundRelative = _appContext.Relatives.FirstOrDefault(r => r.Id == relativeId);
                if (foundRelative != null)
                {
                    foundPatient.Relative = foundRelative;
                    _appContext.Update(foundPatient);
                    _appContext.SaveChanges();
                    return foundRelative;
                }
                return null;
            }
            return null;
        }
    }
}