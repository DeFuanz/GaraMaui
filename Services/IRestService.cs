using Gara.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Services
{
    public interface IRestService
    {

        Task<List<Vehicle>> GetVehicles();

        Task<List<UserVehicle>> GetUserVehicles(string userid);

        Task AddVehicle(UserVehicle userVehicle);
    }
}
