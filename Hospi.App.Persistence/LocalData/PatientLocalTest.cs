using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;

namespace Hospi.App.Persistence.LocalData
{
    public class PatientLocalTest
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new AppRepositories.MyAppContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<AppRepositories.MyAppContext>>()))
            {
                if (_context.Patients.Any())
                {
                    return;
                }
                _context.Patients.AddRange(
                    new Domain.Entities.Patient
                    {
                        Name = "Martin ",
                        Surname = "J. Kang",
                        Cellphone = "9519418938",
                        Genre = Genre.MALE,
                        Address = "21 Murphy Court Fullerton, CA 93632",
                        Latitude = "33.807797",
                        Longitude = "-117.836207",
                        City = "Los Ángeles",
                        DateOfBirth = DateTime.Parse("1981-2-12")
                    });
                _context.SaveChanges();
            }
        }
    }
}