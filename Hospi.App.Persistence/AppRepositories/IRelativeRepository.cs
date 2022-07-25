using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;

namespace Hospi.App.Persistence.AppRepositories
{
    public interface IRelativeRepository
    {
        bool RelativeExists(int id);
        Relative Add(Relative relative);
        void Update(Relative relative);
        void Delete(Relative relative);
        Task<Relative> Get(int? relativeId);
        Task<IList<Relative>> GetAllRelativesOrByName(string searchString = null);
    }
}