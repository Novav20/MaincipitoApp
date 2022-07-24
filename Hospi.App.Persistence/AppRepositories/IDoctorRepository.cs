using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;

namespace Hospi.App.Persistence.AppRepositories
{
    public interface IDoctorRepository
    {
        bool DoctorExists(int id);
        Doctor Add(Doctor doctor);
        void Update(Doctor doctor);
        void Delete(Doctor doctor);
        Doctor Get(int? doctorId);
        Task<IList<Doctor>> GetAllDoctorsOrByName(string searchString=null);
    }
}