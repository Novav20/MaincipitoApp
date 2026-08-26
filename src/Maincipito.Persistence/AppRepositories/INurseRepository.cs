using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;

namespace Hospi.App.Persistence.AppRepositories
{
    public interface INurseRepository
    {
        bool NurseExists(int id);
        Nurse Add(Nurse nurse);
        void Update(Nurse nurse);
        void Delete(Nurse nurse);
        Nurse Get(int? nurseId);
        Task<IList<Nurse>> GetAllNursesOrByName(string searchString=null);
    }
}