using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Services
{
    public class UserService : IUserService
    {
        public string Auth0UserId { get; set; } = default!;
        public string Auth0UserName { get; set; } = default!;
    }
}
