using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospi.App.Persistence.AppRepositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly MyAppContext _appContext;

        public HistoryRepository(MyAppContext appContext)
        {
            _appContext = appContext;
        }
        public bool HistoryExists(int id)
        {
            return _appContext.Histories.Any(e => e.Id == id);
        }
        public History Add(History relative)
        {
            var addedHistory = _appContext.Histories.Add(relative);
            _appContext.SaveChanges();
            return addedHistory.Entity;
        }
        public void Delete(History relative)
        {
            _appContext.Histories.Remove(relative);
            _appContext.SaveChanges();
        }
        public History Get(int? relativeId)
        {
            return _appContext.Histories.FirstOrDefault(p => p.Id == relativeId);
        }
        public void Update(History relative)
        {
            //var foundHistory = _appContext.Histories.FirstOrDefault(p => p.Id == relative.Id);
            _appContext.Attach(relative).State = EntityState.Modified;
            _appContext.SaveChanges();
        }
        public async Task<IList<History>> GetAllHistoriesOrByDiagnosis(string searchString = null)
        {
            var histories = GetIQHistories();
            if (!string.IsNullOrEmpty(searchString))
            {
                histories = histories.Where(s => s.Diagnosis.Contains(searchString));
            }
            return await histories.ToListAsync();
        }
        private IQueryable<History> GetIQHistories()
        {
            return from m in _appContext.Histories select m;
        }
    }
}