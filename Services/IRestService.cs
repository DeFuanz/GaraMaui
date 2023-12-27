using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Services
{
    public interface IRestService
    {
        Task<string> TestApiConnection();
    }
}
