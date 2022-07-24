using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;

namespace Hospi.App.Persistence.AppRepositories
{
    public interface IHistoryRepository
    {
        bool HistoryExists(int id);
        History Add(History history);
        void Update(History history);
        void Delete(History history);
        History Get(int? historyId);
        Task<IList<History>> GetAllHistoriesOrByDiagnosis(string searchString=null);
    }
}