using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospi.App.Persistence.AppRepositories
{
    public class NurseRepository : INurseRepository
    {
        private readonly MyAppContext _appContext;

        public NurseRepository(MyAppContext appContext)
        {
            _appContext = appContext;
        }
        public bool NurseExists(int id)
        {
            return _appContext.Nurses.Any(e => e.Id == id);
        }
        public Nurse Add(Nurse nurse)
        {
            var addedNurse = _appContext.Nurses.Add(nurse);
            _appContext.SaveChanges();
            return addedNurse.Entity;
        }
        public void Delete(Nurse nurse)
        {
            _appContext.Nurses.Remove(nurse);
            _appContext.SaveChanges();
        }
        public Nurse Get(int? nurseId)
        {
            return _appContext.Nurses.FirstOrDefault(p => p.Id == nurseId);
        }
        public void Update(Nurse nurse)
        {
            _appContext.Attach(nurse).State = EntityState.Modified;
            _appContext.SaveChanges();
        }
        public async Task<IList<Nurse>> GetAllNursesOrByName(string searchString = null)
        {
            var nurses = GetIQNurses();
            if (!string.IsNullOrEmpty(searchString))
            {
                nurses = nurses.Where(s => s.Name.Contains(searchString));
            }
            return await nurses.ToListAsync();
        }
        private IQueryable<Nurse> GetIQNurses()
        {
            return from m in _appContext.Nurses select m;
        }
    }
}