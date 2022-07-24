using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospi.App.Persistence.AppRepositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MyAppContext _appContext;

        public DoctorRepository(MyAppContext appContext)
        {
            _appContext = appContext;
        }
        public bool DoctorExists(int id)
        {
            return _appContext.Doctors.Any(e => e.Id == id);
        }
        public Doctor Add(Doctor doctor)
        {
            var addedDoctor = _appContext.Doctors.Add(doctor);
            _appContext.SaveChanges();
            return addedDoctor.Entity;
        }
        public void Delete(Doctor doctor)
        {
            _appContext.Doctors.Remove(doctor);
            _appContext.SaveChanges();
        }
        public Doctor Get(int? doctorId)
        {
            return _appContext.Doctors.FirstOrDefault(p => p.Id == doctorId);
        }
        public void Update(Doctor doctor)
        {
            //var foundDoctor = _appContext.Doctors.FirstOrDefault(p => p.Id == doctor.Id);
            _appContext.Attach(doctor).State = EntityState.Modified;
            _appContext.SaveChanges();
        }
        public async Task<IList<Doctor>> GetAllDoctorsOrByName(string searchString = null)
        {
            var doctors = GetIQDoctors();
            if (!string.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.Name.Contains(searchString));
            }
            return await doctors.ToListAsync();
        }
        private IQueryable<Doctor> GetIQDoctors()
        {
            return from m in _appContext.Doctors select m;
        }
    }
}