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
        public History Add(History history)
        {
            var addedHistory = _appContext.Histories.Add(history);
            _appContext.SaveChanges();
            return addedHistory.Entity;
        }
        public void Delete(History history)
        {
            _appContext.Histories.Remove(history);
            _appContext.SaveChanges();
        }
        public async Task<History> Get(int? historyId)
        {
            return await _appContext.Histories
            .Include(h => h.Suggestions)
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.Id == historyId);
        }
        public void Update(History history)
        {
            //var foundHistory = _appContext.Histories.FirstOrDefault(p => p.Id == history.Id);
            _appContext.Attach(history).State = EntityState.Modified;
            _appContext.SaveChanges();
        }
        public async Task<IList<History>> GetAllHistories()
        {
            return await _appContext.Histories.ToListAsync();
        }
        public async Task<CareSuggestion> AssignCareSuggestion(int historyId, CareSuggestion careSuggestion)
        {
            var foundHistory = await _appContext.Histories
            .Include(h => h.Suggestions)
            .FirstOrDefaultAsync(p => p.Id == historyId);
            if (foundHistory != null)
            {
                var addedCareSuggestion = await _appContext.CareSuggestions.AddAsync(careSuggestion);
                addedCareSuggestion.Entity.DateTime = DateTime.Now;

                await _appContext.SaveChangesAsync();

                if (addedCareSuggestion != null)
                {
                    foundHistory.Suggestions.Add(addedCareSuggestion.Entity);
                    await _appContext.SaveChangesAsync();
                    return addedCareSuggestion.Entity;
                }
            }
            return null;
        }
    }
}