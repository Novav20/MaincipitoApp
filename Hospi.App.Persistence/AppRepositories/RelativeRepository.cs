using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospi.App.Persistence.AppRepositories
{
    public class RelativeRepository : IRelativeRepository
    {
        private readonly MyAppContext _appContext;

        public RelativeRepository(MyAppContext appContext)
        {
            _appContext = appContext;
        }
        public bool RelativeExists(int id)
        {
            return _appContext.Relatives.Any(e => e.Id == id);
        }
        public Relative Add(Relative relative)
        {
            var addedRelative = _appContext.Relatives.Add(relative);
            _appContext.SaveChanges();
            return addedRelative.Entity;
        }
        public void Delete(Relative relative)
        {
            _appContext.Relatives.Remove(relative);
            _appContext.SaveChanges();
        }
        public async Task<Relative> Get(int? relativeId) => await _appContext.Relatives
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == relativeId);
        public void Update(Relative relative)
        {
            //var foundRelative = _appContext.Relatives.FirstOrDefault(p => p.Id == relative.Id);
            _appContext.Attach(relative).State = EntityState.Modified;
            _appContext.SaveChanges();
        }
        public async Task<IList<Relative>> GetAllRelativesOrByName(string searchString = null)
        {
            var relatives = GetIQRelatives();
            if (!string.IsNullOrEmpty(searchString))
            {
                relatives = relatives.Where(s => s.Name.Contains(searchString));
            }
            return await relatives.ToListAsync();
        }
        private IQueryable<Relative> GetIQRelatives()
        {
            return from m in _appContext.Relatives select m;
        }
    }
}