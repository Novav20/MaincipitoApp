using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;

namespace Hospi.App.Persistence.AppRepositories
{
    public interface IVitalSignRepository
    {
        bool VitalSignExists(int id);
        VitalSign Add(VitalSign vitalSign);
        void Update(VitalSign vitalSign);
        void Delete(VitalSign vitalSign);
        VitalSign Get(int? vitalSignId);
        Task<IList<VitalSign>> GetAllVitalSignsOrByType(Sign? sign=null);
    }
}