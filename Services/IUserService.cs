using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Services
{
    public interface IUserService
    {
        string Auth0UserId { get; set; }
        string Auth0UserName { get; set; }
    }
}
