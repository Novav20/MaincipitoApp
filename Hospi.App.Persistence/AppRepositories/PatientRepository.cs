using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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
        public async Task<Patient> Get(int? patientId) => await _appContext.Patients
            .Include(p => p.Doctor)
            .Include(p => p.Relative)
            .Include(p => p.History)
            .Include(p => p.VitalSigns)
            .Include(p => p.Nurse)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == patientId);

        public void Update(Patient patient)
        {
            _appContext.Attach(patient).State = EntityState.Modified;
            _appContext.SaveChanges();
        }
        public async Task<IList<Patient>> GetAllOrFilterPatients(string searchString = null)
        {
            var patients = from m in _appContext.Patients select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(s => s.Name.Contains(searchString));
            }
            return await patients.ToListAsync();
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
                    _appContext.SaveChanges();
                    return foundDoctor;
                }
                return null;
            }
            return null;
        }
        public async Task<Relative> AssignRelative(int patientId, Relative relative)
        {
            var foundPatient = await _appContext.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
            if (foundPatient != null)
            {
                var addedRelative = await _appContext.Relatives.AddAsync(relative);

                await _appContext.SaveChangesAsync();

                if (addedRelative != null)
                {
                    foundPatient.Relative = addedRelative.Entity;
                    await _appContext.SaveChangesAsync();
                    return addedRelative.Entity;
                }
            }
            return null;
        }
        public async Task<History> AssignHistory(int patientId, History history)
        {
            var foundPatient = await _appContext.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
            if (foundPatient != null)
            {
                var addedHistory = await _appContext.Histories.AddAsync(history);

                await _appContext.SaveChangesAsync();

                if (addedHistory != null)
                {
                    foundPatient.History = addedHistory.Entity;
                    await _appContext.SaveChangesAsync();
                    return addedHistory.Entity;
                }
            }
            return null;
        }
        public async Task<VitalSign> AssignVitalSign(int patientId, VitalSign vitalSign)
        {
            var foundPatient = await _appContext.Patients
            .Include(p => p.VitalSigns)
            .FirstOrDefaultAsync(p => p.Id == patientId);
            if (foundPatient != null)
            {
                var addedVitalSign = await _appContext.VitalSigns.AddAsync(vitalSign);

                await _appContext.SaveChangesAsync();

                if (addedVitalSign != null)
                {
                    foundPatient.VitalSigns.Add(addedVitalSign.Entity);
                    await _appContext.SaveChangesAsync();
                    return addedVitalSign.Entity;
                }
            }
            return null;
        }

        public async Task<Patient> Get(Expression<Func<Patient, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _appContext.Patients
            .Include(p => p.Doctor)
            .Include(p => p.Relative)
            .Include(p => p.History)
            .Include(p => p.VitalSigns)
            .Include(p => p.Nurse)
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);
        }
        public async Task<IList<Patient>> GetAllOrFilterPatients(int Id)
        {
            var patients = from m in _appContext.Patients select m;
            patients = patients.Where(s => s.Id == Id);
            return await patients.ToListAsync();
        }

    }
}