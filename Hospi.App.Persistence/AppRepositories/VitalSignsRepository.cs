using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospi.App.Persistence.AppRepositories
{
    public class VitalSignRepository : IVitalSignRepository
    {
        private readonly MyAppContext _appContext;

        public VitalSignRepository(MyAppContext appContext)
        {
            _appContext = appContext;
        }
        public bool VitalSignExists(int id)
        {
            return _appContext.VitalSigns.Any(e => e.Id == id);
        }
        public VitalSign Add(VitalSign vitalSign)
        {
            var addedVitalSign = _appContext.VitalSigns.Add(vitalSign);
            _appContext.SaveChanges();
            return addedVitalSign.Entity;
        }
        public void Delete(VitalSign vitalSign)
        {
            _appContext.VitalSigns.Remove(vitalSign);
            _appContext.SaveChanges();
        }
        public VitalSign Get(int? vitalSignId)
        {
            return _appContext.VitalSigns.FirstOrDefault(p => p.Id == vitalSignId);
        }
        public void Update(VitalSign vitalSign)
        {
            _appContext.Attach(vitalSign).State = EntityState.Modified;
            _appContext.SaveChanges();
        }
        public async Task<IList<VitalSign>> GetAllOrByTypeVitalSigns(Sign? sign = null)
        {
            var vitalSigns = from m in _appContext.VitalSigns select m;
            if (sign != null)
            {
                vitalSigns = vitalSigns.Where(s => s.Sign.Equals(sign));
            }
            return await vitalSigns.ToListAsync();
        }
    }
}